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
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;


        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, ILogger<ProductService> logger, IMapper mapper)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
        {
            try
            {
                var products = await _productRepository.GetAllProductsAsync();
                return _mapper.Map<IEnumerable<ProductDTO>>(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all products.");
                throw new ApplicationException("An error occurred while getting all products.", ex);
            }
        }

        public async Task<ProductDTO> GetProductByIdAsync(int productId)
        {
            try
            {
                var product = await _productRepository.GetProductByIdAsync(productId);
                return _mapper.Map<ProductDTO>(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting the product with ID {productId}.");
                throw new ApplicationException($"An error occurred while getting the product with ID {productId}.", ex);
            }
        }

        public async Task AddProductAsync(ProductDTO productDto)
        {
            try
            {
                var product = _mapper.Map<Product>(productDto);
                await _productRepository.AddProductAsync(product);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new product.");
                throw new ApplicationException("An error occurred while adding a new product.", ex);
            }
        }

        public async Task UpdateProductAsync(ProductDTO productDto)
        {
            try
            {
                var product = _mapper.Map<Product>(productDto);
                await _productRepository.UpdateProductAsync(product);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the product with ID {productDto.Id}.");
                throw new ApplicationException($"An error occurred while updating the product with ID {productDto.Id}.", ex);
            }
        }

        public async Task DeleteProductAsync(int productId)
        {
            try
            {
                await _productRepository.DeleteProductAsync(productId);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the product with ID {productId}.");
                throw new ApplicationException($"An error occurred while deleting the product with ID {productId}.", ex);
            }
        }
    }
}
