using e_commerce_app.Server.Core.Entities;

namespace e_commerce_app.Server.Infrastructure.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<Product> GetProductByIdAsync(int productId);

        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int productId);

    }
}
