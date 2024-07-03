using LibraryArchive.Services.DTOs.Notification;
using System.Threading.Tasks;

namespace LibraryArchive.Services.TaskManager.Interfaces
{
    public interface INotificationSenderService
    {
        Task SendNotificationAsync(NotificationCreateDto notification);
    }
}
