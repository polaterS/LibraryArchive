using AutoMapper;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.Notification;
using LibraryArchive.Services.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<NotificationSettingsDto> AddNotificationSettingsAsync(NotificationSettingsCreateDto notificationSettingsDto)
        {
            var notificationSettings = _mapper.Map<NotificationSettings>(notificationSettingsDto);
            var createdNotificationSettings = await _notificationSettingsRepository.AddAsync(notificationSettings);
            return _mapper.Map<NotificationSettingsDto>(createdNotificationSettings);
        }

        public async Task<NotificationSettingsDto> UpdateNotificationSettingsAsync(NotificationSettingsUpdateDto notificationSettingsDto)
        {
            var notificationSettings = await _notificationSettingsRepository.GetByIdAsync(notificationSettingsDto.NotificationSettingsId);
            if (notificationSettings == null)
            {
                _logger.LogError($"Notification settings with ID {notificationSettingsDto.NotificationSettingsId} not found.");
                return null;
            }

            _mapper.Map(notificationSettingsDto, notificationSettings);
            var updatedNotificationSettings = await _notificationSettingsRepository.UpdateAsync(notificationSettings);
            return _mapper.Map<NotificationSettingsDto>(updatedNotificationSettings);
        }

        public async Task<bool> DeleteNotificationSettingsAsync(int id)
        {
            var notificationSettings = await _notificationSettingsRepository.GetByIdAsync(id);
            if (notificationSettings == null)
            {
                _logger.LogError($"Notification settings with ID {id} not found.");
                return false;
            }

            await _notificationSettingsRepository.DeleteAsync(id);
            return true;
        }
    }
}
