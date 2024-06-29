using LibraryArchive.Data.Context;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LibraryArchive.Services.Repositories.Concrete
{
    public class OrderRepository : IOrderRepository
    {
        private readonly LibraryArchiveContext _context;
        private readonly ILogger _logger;

        public OrderRepository(LibraryArchiveContext context)
        {
            _context = context;
            _logger = Log.ForContext<OrderRepository>();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            try
            {
                _logger.Information("Getting order by ID: {OrderId}", orderId);
                return await _context.Orders
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Book)
                    .Include(o => o.User)
                    .FirstOrDefaultAsync(o => o.OrderId == orderId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting order by ID: {OrderId}", orderId);
                throw;
            }
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            try
            {
                _logger.Information("Getting all orders");
                return await _context.Orders
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Book)
                    .Include(o => o.User)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting all orders");
                throw;
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
        {
            try
            {
                _logger.Information("Getting orders by user ID: {UserId}", userId);
                return await _context.Orders
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Book)
                    .Include(o => o.User)
                    .Where(o => o.UserId == userId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting orders by user ID: {UserId}", userId);
                throw;
            }
        }

        public async Task AddOrderAsync(Order order)
        {
            try
            {
                _logger.Information("Adding order: {Order}", order);
                await _context.Orders.AddAsync(order);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error adding order: {Order}", order);
                throw;
            }
        }

        public void RemoveOrder(Order order)
        {
            try
            {
                _logger.Information("Removing order: {Order}", order);
                _context.Orders.Remove(order);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error removing order: {Order}", order);
                throw;
            }
        }

        public void UpdateOrder(Order order)
        {
            try
            {
                _logger.Information("Updating order: {Order}", order);
                _context.Orders.Update(order);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating order: {Order}", order);
                throw;
            }
        }
    }
}
