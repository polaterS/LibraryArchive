using AutoMapper;
using FluentValidation;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.Book;
using LibraryArchive.Services.Repositories.Interfaces;
using Serilog;

namespace LibraryArchive.Services
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IValidator<BookCreateDto> _bookCreateValidator;
        private readonly IValidator<BookUpdateDto> _bookUpdateValidator;
        private readonly IValidator<BookDeleteDto> _bookDeleteValidator;

        public BookService(
            IBookRepository bookRepository,
            IMapper mapper,
            IValidator<BookCreateDto> bookCreateValidator,
            IValidator<BookUpdateDto> bookUpdateValidator,
            IValidator<BookDeleteDto> bookDeleteValidator)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _logger = Log.ForContext<BookService>();
            _bookCreateValidator = bookCreateValidator;
            _bookUpdateValidator = bookUpdateValidator;
            _bookDeleteValidator = bookDeleteValidator;
        }

        public async Task<BookReadDto> GetBookByIdAsync(int bookId)
        {
            try
            {
                _logger.Information("Getting book by ID: {BookId}", bookId);
                var book = await _bookRepository.GetBookByIdAsync(bookId);
                return _mapper.Map<BookReadDto>(book);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting book by ID: {BookId}", bookId);
                throw;
            }
        }

        public async Task<IEnumerable<BookReadDto>> GetAllBooksAsync()
        {
            try
            {
                _logger.Information("Getting all books");
                var books = await _bookRepository.GetAllBooksAsync();
                return _mapper.Map<IEnumerable<BookReadDto>>(books);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting all books");
                throw;
            }
        }

        public async Task<IEnumerable<BookReadDto>> GetBooksByUserIdAsync(string userId)
        {
            try
            {
                _logger.Information("Getting books by user ID: {UserId}", userId);
                var books = await _bookRepository.GetBooksByUserIdAsync(userId);
                return _mapper.Map<IEnumerable<BookReadDto>>(books);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting books by user ID: {UserId}", userId);
                throw;
            }
        }

        public async Task AddBookAsync(BookCreateDto bookCreateDto)
        {
            await _bookCreateValidator.ValidateAndThrowAsync(bookCreateDto);
            try
            {
                var book = _mapper.Map<Book>(bookCreateDto);
                _logger.Information("Adding book: {Book}", book);
                await _bookRepository.AddBookAsync(book);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error adding book: {BookCreateDto}", bookCreateDto);
                throw;
            }
        }

        public void RemoveBook(BookDeleteDto bookDeleteDto)
        {
            _bookDeleteValidator.ValidateAndThrow(bookDeleteDto);
            try
            {
                var book = _mapper.Map<Book>(bookDeleteDto);
                _logger.Information("Removing book: {Book}", book);
                _bookRepository.RemoveBook(book);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error removing book: {BookDeleteDto}", bookDeleteDto);
                throw;
            }
        }

        public void UpdateBook(BookUpdateDto bookUpdateDto)
        {
            _bookUpdateValidator.ValidateAndThrow(bookUpdateDto);
            try
            {
                var book = _mapper.Map<Book>(bookUpdateDto);
                _logger.Information("Updating book: {Book}", book);
                _bookRepository.UpdateBook(book);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating book: {BookUpdateDto}", bookUpdateDto);
                throw;
            }
        }
    }
}
