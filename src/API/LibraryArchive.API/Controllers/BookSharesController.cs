using LibraryArchive.Services;
using LibraryArchive.Services.DTOs.BookShare;
using Microsoft.AspNetCore.Mvc;

namespace LibraryArchive.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookSharesController : ControllerBase
    {
        private readonly BookShareService _bookShareService;

        public BookSharesController(BookShareService bookShareService)
        {
            _bookShareService = bookShareService;
        }

        // GET: api/BookShares
        [HttpGet]
        public async Task<IActionResult> GetAllBookShares()
        {
            var bookShares = await _bookShareService.GetAllBookSharesAsync();
            return Ok(bookShares);
        }

        // GET: api/BookShares/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookShareById(int id)
        {
            var bookShare = await _bookShareService.GetBookShareByIdAsync(id);
            if (bookShare == null)
            {
                return NotFound($"Book share with ID {id} not found.");
            }
            return Ok(bookShare);
        }

        // POST: api/BookShares
        [HttpPost]
        public async Task<IActionResult> AddBookShare([FromBody] BookShareCreateDto bookShareDto)
        {
            var bookShare = await _bookShareService.AddBookShareAsync(bookShareDto);
            return CreatedAtAction(nameof(GetBookShareById), new { id = bookShare.BookShareId }, bookShare);
        }

        // PUT: api/BookShares/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookShare(int id, [FromBody] BookShareUpdateDto bookShareDto)
        {
            if (id != bookShareDto.BookShareId)
            {
                return BadRequest("Book share ID mismatch");
            }

            var updatedBookShare = await _bookShareService.UpdateBookShareAsync(bookShareDto);
            if (updatedBookShare == null)
            {
                return NotFound($"Book share with ID {id} not found.");
            }
            return NoContent();
        }

        // DELETE: api/BookShares/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookShare(int id)
        {
            bool result = await _bookShareService.DeleteBookShareAsync(id);
            if (!result)
            {
                return NotFound($"Book share with ID {id} not found.");
            }
            return NoContent();
        }
    }
}
