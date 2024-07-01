using AutoMapper;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.OrderDetail;
using LibraryArchive.Services.Repositories.Interfaces;

namespace LibraryArchive.Services
{
    public class OrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository, IMapper mapper)
        {
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDetailReadDto>> GetAllOrderDetailsAsync()
        {
            var orderDetails = await _orderDetailRepository.GetAllOrderDetailsAsync();
            return _mapper.Map<IEnumerable<OrderDetailReadDto>>(orderDetails);
        }

        public async Task<OrderDetailReadDto> GetOrderDetailByIdAsync(int orderDetailId)
        {
            var orderDetail = await _orderDetailRepository.GetOrderDetailByIdAsync(orderDetailId);
            return orderDetail != null ? _mapper.Map<OrderDetailReadDto>(orderDetail) : null;
        }

        public async Task<OrderDetailReadDto> AddOrderDetailAsync(OrderDetailCreateDto orderDetailDto)
        {
            var orderDetail = _mapper.Map<OrderDetail>(orderDetailDto);
            var addedOrderDetail = await _orderDetailRepository.AddOrderDetailAsync(orderDetail);
            return _mapper.Map<OrderDetailReadDto>(addedOrderDetail);
        }

        public async Task<OrderDetailReadDto> UpdateOrderDetailAsync(OrderDetailUpdateDto orderDetailDto)
        {
            var orderDetail = await _orderDetailRepository.GetOrderDetailByIdAsync(orderDetailDto.OrderDetailId);
            if (orderDetail != null)
            {
                _mapper.Map(orderDetailDto, orderDetail);
                var updatedOrderDetail = await _orderDetailRepository.UpdateOrderDetailAsync(orderDetail);
                return _mapper.Map<OrderDetailReadDto>(updatedOrderDetail);
            }
            return null;
        }

        public async Task<bool> DeleteOrderDetailAsync(int orderDetailId)
        {
            return await _orderDetailRepository.DeleteOrderDetailAsync(orderDetailId);
        }
    }
}
