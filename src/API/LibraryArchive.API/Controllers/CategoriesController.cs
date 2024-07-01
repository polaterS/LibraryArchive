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

        // GET: api/Categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        // GET: api/Categories/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }
            return Ok(category);
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryCreateDto categoryDto)
        {
            var category = await _categoryService.AddCategoryAsync(categoryDto);
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.CategoryId }, category);
        }

        // PUT: api/Categories/{id}
        [HttpPut("{id}")]
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

        // DELETE: api/Categories/{id}
        [HttpDelete("{id}")]
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
