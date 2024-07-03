using LibraryArchive.Services.DTOs.Book;

namespace LibraryArchive.Services.DTOs.Category
{
    public class CategoryReadDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int BooksCount { get; set; }
        public List<BookReadDto> Books { get; set; }
    }
}
