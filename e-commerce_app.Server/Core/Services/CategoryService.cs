using AutoMapper;
using e_commerce_app.Server.APIs.DTOs.CategoryDTOs;
using e_commerce_app.Server.Core.Entities;
using e_commerce_app.Server.Core.Services.Interfaces;
using e_commerce_app.Server.Infrastructure.Repositories.Interfaces;
using e_commerce_app.Server.Infrastructure.unitOfWork;
using Microsoft.Extensions.Logging;

namespace e_commerce_app.Server.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, ILogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await _categoryRepository.GetAllCategoriesAsync();
                return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving categories.");
                throw;
            }
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(int categoryId)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
                if (category == null) throw new KeyNotFoundException($"Category with ID {categoryId} not found.");
                return _mapper.Map<CategoryDTO>(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving category with ID {categoryId}.");
                throw;
            }
        }

        public async Task AddCategoryAsync(CategoryDTO categoryDto)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryDto);
                await _categoryRepository.AddCategoryAsync(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new category.");
                throw;
            }
        }

        public async Task UpdateCategoryAsync(int categoryId, CategoryDTO categoryDto)
        {
            try
            {
                var existingCategory = await _categoryRepository.GetCategoryByIdAsync(categoryId);
                if (existingCategory == null) throw new KeyNotFoundException($"Category with ID {categoryId} not found.");

                _mapper.Map(categoryDto, existingCategory);
                await _categoryRepository.UpdateCategoryAsync(existingCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating category with ID {categoryId}.");
                throw;
            }
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            try
            {
                await _categoryRepository.DeleteCategoryAsync(categoryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting category with ID {categoryId}.");
                throw;
            }
        }
    }
}
