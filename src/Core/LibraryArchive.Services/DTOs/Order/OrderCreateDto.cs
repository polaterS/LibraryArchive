using LibraryArchive.Services.DTOs.OrderDetail;
using LibraryArchive.Services.DTOs.Address;

namespace LibraryArchive.Services.DTOs.Order
{
    public class OrderCreateDto
    {
        public List<OrderDetailCreateDto> OrderDetails { get; set; }
        public AddressCreateDto Address { get; set; }
    }
}
