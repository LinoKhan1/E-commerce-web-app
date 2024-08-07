using AutoMapper;
using e_commerce_app.Server.Core.Entities;
using e_commerce_app.Server.Core.Services;
using e_commerce_app.Server.Infrastructure.Repositories.Interfaces;
using e_commerce_app.Server.Infrastructure.unitOfWork;
using e_commerce_app.Server.APIs.DTOs.CategoryDTOs;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace e_commerce_app.tests.Services
{
    public class CategoryServiceTests
    {
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<CategoryService>> _mockLogger;
        private readonly CategoryService _categoryService;

        public CategoryServiceTests()
        {
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<CategoryService>>();
            _categoryService = new CategoryService(_mockCategoryRepository.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAllCategoriesAsync_ReturnsCategories()
        {
            // Arrange
            var categories = new List<Category> { new Category { CategoryId = 1, Name = "Category 1" } };
            var categoryDtos = new List<CategoryDTO> { new CategoryDTO { CategoryId = 1, Name = "Category 1" } };

            _mockCategoryRepository.Setup(repo => repo.GetAllCategoriesAsync()).ReturnsAsync(categories);
            _mockMapper.Setup(m => m.Map<IEnumerable<CategoryDTO>>(categories)).Returns(categoryDtos);

            // Act
            var result = await _categoryService.GetAllCategoriesAsync();

            // Assert
            Assert.Equal(categoryDtos, result);
        }

        [Fact]
        public async Task GetCategoryByIdAsync_ReturnsCategory()
        {
            // Arrange
            var category = new Category { CategoryId = 1, Name = "Category 1" };
            var categoryDto = new CategoryDTO { CategoryId = 1, Name = "Category 1" };

            _mockCategoryRepository.Setup(repo => repo.GetCategoryByIdAsync(1)).ReturnsAsync(category);
            _mockMapper.Setup(m => m.Map<CategoryDTO>(category)).Returns(categoryDto);

            // Act
            var result = await _categoryService.GetCategoryByIdAsync(1);

            // Assert
            Assert.Equal(categoryDto, result);
        }

        [Fact]
        public async Task GetCategoryByIdAsync_ThrowsKeyNotFoundException()
        {
            // Arrange
            _mockCategoryRepository.Setup(repo => repo.GetCategoryByIdAsync(1)).ReturnsAsync((Category)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _categoryService.GetCategoryByIdAsync(1));
        }

        [Fact]
        public async Task AddCategoryAsync_AddsCategory()
        {
            // Arrange
            var categoryDto = new CategoryDTO { CategoryId = 1, Name = "Category 1" };
            var category = new Category { CategoryId = 1, Name = "Category 1" };

            _mockMapper.Setup(m => m.Map<Category>(categoryDto)).Returns(category);

            // Act
            await _categoryService.AddCategoryAsync(categoryDto);

            // Assert
            _mockCategoryRepository.Verify(repo => repo.AddCategoryAsync(category), Times.Once);
        }

        [Fact]
        public async Task UpdateCategoryAsync_UpdatesCategory()
        {
            // Arrange
            var existingCategory = new Category { CategoryId = 1, Name = "Old Name" };
            var categoryDto = new CategoryDTO { CategoryId = 1, Name = "New Name" };
            var updatedCategory = new Category { CategoryId = 1, Name = "New Name" };

            _mockCategoryRepository.Setup(repo => repo.GetCategoryByIdAsync(1)).ReturnsAsync(existingCategory);
            _mockMapper.Setup(m => m.Map(categoryDto, existingCategory)).Callback(() =>
            {
                existingCategory.Name = updatedCategory.Name;
            });

            // Act
            await _categoryService.UpdateCategoryAsync(1, categoryDto);

            // Assert
            _mockCategoryRepository.Verify(repo => repo.UpdateCategoryAsync(existingCategory), Times.Once);
        }

        [Fact]
        public async Task UpdateCategoryAsync_ThrowsKeyNotFoundException()
        {
            // Arrange
            _mockCategoryRepository.Setup(repo => repo.GetCategoryByIdAsync(1)).ReturnsAsync((Category)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _categoryService.UpdateCategoryAsync(1, new CategoryDTO()));
        }

        [Fact]
        public async Task DeleteCategoryAsync_DeletesCategory()
        {
            // Act
            await _categoryService.DeleteCategoryAsync(1);

            // Assert
            _mockCategoryRepository.Verify(repo => repo.DeleteCategoryAsync(1), Times.Once);
        }

    }
}
