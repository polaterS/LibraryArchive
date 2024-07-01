using LibraryArchive.Data.Entities;

namespace LibraryArchive.Services.Repositories.Interfaces
{
    public interface INoteShareRepository
    {
        Task<IEnumerable<NoteShare>> GetAllNoteSharesAsync();
        Task<NoteShare> GetNoteShareByIdAsync(int noteShareId);
        Task<NoteShare> AddNoteShareAsync(NoteShare noteShare);
        Task<NoteShare> UpdateNoteShareAsync(NoteShare noteShare);
        Task<bool> DeleteNoteShareAsync(int noteShareId);
    }
}
