using AutoMapper;
using e_commerce_app.Server.APIs.DTOs.ProductDTOs;
using e_commerce_app.Server.Core.Entities;
using e_commerce_app.Server.Core.Services;
using e_commerce_app.Server.Infrastructure.Repositories.Interfaces;
using e_commerce_app.Server.Infrastructure.unitOfWork;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_app.tests.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<ProductService>> _mockLogger;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<ProductService>>();
            _productService = new ProductService(_mockProductRepository.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAllProductsAsync_ReturnsProducts()
        {
            // Arrange
            var products = new List<Product> { new Product { ProductId = 1, Name = "Product 1" } };
            var productDtos = new List<ProductDTO> { new ProductDTO { ProductId = 1, Name = "Product 1" } };

            _mockProductRepository.Setup(repo => repo.GetAllProductsAsync()).ReturnsAsync(products);
            _mockMapper.Setup(m => m.Map<IEnumerable<ProductDTO>>(products)).Returns(productDtos);

            // Act
            var result = await _productService.GetAllProductsAsync();

            // Assert
            Assert.Equal(productDtos, result);
        }

        [Fact]
        public async Task GetProductByIdAsync_ReturnsProduct()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Product 1" };
            var productDto = new ProductDTO { ProductId = 1, Name = "Product 1" };

            _mockProductRepository.Setup(repo => repo.GetProductByIdAsync(1)).ReturnsAsync(product);
            _mockMapper.Setup(m => m.Map<ProductDTO>(product)).Returns(productDto);

            // Act
            var result = await _productService.GetProductByIdAsync(1);

            // Assert
            Assert.Equal(productDto, result);
        }

        [Fact]
        public async Task GetProductByIdAsync_ThrowsKeyNotFoundException()
        {
            // Arrange
            _mockProductRepository.Setup(repo => repo.GetProductByIdAsync(1)).ReturnsAsync((Product)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _productService.GetProductByIdAsync(1));
        }

        [Fact]
        public async Task AddProductAsync_AddsProduct()
        {
            // Arrange
            var productDto = new ProductDTO { ProductId = 1, Name = "Product 1" };
            var product = new Product { ProductId = 1, Name = "Product 1" };

            _mockMapper.Setup(m => m.Map<Product>(productDto)).Returns(product);

            // Act
            await _productService.AddProductAsync(productDto);

            // Assert
            _mockProductRepository.Verify(repo => repo.AddProductAsync(product), Times.Once);
        }

        [Fact]
        public async Task AddProductAsync_ThrowsArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _productService.AddProductAsync(null));
        }

        [Fact]
        public async Task UpdateProductAsync_UpdatesProduct()
        {
            // Arrange
            var existingProduct = new Product { ProductId = 1, Name = "Old Name" };
            var productDto = new ProductDTO { ProductId = 1, Name = "New Name" };
            var updatedProduct = new Product { ProductId = 1, Name = "New Name" };

            _mockProductRepository.Setup(repo => repo.GetProductByIdAsync(1)).ReturnsAsync(existingProduct);
            _mockMapper.Setup(m => m.Map(productDto, existingProduct)).Callback(() =>
            {
                existingProduct.Name = updatedProduct.Name;
            });

            // Act
            await _productService.UpdateProductAsync(1, productDto);

            // Assert
            _mockProductRepository.Verify(repo => repo.UpdateProductAsync(existingProduct), Times.Once);
        }

        [Fact]
        public async Task UpdateProductAsync_ThrowsKeyNotFoundException()
        {
            // Arrange
            _mockProductRepository.Setup(repo => repo.GetProductByIdAsync(1)).ReturnsAsync((Product)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _productService.UpdateProductAsync(1, new ProductDTO()));
        }

        [Fact]
        public async Task DeleteProductAsync_DeletesProduct()
        {
            // Act
            await _productService.DeleteProductAsync(1);

            // Assert
            _mockProductRepository.Verify(repo => repo.DeleteProductAsync(1), Times.Once);
        }
    }
}
