using AutoMapper;
using FluentValidation;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.OrderDetail;
using LibraryArchive.Services.Repositories.Interfaces;
using Serilog;

namespace LibraryArchive.Services
{
    public class OrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IValidator<OrderDetailCreateDto> _orderDetailCreateValidator;
        private readonly IValidator<OrderDetailUpdateDto> _orderDetailUpdateValidator;
        private readonly IValidator<OrderDetailDeleteDto> _orderDetailDeleteValidator;

        public OrderDetailService(
            IOrderDetailRepository orderDetailRepository,
            IMapper mapper,
            IValidator<OrderDetailCreateDto> orderDetailCreateValidator,
            IValidator<OrderDetailUpdateDto> orderDetailUpdateValidator,
            IValidator<OrderDetailDeleteDto> orderDetailDeleteValidator)
        {
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
            _logger = Log.ForContext<OrderDetailService>();
            _orderDetailCreateValidator = orderDetailCreateValidator;
            _orderDetailUpdateValidator = orderDetailUpdateValidator;
            _orderDetailDeleteValidator = orderDetailDeleteValidator;
        }

        public async Task<OrderDetailReadDto> GetOrderDetailByIdAsync(int orderDetailId)
        {
            try
            {
                _logger.Information("Getting order detail by ID: {OrderDetailId}", orderDetailId);
                var orderDetail = await _orderDetailRepository.GetOrderDetailByIdAsync(orderDetailId);
                return _mapper.Map<OrderDetailReadDto>(orderDetail);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting order detail by ID: {OrderDetailId}", orderDetailId);
                throw;
            }
        }

        public async Task<IEnumerable<OrderDetailReadDto>> GetAllOrderDetailsAsync()
        {
            try
            {
                _logger.Information("Getting all order details");
                var orderDetails = await _orderDetailRepository.GetAllOrderDetailsAsync();
                return _mapper.Map<IEnumerable<OrderDetailReadDto>>(orderDetails);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting all order details");
                throw;
            }
        }

        public async Task<IEnumerable<OrderDetailReadDto>> GetOrderDetailsByOrderIdAsync(int orderId)
        {
            try
            {
                _logger.Information("Getting order details by order ID: {OrderId}", orderId);
                var orderDetails = await _orderDetailRepository.GetOrderDetailsByOrderIdAsync(orderId);
                return _mapper.Map<IEnumerable<OrderDetailReadDto>>(orderDetails);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting order details by order ID: {OrderId}", orderId);
                throw;
            }
        }

        public async Task AddOrderDetailAsync(OrderDetailCreateDto orderDetailCreateDto)
        {
            await _orderDetailCreateValidator.ValidateAndThrowAsync(orderDetailCreateDto);
            try
            {
                var orderDetail = _mapper.Map<OrderDetail>(orderDetailCreateDto);
                _logger.Information("Adding order detail: {OrderDetail}", orderDetail);
                await _orderDetailRepository.AddOrderDetailAsync(orderDetail);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error adding order detail: {OrderDetailCreateDto}", orderDetailCreateDto);
                throw;
            }
        }

        public void RemoveOrderDetail(OrderDetailDeleteDto orderDetailDeleteDto)
        {
            _orderDetailDeleteValidator.ValidateAndThrow(orderDetailDeleteDto);
            try
            {
                var orderDetail = _mapper.Map<OrderDetail>(orderDetailDeleteDto);
                _logger.Information("Removing order detail: {OrderDetail}", orderDetail);
                _orderDetailRepository.RemoveOrderDetail(orderDetail);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error removing order detail: {OrderDetailDeleteDto}", orderDetailDeleteDto);
                throw;
            }
        }

        public void UpdateOrderDetail(OrderDetailUpdateDto orderDetailUpdateDto)
        {
            _orderDetailUpdateValidator.ValidateAndThrow(orderDetailUpdateDto);
            try
            {
                var orderDetail = _mapper.Map<OrderDetail>(orderDetailUpdateDto);
                _logger.Information("Updating order detail: {OrderDetail}", orderDetail);
                _orderDetailRepository.UpdateOrderDetail(orderDetail);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating order detail: {OrderDetailUpdateDto}", orderDetailUpdateDto);
                throw;
            }
        }
    }
}
