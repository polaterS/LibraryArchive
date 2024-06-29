using AutoMapper;
using FluentValidation;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.Category;
using LibraryArchive.Services.Repositories.Interfaces;
using Serilog;

namespace LibraryArchive.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IValidator<CategoryCreateDto> _categoryCreateValidator;
        private readonly IValidator<CategoryUpdateDto> _categoryUpdateValidator;
        private readonly IValidator<CategoryDeleteDto> _categoryDeleteValidator;

        public CategoryService(
            ICategoryRepository categoryRepository,
            IMapper mapper,
            IValidator<CategoryCreateDto> categoryCreateValidator,
            IValidator<CategoryUpdateDto> categoryUpdateValidator,
            IValidator<CategoryDeleteDto> categoryDeleteValidator)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = Log.ForContext<CategoryService>();
            _categoryCreateValidator = categoryCreateValidator;
            _categoryUpdateValidator = categoryUpdateValidator;
            _categoryDeleteValidator = categoryDeleteValidator;
        }

        public async Task<CategoryReadDto> GetCategoryByIdAsync(int categoryId)
        {
            try
            {
                _logger.Information("Getting category by ID: {CategoryId}", categoryId);
                var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
                return _mapper.Map<CategoryReadDto>(category);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting category by ID: {CategoryId}", categoryId);
                throw;
            }
        }

        public async Task<IEnumerable<CategoryReadDto>> GetAllCategoriesAsync()
        {
            try
            {
                _logger.Information("Getting all categories");
                var categories = await _categoryRepository.GetAllCategoriesAsync();
                return _mapper.Map<IEnumerable<CategoryReadDto>>(categories);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting all categories");
                throw;
            }
        }

        public async Task AddCategoryAsync(CategoryCreateDto categoryCreateDto)
        {
            await _categoryCreateValidator.ValidateAndThrowAsync(categoryCreateDto);
            try
            {
                var category = _mapper.Map<Category>(categoryCreateDto);
                _logger.Information("Adding category: {Category}", category);
                await _categoryRepository.AddCategoryAsync(category);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error adding category: {CategoryCreateDto}", categoryCreateDto);
                throw;
            }
        }

        public void RemoveCategory(CategoryDeleteDto categoryDeleteDto)
        {
            _categoryDeleteValidator.ValidateAndThrow(categoryDeleteDto);
            try
            {
                var category = _mapper.Map<Category>(categoryDeleteDto);
                _logger.Information("Removing category: {Category}", category);
                _categoryRepository.RemoveCategory(category);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error removing category: {CategoryDeleteDto}", categoryDeleteDto);
                throw;
            }
        }

        public void UpdateCategory(CategoryUpdateDto categoryUpdateDto)
        {
            _categoryUpdateValidator.ValidateAndThrow(categoryUpdateDto);
            try
            {
                var category = _mapper.Map<Category>(categoryUpdateDto);
                _logger.Information("Updating category: {Category}", category);
                _categoryRepository.UpdateCategory(category);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating category: {CategoryUpdateDto}", categoryUpdateDto);
                throw;
            }
        }
    }
}
