using LibraryArchive.Services.DTOs.OrderDetail;

namespace LibraryArchive.Services.DTOs.Order
{
    public class OrderCreateDto
    {
        public string UserId { get; set; }
        public List<OrderDetailCreateDto> OrderDetails { get; set; }
    }
}
