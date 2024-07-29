using e_commerce_app.Server.APIs.Controllers;
using e_commerce_app.Server.APIs.DTOs.CategoryDTOs;
using e_commerce_app.Server.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_app.tests.Controllers
{
    /// <summary>
    /// Unit tests for the CategoryController.
    /// </summary>
    public class CategoryControllerTests
    {
        private readonly Mock<ICategoryService> _mockCategoryService;
        private readonly Mock<ILogger> _mockLogger;
        private readonly CategoryController _controller;


        /// <summary>
        /// Initializes a new instance of the CategoryControllerTests class.
        /// </summary>
        public CategoryControllerTests()
        {
            _mockCategoryService = new Mock<ICategoryService>();
            _mockLogger = new Mock<ILogger>();
            _controller = new CategoryController(_mockCategoryService.Object, _mockLogger.Object);
        }

        /// <summary>
        /// Tests the GetCategories method of CategoryController.
        /// </summary>
        [Fact]
        public async Task GetCategories_ReturnsOkResult()
        {
            // Arrange
            var mockCategories = new List<CategoryDTO>
            {
                new CategoryDTO { Id = 1, Name = "Category 1" },
                new CategoryDTO { Id = 2, Name = "Category 2" }
            };
            _mockCategoryService.Setup(service => service.GetAllCategoriesAsync())
                                .ReturnsAsync(mockCategories);

            // Act
            var result = await _controller.GetCategories();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var categories = Assert.IsAssignableFrom<IEnumerable<CategoryDTO>>(okResult.Value);
            Assert.Equal(2, categories.Count());
        }

        /// <summary>
        /// Tests the GetCategory method of CategoryController.
        /// </summary>
        [Fact]
        public async Task GetCategory_ReturnsOkResult()
        {
            // Arrange
            int categoryId = 1;
            var mockCategory = new CategoryDTO { Id = categoryId, Name = "Category 1" };
            _mockCategoryService.Setup(service => service.GetCategoryByIdAsync(categoryId))
                                .ReturnsAsync(mockCategory);

            // Act
            var result = await _controller.GetCategory(categoryId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var category = Assert.IsType<CategoryDTO>(okResult.Value);
            Assert.Equal(categoryId, category.Id);
        }

        /// <summary>
        /// Tests the PostCategory method of CategoryController.
        /// </summary>
        [Fact]
        public async Task PostCategory_ReturnsCreatedAtAction()
        {
            // Arrange
            var newCategory = new CategoryDTO { Id = 3, Name = "New Category" };

            // Act
            var result = await _controller.PostCategory(newCategory);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(_controller.GetCategory), createdAtActionResult.ActionName);
            Assert.Equal(newCategory.Id, createdAtActionResult.RouteValues["id"]);
        }

        /// <summary>
        /// Tests the PutCategory method of CategoryController.
        /// </summary>
        [Fact]
        public async Task PutCategory_ReturnsNoContentResult()
        {
            // Arrange
            int categoryId = 1;
            var categoryToUpdate = new CategoryDTO { Id = categoryId, Name = "Updated Category" };

            // Act
            var result = await _controller.PutCategory(categoryId, categoryToUpdate);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        /// <summary>
        /// Tests the DeleteCategory method of CategoryController.
        /// </summary>
        [Fact]
        public async Task DeleteCategory_ReturnsNoContentResult()
        {
            // Arrange
            int categoryId = 1;

            // Act
            var result = await _controller.DeleteCategory(categoryId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
