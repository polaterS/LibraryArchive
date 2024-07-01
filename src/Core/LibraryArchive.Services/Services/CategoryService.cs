using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.Category;
using LibraryArchive.Services.Repositories.Interfaces;

namespace LibraryArchive.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryReadDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return categories.Select(c => new CategoryReadDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                BooksCount = c.Books.Count
            }).ToList();
        }

        public async Task<CategoryReadDto> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            if (category != null)
            {
                return new CategoryReadDto
                {
                    CategoryId = category.CategoryId,
                    Name = category.Name,
                    BooksCount = category.Books.Count
                };
            }
            return null;
        }

        public async Task<Category> AddCategoryAsync(CategoryCreateDto categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name
            };

            return await _categoryRepository.AddCategoryAsync(category);
        }

        public async Task<Category> UpdateCategoryAsync(CategoryUpdateDto categoryDto)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryDto.CategoryId);
            if (category != null)
            {
                category.Name = categoryDto.Name;
                return await _categoryRepository.UpdateCategoryAsync(category);
            }
            return null;
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            return await _categoryRepository.DeleteCategoryAsync(categoryId);
        }
    }
}
