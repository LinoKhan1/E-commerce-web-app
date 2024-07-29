using e_commerce_app.Server.Core.Entities;
using e_commerce_app.Server.Infrastructure.Data;
using e_commerce_app.Server.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_app.Server.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CategoryRepository> _logger;

        public CategoryRepository(ApplicationDbContext context, ILogger<CategoryRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all categories from the database.");
                return await _context.Categories.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching categories.");
                throw;
            }
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            try
            {
                _logger.LogInformation("Fetching category with ID {CategoryId} from the database.", categoryId);
                var category = await _context.Categories.FindAsync(categoryId);

                if (category == null)
                {
                    _logger.LogWarning("Category with ID {CategoryId} not found.", categoryId);
                }

                return category;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the category with ID {CategoryId}.", categoryId);
                throw;
            }
        }

        public async Task AddCategoryAsync(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            try
            {
                _logger.LogInformation("Adding a new category to the database.");
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the category.");
                throw;
            }
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            try
            {
                _logger.LogInformation("Updating category with ID {CategoryId} in the database.", category.CategoryId);
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the category with ID {CategoryId}.", category.CategoryId);
                throw;
            }
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            try
            {
                var category = await _context.Categories.FindAsync(categoryId);
                if (category == null)
                {
                    _logger.LogWarning("Category with ID {CategoryId} not found for deletion.", categoryId);
                    return;
                }

                _logger.LogInformation("Deleting category with ID {CategoryId} from the database.", categoryId);
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the category with ID {CategoryId}.", categoryId);
                throw;
            }
        }
    }
}
