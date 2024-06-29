using LibraryArchive.Services.DTOs.OrderDetail;

namespace LibraryArchive.Services.DTOs.Order
{
    public class OrderUpdateDto
    {
        public int OrderId { get; set; }
        public List<OrderDetailUpdateDto> OrderDetails { get; set; }
    }
}
