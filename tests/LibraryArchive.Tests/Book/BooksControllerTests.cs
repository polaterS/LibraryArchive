using AutoMapper;
using LibraryArchive.API.Controllers;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services;
using LibraryArchive.Services.DTOs.Book;
using LibraryArchive.Services.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace LibraryArchive.Tests.Book
{
    public class BooksControllerTests
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<ILogger<BooksController>> _loggerMock; // Logger mock nesnesi
        private readonly BooksController _booksController;
        private readonly BookService _bookService;

        public BooksControllerTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _mapperMock = new Mock<IMapper>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            _loggerMock = new Mock<ILogger<BooksController>>(); // Logger mock nesnesi oluşturuluyor

            //_bookService = new BookService(_bookRepositoryMock.Object, _mapperMock.Object, _userManagerMock.Object);
            // BooksController nesnesi oluşturulurken logger mock nesnesi de sağlanıyor
            _booksController = new BooksController(_bookService, _userManagerMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetAllBooks_Should_Return_Ok()
        {
            var books = new List<LibraryArchive.Data.Entities.Book> { new LibraryArchive.Data.Entities.Book { BookId = 1, Title = "Test Book" } };
            var bookDtos = new List<BookReadDto> { new BookReadDto { BookId = 1, Title = "Test Book" } };

            _bookRepositoryMock.Setup(repo => repo.GetAllBooksAsync()).ReturnsAsync(books);
            _mapperMock.Setup(m => m.Map<IEnumerable<BookReadDto>>(books)).Returns(bookDtos);

            var result = await _booksController.GetAllBooks();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<BookReadDto>>(okResult.Value);
            Assert.Single(returnValue);
            Assert.Equal(bookDtos[0].Title, returnValue[0].Title);
        }

        [Fact]
        public async Task GetBookById_Should_Return_Ok()
        {
            var book = new LibraryArchive.Data.Entities.Book { BookId = 1, Title = "Test Book" };
            var bookDto = new BookReadDto { BookId = 1, Title = "Test Book" };

            _bookRepositoryMock.Setup(repo => repo.GetBookByIdAsync(1)).ReturnsAsync(book);
            _mapperMock.Setup(m => m.Map<BookReadDto>(book)).Returns(bookDto);

            var result = await _booksController.GetBookById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<BookReadDto>(okResult.Value);
            Assert.Equal(bookDto.Title, returnValue.Title);
        }

        [Fact]
        public async Task GetBookById_Should_Return_NotFound()
        {
            _bookRepositoryMock.Setup(repo => repo.GetBookByIdAsync(1)).ReturnsAsync((LibraryArchive.Data.Entities.Book)null);

            var result = await _booksController.GetBookById(1);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task AddBook_Should_Return_Ok()
        {
            var user = new ApplicationUser { Id = "1", UserName = "testuser" };
            var bookDto = new BookCreateDto { Title = "New Book" };
            var book = new LibraryArchive.Data.Entities.Book { BookId = 1, Title = "New Book" };
            var bookReadDto = new BookReadDto { BookId = 1, Title = "New Book" };

            // Kullanıcı bulunduğunu ve kitabın başarıyla eklendiğini simüle ediyoruz.
            _userManagerMock.Setup(um => um.GetUserAsync(It.IsAny<System.Security.Claims.ClaimsPrincipal>())).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map<LibraryArchive.Data.Entities.Book>(bookDto)).Returns(book);
            _bookRepositoryMock.Setup(repo => repo.AddBookAsync(book)).ReturnsAsync(book);
            _mapperMock.Setup(m => m.Map<BookReadDto>(book)).Returns(bookReadDto);

            var result = await _booksController.AddBook(bookDto);

            // 'OkObjectResult' bekliyoruz.
            Assert.IsType<OkObjectResult>(result);
        }



        [Fact]
        public async Task AddBook_Should_Return_Unauthorized_When_User_Not_Found()
        {
            var bookDto = new BookCreateDto { Title = "New Book" };

            _userManagerMock.Setup(um => um.GetUserAsync(It.IsAny<System.Security.Claims.ClaimsPrincipal>())).ReturnsAsync((ApplicationUser)null);

            var result = await _booksController.AddBook(bookDto);

            Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public async Task UpdateBook_Should_Return_NoContent()
        {
            var bookDto = new BookUpdateDto { BookId = 1, Title = "Updated Book" };
            var book = new LibraryArchive.Data.Entities.Book { BookId = 1, Title = "Old Book" };

            // Kitap bulunduğunu simüle ediyoruz.
            _bookRepositoryMock.Setup(repo => repo.GetBookByIdAsync(1)).ReturnsAsync(book);
            // Güncelleme işlemi simüle ediliyor.
            _bookRepositoryMock.Setup(repo => repo.UpdateBookAsync(book)).ReturnsAsync(book);

            var result = await _booksController.UpdateBook(1, bookDto);

            // Bu noktada 'NoContentResult' bekliyoruz.
            Assert.IsType<NoContentResult>(result);
        }





        [Fact]
        public async Task UpdateBook_Should_Return_NotFound()
        {
            var bookDto = new BookUpdateDto { BookId = 1, Title = "Updated Book" };

            _bookRepositoryMock.Setup(repo => repo.GetBookByIdAsync(1)).ReturnsAsync((LibraryArchive.Data.Entities.Book)null);

            var result = await _booksController.UpdateBook(1, bookDto);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteBook_Should_Return_NoContent()
        {
            var book = new LibraryArchive.Data.Entities.Book { BookId = 1, Title = "Book to Delete" };

            _bookRepositoryMock.Setup(repo => repo.DeleteBookAsync(1)).ReturnsAsync(book);

            var result = await _booksController.DeleteBook(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteBook_Should_Return_NotFound()
        {
            _bookRepositoryMock.Setup(repo => repo.DeleteBookAsync(1)).ReturnsAsync((LibraryArchive.Data.Entities.Book)null);

            var result = await _booksController.DeleteBook(1);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task SearchBooks_Should_Return_Ok()
        {
            var searchTerm = "Test";
            var books = new List<LibraryArchive.Data.Entities.Book> { new LibraryArchive.Data.Entities.Book { BookId = 1, Title = "Test Book" } };
            var bookDtos = new List<BookReadDto> { new BookReadDto { BookId = 1, Title = "Test Book" } };

            _bookRepositoryMock.Setup(repo => repo.SearchBooksAsync(searchTerm)).ReturnsAsync(books);
            _mapperMock.Setup(m => m.Map<IEnumerable<BookReadDto>>(books)).Returns(bookDtos);

            var result = await _booksController.SearchBooks(searchTerm);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<BookReadDto>>(okResult.Value);
            Assert.Single(returnValue);
            Assert.Equal(bookDtos[0].Title, returnValue[0].Title);
        }
    }
}