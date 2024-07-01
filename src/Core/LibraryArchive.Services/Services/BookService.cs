using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.Book;
using LibraryArchive.Services.Repositories.Interfaces;

namespace LibraryArchive.Services
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookReadDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            var bookDtos = new List<BookReadDto>();

            foreach (var book in books)
            {
                bookDtos.Add(new BookReadDto
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Author = book.Author,
                    ISBN = book.ISBN,
                    CoverImageUrl = book.CoverImageUrl,
                    ShelfLocation = book.ShelfLocation,
                    CategoryName = book.Category?.Name
                });
            }

            return bookDtos;
        }

        public async Task<BookReadDto> GetBookByIdAsync(int bookId)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            if (book != null)
            {
                return new BookReadDto
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Author = book.Author,
                    ISBN = book.ISBN,
                    CoverImageUrl = book.CoverImageUrl,
                    ShelfLocation = book.ShelfLocation,
                    CategoryName = book.Category?.Name
                };
            }
            return null;
        }

        public async Task<Book> AddBookAsync(BookCreateDto bookDto)
        {
            var book = new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                ISBN = bookDto.ISBN,
                CoverImageUrl = bookDto.CoverImageUrl,
                ShelfLocation = bookDto.ShelfLocation,
                CategoryId = bookDto.CategoryId
            };

            return await _bookRepository.AddBookAsync(book);
        }

        public async Task<Book> UpdateBookAsync(BookUpdateDto bookDto)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookDto.BookId);
            if (book != null)
            {
                book.Title = bookDto.Title;
                book.Author = bookDto.Author;
                book.ISBN = bookDto.ISBN;
                book.CoverImageUrl = bookDto.CoverImageUrl;
                book.ShelfLocation = bookDto.ShelfLocation;
                book.CategoryId = bookDto.CategoryId;

                return await _bookRepository.UpdateBookAsync(book);
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
            var bookDtos = new List<BookReadDto>();

            foreach (var book in books)
            {
                bookDtos.Add(new BookReadDto
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Author = book.Author,
                    ISBN = book.ISBN,
                    CoverImageUrl = book.CoverImageUrl,
                    ShelfLocation = book.ShelfLocation,
                    CategoryName = book.Category?.Name
                });
            }

            return bookDtos;
        }
    }
}
