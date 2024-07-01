using LibraryArchive.Data.Entities;

namespace LibraryArchive.Services.Repositories.Interfaces
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetAllNotesAsync();
        Task<Note> GetNoteByIdAsync(int noteId);
        Task<Note> AddNoteAsync(Note note);
        Task<Note> UpdateNoteAsync(Note note);
        Task<bool> DeleteNoteAsync(int noteId);
    }
}
