namespace LibraryArchive.Services.DTOs.Book
{
    public class BookReadDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string CoverImageUrl { get; set; }
        public string ShelfLocation { get; set; }
        public string CategoryName { get; set; } 
    }
}
