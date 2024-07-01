using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.Auth.Login;
using LibraryArchive.Services.DTOs.Auth.Register;
using LibraryArchive.Services.DTOs.Auth.Role;
using LibraryArchive.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryArchive.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly AuthService _authService;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            AuthService authService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                Name = model.Name,
                Surname = model.Surname
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { result = "Kullanıcı başarıyla oluşturuldu." });
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var token = GenerateJwtToken(user);

                return Ok(new { token });
            }

            return Unauthorized();
        }

        [HttpPost("createrole")]
        public async Task<IActionResult> CreateRole([FromBody] RoleDto model)
        {
            var result = await _authService.CreateRoleAsync(model.RoleName);

            if (result.Succeeded)
            {
                return Ok(new { result = $"'{model.RoleName}' Rolu başarıyla oluşturuldu." });
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("assignrole")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto model)
        {
            var result = await _authService.AssignRoleAsync(model.Email, model.RoleName);

            if (result.Succeeded)
            {
                return Ok(new { result = $"'{model.RoleName}' Rolu '{model.Email}' kullanıcısına başarıyla atandı." });
            }

            return BadRequest(result.Errors);
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
