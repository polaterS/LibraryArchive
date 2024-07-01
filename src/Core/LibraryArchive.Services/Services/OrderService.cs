using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.Order;
using LibraryArchive.Services.DTOs.OrderDetail;
using LibraryArchive.Services.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryArchive.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderReadDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return orders.Select(o => new OrderReadDto
            {
                OrderId = o.OrderId,
                OrderDate = o.OrderDate,
                UserName = o.User.UserName, // Assuming User is included and loaded
                OrderDetails = o.OrderDetails.Select(od => new OrderDetailReadDto
                {
                    OrderDetailId = od.OrderDetailId,
                    BookId = od.BookId,
                    BookTitle = od.Book.Title, // Assuming Book is loaded
                    Quantity = od.Quantity,
                    Price = od.Price
                }).ToList()
            }).ToList();
        }

        public async Task<OrderReadDto> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order != null)
            {
                return new OrderReadDto
                {
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate,
                    UserName = order.User.UserName, // Assuming User is included
                    OrderDetails = order.OrderDetails.Select(od => new OrderDetailReadDto
                    {
                        OrderDetailId = od.OrderDetailId,
                        BookId = od.BookId,
                        BookTitle = od.Book.Title, // Assuming Book is loaded
                        Quantity = od.Quantity,
                        Price = od.Price
                    }).ToList()
                };
            }
            return null;
        }

        public async Task<Order> AddOrderAsync(OrderCreateDto orderDto)
        {
            var order = new Order
            {
                UserId = orderDto.UserId, // Assuming UserId is part of OrderCreateDto
                OrderDate = System.DateTime.Now, // Set order date to current time
                OrderDetails = orderDto.OrderDetails.Select(od => new OrderDetail
                {
                    BookId = od.BookId,
                    Quantity = od.Quantity,
                    Price = od.Price
                }).ToList()
            };

            return await _orderRepository.AddOrderAsync(order);
        }

        public async Task<Order> UpdateOrderAsync(OrderUpdateDto orderDto)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderDto.OrderId);
            if (order != null)
            {
                // Assuming that updating order might only change its details
                order.OrderDetails = orderDto.OrderDetails.Select(od => new OrderDetail
                {
                    OrderDetailId = od.OrderDetailId,
                    BookId = od.BookId,
                    Quantity = od.Quantity,
                    Price = od.Price
                }).ToList();

                return await _orderRepository.UpdateOrderAsync(order);
            }
            return null;
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            return await _orderRepository.DeleteOrderAsync(orderId);
        }
    }
}
