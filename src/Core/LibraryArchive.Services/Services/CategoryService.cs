using AutoMapper;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.Book;
using LibraryArchive.Services.DTOs.Category;
using LibraryArchive.Services.Repositories.Interfaces;

namespace LibraryArchive.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryReadDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            var categoryDtos = _mapper.Map<IEnumerable<CategoryReadDto>>(categories);

            foreach (var categoryDto in categoryDtos)
            {
                var category = categories.First(c => c.CategoryId == categoryDto.CategoryId);
                categoryDto.BooksCount = category.Books.Count;
            }

            return categoryDtos;
        }

        public async Task<CategoryReadDto> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            var categoryDto = _mapper.Map<CategoryReadDto>(category);

            if (category != null)
            {
                categoryDto.BooksCount = category.Books.Count;
                categoryDto.Books = _mapper.Map<List<BookReadDto>>(category.Books);
            }

            return categoryDto;
        }

        public async Task<CategoryReadDto> AddCategoryAsync(CategoryCreateDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            var addedCategory = await _categoryRepository.AddCategoryAsync(category);
            return _mapper.Map<CategoryReadDto>(addedCategory);
        }

        public async Task<CategoryReadDto> UpdateCategoryAsync(CategoryUpdateDto categoryDto)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryDto.CategoryId);
            if (category != null)
            {
                _mapper.Map(categoryDto, category);
                var updatedCategory = await _categoryRepository.UpdateCategoryAsync(category);
                return _mapper.Map<CategoryReadDto>(updatedCategory);
            }
            return null;
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            return await _categoryRepository.DeleteCategoryAsync(categoryId);
        }
    }
}
