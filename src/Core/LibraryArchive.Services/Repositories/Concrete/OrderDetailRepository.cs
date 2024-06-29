using LibraryArchive.Data.Context;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LibraryArchive.Services.Repositories.Concrete
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly LibraryArchiveContext _context;
        private readonly ILogger _logger;

        public OrderDetailRepository(LibraryArchiveContext context)
        {
            _context = context;
            _logger = Log.ForContext<OrderDetailRepository>();
        }

        public async Task<OrderDetail> GetOrderDetailByIdAsync(int orderDetailId)
        {
            try
            {
                _logger.Information("Getting order detail by ID: {OrderDetailId}", orderDetailId);
                return await _context.OrderDetails
                    .Include(od => od.Book)
                    .Include(od => od.Order)
                    .FirstOrDefaultAsync(od => od.OrderDetailId == orderDetailId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting order detail by ID: {OrderDetailId}", orderDetailId);
                throw;
            }
        }

        public async Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync()
        {
            try
            {
                _logger.Information("Getting all order details");
                return await _context.OrderDetails
                    .Include(od => od.Book)
                    .Include(od => od.Order)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting all order details");
                throw;
            }
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderIdAsync(int orderId)
        {
            try
            {
                _logger.Information("Getting order details by order ID: {OrderId}", orderId);
                return await _context.OrderDetails
                    .Include(od => od.Book)
                    .Include(od => od.Order)
                    .Where(od => od.OrderId == orderId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting order details by order ID: {OrderId}", orderId);
                throw;
            }
        }

        public async Task AddOrderDetailAsync(OrderDetail orderDetail)
        {
            try
            {
                _logger.Information("Adding order detail: {OrderDetail}", orderDetail);
                await _context.OrderDetails.AddAsync(orderDetail);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error adding order detail: {OrderDetail}", orderDetail);
                throw;
            }
        }

        public void RemoveOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                _logger.Information("Removing order detail: {OrderDetail}", orderDetail);
                _context.OrderDetails.Remove(orderDetail);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error removing order detail: {OrderDetail}", orderDetail);
                throw;
            }
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                _logger.Information("Updating order detail: {OrderDetail}", orderDetail);
                _context.OrderDetails.Update(orderDetail);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating order detail: {OrderDetail}", orderDetail);
                throw;
            }
        }
    }
}
