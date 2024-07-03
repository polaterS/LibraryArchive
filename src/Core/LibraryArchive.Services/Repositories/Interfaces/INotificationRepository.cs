using LibraryArchive.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryArchive.Services.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        Task<Notification> GetByIdAsync(int id);
        Task<IEnumerable<Notification>> GetByUserIdAsync(string userId);
        Task<IEnumerable<Notification>> GetAllAsync();
        Task<Notification> AddAsync(Notification notification);
        System.Threading.Tasks.Task UpdateAsync(Notification notification);
        System.Threading.Tasks.Task DeleteAsync(int id);
    }
}
