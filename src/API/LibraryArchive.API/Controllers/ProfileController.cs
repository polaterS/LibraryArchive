using LibraryArchive.Services;
using LibraryArchive.Services.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryArchive.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly UserService _userService;

        public ProfileController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Kullanıcı profilini alır.
        /// </summary>
        /// <returns>Kullanıcı profili</returns>
        /// <response code="200">Kullanıcı profili başarıyla döndürüldü</response>
        /// <response code="404">Kullanıcı bulunamadı</response>
        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        [ProducesResponseType(typeof(UserProfileDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProfile()
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userProfile = await _userService.GetUserProfileAsync(email);
            if (userProfile == null)
            {
                return NotFound("User not found");
            }
            return Ok(userProfile);
        }

        /// <summary>
        /// Kullanıcı profilini günceller.
        /// </summary>
        /// <param name="userProfileUpdateDto">Güncellenmiş profil detayları</param>
        /// <returns>NoContent</returns>
        /// <response code="204">Profil başarıyla güncellendi</response>
        /// <response code="400">Profil güncelleme başarısız</response>
        [Authorize(Roles = "Admin, User")]
        [HttpPut("profile")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProfile([FromBody] UserProfileUpdateDto userProfileUpdateDto)
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _userService.UpdateUserProfileAsync(email, userProfileUpdateDto);
            if (!result)
            {
                return BadRequest("Failed to update profile");
            }
            return NoContent();
        }

        /// <summary>
        /// Kullanıcı email adresini günceller.
        /// </summary>
        /// <param name="userEmailUpdateDto">Güncellenmiş email detayları</param>
        /// <returns>NoContent</returns>
        /// <response code="204">Email başarıyla güncellendi</response>
        /// <response code="400">Email güncelleme başarısız</response>
        [Authorize(Roles = "Admin, User")]
        [HttpPut("email")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateEmail([FromBody] UserEmailUpdateDto userEmailUpdateDto)
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _userService.UpdateUserEmailAsync(email, userEmailUpdateDto);
            if (!result)
            {
                return BadRequest("Failed to update email");
            }
            return NoContent();
        }

        /// <summary>
        /// Kullanıcı şifresini günceller.
        /// </summary>
        /// <param name="userPasswordUpdateDto">Güncellenmiş şifre detayları</param>
        /// <returns>NoContent</returns>
        /// <response code="204">Şifre başarıyla güncellendi</response>
        /// <response code="400">Şifre güncelleme başarısız</response>
        [Authorize(Roles = "Admin, User")]
        [HttpPut("password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePassword([FromBody] UserPasswordUpdateDto userPasswordUpdateDto)
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _userService.UpdateUserPasswordAsync(email, userPasswordUpdateDto);
            if (!result.Succeeded)
            {
                return BadRequest("Failed to update password");
            }
            return NoContent();
        }

        /// <summary>
        /// Kullanıcı profilini siler.
        /// </summary>
        /// <returns>NoContent</returns>
        /// <response code="204">Profil başarıyla silindi</response>
        /// <response code="400">Profil silme başarısız</response>
        [Authorize(Roles = "Admin, User")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProfile()
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _userService.DeleteUserAsync(email);
            if (!result.Succeeded)
            {
                return BadRequest("Failed to delete profile");
            }
            return NoContent();
        }
    }
}
