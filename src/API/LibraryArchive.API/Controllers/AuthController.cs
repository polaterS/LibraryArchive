using LibraryArchive.Services;
using LibraryArchive.Services.DTOs.Auth.Login;
using LibraryArchive.Services.DTOs.Auth.Register;
using LibraryArchive.Services.DTOs.Auth.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryArchive.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Yeni bir kullanıcı kaydeder.
        /// </summary>
        /// <param name="registerDto">Kayıt Detayları</param>
        /// <returns>Kayıtlı kullanıcı için JWT belirteci</returns>
        /// <response code="200">Kayıtlı kullanıcı için JWT token döndürür</response>
        /// <response code="400">Kayıt ayrıntıları yanlışsa</response>
        [HttpPost("Register")]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                var token = await _authService.RegisterAsync(registerDto);
                return Ok(new TokenResponse { Token = token });
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Kullanıcı Girişi.
        /// </summary>
        /// <param name="loginDto">Giriş detayları</param>
        /// <returns>JWT token</returns>
        /// <response code="200">JWT token döndürür</response>
        /// <response code="400">Giriş bilgileri yanlışsa</response>
        [HttpPost("Login")]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var token = await _authService.LoginAsync(loginDto);
                return Ok(new TokenResponse { Token = token });
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Bir kullanıcıya bir rol atar.
        /// </summary>
        /// <param name="assignRoleDto">Rol atama ayrıntıları</param>
        /// <returns>Onay mesajı döndürür.</returns>
        /// <response code="200">Rol başarıyla atandı</response>
        /// <response code="400">Rol atama ayrıntıları yanlışsa</response>
        /// <response code="403">Kullanıcı yetkili değilse</response>
        [Authorize(Roles = "Admin")]
        [HttpPost("AssignRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto assignRoleDto)
        {
            try
            {
                await _authService.AssignRoleAsync(assignRoleDto);
                return Ok("Rol başarıyla atandı.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Yeni bir rol oluşturur.
        /// </summary>
        /// <param name="roleDto">Rol ayrıntıları</param>
        /// <returns>Onay mesajı döndürür.</returns>
        /// <response code="200">Rol başarıyla oluşturuldu</response>
        /// <response code="400">Rol ayrıntıları yanlışsa</response>
        /// <response code="403">Kullanıcı yetkili değilse</response>
        [Authorize(Roles = "Admin")]
        [HttpPost("CreateRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateRole([FromBody] RoleDto roleDto)
        {
            try
            {
                await _authService.CreateRoleAsync(roleDto);
                return Ok("Rol başarıyla oluşturuldu");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class TokenResponse
    {
        public string Token { get; set; }
    }
}
