using LibraryArchive.Data.Context;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryArchive.Services.Repositories.Concrete
{
    public class NotificationSettingsRepository : INotificationSettingsRepository
    {
        private readonly LibraryArchiveContext _context;

        public NotificationSettingsRepository(LibraryArchiveContext context)
        {
            _context = context;
        }

        public async Task<NotificationSettings> GetByIdAsync(int id)
        {
            return await _context.NotificationSettings
                .FirstOrDefaultAsync(ns => ns.NotificationSettingsId == id);
        }

        public async Task<IEnumerable<NotificationSettings>> GetByUserIdAsync(string userId)
        {
            return await _context.NotificationSettings
                .Where(ns => ns.UserId == userId)
                .ToListAsync();
        }

        public async Task<NotificationSettings> AddAsync(NotificationSettings notificationSettings)
        {
            await _context.NotificationSettings.AddAsync(notificationSettings);
            await _context.SaveChangesAsync();
            return notificationSettings;
        }

        public async Task<NotificationSettings> UpdateAsync(NotificationSettings notificationSettings)
        {
            _context.NotificationSettings.Update(notificationSettings);
            await _context.SaveChangesAsync();
            return notificationSettings;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var notificationSettings = await GetByIdAsync(id);
            if (notificationSettings == null)
            {
                return false;
            }

            _context.NotificationSettings.Remove(notificationSettings);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}