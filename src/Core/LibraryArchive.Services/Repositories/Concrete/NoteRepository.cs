using LibraryArchive.Data.Context;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryArchive.Services.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly LibraryArchiveContext _context;

        public NoteRepository(LibraryArchiveContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Note>> GetAllNotesAsync()
        {
            return await _context.Notes.Include(n => n.Book).ToListAsync();
        }

        public async Task<Note> GetNoteByIdAsync(int noteId)
        {
            return await _context.Notes
                .Include(n => n.Book)
                .FirstOrDefaultAsync(n => n.NoteId == noteId);
        }

        public async Task<Note> AddNoteAsync(Note note)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
            return note;
        }

        public async Task<Note> UpdateNoteAsync(Note note)
        {
            _context.Notes.Update(note);
            await _context.SaveChangesAsync();
            return note;
        }

        public async Task<bool> DeleteNoteAsync(int noteId)
        {
            var note = await GetNoteByIdAsync(noteId);
            if (note != null)
            {
                _context.Notes.Remove(note);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
