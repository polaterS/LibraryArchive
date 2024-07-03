using AutoMapper;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.Notification;
using LibraryArchive.Services.DTOs.Order;
using LibraryArchive.Services.DTOs.Address;
using LibraryArchive.Services.Repositories.Interfaces;
using LibraryArchive.Services.TaskManager.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryArchive.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly INotificationSenderService _notificationSenderService;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderService> _logger;

        public OrderService(
            IOrderRepository orderRepository,
            IAddressRepository addressRepository,
            INotificationSenderService notificationSenderService,
            IMapper mapper,
            ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _addressRepository = addressRepository;
            _notificationSenderService = notificationSenderService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<OrderReadDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return _mapper.Map<IEnumerable<OrderReadDto>>(orders);
        }

        public async Task<OrderReadDto> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            return order != null ? _mapper.Map<OrderReadDto>(order) : null;
        }

        public async Task<OrderReadDto> AddOrderAsync(OrderCreateDto orderDto, string userId)
        {
            var address = new Address
            {
                UserId = userId,
                Street = orderDto.Address.Street,
                City = orderDto.Address.City,
                State = orderDto.Address.State,
                PostalCode = orderDto.Address.PostalCode,
                Country = orderDto.Address.Country,
                IsDefault = false
            };

            await _addressRepository.AddAddressAsync(address);

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                OrderDetails = new List<OrderDetail>()
            };

            foreach (var orderDetailDto in orderDto.OrderDetails)
            {
                var orderDetail = new OrderDetail
                {
                    BookId = orderDetailDto.BookId,
                    Quantity = orderDetailDto.Quantity,
                    Price = orderDetailDto.Price,
                    AddressId = address.AddressId 
                };
                order.OrderDetails.Add(orderDetail);
            }

            var addedOrder = await _orderRepository.AddOrderAsync(order);

            // Bildirim gönder
            var notificationDto = new NotificationCreateDto
            {
                UserId = addedOrder.UserId,
                Title = "Order Placed",
                Message = $"Your order {addedOrder.OrderId} has been placed successfully.",
                Date = DateTime.Now,
                NotificationType = "Email"
            };

            await _notificationSenderService.SendNotificationAsync(notificationDto);

            return _mapper.Map<OrderReadDto>(addedOrder);
        }

        public async Task<OrderReadDto> UpdateOrderAsync(OrderUpdateDto orderDto)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderDto.OrderId);
            if (order != null)
            {
                _mapper.Map(orderDto, order);
                var updatedOrder = await _orderRepository.UpdateOrderAsync(order);
                return _mapper.Map<OrderReadDto>(updatedOrder);
            }
            return null;
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            return await _orderRepository.DeleteOrderAsync(orderId);
        }
    }
}
