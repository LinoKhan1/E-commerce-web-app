using e_commerce_app.Server.Core.Entities;
using e_commerce_app.Server.Infrastructure.Data;
using e_commerce_app.Server.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace e_commerce_app.Server.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CategoryRepository> _logger;

        public CategoryRepository(ApplicationDbContext context, ILogger<CategoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            try
            {
                return await _context.Categories.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving categories.");
                throw;
            }
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            try
            {
                return await _context.Categories.FindAsync(categoryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving category with ID {categoryId}.");
                throw;
            }
        }

        public async Task AddCategoryAsync(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new category.");
                throw;
            }
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            try
            {
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating category with ID {category.CategoryId}.");
                throw;
            }
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            try
            {
                var category = await _context.Categories.FindAsync(categoryId);
                if (category == null) throw new KeyNotFoundException($"Category with ID {categoryId} not found.");

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting category with ID {categoryId}.");
                throw;
            }
        }
    }
}
