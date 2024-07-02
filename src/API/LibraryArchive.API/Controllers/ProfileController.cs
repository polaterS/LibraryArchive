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

        [HttpGet]
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

        [HttpPut("profile")]
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

        [HttpPut("email")]
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

        [HttpPut("password")]
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

        [HttpDelete]
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
