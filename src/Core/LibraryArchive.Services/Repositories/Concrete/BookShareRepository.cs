using LibraryArchive.Data.Context;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryArchive.Services.Repositories
{
    public class BookShareRepository : IBookShareRepository
    {
        private readonly LibraryArchiveContext _context;

        public BookShareRepository(LibraryArchiveContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookShare>> GetAllBookSharesAsync()
        {
            return await _context.BookShares.Include(bs => bs.Note).Include(bs => bs.SharedWithUser).ToListAsync();
        }

        public async Task<BookShare> GetBookShareByIdAsync(int bookShareId)
        {
            return await _context.BookShares
                .Include(bs => bs.Note)
                .Include(bs => bs.SharedWithUser)
                .FirstOrDefaultAsync(bs => bs.BookShareId == bookShareId);
        }

        public async Task<BookShare> AddBookShareAsync(BookShare bookShare)
        {
            _context.BookShares.Add(bookShare);
            await _context.SaveChangesAsync();
            return bookShare;
        }

        public async Task<BookShare> UpdateBookShareAsync(BookShare bookShare)
        {
            _context.BookShares.Update(bookShare);
            await _context.SaveChangesAsync();
            return bookShare;
        }

        public async Task<BookShare> DeleteBookShareAsync(int bookShareId)
        {
            var bookShare = await GetBookShareByIdAsync(bookShareId);
            if (bookShare != null)
            {
                _context.BookShares.Remove(bookShare);
                await _context.SaveChangesAsync();
            }
            return bookShare;
        }
    }
}
