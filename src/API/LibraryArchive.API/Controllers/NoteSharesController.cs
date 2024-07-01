using LibraryArchive.Data.Entities;
using LibraryArchive.Services;
using LibraryArchive.Services.DTOs.NoteShare;
using Microsoft.AspNetCore.Mvc;

namespace LibraryArchive.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteSharesController : ControllerBase
    {
        private readonly NoteShareService _noteShareService;

        public NoteSharesController(NoteShareService noteShareService)
        {
            _noteShareService = noteShareService;
        }

        /// <summary>
        /// Tüm not paylaşımlarını alır.
        /// </summary>
        /// <returns>Not paylaşımlarının listesi</returns>
        /// <response code="200">Not paylaşımlarının listesi başarıyla döndürüldü</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<NoteShareReadDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllNoteShares()
        {
            var noteShares = await _noteShareService.GetAllNoteSharesAsync();
            return Ok(noteShares);
        }

        /// <summary>
        /// Belirli bir ID'ye sahip not paylaşımını alır.
        /// </summary>
        /// <param name="id">Not paylaşımı ID'si</param>
        /// <returns>Not paylaşımı detayları</returns>
        /// <response code="200">Not paylaşımı detayları başarıyla döndürüldü</response>
        /// <response code="404">Not paylaşımı bulunamadı</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(NoteShareReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetNoteShareById(int id)
        {
            var noteShare = await _noteShareService.GetNoteShareByIdAsync(id);
            if (noteShare == null)
            {
                return NotFound($"Note share with ID {id} not found.");
            }
            return Ok(noteShare);
        }

        /// <summary>
        /// Yeni bir not paylaşımı ekler.
        /// </summary>
        /// <param name="noteShareDto">Not paylaşımı detayları</param>
        /// <returns>Eklenen not paylaşımı detayları</returns>
        /// <response code="201">Not paylaşımı başarıyla eklendi</response>
        /// <response code="400">Not paylaşımı detayları yanlışsa</response>
        [HttpPost]
        [ProducesResponseType(typeof(NoteShare), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddNoteShare([FromBody] NoteShareCreateDto noteShareDto)
        {
            var noteShare = await _noteShareService.AddNoteShareAsync(noteShareDto);
            return CreatedAtAction(nameof(GetNoteShareById), new { id = noteShare.NoteShareId }, noteShare);
        }

        /// <summary>
        /// Belirli bir ID'ye sahip not paylaşımını günceller.
        /// </summary>
        /// <param name="id">Not paylaşımı ID'si</param>
        /// <param name="noteShareDto">Güncellenmiş not paylaşımı detayları</param>
        /// <returns>NoContent</returns>
        /// <response code="204">Not paylaşımı başarıyla güncellendi</response>
        /// <response code="400">Not paylaşımı ID uyumsuzluğu veya detayları yanlışsa</response>
        /// <response code="404">Not paylaşımı bulunamadı</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateNoteShare(int id, [FromBody] NoteShareUpdateDto noteShareDto)
        {
            if (id != noteShareDto.NoteShareId)
            {
                return BadRequest("Note share ID mismatch");
            }

            var updatedNoteShare = await _noteShareService.UpdateNoteShareAsync(noteShareDto);
            if (updatedNoteShare == null)
            {
                return NotFound($"Note share with ID {id} not found.");
            }
            return NoContent();
        }

        /// <summary>
        /// Belirli bir ID'ye sahip not paylaşımını siler.
        /// </summary>
        /// <param name="id">Not paylaşımı ID'si</param>
        /// <returns>NoContent</returns>
        /// <response code="204">Not paylaşımı başarıyla silindi</response>
        /// <response code="404">Not paylaşımı bulunamadı</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNoteShare(int id)
        {
            bool result = await _noteShareService.DeleteNoteShareAsync(id);
            if (!result)
            {
                return NotFound($"Note share with ID {id} not found.");
            }
            return NoContent();
        }
    }
}
