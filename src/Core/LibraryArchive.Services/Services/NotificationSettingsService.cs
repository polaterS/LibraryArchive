using AutoMapper;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.Notification;
using LibraryArchive.Services.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace LibraryArchive.Services
{
    public class NotificationSettingsService
    {
        private readonly INotificationSettingsRepository _notificationSettingsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<NotificationSettingsService> _logger;

        public NotificationSettingsService(INotificationSettingsRepository notificationSettingsRepository, IMapper mapper, ILogger<NotificationSettingsService> logger)
        {
            _notificationSettingsRepository = notificationSettingsRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<NotificationSettingsDto> GetNotificationSettingsByIdAsync(int id)
        {
            var notificationSettings = await _notificationSettingsRepository.GetByIdAsync(id);
            return _mapper.Map<NotificationSettingsDto>(notificationSettings);
        }

        public async Task<IEnumerable<NotificationSettingsDto>> GetNotificationSettingsByUserIdAsync(string userId)
        {
            var notificationSettings = await _notificationSettingsRepository.GetByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<NotificationSettingsDto>>(notificationSettings);
        }

        public async Task<NotificationSettingsDto> AddNotificationSettingsAsync(NotificationSettingsDto notificationSettingsDto)
        {
            var notificationSettings = _mapper.Map<NotificationSettings>(notificationSettingsDto);
            var addedNotificationSettings = await _notificationSettingsRepository.AddAsync(notificationSettings);
            return _mapper.Map<NotificationSettingsDto>(addedNotificationSettings);
        }

        public async Task<NotificationSettingsDto> UpdateNotificationSettingsAsync(NotificationSettingsDto notificationSettingsDto)
        {
            var notificationSettings = _mapper.Map<NotificationSettings>(notificationSettingsDto);
            var updatedNotificationSettings = await _notificationSettingsRepository.UpdateAsync(notificationSettings);
            return _mapper.Map<NotificationSettingsDto>(updatedNotificationSettings);
        }

        public async Task<bool> DeleteNotificationSettingsAsync(int id)
        {
            return await _notificationSettingsRepository.DeleteAsync(id);
        }
    }
}
