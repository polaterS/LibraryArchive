using AutoMapper;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.Notification;
using LibraryArchive.Services.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryArchive.Services
{
    public class NotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(INotificationRepository notificationRepository, IMapper mapper, ILogger<NotificationService> logger)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<NotificationReadDto> GetNotificationByIdAsync(int id)
        {
            var notification = await _notificationRepository.GetByIdAsync(id);
            return _mapper.Map<NotificationReadDto>(notification);
        }

        public async Task<IEnumerable<NotificationReadDto>> GetNotificationsByUserIdAsync(string userId)
        {
            var notifications = await _notificationRepository.GetByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<NotificationReadDto>>(notifications);
        }

        public async Task<IEnumerable<NotificationReadDto>> GetAllNotificationsAsync()
        {
            var notifications = await _notificationRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<NotificationReadDto>>(notifications);
        }

        public async Task<NotificationReadDto> AddNotificationAsync(NotificationCreateDto notificationDto)
        {
            var notification = _mapper.Map<Notification>(notificationDto);
            var createdNotification = await _notificationRepository.AddAsync(notification);
            return _mapper.Map<NotificationReadDto>(createdNotification);
        }

        public async Task<bool> UpdateNotificationAsync(NotificationUpdateDto notificationDto)
        {
            var notification = await _notificationRepository.GetByIdAsync(notificationDto.NotificationId);
            if (notification == null)
            {
                _logger.LogError($"Notification with ID {notificationDto.NotificationId} not found.");
                return false;
            }

            _mapper.Map(notificationDto, notification);
            await _notificationRepository.UpdateAsync(notification);
            return true;
        }

        public async Task<bool> DeleteNotificationAsync(int id)
        {
            var notification = await _notificationRepository.GetByIdAsync(id);
            if (notification == null)
            {
                _logger.LogError($"Notification with ID {id} not found.");
                return false;
            }

            await _notificationRepository.DeleteAsync(id);
            return true;
        }
    }
}
