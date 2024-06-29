using LibraryArchive.Data.Context;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryArchive.Services.Repositories.Concrete
{
    public class NoteRepository : INoteRepository
    {
        private readonly LibraryArchiveContext _context;
        private readonly ILogger _logger;

        public NoteRepository(LibraryArchiveContext context)
        {
            _context = context;
            _logger = Log.ForContext<NoteRepository>();
        }

        public async Task<Note> GetNoteByIdAsync(int noteId)
        {
            try
            {
                _logger.Information("Getting note by ID: {NoteId}", noteId);
                return await _context.Notes
                    .Include(n => n.Book)
                    .Include(n => n.User)
                    .FirstOrDefaultAsync(n => n.NoteId == noteId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting note by ID: {NoteId}", noteId);
                throw;
            }
        }

        public async Task<IEnumerable<Note>> GetAllNotesAsync()
        {
            try
            {
                _logger.Information("Getting all notes");
                return await _context.Notes
                    .Include(n => n.Book)
                    .Include(n => n.User)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting all notes");
                throw;
            }
        }

        public async Task<IEnumerable<Note>> GetNotesByUserIdAsync(string userId)
        {
            try
            {
                _logger.Information("Getting notes by user ID: {UserId}", userId);
                return await _context.Notes
                    .Include(n => n.Book)
                    .Include(n => n.User)
                    .Where(n => n.UserId == userId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting notes by user ID: {UserId}", userId);
                throw;
            }
        }

        public async Task AddNoteAsync(Note note)
        {
            try
            {
                _logger.Information("Adding note: {Note}", note);
                await _context.Notes.AddAsync(note);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error adding note: {Note}", note);
                throw;
            }
        }

        public void RemoveNote(Note note)
        {
            try
            {
                _logger.Information("Removing note: {Note}", note);
                _context.Notes.Remove(note);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error removing note: {Note}", note);
                throw;
            }
        }

        public void UpdateNote(Note note)
        {
            try
            {
                _logger.Information("Updating note: {Note}", note);
                _context.Notes.Update(note);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating note: {Note}", note);
                throw;
            }
        }
    }
}
