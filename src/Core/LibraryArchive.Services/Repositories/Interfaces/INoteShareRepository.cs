using LibraryArchive.Data.Entities;

namespace LibraryArchive.Services.Repositories.Interfaces
{
    public interface INoteShareRepository
    {
        Task<NoteShare> GetNoteShareByIdAsync(int noteShareId);
        Task<IEnumerable<NoteShare>> GetAllNoteSharesAsync();
        Task<IEnumerable<NoteShare>> GetNoteSharesByUserIdAsync(string userId);
        Task AddNoteShareAsync(NoteShare noteShare);
        void RemoveNoteShare(NoteShare noteShare);
        void UpdateNoteShare(NoteShare noteShare);
    }
}
