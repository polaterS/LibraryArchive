using LibraryArchive.Data.Entities;

namespace LibraryArchive.Services.Repositories.Interfaces
{
    public interface INoteRepository
    {
        Task<Note> GetNoteByIdAsync(int noteId);
        Task<IEnumerable<Note>> GetAllNotesAsync();
        Task<IEnumerable<Note>> GetNotesByUserIdAsync(string userId);
        Task AddNoteAsync(Note note);
        void RemoveNote(Note note);
        void UpdateNote(Note note);
    }
}
