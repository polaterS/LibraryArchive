using LibraryArchive.Data.Entities;
using LibraryArchive.Services;
using LibraryArchive.Services.DTOs.Note;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryArchive.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly NoteService _noteService;

        public NotesController(NoteService noteService)
        {
            _noteService = noteService;
        }

        /// <summary>
        /// Tüm notları alır.
        /// </summary>
        /// <returns>Notların listesi</returns>
        /// <response code="200">Notların listesi başarıyla döndürüldü</response>
        [Authorize(Roles = "Admin, Moderator, User")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<NoteReadDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllNotes()
        {
            var notes = await _noteService.GetAllNotesAsync();
            return Ok(notes);
        }

        /// <summary>
        /// Belirli bir ID'ye sahip notu alır.
        /// </summary>
        /// <param name="id">Not ID'si</param>
        /// <returns>Not detayları</returns>
        /// <response code="200">Not detayları başarıyla döndürüldü</response>
        /// <response code="404">Not bulunamadı</response>
        [Authorize(Roles = "Admin, Moderator, User")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(NoteReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetNoteById(int id)
        {
            var note = await _noteService.GetNoteByIdAsync(id);
            if (note == null)
            {
                return NotFound($"Note with ID {id} not found.");
            }
            return Ok(note);
        }

        /// <summary>
        /// Yeni bir not ekler.
        /// </summary>
        /// <param name="noteDto">Not detayları</param>
        /// <returns>Eklenen not detayları</returns>
        /// <response code="201">Not başarıyla eklendi</response>
        /// <response code="400">Not detayları yanlışsa</response>
        [Authorize(Roles = "Admin, Moderator, User")]
        [HttpPost]
        [ProducesResponseType(typeof(Note), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddNote([FromBody] NoteCreateDto noteDto)
        {
            var note = await _noteService.AddNoteAsync(noteDto);
            return CreatedAtAction(nameof(GetNoteById), new { id = note.NoteId }, note);
        }

        /// <summary>
        /// Belirli bir ID'ye sahip notu günceller.
        /// </summary>
        /// <param name="id">Not ID'si</param>
        /// <param name="noteDto">Güncellenmiş not detayları</param>
        /// <returns>NoContent</returns>
        /// <response code="204">Not başarıyla güncellendi</response>
        /// <response code="400">Not ID uyumsuzluğu veya detayları yanlışsa</response>
        /// <response code="404">Not bulunamadı</response>
        [Authorize(Roles = "Admin, Moderator, User")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateNote(int id, [FromBody] NoteUpdateDto noteDto)
        {
            if (id != noteDto.NoteId)
            {
                return BadRequest("Note ID mismatch");
            }

            var updatedNote = await _noteService.UpdateNoteAsync(noteDto);
            if (updatedNote == null)
            {
                return NotFound($"Note with ID {id} not found.");
            }
            return NoContent();
        }

        /// <summary>
        /// Belirli bir ID'ye sahip notu siler.
        /// </summary>
        /// <param name="id">Not ID'si</param>
        /// <returns>NoContent</returns>
        /// <response code="204">Not başarıyla silindi</response>
        /// <response code="404">Not bulunamadı</response>
        [Authorize(Roles = "Admin, Moderator, User")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNote(int id)
        {
            bool result = await _noteService.DeleteNoteAsync(id);
            if (!result)
            {
                return NotFound($"Note with ID {id} not found.");
            }
            return NoContent();
        }
    }
}
