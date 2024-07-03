using LibraryArchive.Services;
using LibraryArchive.Services.DTOs.OrderDetail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryArchive.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailsController : ControllerBase
    {
        private readonly OrderDetailService _orderDetailService;

        public OrderDetailsController(OrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        /// <summary>
        /// Tüm sipariş detaylarını alır.
        /// </summary>
        /// <returns>Sipariş detaylarının listesi</returns>
        /// <response code="200">Sipariş detaylarının listesi başarıyla döndürüldü</response>
        [Authorize(Roles = "Admin, Moderator, User")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderDetailReadDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllOrderDetails()
        {
            var orderDetails = await _orderDetailService.GetAllOrderDetailsAsync();
            return Ok(orderDetails);
        }

        /// <summary>
        /// Belirli bir ID'ye sahip sipariş detayını alır.
        /// </summary>
        /// <param name="id">Sipariş detayı ID'si</param>
        /// <returns>Sipariş detayı</returns>
        /// <response code="200">Sipariş detayı başarıyla döndürüldü</response>
        /// <response code="404">Sipariş detayı bulunamadı</response>
        [Authorize(Roles = "Admin, Moderator, User")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderDetailReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrderDetailById(int id)
        {
            var orderDetail = await _orderDetailService.GetOrderDetailByIdAsync(id);
            if (orderDetail == null)
            {
                return NotFound($"Order detail with ID {id} not found.");
            }
            return Ok(orderDetail);
        }

        /// <summary>
        /// Yeni bir sipariş detayı ekler.
        /// </summary>
        /// <param name="orderDetailDto">Sipariş detayı detayları</param>
        /// <returns>Eklenen sipariş detayı</returns>
        /// <response code="201">Sipariş detayı başarıyla eklendi</response>
        /// <response code="400">Sipariş detayı detayları yanlışsa</response>
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost]
        [ProducesResponseType(typeof(OrderDetailReadDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddOrderDetail([FromBody] OrderDetailCreateDto orderDetailDto)
        {
            var orderDetail = await _orderDetailService.AddOrderDetailAsync(orderDetailDto);
            return CreatedAtAction(nameof(GetOrderDetailById), new { id = orderDetail.OrderDetailId }, orderDetail);
        }

        /// <summary>
        /// Belirli bir ID'ye sahip sipariş detayını günceller.
        /// </summary>
        /// <param name="id">Sipariş detayı ID'si</param>
        /// <param name="orderDetailDto">Güncellenmiş sipariş detayı detayları</param>
        /// <returns>NoContent</returns>
        /// <response code="204">Sipariş detayı başarıyla güncellendi</response>
        /// <response code="400">Sipariş detayı ID uyumsuzluğu veya detayları yanlışsa</response>
        /// <response code="404">Sipariş detayı bulunamadı</response>
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOrderDetail(int id, [FromBody] OrderDetailUpdateDto orderDetailDto)
        {
            if (id != orderDetailDto.OrderDetailId)
            {
                return BadRequest("Order detail ID mismatch");
            }

            var updatedOrderDetail = await _orderDetailService.UpdateOrderDetailAsync(orderDetailDto);
            if (updatedOrderDetail == null)
            {
                return NotFound($"Order detail with ID {id} not found.");
            }
            return NoContent();
        }

        /// <summary>
        /// Belirli bir ID'ye sahip sipariş detayını siler.
        /// </summary>
        /// <param name="id">Sipariş detayı ID'si</param>
        /// <returns>NoContent</returns>
        /// <response code="204">Sipariş detayı başarıyla silindi</response>
        /// <response code="404">Sipariş detayı bulunamadı</response>
        [Authorize(Roles = "Admin, Moderator")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            bool result = await _orderDetailService.DeleteOrderDetailAsync(id);
            if (!result)
            {
                return NotFound($"Order detail with ID {id} not found.");
            }
            return NoContent();
        }
    }
}
