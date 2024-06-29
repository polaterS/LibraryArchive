using LibraryArchive.Data.Entities;

namespace LibraryArchive.Services.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryByIdAsync(int categoryId);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task AddCategoryAsync(Category category);
        void RemoveCategory(Category category);
        void UpdateCategory(Category category);
    }
}
