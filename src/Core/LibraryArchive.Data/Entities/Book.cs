namespace LibraryArchive.Data.Entities
{
    public class Book
    {
        public string UserId { get; set; }
        public int CategoryId { get; set; }
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string CoverImageUrl { get; set; }
        public string ShelfLocation { get; set; }
        public virtual Category Category { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}