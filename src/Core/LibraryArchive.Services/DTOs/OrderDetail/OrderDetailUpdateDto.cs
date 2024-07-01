namespace LibraryArchive.Services.DTOs.OrderDetail
{
    public class OrderDetailUpdateDto
    {
        public int OrderDetailId { get; set; }
        public int BookId { get; set; }  // Kitabın ID'si
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
