using LibraryArchive.Data.Entities;

namespace LibraryArchive.Services.Repositories.Interfaces
{
    public interface IBookShareRepository
    {
        Task<IEnumerable<BookShare>> GetAllBookSharesAsync();
        Task<BookShare> GetBookShareByIdAsync(int bookShareId);
        Task<BookShare> AddBookShareAsync(BookShare bookShare);
        Task<BookShare> UpdateBookShareAsync(BookShare bookShare);
        Task<BookShare> DeleteBookShareAsync(int bookShareId);
    }
}
