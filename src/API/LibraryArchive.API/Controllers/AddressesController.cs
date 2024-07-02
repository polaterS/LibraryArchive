using LibraryArchive.Services;
using LibraryArchive.Services.DTOs.Address;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibraryArchive.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AddressesController : ControllerBase
    {
        private readonly AddressService _addressService;
        private readonly UserService _userService;
        private readonly ILogger<BooksController> _logger;

        public AddressesController(AddressService addressService, UserService userService, ILogger<BooksController> logger)
        {
            _addressService = addressService;
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Kullanıcının tüm adreslerini alır.
        /// </summary>
        /// <returns>Kullanıcının adresleri</returns>
        /// <response code="200">Adresler başarıyla döndürüldü</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AddressReadDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAddresses()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var addresses = await _addressService.GetAddressesByUserIdAsync(userId);
            return Ok(addresses);
        }

        /// <summary>
        /// Belirli bir ID'ye sahip adresi alır.
        /// </summary>
        /// <param name="id">Adres ID'si</param>
        /// <returns>Adres detayları</returns>
        /// <response code="200">Adres detayları başarıyla döndürüldü</response>
        /// <response code="404">Adres bulunamadı</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AddressReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAddressById(int id)
        {
            var userId = User.FindFirstValue("CustomUserId");
            var address = await _addressService.GetAddressByIdAsync(userId, id);
            if (address == null)
            {
                return NotFound($"Address with ID {id} not found.");
            }
            return Ok(address);
        }

        /// <summary>
        /// Yeni bir adres ekler.
        /// </summary>
        /// <param name="addressDto">Adres detayları</param>
        /// <returns>Eklenen adres detayları</returns>
        /// <response code="201">Adres başarıyla eklendi</response>
        /// <response code="400">Adres detayları yanlışsa</response>
        [HttpPost]
        [ProducesResponseType(typeof(AddressReadDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAddress([FromBody] AddressCreateDto addressDto)
        {
            var userId = User.FindFirstValue("CustomUserId"); // CustomUserId'yi kullanarak userId'yi alıyoruz
            _logger.LogInformation($"UserId: {userId}");

            var user = await _userService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            addressDto.UserId = user.Id;
            var result = await _addressService.AddAddressAsync(addressDto);
            if (result == null)
            {
                return BadRequest("Failed to add address.");
            }

            return CreatedAtAction(nameof(GetAddressById), new { id = result.AddressId }, result);
        }

        /// <summary>
        /// Belirli bir ID'ye sahip adresi günceller.
        /// </summary>
        /// <param name="id">Adres ID'si</param>
        /// <param name="addressDto">Güncellenmiş adres detayları</param>
        /// <returns>NoContent</returns>
        /// <response code="204">Adres başarıyla güncellendi</response>
        /// <response code="400">Adres ID uyumsuzluğu veya detayları yanlışsa</response>
        /// <response code="404">Adres bulunamadı</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAddress(int id, [FromBody] AddressUpdateDto addressDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id != addressDto.AddressId)
            {
                return BadRequest("Address ID mismatch");
            }

            var result = await _addressService.UpdateAddressAsync(userId, addressDto);
            if (!result)
            {
                return NotFound($"Address with ID {id} not found.");
            }
            return NoContent();
        }

        /// <summary>
        /// Belirli bir ID'ye sahip adresi siler.
        /// </summary>
        /// <param name="id">Adres ID'si</param>
        /// <returns>NoContent</returns>
        /// <response code="204">Adres başarıyla silindi</response>
        /// <response code="404">Adres bulunamadı</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _addressService.DeleteAddressAsync(userId, id);
            if (!result)
            {
                return NotFound($"Address with ID {id} not found.");
            }
            return NoContent();
        }
    }
}
