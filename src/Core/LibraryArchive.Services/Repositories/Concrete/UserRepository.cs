using LibraryArchive.Data.Context;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LibraryArchive.Services.Repositories.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryArchiveContext _context;
        private readonly ILogger _logger;

        public UserRepository(LibraryArchiveContext context)
        {
            _context = context;
            _logger = Log.ForContext<UserRepository>();
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            try
            {
                _logger.Information("Getting user by ID: {UserId}", userId);
                return await _context.Users.FindAsync(userId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting user by ID: {UserId}", userId);
                throw;
            }
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            try
            {
                _logger.Information("Getting user by email: {Email}", email);
                return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting user by email: {Email}", email);
                throw;
            }
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            try
            {
                _logger.Information("Getting all users");
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting all users");
                throw;
            }
        }

        public async Task AddUserAsync(ApplicationUser user)
        {
            try
            {
                _logger.Information("Adding user: {User}", user);
                await _context.Users.AddAsync(user);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error adding user: {User}", user);
                throw;
            }
        }

        public void RemoveUser(ApplicationUser user)
        {
            try
            {
                _logger.Information("Removing user: {User}", user);
                _context.Users.Remove(user);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error removing user: {User}", user);
                throw;
            }
        }

        public void UpdateUser(ApplicationUser user)
        {
            try
            {
                _logger.Information("Updating user: {User}", user);
                _context.Users.Update(user);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating user: {User}", user);
                throw;
            }
        }
    }
}
