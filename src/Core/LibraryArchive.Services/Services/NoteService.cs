using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.Note;
using LibraryArchive.Services.Repositories.Interfaces;

namespace LibraryArchive.Services
{
    public class NoteService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<IEnumerable<NoteReadDto>> GetAllNotesAsync()
        {
            var notes = await _noteRepository.GetAllNotesAsync();
            return notes.Select(n => new NoteReadDto
            {
                NoteId = n.NoteId,
                BookId = n.BookId,
                Content = n.Content,
                IsPrivate = n.IsPrivate,
                UserName = n.User.UserName,  // Assuming that the User relation is included
                BookTitle = n.Book.Title     // Assuming that the Book relation is included
            }).ToList();
        }

        public async Task<NoteReadDto> GetNoteByIdAsync(int noteId)
        {
            var note = await _noteRepository.GetNoteByIdAsync(noteId);
            if (note != null)
            {
                return new NoteReadDto
                {
                    NoteId = note.NoteId,
                    BookId = note.BookId,
                    Content = note.Content,
                    IsPrivate = note.IsPrivate,
                    UserName = note.User.UserName,  // Assuming User is loaded
                    BookTitle = note.Book.Title     // Assuming Book is loaded
                };
            }
            return null;
        }

        public async Task<Note> AddNoteAsync(NoteCreateDto noteDto)
        {
            var note = new Note
            {
                BookId = noteDto.BookId,
                Content = noteDto.Content,
                IsPrivate = noteDto.IsPrivate,
                UserId = noteDto.UserId  // Assuming UserId is part of NoteCreateDto
            };

            return await _noteRepository.AddNoteAsync(note);
        }

        public async Task<Note> UpdateNoteAsync(NoteUpdateDto noteDto)
        {
            var note = await _noteRepository.GetNoteByIdAsync(noteDto.NoteId);
            if (note != null)
            {
                note.Content = noteDto.Content;
                note.IsPrivate = noteDto.IsPrivate;
                return await _noteRepository.UpdateNoteAsync(note);
            }
            return null;
        }

        public async Task<bool> DeleteNoteAsync(int noteId)
        {
            return await _noteRepository.DeleteNoteAsync(noteId);
        }
    }
}
