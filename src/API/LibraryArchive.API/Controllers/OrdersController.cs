using LibraryArchive.Services;
using LibraryArchive.Services.DTOs.Order;
using Microsoft.AspNetCore.Mvc;

namespace LibraryArchive.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        // GET: api/Orders/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }
            return Ok(order);
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] OrderCreateDto orderDto)
        {
            var order = await _orderService.AddOrderAsync(orderDto);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.OrderId }, order);
        }

        // PUT: api/Orders/{id}
        [HttpPut("{id}")]
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

        // DELETE: api/Orders/{id}
        [HttpDelete("{id}")]
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
