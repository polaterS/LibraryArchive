using LibraryArchive.Data.Context;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LibraryArchive.Services.Repositories.Concrete
{
    public class NoteShareRepository : INoteShareRepository
    {
        private readonly LibraryArchiveContext _context;
        private readonly ILogger _logger;

        public NoteShareRepository(LibraryArchiveContext context)
        {
            _context = context;
            _logger = Log.ForContext<NoteShareRepository>();
        }

        public async Task<NoteShare> GetNoteShareByIdAsync(int noteShareId)
        {
            try
            {
                _logger.Information("Getting note share by ID: {NoteShareId}", noteShareId);
                return await _context.NoteShares
                    .Include(ns => ns.Note)
                    .Include(ns => ns.SharedWithUser)
                    .FirstOrDefaultAsync(ns => ns.NoteShareId == noteShareId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting note share by ID: {NoteShareId}", noteShareId);
                throw;
            }
        }

        public async Task<IEnumerable<NoteShare>> GetAllNoteSharesAsync()
        {
            try
            {
                _logger.Information("Getting all note shares");
                return await _context.NoteShares
                    .Include(ns => ns.Note)
                    .Include(ns => ns.SharedWithUser)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting all note shares");
                throw;
            }
        }

        public async Task<IEnumerable<NoteShare>> GetNoteSharesByUserIdAsync(string userId)
        {
            try
            {
                _logger.Information("Getting note shares by user ID: {UserId}", userId);
                return await _context.NoteShares
                    .Include(ns => ns.Note)
                    .Include(ns => ns.SharedWithUser)
                    .Where(ns => ns.SharedWithUserId == userId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting note shares by user ID: {UserId}", userId);
                throw;
            }
        }

        public async Task AddNoteShareAsync(NoteShare noteShare)
        {
            try
            {
                _logger.Information("Adding note share: {NoteShare}", noteShare);
                await _context.NoteShares.AddAsync(noteShare);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error adding note share: {NoteShare}", noteShare);
                throw;
            }
        }

        public void RemoveNoteShare(NoteShare noteShare)
        {
            try
            {
                _logger.Information("Removing note share: {NoteShare}", noteShare);
                _context.NoteShares.Remove(noteShare);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error removing note share: {NoteShare}", noteShare);
                throw;
            }
        }

        public void UpdateNoteShare(NoteShare noteShare)
        {
            try
            {
                _logger.Information("Updating note share: {NoteShare}", noteShare);
                _context.NoteShares.Update(noteShare);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating note share: {NoteShare}", noteShare);
                throw;
            }
        }
    }
}
