using LibraryArchive.Data.Context;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryArchive.Services.Repositories.Concrete
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryArchiveContext _context;
        private readonly ILogger _logger;

        public BookRepository(LibraryArchiveContext context)
        {
            _context = context;
            _logger = Log.ForContext<BookRepository>();
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            try
            {
                _logger.Information("Getting book by ID: {BookId}", bookId);
                return await _context.Books
                    .Include(b => b.Category)
                    .Include(b => b.User)
                    .FirstOrDefaultAsync(b => b.BookId == bookId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting book by ID: {BookId}", bookId);
                throw;
            }
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            try
            {
                _logger.Information("Getting all books");
                return await _context.Books
                    .Include(b => b.Category)
                    .Include(b => b.User)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting all books");
                throw;
            }
        }

        public async Task<IEnumerable<Book>> GetBooksByUserIdAsync(string userId)
        {
            try
            {
                _logger.Information("Getting books by user ID: {UserId}", userId);
                return await _context.Books
                    .Include(b => b.Category)
                    .Include(b => b.User)
                    .Where(b => b.UserId == userId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting books by user ID: {UserId}", userId);
                throw;
            }
        }

        public async Task AddBookAsync(Book book)
        {
            try
            {
                _logger.Information("Adding book: {Book}", book);
                await _context.Books.AddAsync(book);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error adding book: {Book}", book);
                throw;
            }
        }

        public void RemoveBook(Book book)
        {
            try
            {
                _logger.Information("Removing book: {Book}", book);
                _context.Books.Remove(book);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error removing book: {Book}", book);
                throw;
            }
        }

        public void UpdateBook(Book book)
        {
            try
            {
                _logger.Information("Updating book: {Book}", book);
                _context.Books.Update(book);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating book: {Book}", book);
                throw;
            }
        }
    }
}
