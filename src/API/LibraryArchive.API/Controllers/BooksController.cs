using LibraryArchive.Data.Entities;
using LibraryArchive.Services;
using LibraryArchive.Services.DTOs.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryArchive.API.Controllers
{
    [Authorize(Roles ="Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<BooksController> _logger;  // Logger ekliyoruz

        public BooksController(BookService bookService, UserManager<ApplicationUser> userManager, ILogger<BooksController> logger)
        {
            _bookService = bookService;
            _userManager = userManager;
            _logger = logger;
        }

        /// <summary>
        /// Tüm kitapları alır.
        /// </summary>
        /// <returns>Kitap listesi</returns>
        /// <response code="200">Kitap listesi başarıyla döndürüldü</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BookReadDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        /// <summary>
        /// Belirli bir ID'ye sahip kitabı alır.
        /// </summary>
        /// <param name="id">Kitap ID'si</param>
        /// <returns>Kitap detayları</returns>
        /// <response code="200">Kitap detayları başarıyla döndürüldü</response>
        /// <response code="404">Kitap bulunamadı</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BookReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound($"Book with ID {id} not found.");
            }
            return Ok(book);
        }

        /// <summary>
        /// Yeni bir kitap ekler.
        /// </summary>
        /// <param name="bookDto">Kitap detayları</param>
        /// <returns>Eklenen kitap detayları</returns>
        /// <response code="200">Kitap başarıyla eklendi</response>
        /// <response code="400">Kitap detayları yanlışsa</response>
        /// <response code="401">Kullanıcı yetkili değilse</response>
        [HttpPost]
        [ProducesResponseType(typeof(BookReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddBook([FromBody] BookCreateDto bookDto)
        {
            try
            {
                // Kullanıcı email'ini JWT token'dan al
                var userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Console.WriteLine($"User Email from token: {userEmail}");
                if (string.IsNullOrEmpty(userEmail))
                {
                    Console.WriteLine("User Email is null or empty");
                    return Unauthorized(new { Message = "Invalid user email." });
                }

                // Kullanıcıyı UserManager ile email üzerinden al
                var user = await _userManager.FindByEmailAsync(userEmail);
                if (user == null)
                {
                    Console.WriteLine("User not found");
                    return Unauthorized(new { Message = "Invalid user." });
                }

                Console.WriteLine($"User found: {user.UserName}");

                // Kitabı ekle
                var createdBook = await _bookService.AddBookAsync(bookDto, user.Id);
                return Ok(createdBook);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return BadRequest(new { Message = ex.Message });
            }
        }





        /// <summary>
        /// Belirli bir ID'ye sahip kitabı günceller.
        /// </summary>
        /// <param name="id">Kitap ID'si</param>
        /// <param name="bookDto">Güncellenmiş kitap detayları</param>
        /// <returns>NoContent</returns>
        /// <response code="204">Kitap başarıyla güncellendi</response>
        /// <response code="400">Kitap ID uyumsuzluğu veya detayları yanlışsa</response>
        /// <response code="404">Kitap bulunamadı</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookUpdateDto bookDto)
        {
            if (id != bookDto.BookId)
            {
                return BadRequest("Book ID mismatch");
            }

            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound($"Book with ID {id} not found.");
            }

            await _bookService.UpdateBookAsync(bookDto);
            return NoContent();
        }

        /// <summary>
        /// Belirli bir ID'ye sahip kitabı siler.
        /// </summary>
        /// <param name="id">Kitap ID'si</param>
        /// <returns>NoContent</returns>
        /// <response code="204">Kitap başarıyla silindi</response>
        /// <response code="404">Kitap bulunamadı</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBook(int id)
        {
            bool result = await _bookService.DeleteBookAsync(id);
            if (!result)
            {
                return NotFound($"Book with ID {id} not found.");
            }
            return NoContent();
        }

        /// <summary>
        /// Arama terimine göre kitapları arar.
        /// </summary>
        /// <param name="term">Arama terimi</param>
        /// <returns>Arama sonuçları</returns>
        /// <response code="200">Arama sonuçları başarıyla döndürüldü</response>
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<BookReadDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchBooks([FromQuery] string term)
        {
            var books = await _bookService.SearchBooksAsync(term);
            return Ok(books);
        }
    }
}
