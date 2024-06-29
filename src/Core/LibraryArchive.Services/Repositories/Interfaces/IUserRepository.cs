using LibraryArchive.Data.Entities;

namespace LibraryArchive.Services.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task AddUserAsync(ApplicationUser user);
        void RemoveUser(ApplicationUser user);
        void UpdateUser(ApplicationUser user);
    }
}
