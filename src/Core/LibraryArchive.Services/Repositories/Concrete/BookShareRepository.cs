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
    public class BookShareRepository : IBookShareRepository
    {
        private readonly LibraryArchiveContext _context;
        private readonly ILogger _logger;

        public BookShareRepository(LibraryArchiveContext context)
        {
            _context = context;
            _logger = Log.ForContext<BookShareRepository>();
        }

        public async Task<BookShare> GetBookShareByIdAsync(int bookShareId)
        {
            try
            {
                _logger.Information("Getting book share by ID: {BookShareId}", bookShareId);
                return await _context.BookShares
                    .Include(bs => bs.Note)
                    .Include(bs => bs.SharedWithUser)
                    .FirstOrDefaultAsync(bs => bs.BookShareId == bookShareId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting book share by ID: {BookShareId}", bookShareId);
                throw;
            }
        }

        public async Task<IEnumerable<BookShare>> GetAllBookSharesAsync()
        {
            try
            {
                _logger.Information("Getting all book shares");
                return await _context.BookShares
                    .Include(bs => bs.Note)
                    .Include(bs => bs.SharedWithUser)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting all book shares");
                throw;
            }
        }

        public async Task<IEnumerable<BookShare>> GetBookSharesByUserIdAsync(string userId)
        {
            try
            {
                _logger.Information("Getting book shares by user ID: {UserId}", userId);
                return await _context.BookShares
                    .Include(bs => bs.Note)
                    .Include(bs => bs.SharedWithUser)
                    .Where(bs => bs.SharedWithUserId == userId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting book shares by user ID: {UserId}", userId);
                throw;
            }
        }

        public async Task AddBookShareAsync(BookShare bookShare)
        {
            try
            {
                _logger.Information("Adding book share: {BookShare}", bookShare);
                await _context.BookShares.AddAsync(bookShare);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error adding book share: {BookShare}", bookShare);
                throw;
            }
        }

        public void RemoveBookShare(BookShare bookShare)
        {
            try
            {
                _logger.Information("Removing book share: {BookShare}", bookShare);
                _context.BookShares.Remove(bookShare);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error removing book share: {BookShare}", bookShare);
                throw;
            }
        }

        public void UpdateBookShare(BookShare bookShare)
        {
            try
            {
                _logger.Information("Updating book share: {BookShare}", bookShare);
                _context.BookShares.Update(bookShare);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating book share: {BookShare}", bookShare);
                throw;
            }
        }
    }
}
