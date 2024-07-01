using LibraryArchive.Data.Context;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryArchive.Services.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly LibraryArchiveContext _context;

        public CategoryRepository(LibraryArchiveContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var category = await GetCategoryByIdAsync(categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
