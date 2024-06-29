namespace LibraryArchive.Services.DTOs.Book
{
    public class BookUpdateDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string CoverImageUrl { get; set; }
        public string ShelfLocation { get; set; }
        public int CategoryId { get; set; }
    }
}
