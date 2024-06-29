namespace LibraryArchive.Data.Entities
{
    public class Order
    {
        public string UserId { get; set; }
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
