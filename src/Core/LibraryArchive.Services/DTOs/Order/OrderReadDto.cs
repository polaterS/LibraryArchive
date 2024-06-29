using LibraryArchive.Services.DTOs.OrderDetail;

namespace LibraryArchive.Services.DTOs.Order
{
    public class OrderReadDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserName { get; set; }  // Siparişi veren kullanıcının adı
        public List<OrderDetailReadDto> OrderDetails { get; set; }
    }
}
