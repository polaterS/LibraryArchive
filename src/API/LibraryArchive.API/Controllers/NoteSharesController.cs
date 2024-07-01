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

        // GET: api/NoteShares
        [HttpGet]
        public async Task<IActionResult> GetAllNoteShares()
        {
            var noteShares = await _noteShareService.GetAllNoteSharesAsync();
            return Ok(noteShares);
        }

        // GET: api/NoteShares/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNoteShareById(int id)
        {
            var noteShare = await _noteShareService.GetNoteShareByIdAsync(id);
            if (noteShare == null)
            {
                return NotFound($"Note share with ID {id} not found.");
            }
            return Ok(noteShare);
        }

        // POST: api/NoteShares
        [HttpPost]
        public async Task<IActionResult> AddNoteShare([FromBody] NoteShareCreateDto noteShareDto)
        {
            var noteShare = await _noteShareService.AddNoteShareAsync(noteShareDto);
            return CreatedAtAction(nameof(GetNoteShareById), new { id = noteShare.NoteShareId }, noteShare);
        }

        // PUT: api/NoteShares/{id}
        [HttpPut("{id}")]
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

        // DELETE: api/NoteShares/{id}
        [HttpDelete("{id}")]
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
