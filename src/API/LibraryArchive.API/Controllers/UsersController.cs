using LibraryArchive.Services;
using LibraryArchive.Services.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryArchive.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Tüm kullanıcıları alır.
        /// </summary>
        /// <returns>Kullanıcıların listesi</returns>
        /// <response code="200">Kullanıcıların listesi başarıyla döndürüldü</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserReadDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }


        /// <summary>
        /// Belirli bir ID'ye sahip kullanıcıyı alır.
        /// </summary>
        /// <param name="id">Kullanıcı ID'si</param>
        /// <returns>Kullanıcı</returns>
        /// <response code="200">Kullanıcı başarıyla döndürüldü</response>
        /// <response code="404">Kullanıcı bulunamadı</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(string id)
        {
            var userDto = await _userService.GetUserByIdAsync(id);
            if (userDto == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return Ok(userDto);
        }

        /// <summary>
        /// Yeni bir kullanıcı kaydeder.
        /// </summary>
        /// <param name="userDto">Kullanıcı detayları</param>
        /// <returns>Kayıtlı kullanıcı</returns>
        /// <response code="201">Kullanıcı başarıyla kaydedildi</response>
        /// <response code="400">Kullanıcı detayları yanlışsa</response>
        [HttpPost]
        [ProducesResponseType(typeof(UserReadDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser([FromBody] UserCreateDto userDto)
        {
            var (result, createdUser) = await _userService.RegisterUserAsync(userDto);
            if (result.Succeeded && createdUser != null)
            {
                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
            }
            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Belirli bir ID'ye sahip kullanıcıyı günceller.
        /// </summary>
        /// <param name="id">Kullanıcı ID'si</param>
        /// <param name="userDto">Güncellenmiş kullanıcı detayları</param>
        /// <returns>NoContent</returns>
        /// <response code="204">Kullanıcı başarıyla güncellendi</response>
        /// <response code="400">Kullanıcı ID uyumsuzluğu veya detayları yanlışsa</response>
        /// <response code="404">Kullanıcı bulunamadı</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserUpdateDto userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest("User ID mismatch");
            }

            var result = await _userService.UpdateUserAsync(userDto);
            if (result.Succeeded)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Belirli bir ID'ye sahip kullanıcıyı siler.
        /// </summary>
        /// <param name="id">Kullanıcı ID'si</param>
        /// <returns>NoContent</returns>
        /// <response code="204">Kullanıcı başarıyla silindi</response>
        /// <response code="400">Kullanıcı bulunamadı</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (result.Succeeded)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }
    }

}
