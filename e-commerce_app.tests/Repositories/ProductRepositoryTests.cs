﻿using e_commerce_app.Server.Infrastructure.Data;
using e_commerce_app.Server.Infrastructure.Repositories;
using e_commerce_app.Server.Infrastructure.Repositories.Interfaces;
using e_commerce_app.tests.Repositories.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_app.tests.Repositories
{
    public class ProductRepositoryTests : IClassFixture<DbContextFixture>
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductRepository _productRepository;
        private readonly Mock<ILogger<ProductRepository>> _loggerMock;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepositoryTests"/> class.
        /// </summary>
        public ProductRepositoryTests(DbContextFixture fixture)
        {
            _context = fixture.Context;
            _loggerMock = new Mock<ILogger<ProductRepository>>();
            _productRepository = new ProductRepository(_context, _loggerMock.Object);
        }

        /// <summary>
        /// Tests that GetAllProductsAsync method returns all products.
        /// </summary>
        [Fact]
        public async Task GetAllProductsAsync_Should_Return_All_Products()
        {
            // Act
            var result = await _productRepository.GetAllProductsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
        }

        /// <summary>
        /// Tests that GetProductByIdAsync method returns the correct product for a valid ID.
        /// </summary>
        [Fact]
        public async Task GetProductByIdAsync_Should_Return_Correct_Product()
        {
            // Act
            var result = await _productRepository.GetProductByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Laptop", result.Name);
        }

        /// <summary>
        /// Tests that GetProductByIdAsync method returns null for an invalid ID.
        /// </summary>
        [Fact]
        public async Task GetProductByIdAsync_Should_Return_Null_For_Invalid_Id()
        {
            // Act
            var result = await _productRepository.GetProductByIdAsync(99);

            // Assert
            Assert.Null(result);
        }

    }
}
