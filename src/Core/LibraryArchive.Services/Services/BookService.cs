using AutoMapper;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.Book;
using LibraryArchive.Services.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LibraryArchive.Services
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookService(IBookRepository bookRepository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IEnumerable<BookReadDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return _mapper.Map<IEnumerable<BookReadDto>>(books);
        }

        public async Task<BookReadDto> GetBookByIdAsync(int bookId)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            return book != null ? _mapper.Map<BookReadDto>(book) : null;
        }

        public async Task<BookReadDto> AddBookAsync(BookCreateDto bookDto, string userId)
        {
            // Kullanıcı kimliğinin geçerli olup olmadığını kontrol et
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("Invalid user ID");
            }

            var book = _mapper.Map<Book>(bookDto);
            book.UserId = userId; // Kullanıcı kimliğini ayarla

            await _bookRepository.AddBookAsync(book);
            return _mapper.Map<BookReadDto>(book);
        }

        public async Task<BookReadDto> UpdateBookAsync(BookUpdateDto bookDto)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookDto.BookId);
            if (book != null)
            {
                _mapper.Map(bookDto, book);
                await _bookRepository.UpdateBookAsync(book);
                return _mapper.Map<BookReadDto>(book);
            }
            return null;
        }

        public async Task<bool> DeleteBookAsync(int bookId)
        {
            var book = await _bookRepository.DeleteBookAsync(bookId);
            return book != null;
        }

        public async Task<IEnumerable<BookReadDto>> SearchBooksAsync(string searchTerm)
        {
            var books = await _bookRepository.SearchBooksAsync(searchTerm);
            return _mapper.Map<IEnumerable<BookReadDto>>(books);
        }
    }
}
