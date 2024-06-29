using AutoMapper;
using FluentValidation;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.Order;
using LibraryArchive.Services.Repositories.Interfaces;
using Serilog;

namespace LibraryArchive.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IValidator<OrderCreateDto> _orderCreateValidator;
        private readonly IValidator<OrderUpdateDto> _orderUpdateValidator;
        private readonly IValidator<OrderDeleteDto> _orderDeleteValidator;

        public OrderService(
            IOrderRepository orderRepository,
            IMapper mapper,
            IValidator<OrderCreateDto> orderCreateValidator,
            IValidator<OrderUpdateDto> orderUpdateValidator,
            IValidator<OrderDeleteDto> orderDeleteValidator)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = Log.ForContext<OrderService>();
            _orderCreateValidator = orderCreateValidator;
            _orderUpdateValidator = orderUpdateValidator;
            _orderDeleteValidator = orderDeleteValidator;
        }

        public async Task<OrderReadDto> GetOrderByIdAsync(int orderId)
        {
            try
            {
                _logger.Information("Getting order by ID: {OrderId}", orderId);
                var order = await _orderRepository.GetOrderByIdAsync(orderId);
                return _mapper.Map<OrderReadDto>(order);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting order by ID: {OrderId}", orderId);
                throw;
            }
        }

        public async Task<IEnumerable<OrderReadDto>> GetAllOrdersAsync()
        {
            try
            {
                _logger.Information("Getting all orders");
                var orders = await _orderRepository.GetAllOrdersAsync();
                return _mapper.Map<IEnumerable<OrderReadDto>>(orders);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting all orders");
                throw;
            }
        }

        public async Task<IEnumerable<OrderReadDto>> GetOrdersByUserIdAsync(string userId)
        {
            try
            {
                _logger.Information("Getting orders by user ID: {UserId}", userId);
                var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
                return _mapper.Map<IEnumerable<OrderReadDto>>(orders);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting orders by user ID: {UserId}", userId);
                throw;
            }
        }

        public async Task AddOrderAsync(OrderCreateDto orderCreateDto)
        {
            await _orderCreateValidator.ValidateAndThrowAsync(orderCreateDto);
            try
            {
                var order = _mapper.Map<Order>(orderCreateDto);
                _logger.Information("Adding order: {Order}", order);
                await _orderRepository.AddOrderAsync(order);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error adding order: {OrderCreateDto}", orderCreateDto);
                throw;
            }
        }

        public void RemoveOrder(OrderDeleteDto orderDeleteDto)
        {
            _orderDeleteValidator.ValidateAndThrow(orderDeleteDto);
            try
            {
                var order = _mapper.Map<Order>(orderDeleteDto);
                _logger.Information("Removing order: {Order}", order);
                _orderRepository.RemoveOrder(order);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error removing order: {OrderDeleteDto}", orderDeleteDto);
                throw;
            }
        }

        public void UpdateOrder(OrderUpdateDto orderUpdateDto)
        {
            _orderUpdateValidator.ValidateAndThrow(orderUpdateDto);
            try
            {
                var order = _mapper.Map<Order>(orderUpdateDto);
                _logger.Information("Updating order: {Order}", order);
                _orderRepository.UpdateOrder(order);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating order: {OrderUpdateDto}", orderUpdateDto);
                throw;
            }
        }
    }
}
