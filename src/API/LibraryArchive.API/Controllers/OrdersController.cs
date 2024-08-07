﻿using LibraryArchive.Services;
using LibraryArchive.Services.DTOs.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LibraryArchive.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Tüm siparişleri alır.
        /// </summary>
        /// <returns>Siparişlerin listesi</returns>
        /// <response code="200">Siparişlerin listesi başarıyla döndürüldü</response>
        [Authorize(Roles = "Admin,Moderator")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderReadDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        /// <summary>
        /// Belirli bir ID'ye sahip siparişi alır.
        /// </summary>
        /// <param name="id">Sipariş ID'si</param>
        /// <returns>Sipariş detayları</returns>
        /// <response code="200">Sipariş detayları başarıyla döndürüldü</response>
        /// <response code="404">Sipariş bulunamadı</response>
        [Authorize(Roles = "Admin,Moderator,User")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }
            return Ok(order);
        }

        /// <summary>
        /// Yeni bir sipariş ekler.
        /// </summary>
        /// <param name="orderDto">Sipariş detayları</param>
        /// <returns>Eklenen sipariş detayları</returns>
        /// <response code="201">Sipariş başarıyla eklendi</response>
        /// <response code="400">Sipariş detayları yanlışsa</response>
        /// <response code="401">Kullanıcı kimliği bulunamadıysa</response>
        [Authorize(Roles = "Admin,Moderator,User")]
        [HttpPost]
        [ProducesResponseType(typeof(OrderReadDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddOrder([FromBody] OrderCreateDto orderDto)
        {
            var userId = User.FindFirst("CustomUserId")?.Value;
            if (userId == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            var order = await _orderService.AddOrderAsync(orderDto, userId);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.OrderId }, order);
        }

        /// <summary>
        /// Belirli bir ID'ye sahip siparişi günceller.
        /// </summary>
        /// <param name="id">Sipariş ID'si</param>
        /// <param name="orderDto">Güncellenmiş sipariş detayları</param>
        /// <returns>NoContent</returns>
        /// <response code="204">Sipariş başarıyla güncellendi</response>
        /// <response code="400">Sipariş ID uyumsuzluğu veya detayları yanlışsa</response>
        /// <response code="404">Sipariş bulunamadı</response>
        [Authorize(Roles = "Admin,Moderator,User")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderUpdateDto orderDto)
        {
            if (id != orderDto.OrderId)
            {
                return BadRequest("Order ID mismatch");
            }

            var updatedOrder = await _orderService.UpdateOrderAsync(orderDto);
            if (updatedOrder == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }
            return NoContent();
        }

        /// <summary>
        /// Belirli bir ID'ye sahip siparişi siler.
        /// </summary>
        /// <param name="id">Sipariş ID'si</param>
        /// <returns>NoContent</returns>
        /// <response code="204">Sipariş başarıyla silindi</response>
        /// <response code="404">Sipariş bulunamadı</response>
        [Authorize(Roles = "Admin,Moderator")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            bool result = await _orderService.DeleteOrderAsync(id);
            if (!result)
            {
                return NotFound($"Order with ID {id} not found.");
            }
            return NoContent();
        }
    }
}
