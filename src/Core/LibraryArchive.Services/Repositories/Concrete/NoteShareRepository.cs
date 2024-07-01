using LibraryArchive.Data.Context;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryArchive.Services.Repositories
{
    public class NoteShareRepository : INoteShareRepository
    {
        private readonly LibraryArchiveContext _context;

        public NoteShareRepository(LibraryArchiveContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NoteShare>> GetAllNoteSharesAsync()
        {
            return await _context.NoteShares
                                 .Include(ns => ns.Note)
                                 .Include(ns => ns.SharedWithUser)
                                 .ToListAsync();
        }

        public async Task<NoteShare> GetNoteShareByIdAsync(int noteShareId)
        {
            return await _context.NoteShares
                                 .Include(ns => ns.Note)
                                 .Include(ns => ns.SharedWithUser)
                                 .FirstOrDefaultAsync(ns => ns.NoteShareId == noteShareId);
        }

        public async Task<NoteShare> AddNoteShareAsync(NoteShare noteShare)
        {
            _context.NoteShares.Add(noteShare);
            await _context.SaveChangesAsync();
            return noteShare;
        }

        public async Task<NoteShare> UpdateNoteShareAsync(NoteShare noteShare)
        {
            _context.NoteShares.Update(noteShare);
            await _context.SaveChangesAsync();
            return noteShare;
        }

        public async Task<bool> DeleteNoteShareAsync(int noteShareId)
        {
            var noteShare = await GetNoteShareByIdAsync(noteShareId);
            if (noteShare != null)
            {
                _context.NoteShares.Remove(noteShare);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
