namespace LibraryArchive.Services.DTOs.OrderDetail
{
    public class OrderDetailUpdateDto
    {
        public int OrderDetailId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
