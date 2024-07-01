using LibraryArchive.Services;
using LibraryArchive.Services.DTOs.Note;
using Microsoft.AspNetCore.Mvc;

namespace LibraryArchive.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly NoteService _noteService;

        public NotesController(NoteService noteService)
        {
            _noteService = noteService;
        }

        // GET: api/Notes
        [HttpGet]
        public async Task<IActionResult> GetAllNotes()
        {
            var notes = await _noteService.GetAllNotesAsync();
            return Ok(notes);
        }

        // GET: api/Notes/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNoteById(int id)
        {
            var note = await _noteService.GetNoteByIdAsync(id);
            if (note == null)
            {
                return NotFound($"Note with ID {id} not found.");
            }
            return Ok(note);
        }

        // POST: api/Notes
        [HttpPost]
        public async Task<IActionResult> AddNote([FromBody] NoteCreateDto noteDto)
        {
            var note = await _noteService.AddNoteAsync(noteDto);
            return CreatedAtAction(nameof(GetNoteById), new { id = note.NoteId }, note);
        }

        // PUT: api/Notes/{id}
        [HttpPut("{id}")]
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

        // DELETE: api/Notes/{id}
        [HttpDelete("{id}")]
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
