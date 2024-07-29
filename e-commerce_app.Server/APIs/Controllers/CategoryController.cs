using e_commerce_app.Server.APIs.DTOs.CategoryDTOs;
using e_commerce_app.Server.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_app.Server.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor for CategoryController.
        /// </summary>
        /// <param name="categoryService">The category service instance.</param>
        /// <param name="logger">The logger instance.</param>
        public CategoryController(ICategoryService categoryService, ILogger logger)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns>A list of all categories.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                _logger.LogInformation("Retrieved all categories successfully.");
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all categories.");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Retrieves a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category.</param>
        /// <returns>The category with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    _logger.LogWarning("Category with ID {CategoryId} not found.", id);
                    return NotFound();
                }
                _logger.LogInformation("Retrieved category with ID {CategoryId} successfully.", id);
                return Ok(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the category with ID {CategoryId}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Adds a new category.
        /// </summary>
        /// <param name="categoryDTO">The category to add.</param>
        /// <returns>The newly created category.</returns>
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> PostCategory(CategoryDTO categoryDTO)
        {
            try
            {
                await _categoryService.AddCategoryAsync(categoryDTO);
                _logger.LogInformation("Category with ID {CategoryId} added successfully.", categoryDTO.Id);
                return CreatedAtAction(nameof(GetCategory), new { id = categoryDTO.Id }, categoryDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new category.");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="id">The ID of the category to update.</param>
        /// <param name="categoryDTO">The updated category information.</param>
        /// <returns>No content.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.Id)
            {
                _logger.LogWarning("ID mismatch: route ID {RouteId} does not match category ID {CategoryId}.", id, categoryDTO.Id);
                return BadRequest();
            }

            try
            {
                await _categoryService.UpdateCategoryAsync(categoryDTO);
                _logger.LogInformation("Category with ID {CategoryId} updated successfully.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the category with ID {CategoryId}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Deletes a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(id);
                _logger.LogInformation("Category with ID {CategoryId} deleted successfully.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the category with ID {CategoryId}.", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
