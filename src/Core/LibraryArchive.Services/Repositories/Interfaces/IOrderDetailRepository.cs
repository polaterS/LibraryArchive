using LibraryArchive.Data.Entities;

namespace LibraryArchive.Services.Repositories.Interfaces
{
    public interface IOrderDetailRepository
    {
        Task<OrderDetail> GetOrderDetailByIdAsync(int orderDetailId);
        Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync();
        Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderIdAsync(int orderId);
        Task AddOrderDetailAsync(OrderDetail orderDetail);
        void RemoveOrderDetail(OrderDetail orderDetail);
        void UpdateOrderDetail(OrderDetail orderDetail);
    }
}
