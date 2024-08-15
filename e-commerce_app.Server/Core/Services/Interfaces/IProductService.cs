using e_commerce_app.Server.APIs.DTOs.ProductDTOs;
using e_commerce_app.Server.Core.Entities;
using System.Diagnostics.Eventing.Reader;

namespace e_commerce_app.Server.Core.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO> GetProductByIdAsync(int productId);
        Task<IEnumerable<ProductDTO>> GetProductsByCategoryAsync(int categoryId);   
        Task AddProductAsync(ProductDTO productDto);
        Task UpdateProductAsync(int productId, ProductDTO productDto);
        Task DeleteProductAsync(int productId);

    }
}
