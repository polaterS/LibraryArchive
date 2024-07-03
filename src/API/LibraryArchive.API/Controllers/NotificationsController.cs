using LibraryArchive.Services;
using LibraryArchive.Services.DTOs.Notification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;

namespace LibraryArchive.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly NotificationService _notificationService;

        public NotificationsController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        /// <summary>
        /// Belirli bir kullanıcıya ait tüm bildirimleri alır.
        /// </summary>
        /// <returns>Bildirim listesi</returns>
        /// <response code="200">Bildirimler başarıyla döndürüldü</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<NotificationReadDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNotifications()
        {
            var userId = User.FindFirstValue("CustomUserId");
            var notifications = await _notificationService.GetNotificationsByUserIdAsync(userId);
            return Ok(notifications);
        }

        /// <summary>
        /// Belirli bir ID'ye sahip bildirimi alır.
        /// </summary>
        /// <param name="id">Bildirim ID'si</param>
        /// <returns>Bildirim detayları</returns>
        /// <response code="200">Bildirim detayları başarıyla döndürüldü</response>
        /// <response code="404">Bildirim bulunamadı</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(NotificationReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetNotificationById(int id)
        {
            var notification = await _notificationService.GetNotificationByIdAsync(id);
            if (notification == null)
            {
                return NotFound($"Notification with ID {id} not found.");
            }
            return Ok(notification);
        }

        /// <summary>
        /// Yeni bir bildirim ekler.
        /// </summary>
        /// <param name="notificationDto">Bildirim detayları</param>
        /// <returns>Eklenen bildirim detayları</returns>
        /// <response code="201">Bildirim başarıyla eklendi</response>
        /// <response code="400">Bildirim detayları yanlışsa</response>
        [HttpPost]
        [ProducesResponseType(typeof(NotificationReadDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddNotification([FromBody] NotificationCreateDto notificationDto)
        {
            var createdNotification = await _notificationService.AddNotificationAsync(notificationDto);
            return CreatedAtAction(nameof(GetNotificationById), new { id = createdNotification.NotificationId }, createdNotification);
        }

        /// <summary>
        /// Belirli bir ID'ye sahip bildirimi günceller.
        /// </summary>
        /// <param name="id">Bildirim ID'si</param>
        /// <param name="notificationDto">Güncellenmiş bildirim detayları</param>
        /// <returns>NoContent</returns>
        /// <response code="204">Bildirim başarıyla güncellendi</response>
        /// <response code="400">Bildirim ID uyumsuzluğu veya detayları yanlışsa</response>
        /// <response code="404">Bildirim bulunamadı</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateNotification(int id, [FromBody] NotificationUpdateDto notificationDto)
        {
            if (id != notificationDto.NotificationId)
            {
                return BadRequest("Notification ID mismatch");
            }

            var result = await _notificationService.UpdateNotificationAsync(notificationDto);
            if (!result)
            {
                return NotFound($"Notification with ID {id} not found.");
            }
            return NoContent();
        }

        /// <summary>
        /// Belirli bir ID'ye sahip bildirimi siler.
        /// </summary>
        /// <param name="id">Bildirim ID'si</param>
        /// <returns>NoContent</returns>
        /// <response code="204">Bildirim başarıyla silindi</response>
        /// <response code="404">Bildirim bulunamadı</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var result = await _notificationService.DeleteNotificationAsync(id);
            if (!result)
            {
                return NotFound($"Notification with ID {id} not found.");
            }
            return NoContent();
        }
    }
}
