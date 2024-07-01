using LibraryArchive.Data.Entities;
using LibraryArchive.Services;
using LibraryArchive.Services.DTOs.Category;
using Microsoft.AspNetCore.Mvc;

namespace LibraryArchive.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoriesController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Tüm kategorileri alır.
        /// </summary>
        /// <returns>Kategori listesi</returns>
        /// <response code="200">Kategori listesi başarıyla döndürüldü</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryReadDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        /// <summary>
        /// Belirli bir ID'ye sahip kategoriyi alır.
        /// </summary>
        /// <param name="id">Kategori ID'si</param>
        /// <returns>Kategori detayları</returns>
        /// <response code="200">Kategori detayları başarıyla döndürüldü</response>
        /// <response code="404">Kategori bulunamadı</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }
            return Ok(category);
        }

        /// <summary>
        /// Yeni bir kategori ekler.
        /// </summary>
        /// <param name="categoryDto">Kategori detayları</param>
        /// <returns>Eklenen kategori detayları</returns>
        /// <response code="201">Kategori başarıyla eklendi</response>
        /// <response code="400">Kategori detayları yanlışsa</response>
        [HttpPost]
        [ProducesResponseType(typeof(Category), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCategory([FromBody] CategoryCreateDto categoryDto)
        {
            var category = await _categoryService.AddCategoryAsync(categoryDto);
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.CategoryId }, category);
        }

        /// <summary>
        /// Belirli bir ID'ye sahip kategoriyi günceller.
        /// </summary>
        /// <param name="id">Kategori ID'si</param>
        /// <param name="categoryDto">Güncellenmiş kategori detayları</param>
        /// <returns>NoContent</returns>
        /// <response code="204">Kategori başarıyla güncellendi</response>
        /// <response code="400">Kategori ID uyumsuzluğu veya detayları yanlışsa</response>
        /// <response code="404">Kategori bulunamadı</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryUpdateDto categoryDto)
        {
            if (id != categoryDto.CategoryId)
            {
                return BadRequest("Category ID mismatch");
            }

            var updatedCategory = await _categoryService.UpdateCategoryAsync(categoryDto);
            if (updatedCategory == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }
            return NoContent();
        }

        /// <summary>
        /// Belirli bir ID'ye sahip kategoriyi siler.
        /// </summary>
        /// <param name="id">Kategori ID'si</param>
        /// <returns>NoContent</returns>
        /// <response code="204">Kategori başarıyla silindi</response>
        /// <response code="404">Kategori bulunamadı</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            bool result = await _categoryService.DeleteCategoryAsync(id);
            if (!result)
            {
                return NotFound($"Category with ID {id} not found.");
            }
            return NoContent();
        }
    }
}
