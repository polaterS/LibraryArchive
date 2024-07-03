using LibraryArchive.Data.Entities;
using LibraryArchive.Services;
using LibraryArchive.Services.DTOs.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryArchive.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<BooksController> _logger;

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
        [Authorize(Roles = "Admin,Moderator")]
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
        [Authorize(Roles = "Admin,Moderator")]
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
        [Authorize(Roles = "Admin,Moderator")]
        [HttpPost]
        [ProducesResponseType(typeof(BookReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddBook([FromBody] BookCreateDto bookDto)
        {
            try
            {
                var userId = User.FindFirstValue("CustomUserId");
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new { Message = "Invalid user Id." });
                }

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return Unauthorized(new { Message = "Invalid user." });
                }

                var createdBook = await _bookService.AddBookAsync(bookDto, user.Id);
                return Ok(createdBook);
            }
            catch (System.Exception ex)
            {
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
        [Authorize(Roles = "Admin,Moderator")]
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
        [Authorize(Roles = "Admin,Moderator")]
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
        [AllowAnonymous]
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<BookReadDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchBooks([FromQuery] string term)
        {
            var books = await _bookService.SearchBooksAsync(term);
            return Ok(books);
        }
    }
}
