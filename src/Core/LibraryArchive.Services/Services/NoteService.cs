using AutoMapper;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.Note;
using LibraryArchive.Services.Repositories.Interfaces;

namespace LibraryArchive.Services
{
    public class NoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;

        public NoteService(INoteRepository noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NoteReadDto>> GetAllNotesAsync()
        {
            var notes = await _noteRepository.GetAllNotesAsync();
            return _mapper.Map<IEnumerable<NoteReadDto>>(notes);
        }

        public async Task<NoteReadDto> GetNoteByIdAsync(int noteId)
        {
            var note = await _noteRepository.GetNoteByIdAsync(noteId);
            return note != null ? _mapper.Map<NoteReadDto>(note) : null;
        }

        public async Task<NoteReadDto> AddNoteAsync(NoteCreateDto noteDto)
        {
            var note = _mapper.Map<Note>(noteDto);
            var addedNote = await _noteRepository.AddNoteAsync(note);
            return _mapper.Map<NoteReadDto>(addedNote);
        }

        public async Task<NoteReadDto> UpdateNoteAsync(NoteUpdateDto noteDto)
        {
            var note = await _noteRepository.GetNoteByIdAsync(noteDto.NoteId);
            if (note != null)
            {
                _mapper.Map(noteDto, note);
                var updatedNote = await _noteRepository.UpdateNoteAsync(note);
                return _mapper.Map<NoteReadDto>(updatedNote);
            }
            return null;
        }

        public async Task<bool> DeleteNoteAsync(int noteId)
        {
            return await _noteRepository.DeleteNoteAsync(noteId);
        }
    }
}
