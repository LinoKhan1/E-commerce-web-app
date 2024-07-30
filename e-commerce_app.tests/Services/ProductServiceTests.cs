﻿using AutoMapper;
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
        private readonly Mock<IProductRepository> _repositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<ProductService>> _loggerMock;

        public ProductServiceTests()
        {
            _repositoryMock = new Mock<IProductRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _loggerMock = new Mock<ILogger<ProductService>>();

            // AutoMapper configurations for test
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile()); // Register the AutoMapperProfile
            });

            _mapper = mapperConfig.CreateMapper();
        }


        /// <summary>
        /// Tests GetAllProductsAsync method of ProductService.
        /// </summary>
        [Fact]
        public async Task GetAllProductAsync_Should_Return_All_Products()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product {Id = 1, Name = "Product 1", Description = "Test Description 1", Price = 10.0m, Stock = 50 },
                new Product {Id = 2, Name = "Product 2", Description = "Test Description 2", Price = 15.0m, Stock = 50 }
            };

            _repositoryMock.Setup(repo => repo.GetAllProductsAsync()).ReturnsAsync(products);

            var productService = new ProductService(_repositoryMock.Object, _unitOfWorkMock.Object, _loggerMock.Object, _mapper);

            // Act
            var result = await productService.GetProductsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

            // Additional assertions to verify properties of ProductDTO
            Assert.Collection(result,
                item =>
                {
                    Assert.Equal(1, item.Id);
                    Assert.Equal("Product 1", item.Name);
                    Assert.Equal(10.0m, item.Price);
                    // Add more assertions for other properties if needed
                },
                item =>
                {
                    Assert.Equal(2, item.Id);
                    Assert.Equal("Product 2", item.Name);
                    Assert.Equal(15.0m, item.Price);
                    // Add more assertions for other properties if needed
                });


        }

        /// <summary>
        /// Tests GetProductByIdAsync method of ProductService.
        /// </summary>
        [Fact]
        public async Task GetProductByIdAsync_Should_Return_Correct_Product()
        {
            // Arrange
            int productId = 1;
            var product = new Product { Id = productId, Name = "Product 1", Price = 10.0m };
            _repositoryMock.Setup(repo => repo.GetProductByIdAsync(productId)).ReturnsAsync(product);

            var productService = new ProductService(_repositoryMock.Object, _unitOfWorkMock.Object, _loggerMock.Object, _mapper);

            // Act
            var result = await productService.GetProductByIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result.Id);
            Assert.Equal("Product 1", result.Name);
            Assert.Equal(10.0m, result.Price);

        }

        [Fact]
        public async Task AddProductAsync_Should_Add_Product()
        {
            // Arrange
            var productDto = new ProductDTO { Name = "New Product", Price = 20.0m };
            var product = new Product { Id = 1, Name = "New Product", Price = 20.0m };
            _repositoryMock.Setup(repo => repo.AddProductAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);

            var productService = new ProductService(_repositoryMock.Object, _unitOfWorkMock.Object, _loggerMock.Object, _mapper);

            // Act
            await productService.AddProductAsync(productDto);

            // Assert
            _repositoryMock.Verify(repo => repo.AddProductAsync(It.Is<Product>(p => p.Name == product.Name && p.Price == product.Price)), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateProductAsync_Should_Update_Product()
        {
            // Arrange
            var productDto = new ProductDTO { Id = 1, Name = "Updated Product", Price = 25.0m };
            var product = new Product { Id = 1, Name = "Updated Product", Price = 25.0m };
            _repositoryMock.Setup(repo => repo.UpdateProductAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);

            var productService = new ProductService(_repositoryMock.Object, _unitOfWorkMock.Object, _loggerMock.Object, _mapper);

            // Act
            await productService.UpdateProductAsync(productDto);

            // Assert
            _repositoryMock.Verify(repo => repo.UpdateProductAsync(It.Is<Product>(p => p.Id == product.Id && p.Name == product.Name && p.Price == product.Price)), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteProductAsync_Should_Delete_Product()
        {
            // Arrange
            int productId = 1;
            _repositoryMock.Setup(repo => repo.DeleteProductAsync(productId)).Returns(Task.CompletedTask);

            var productService = new ProductService(_repositoryMock.Object, _unitOfWorkMock.Object, _loggerMock.Object, _mapper);

            // Act
            await productService.DeleteProductAsync(productId);

            // Assert
            _repositoryMock.Verify(repo => repo.DeleteProductAsync(productId), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
        }

    }
}