using LibraryArchive.Data.Entities;

namespace LibraryArchive.Services.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetByIdAsync(string id);
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task<IEnumerable<ApplicationUser>> SearchUsersAsync(string searchTerm);
        Task<IEnumerable<ApplicationUser>> FilterUsersAsync(string role, bool isActive);
        Task<ApplicationUser> AddAsync(ApplicationUser user);
        Task<ApplicationUser> UpdateAsync(ApplicationUser user);
        Task<ApplicationUser> DeleteAsync(string id);
    }
}
