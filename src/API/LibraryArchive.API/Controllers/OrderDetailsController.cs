using LibraryArchive.Services;
using LibraryArchive.Services.DTOs.OrderDetail;
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

        // GET: api/OrderDetails
        [HttpGet]
        public async Task<IActionResult> GetAllOrderDetails()
        {
            var orderDetails = await _orderDetailService.GetAllOrderDetailsAsync();
            return Ok(orderDetails);
        }

        // GET: api/OrderDetails/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetailById(int id)
        {
            var orderDetail = await _orderDetailService.GetOrderDetailByIdAsync(id);
            if (orderDetail == null)
            {
                return NotFound($"Order detail with ID {id} not found.");
            }
            return Ok(orderDetail);
        }

        // POST: api/OrderDetails
        [HttpPost]
        public async Task<IActionResult> AddOrderDetail([FromBody] OrderDetailCreateDto orderDetailDto)
        {
            var orderDetail = await _orderDetailService.AddOrderDetailAsync(orderDetailDto);
            return CreatedAtAction(nameof(GetOrderDetailById), new { id = orderDetail.OrderDetailId }, orderDetail);
        }

        // PUT: api/OrderDetails/{id}
        [HttpPut("{id}")]
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

        // DELETE: api/OrderDetails/{id}
        [HttpDelete("{id}")]
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
