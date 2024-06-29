using LibraryArchive.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryArchive.Services.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);
        Task AddOrderAsync(Order order);
        void RemoveOrder(Order order);
        void UpdateOrder(Order order);
    }
}
