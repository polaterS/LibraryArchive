using AutoMapper;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.Notification;
using LibraryArchive.Services.Repositories.Interfaces;
using LibraryArchive.Services.TaskManager.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LibraryArchive.Services.TaskManager.Concrete
{
    public class NotificationSenderService : INotificationSenderService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationSettingsRepository _notificationSettingsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<NotificationSenderService> _logger;

        public NotificationSenderService(
            INotificationRepository notificationRepository,
            INotificationSettingsRepository notificationSettingsRepository,
            IMapper mapper,
            ILogger<NotificationSenderService> logger)
        {
            _notificationRepository = notificationRepository;
            _notificationSettingsRepository = notificationSettingsRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task SendNotificationAsync(NotificationCreateDto notificationDto)
        {
            var notification = _mapper.Map<Notification>(notificationDto);

            var userNotificationSettings = await _notificationSettingsRepository.GetByUserIdAsync(notification.UserId);

            if (userNotificationSettings == null)
            {
                _logger.LogWarning($"Notification settings for user {notification.UserId} not found.");
                return;
            }

            foreach (var settings in userNotificationSettings)
            {
                if (notification.NotificationType == "Email" && settings.EmailNotificationsEnabled)
                {
                    await SendEmailNotification(notification);
                }
                if (notification.NotificationType == "SMS" && settings.SmsNotificationsEnabled)
                {
                    await SendSmsNotification(notification);
                }
                if (notification.NotificationType == "PushNotification" && settings.PushNotificationsEnabled)
                {
                    await SendPushNotification(notification);
                }
            }

            await _notificationRepository.AddAsync(notification);
        }

        private Task SendEmailNotification(Notification notification)
        {
            // Email gönderme işlemi
            _logger.LogInformation($"Email notification sent to {notification.UserId} with message: {notification.Message}");
            return Task.CompletedTask;
        }

        private Task SendSmsNotification(Notification notification)
        {
            // SMS gönderme işlemi
            _logger.LogInformation($"SMS notification sent to {notification.UserId} with message: {notification.Message}");
            return Task.CompletedTask;
        }

        private Task SendPushNotification(Notification notification)
        {
            // Push bildirim gönderme
            _logger.LogInformation($"Push notification sent to {notification.UserId} with message: {notification.Message}");
            return Task.CompletedTask;
        }
    }
}
