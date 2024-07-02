using LibraryArchive.Data.Entities;

namespace LibraryArchive.Services.Repositories.Interfaces
{
    public interface INotificationSettingsRepository
    {
        Task<NotificationSettings> GetByIdAsync(int id);
        Task<IEnumerable<NotificationSettings>> GetByUserIdAsync(string userId);
        Task<NotificationSettings> AddAsync(NotificationSettings notificationSettings);
        Task<NotificationSettings> UpdateAsync(NotificationSettings notificationSettings);
        Task<bool> DeleteAsync(int id);
    }
}
