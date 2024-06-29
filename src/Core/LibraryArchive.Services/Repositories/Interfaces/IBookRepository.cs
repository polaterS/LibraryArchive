using LibraryArchive.Data.Entities;

namespace LibraryArchive.Services.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<Book> GetBookByIdAsync(int bookId);
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<IEnumerable<Book>> GetBooksByUserIdAsync(string userId);
        Task AddBookAsync(Book book);
        void RemoveBook(Book book);
        void UpdateBook(Book book);
    }
}
