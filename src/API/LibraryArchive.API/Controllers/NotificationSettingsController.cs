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
    public class NotificationSettingsController : ControllerBase
    {
        private readonly NotificationSettingsService _notificationSettingsService;

        public NotificationSettingsController(NotificationSettingsService notificationSettingsService)
        {
            _notificationSettingsService = notificationSettingsService;
        }

        /// <summary>
        /// Belirli bir kullanıcıya ait tüm bildirim ayarlarını alır.
        /// </summary>
        /// <returns>Bildirim ayarları listesi</returns>
        /// <response code="200">Bildirim ayarları başarıyla döndürüldü</response>
        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<NotificationSettingsDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNotificationSettings()
        {
            var userId = User.FindFirstValue("CustomUserId");
            var notificationSettings = await _notificationSettingsService.GetNotificationSettingsByUserIdAsync(userId);
            return Ok(notificationSettings);
        }

        /// <summary>
        /// Belirli bir ID'ye sahip bildirim ayarlarını alır.
        /// </summary>
        /// <param name="id">Bildirim ayarları ID'si</param>
        /// <returns>Bildirim ayarları detayları</returns>
        /// <response code="200">Bildirim ayarları başarıyla döndürüldü</response>
        /// <response code="404">Bildirim ayarları bulunamadı</response>
        [Authorize(Roles = "Admin, User")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(NotificationSettingsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetNotificationSettingsById(int id)
        {
            var notificationSettings = await _notificationSettingsService.GetNotificationSettingsByIdAsync(id);
            if (notificationSettings == null)
            {
                return NotFound($"Notification settings with ID {id} not found.");
            }
            return Ok(notificationSettings);
        }

        /// <summary>
        /// Yeni bir bildirim ayarı ekler.
        /// </summary>
        /// <param name="notificationSettingsDto">Bildirim ayarları detayları</param>
        /// <returns>Eklenen bildirim ayarları detayları</returns>
        /// <response code="201">Bildirim ayarı başarıyla eklendi</response>
        /// <response code="400">Bildirim ayarları detayları yanlışsa</response>
        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        [ProducesResponseType(typeof(NotificationSettingsDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddNotificationSettings([FromBody] NotificationSettingsCreateDto notificationSettingsDto)
        {
            var createdNotificationSettings = await _notificationSettingsService.AddNotificationSettingsAsync(notificationSettingsDto);
            return CreatedAtAction(nameof(GetNotificationSettingsById), new { id = createdNotificationSettings.NotificationSettingsId }, createdNotificationSettings);
        }

        /// <summary>
        /// Belirli bir ID'ye sahip bildirim ayarlarını günceller.
        /// </summary>
        /// <param name="id">Bildirim ayarları ID'si</param>
        /// <param name="notificationSettingsDto">Güncellenmiş bildirim ayarları detayları</param>
        /// <returns>NoContent</returns>
        /// <response code="204">Bildirim ayarları başarıyla güncellendi</response>
        /// <response code="400">Bildirim ayarları ID uyumsuzluğu veya detayları yanlışsa</response>
        /// <response code="404">Bildirim ayarları bulunamadı</response>
        [Authorize(Roles = "Admin, User")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateNotificationSettings(int id, [FromBody] NotificationSettingsUpdateDto notificationSettingsDto)
        {
            if (id != notificationSettingsDto.NotificationSettingsId)
            {
                return BadRequest("Notification settings ID mismatch");
            }

            var result = await _notificationSettingsService.UpdateNotificationSettingsAsync(notificationSettingsDto);
            if (result == null)
            {
                return NotFound($"Notification settings with ID {id} not found.");
            }
            return NoContent();
        }

        /// <summary>
        /// Belirli bir ID'ye sahip bildirim ayarlarını siler.
        /// </summary>
        /// <param name="id">Bildirim ayarları ID'si</param>
        /// <returns>NoContent</returns>
        /// <response code="204">Bildirim ayarları başarıyla silindi</response>
        /// <response code="404">Bildirim ayarları bulunamadı</response>
        [Authorize(Roles = "Admin, User")]
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
