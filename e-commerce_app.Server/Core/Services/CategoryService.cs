using AutoMapper;
using e_commerce_app.Server.APIs.DTOs.CategoryDTOs;
using e_commerce_app.Server.Core.Entities;
using e_commerce_app.Server.Core.Services.Interfaces;
using e_commerce_app.Server.Infrastructure.Repositories.Interfaces;
using e_commerce_app.Server.Infrastructure.unitOfWork;

namespace e_commerce_app.Server.Core.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper, ILogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all categories.");
                var categories = await _categoryRepository.GetAllCategoriesAsync();
                return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving categories.");
                throw new ApplicationException("An error occurred while retrieving categories.", ex);
            }
        }


        public async Task<CategoryDTO> GetCategoryByIdAsync(int categoryId)
        {
            try
            {
                _logger.LogInformation($"Retrieving category with ID {categoryId}.");
                var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);

                if (category == null)
                {
                    _logger.LogWarning($"Category with ID {categoryId} not found.");
                    return null;
                }

                return _mapper.Map<CategoryDTO>(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving category with ID {categoryId}.");
                throw new ApplicationException($"An error occurred while retrieving category with ID {categoryId}.", ex);
            }
        }

        public async Task AddCategoryAsync(CategoryDTO categoryDto)
        {
            if (categoryDto == null)
            {
                throw new ArgumentNullException(nameof(categoryDto));
            }

            try
            {
                var category = _mapper.Map<Category>(categoryDto);
                await _categoryRepository.AddCategoryAsync(category);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the category.");
                throw new ApplicationException("An error occurred while adding the category.", ex);
            }
        }

        public async Task UpdateCategoryAsync(CategoryDTO categoryDto)
        {
            if (categoryDto == null)
            {
                throw new ArgumentNullException(nameof(categoryDto));
            }

            try
            {
                var category = _mapper.Map<Category>(categoryDto);
                await _categoryRepository.UpdateCategoryAsync(category);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the category with ID {categoryDto.Id}.");
                throw new ApplicationException($"An error occurred while updating the category with ID {categoryDto.Id}.", ex);
            }
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            try
            {
                await _categoryRepository.DeleteCategoryAsync(categoryId);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the category with ID {categoryId}.");
                throw new ApplicationException($"An error occurred while deleting the category with ID {categoryId}.", ex);
            }
        }

    }
}
