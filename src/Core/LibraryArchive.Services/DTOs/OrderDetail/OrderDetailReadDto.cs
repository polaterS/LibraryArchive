namespace LibraryArchive.Services.DTOs.OrderDetail
{
    public class OrderDetailReadDto
    {
        public int OrderDetailId { get; set; }
        public int BookId { get; set; }  // Kitabın ID'si
        public string BookTitle { get; set; }  // Satın alınan kitabın başlığı
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
