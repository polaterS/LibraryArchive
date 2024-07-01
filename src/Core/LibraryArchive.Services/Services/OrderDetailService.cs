using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.OrderDetail;
using LibraryArchive.Services.Repositories.Interfaces;

namespace LibraryArchive.Services
{
    public class OrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<IEnumerable<OrderDetailReadDto>> GetAllOrderDetailsAsync()
        {
            var orderDetails = await _orderDetailRepository.GetAllOrderDetailsAsync();
            return orderDetails.Select(od => new OrderDetailReadDto
            {
                OrderDetailId = od.OrderDetailId,
                BookId = od.BookId,
                BookTitle = od.Book.Title, // Assuming Book is loaded
                Quantity = od.Quantity,
                Price = od.Price
            }).ToList();
        }

        public async Task<OrderDetailReadDto> GetOrderDetailByIdAsync(int orderDetailId)
        {
            var orderDetail = await _orderDetailRepository.GetOrderDetailByIdAsync(orderDetailId);
            if (orderDetail != null)
            {
                return new OrderDetailReadDto
                {
                    OrderDetailId = orderDetail.OrderDetailId,
                    BookId = orderDetail.BookId,
                    BookTitle = orderDetail.Book.Title, // Assuming Book is loaded
                    Quantity = orderDetail.Quantity,
                    Price = orderDetail.Price
                };
            }
            return null;
        }



        public async Task<OrderDetail> AddOrderDetailAsync(OrderDetailCreateDto orderDetailDto)
        {
            var orderDetail = new OrderDetail
            {
                OrderId = orderDetailDto.OrderId,
                BookId = orderDetailDto.BookId,
                Quantity = orderDetailDto.Quantity,
                Price = orderDetailDto.Price
            };

            return await _orderDetailRepository.AddOrderDetailAsync(orderDetail);
        }

        public async Task<OrderDetail> UpdateOrderDetailAsync(OrderDetailUpdateDto orderDetailDto)
        {
            var orderDetail = await _orderDetailRepository.GetOrderDetailByIdAsync(orderDetailDto.OrderDetailId);
            if (orderDetail != null)
            {
                orderDetail.BookId = orderDetailDto.BookId;
                orderDetail.Quantity = orderDetailDto.Quantity;
                orderDetail.Price = orderDetailDto.Price;

                return await _orderDetailRepository.UpdateOrderDetailAsync(orderDetail);
            }
            return null;
        }

        public async Task<bool> DeleteOrderDetailAsync(int orderDetailId)
        {
            return await _orderDetailRepository.DeleteOrderDetailAsync(orderDetailId);
        }
    }
}
