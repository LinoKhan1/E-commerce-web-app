using e_commerce_app.Server.Core.Entities;
using e_commerce_app.Server.Infrastructure.Data;
using e_commerce_app.Server.Infrastructure.Repositories;
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
    public class CategoryRepositoryTests : IClassFixture<DbContextFixture>
    {
        private readonly ApplicationDbContext _context;
        private readonly CategoryRepository _categoryRepository;
        private readonly Mock<ILogger<CategoryRepository>> _loggerMock;


        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryRepositoryTests"/> class.
        /// Sets up the in-memory database and seeds it with initial data.
        /// </summary>
        public CategoryRepositoryTests(DbContextFixture fixture)
        {
            _context = fixture.Context;
            _loggerMock = new Mock<ILogger<CategoryRepository>>();
            _categoryRepository = new CategoryRepository(_context, _loggerMock.Object);

        }

        /// <summary>
        /// Tests that GetAllCategoriesAsync method returns all categories.
        /// </summary>
        [Fact]
        public async Task GetAllCategoriesAsync_Should_Return_All_Categories()
        {
            // Act
            var result = await _categoryRepository.GetAllCategoriesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }


        /// <summary>
        /// Tests that GetCategoryByIdAsync method returns the correct category for a valid ID.
        /// </summary>
        [Fact]
        public async Task GetCategoryByIdAsync_Should_Return_Correct_Category()
        {
            // Act
            var result = await _categoryRepository.GetCategoryByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.CategoryId);
            Assert.Equal("Electronic", result.Name);
        }

        /// <summary>
        /// Tests that GetCategoryByIdAsync method returns null for an invalid ID.
        /// </summary>
        [Fact]
        public async Task GetCategoryByIdAsync_Should_Return_Null_For_Invalid_Id()
        {
            // Act
            var result = await _categoryRepository.GetCategoryByIdAsync(99);

            // Assert
            Assert.Null(result);
        }

        /// <summary>
        /// Tests that AddCategoryAsync method adds a new category.
        /// </summary>
        [Fact]
        public async Task AddCategoryAsync_Should_Add_Category()
        {
            // Arrange
            var newCategory = new Category
            {
                CategoryId = 3, // Specify an ID (ensure this ID does not conflict with existing IDs)
                Name = "Furniture"
            };

            // Act
            await _categoryRepository.AddCategoryAsync(newCategory);
            var addedCategory = await _categoryRepository.GetCategoryByIdAsync(newCategory.CategoryId);

            // Assert
            Assert.NotNull(addedCategory);
            Assert.Equal(newCategory.CategoryId, addedCategory.CategoryId);
            Assert.Equal(newCategory.Name, addedCategory.Name);
        }


        /// <summary>
        /// Tests that UpdateCategoryAsync updates an existing category.
        /// </summary>
        [Fact]
        public async Task UpdateCategoryAsync_Should_Update_Existing_Category()
        {
            // Arrange
            var category = await _categoryRepository.GetCategoryByIdAsync(1);
            category.Name = "Updated Category";

            // Act
            await _categoryRepository.UpdateCategoryAsync(category);
            await _context.SaveChangesAsync();

            // Assert
            var updatedCategory = await _categoryRepository.GetCategoryByIdAsync(1);
            Assert.NotNull(updatedCategory);
            Assert.Equal("Updated Category", updatedCategory.Name);
        }

        /// <summary>
        /// Tests that DeleteCategoryAsync removes a category.
        /// </summary>
        [Fact]
        public async Task DeleteCategoryAsync_Should_Remove_Category()
        {
            // Arrange
            var categoryId = 1;

            // Act
            await _categoryRepository.DeleteCategoryAsync(categoryId);
            await _context.SaveChangesAsync();

            // Assert
            var deletedCategory = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            Assert.Null(deletedCategory);
        }

    }
}
