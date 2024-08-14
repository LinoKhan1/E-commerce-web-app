using AutoMapper;
using e_commerce_app.Server.APIs.DTOs.ProductDTOs;
using e_commerce_app.Server.Core.Entities;
using e_commerce_app.Server.Core.Services.Interfaces;
using e_commerce_app.Server.Infrastructure.Repositories.Interfaces;
using e_commerce_app.Server.Infrastructure.unitOfWork;
using Microsoft.Extensions.Logging;
namespace e_commerce_app.Server.Core.Services
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, IMapper mapper, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            try
            {
                var products = await _productRepository.GetAllProductsAsync();
                return _mapper.Map<IEnumerable<ProductDTO>>(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving products.");
                throw;
            }
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsAsync(int limit, int offset)
        {
            try
            {
                var products = await _productRepository.GetProductsAsync(limit, offset);
                return _mapper.Map<IEnumerable<ProductDTO>>(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving products.");
                throw;
            }
        }
        public async Task<ProductDTO> GetProductByIdAsync(int productId)
        {
            try
            {
                var product = await _productRepository.GetProductByIdAsync(productId);
                if (product == null) throw new KeyNotFoundException($"Product with ID {productId} not found.");
                return _mapper.Map<ProductDTO>(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving product with ID {productId}.");
                throw;
            }
        }
        public async Task<IEnumerable<ProductDTO>> GetProductsByCategoryAsync(int categoryId)
        {
            try
            {
                var products = await _productRepository.GetProductsByCategoryAsync(categoryId);
                return _mapper.Map<IEnumerable<ProductDTO>>(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving products for category ID {categoryId}.");
                throw;
            }
        }

        public async Task AddProductAsync(ProductDTO productDto)
        {
            try
            {
                if (productDto == null)
                {
                    _logger.LogWarning("AddProductAsync: ProductDTO is null.");
                    throw new ArgumentNullException(nameof(productDto));
                }

                var product = _mapper.Map<Product>(productDto);
                await _productRepository.AddProductAsync(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new product.");
                throw; // Rethrow to handle at the controller level
            }
        }

        public async Task UpdateProductAsync(int productId, ProductDTO productDto)
        {
            try
            {
                var existingProduct = await _productRepository.GetProductByIdAsync(productId);
                if (existingProduct == null) throw new KeyNotFoundException($"Product with ID {productId} not found.");

                _mapper.Map(productDto, existingProduct);
                await _productRepository.UpdateProductAsync(existingProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating product with ID {productId}.");
                throw;
            }
        }

        public async Task DeleteProductAsync(int productId)
        {
            try
            {
                await _productRepository.DeleteProductAsync(productId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting product with ID {productId}.");
                throw;
            }
        }
    }
}
