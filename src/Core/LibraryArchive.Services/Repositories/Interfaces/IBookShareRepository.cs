using LibraryArchive.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryArchive.Services.Repositories.Interfaces
{
    public interface IBookShareRepository
    {
        Task<BookShare> GetBookShareByIdAsync(int bookShareId);
        Task<IEnumerable<BookShare>> GetAllBookSharesAsync();
        Task<IEnumerable<BookShare>> GetBookSharesByUserIdAsync(string userId);
        Task AddBookShareAsync(BookShare bookShare);
        void RemoveBookShare(BookShare bookShare);
        void UpdateBookShare(BookShare bookShare);
    }
}
