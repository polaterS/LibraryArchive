using LibraryArchive.Data.Entities;
using LibraryArchive.Services;
using LibraryArchive.Services.DTOs.BookShare;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryArchive.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookSharesController : ControllerBase
    {
        private readonly BookShareService _bookShareService;

        public BookSharesController(BookShareService bookShareService)
        {
            _bookShareService = bookShareService;
        }

        /// <summary>
        /// Tüm kitap paylaşımlarını alır.
        /// </summary>
        /// <returns>Kitap paylaşım listesi</returns>
        /// <response code="200">Kitap paylaşım listesi başarıyla döndürüldü</response>
        [Authorize(Roles = "Admin, Moderator, User")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BookShareReadDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBookShares()
        {
            var bookShares = await _bookShareService.GetAllBookSharesAsync();
            return Ok(bookShares);
        }

        /// <summary>
        /// Belirli bir ID'ye sahip kitap paylaşımını alır.
        /// </summary>
        /// <param name="id">Kitap paylaşım ID'si</param>
        /// <returns>Kitap paylaşım detayları</returns>
        /// <response code="200">Kitap paylaşım detayları başarıyla döndürüldü</response>
        /// <response code="404">Kitap paylaşımı bulunamadı</response>
        [Authorize(Roles = "Admin, Moderator, User")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BookShareReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBookShareById(int id)
        {
            var bookShare = await _bookShareService.GetBookShareByIdAsync(id);
            if (bookShare == null)
            {
                return NotFound($"Book share with ID {id} not found.");
            }
            return Ok(bookShare);
        }

        /// <summary>
        /// Yeni bir kitap paylaşımı ekler.
        /// </summary>
        /// <param name="bookShareDto">Kitap paylaşım detayları</param>
        /// <returns>Eklenen kitap paylaşımı detayları</returns>
        /// <response code="201">Kitap paylaşımı başarıyla eklendi</response>
        /// <response code="400">Kitap paylaşım detayları yanlışsa</response>
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost]
        [ProducesResponseType(typeof(BookShare), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddBookShare([FromBody] BookShareCreateDto bookShareDto)
        {
            var bookShare = await _bookShareService.AddBookShareAsync(bookShareDto);
            return CreatedAtAction(nameof(GetBookShareById), new { id = bookShare.BookShareId }, bookShare);
        }

        /// <summary>
        /// Belirli bir ID'ye sahip kitap paylaşımını günceller.
        /// </summary>
        /// <param name="id">Kitap paylaşım ID'si</param>
        /// <param name="bookShareDto">Güncellenmiş kitap paylaşım detayları</param>
        /// <returns>NoContent</returns>
        /// <response code="204">Kitap paylaşımı başarıyla güncellendi</response>
        /// <response code="400">Kitap paylaşım ID uyumsuzluğu veya detayları yanlışsa</response>
        /// <response code="404">Kitap paylaşımı bulunamadı</response>
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Belirli bir ID'ye sahip kitap paylaşımını siler.
        /// </summary>
        /// <param name="id">Kitap paylaşım ID'si</param>
        /// <returns>NoContent</returns>
        /// <response code="204">Kitap paylaşımı başarıyla silindi</response>
        /// <response code="404">Kitap paylaşımı bulunamadı</response>
        [Authorize(Roles = "Admin, Moderator")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
