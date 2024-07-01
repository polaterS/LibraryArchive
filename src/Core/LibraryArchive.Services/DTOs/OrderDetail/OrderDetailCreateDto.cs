namespace LibraryArchive.Services.DTOs.OrderDetail
{
    public class OrderDetailCreateDto
    {
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
