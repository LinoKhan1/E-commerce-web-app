using e_commerce_app.Server.APIs.DTOs.CategoryDTOs;
using e_commerce_app.Server.Core.Entities;

namespace e_commerce_app.Server.Core.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO> GetCategoryByIdAsync(int categoryId);
        Task AddCategoryAsync(CategoryDTO categoryDto);
        Task UpdateCategoryAsync(int categoryId, CategoryDTO categoryDto);
        Task DeleteCategoryAsync(int categoryId);
    }
}
