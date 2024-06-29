using LibraryArchive.Data.Context;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LibraryArchive.Services.Repositories.Concrete
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly LibraryArchiveContext _context;
        private readonly ILogger _logger;

        public CategoryRepository(LibraryArchiveContext context)
        {
            _context = context;
            _logger = Log.ForContext<CategoryRepository>();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            try
            {
                _logger.Information("Getting category by ID: {CategoryId}", categoryId);
                return await _context.Categories.FindAsync(categoryId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting category by ID: {CategoryId}", categoryId);
                throw;
            }
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            try
            {
                _logger.Information("Getting all categories");
                return await _context.Categories.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting all categories");
                throw;
            }
        }

        public async Task AddCategoryAsync(Category category)
        {
            try
            {
                _logger.Information("Adding category: {Category}", category);
                await _context.Categories.AddAsync(category);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error adding category: {Category}", category);
                throw;
            }
        }

        public void RemoveCategory(Category category)
        {
            try
            {
                _logger.Information("Removing category: {Category}", category);
                _context.Categories.Remove(category);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error removing category: {Category}", category);
                throw;
            }
        }

        public void UpdateCategory(Category category)
        {
            try
            {
                _logger.Information("Updating category: {Category}", category);
                _context.Categories.Update(category);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating category: {Category}", category);
                throw;
            }
        }
    }
}
