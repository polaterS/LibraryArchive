using LibraryArchive.Services;
using LibraryArchive.Services.DTOs.Book;
using Microsoft.AspNetCore.Mvc;

namespace LibraryArchive.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        // GET: api/Books/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound($"Book with ID {id} not found.");
            }
            return Ok(book);
        }

        // POST: api/Books
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookCreateDto bookDto)
        {
            var book = await _bookService.AddBookAsync(bookDto);
            return CreatedAtAction(nameof(GetBookById), new { id = book.BookId }, book);
        }

        // PUT: api/Books/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookUpdateDto bookDto)
        {
            if (id != bookDto.BookId)
            {
                return BadRequest("Book ID mismatch");
            }

            var updatedBook = await _bookService.UpdateBookAsync(bookDto);
            if (updatedBook == null)
            {
                return NotFound($"Book with ID {id} not found.");
            }
            return NoContent();
        }

        // DELETE: api/Books/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            bool result = await _bookService.DeleteBookAsync(id);
            if (!result)
            {
                return NotFound($"Book with ID {id} not found.");
            }
            return NoContent();
        }

        // GET: api/Books/search?term=searchTerm
        [HttpGet("search")]
        public async Task<IActionResult> SearchBooks([FromQuery] string term)
        {
            var books = await _bookService.SearchBooksAsync(term);
            return Ok(books);
        }
    }
}
