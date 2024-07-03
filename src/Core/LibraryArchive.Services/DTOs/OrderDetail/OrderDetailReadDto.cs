namespace LibraryArchive.Services.DTOs.OrderDetail
{
    public class OrderDetailReadDto
    {
        public int OrderDetailId { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
