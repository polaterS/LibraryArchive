using LibraryArchive.Services;
using LibraryArchive.Services.DTOs.Notification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibraryArchive.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationSettingsController : ControllerBase
    {
        private readonly NotificationSettingsService _notificationSettingsService;
        private readonly ILogger<NotificationSettingsController> _logger;

        public NotificationSettingsController(NotificationSettingsService notificationSettingsService, ILogger<NotificationSettingsController> logger)
        {
            _notificationSettingsService = notificationSettingsService;
            _logger = logger;
        }

        /// <summary>
        /// Kullanıcının tüm bildirim ayarlarını alır.
        /// </summary>
        /// <returns>Bildirim ayarları listesi</returns>
        /// <response code="200">Bildirim ayarları başarıyla döndürüldü</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<NotificationSettingsDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNotificationSettings()
        {
            var userId = User.FindFirstValue("CustomUserId");
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogError("User Id is null or empty");
                return Unauthorized(new { Message = "Invalid user Id." });
            }

            var settings = await _notificationSettingsService.GetNotificationSettingsByUserIdAsync(userId);
            return Ok(settings);
        }

        /// <summary>
        /// Belirli bir ID'ye sahip bildirim ayarını alır.
        /// </summary>
        /// <param name="id">Bildirim ayarı ID'si</param>
        /// <returns>Bildirim ayarları detayları</returns>
        /// <response code="200">Bildirim ayarları detayları başarıyla döndürüldü</response>
        /// <response code="404">Bildirim ayarı bulunamadı</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(NotificationSettingsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetNotificationSettingsById(int id)
        {
            var settings = await _notificationSettingsService.GetNotificationSettingsByIdAsync(id);
            if (settings == null)
            {
                return NotFound($"Notification settings with ID {id} not found.");
            }
            return Ok(settings);
        }

        /// <summary>
        /// Yeni bir bildirim ayarı ekler.
        /// </summary>
        /// <param name="notificationSettingsDto">Bildirim ayarları detayları</param>
        /// <returns>Eklenen bildirim ayarları detayları</returns>
        /// <response code="201">Bildirim ayarları başarıyla eklendi</response>
        /// <response code="400">Bildirim ayarları detayları yanlışsa</response>
        [HttpPost]
        [ProducesResponseType(typeof(NotificationSettingsDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddNotificationSettings([FromBody] NotificationSettingsDto notificationSettingsDto)
        {
            var userId = User.FindFirstValue("CustomUserId");
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogError("User Id is null or empty");
                return Unauthorized(new { Message = "Invalid user Id." });
            }

            notificationSettingsDto.UserId = userId;
            var result = await _notificationSettingsService.AddNotificationSettingsAsync(notificationSettingsDto);
            if (result == null)
            {
                return BadRequest("Failed to add notification settings.");
            }

            return CreatedAtAction(nameof(GetNotificationSettingsById), new { id = result.NotificationSettingsId }, result);
        }

        /// <summary>
        /// Belirli bir ID'ye sahip bildirim ayarını günceller.
        /// </summary>
        /// <param name="id">Bildirim ayarı ID'si</param>
        /// <param name="notificationSettingsDto">Güncellenmiş bildirim ayarları detayları</param>
        /// <returns>NoContent</returns>
        /// <response code="204">Bildirim ayarları başarıyla güncellendi</response>
        /// <response code="400">Bildirim ayarları ID uyumsuzluğu veya detayları yanlışsa</response>
        /// <response code="404">Bildirim ayarı bulunamadı</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateNotificationSettings(int id, [FromBody] NotificationSettingsDto notificationSettingsDto)
        {
            var userId = User.FindFirstValue("CustomUserId");
            if (id != notificationSettingsDto.NotificationSettingsId)
            {
                return BadRequest("Notification settings ID mismatch");
            }

            notificationSettingsDto.UserId = userId;
            var result = await _notificationSettingsService.UpdateNotificationSettingsAsync(notificationSettingsDto);
            if (result == null)
            {
                return NotFound($"Notification settings with ID {id} not found.");
            }

            return NoContent();
        }

        /// <summary>
        /// Belirli bir ID'ye sahip bildirim ayarını siler.
        /// </summary>
        /// <param name="id">Bildirim ayarı ID'si</param>
        /// <returns>NoContent</returns>
        /// <response code="204">Bildirim ayarı başarıyla silindi</response>
        /// <response code="404">Bildirim ayarı bulunamadı</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNotificationSettings(int id)
        {
            var result = await _notificationSettingsService.DeleteNotificationSettingsAsync(id);
            if (!result)
            {
                return NotFound($"Notification settings with ID {id} not found.");
            }

            return NoContent();
        }
    }
}
