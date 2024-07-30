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
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper; // Changed to Mock<IMapper>
        private readonly Mock<ILogger<CategoryService>> _loggerMock;

        public CategoryServiceTests()
        {
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _loggerMock = new Mock<ILogger<CategoryService>>();
            _mockMapper = new Mock<IMapper>(); // Initialize Mock<IMapper>

            // Configure the Mock<IMapper> to return the appropriate values
            _mockMapper.Setup(m => m.Map<CategoryDTO>(It.IsAny<Category>()))
                .Returns((Category c) => new CategoryDTO { Id = c.CategoryId, Name = c.Name });

            _mockMapper.Setup(m => m.Map<Category>(It.IsAny<CategoryDTO>()))
                .Returns((CategoryDTO dto) => new Category { CategoryId = dto.Id, Name = dto.Name });
        }

        /*[Fact]
        public async Task GetAllCategoriesAsync_Should_Return_All_Categories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { CategoryId = 1, Name = "Electronic" },
                new Category { CategoryId = 2, Name = "Books" }
            };
            _mockCategoryRepository.Setup(repo => repo.GetAllCategoriesAsync()).ReturnsAsync(categories);

            var categoryService = new CategoryService(_mockCategoryRepository.Object, _mockUnitOfWork.Object, _mockMapper.Object, _loggerMock.Object);

            // Act
            var result = await categoryService.GetAllCategoriesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

            Assert.Collection(result,
                item =>
                {
                    Assert.Equal(1, item.Id);
                    Assert.Equal("Electronic", item.Name);
                },
                item =>
                {
                    Assert.Equal(2, item.Id);
                    Assert.Equal("Books", item.Name);
                });
        }*/

        [Fact]
        public async Task GetCategoryByIdAsync_Should_Return_Correct_Category()
        {
            // Arrange
            int categoryId = 1;
            var category = new Category { CategoryId = categoryId, Name = "Electronic" };
            _mockCategoryRepository.Setup(repo => repo.GetCategoryByIdAsync(categoryId)).ReturnsAsync(category);

            var categoryService = new CategoryService(_mockCategoryRepository.Object, _mockUnitOfWork.Object, _mockMapper.Object, _loggerMock.Object);

            // Act
            var result = await categoryService.GetCategoryByIdAsync(categoryId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(categoryId, result.Id);
            Assert.Equal("Electronic", result.Name);
        }

        [Fact]
        public async Task GetCategoryByIdAsync_Should_Return_Null_For_Invalid_Id()
        {
            // Arrange
            int invalidCategoryId = 99;
            _mockCategoryRepository.Setup(repo => repo.GetCategoryByIdAsync(invalidCategoryId)).ReturnsAsync((Category)null);

            var categoryService = new CategoryService(_mockCategoryRepository.Object, _mockUnitOfWork.Object, _mockMapper.Object, _loggerMock.Object);

            // Act
            var result = await categoryService.GetCategoryByIdAsync(invalidCategoryId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddCategoryAsync_Should_Add_Category()
        {
            // Arrange
            var categoryDto = new CategoryDTO { Id = 0, Name = "Furniture" };
            var category = new Category { Name = "Furniture" };
            _mockMapper.Setup(m => m.Map<Category>(categoryDto)).Returns(category);

            var categoryService = new CategoryService(_mockCategoryRepository.Object, _mockUnitOfWork.Object, _mockMapper.Object, _loggerMock.Object);

            // Act
            await categoryService.AddCategoryAsync(categoryDto);

            // Assert
            _mockCategoryRepository.Verify(repo => repo.AddCategoryAsync(It.Is<Category>(c => c.Name == "Furniture")), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateCategoryAsync_Should_Update_Category()
        {
            // Arrange
            var categoryDto = new CategoryDTO { Id = 1, Name = "Updated Name" };
            var category = new Category { CategoryId = 1, Name = "Updated Name" };
            _mockMapper.Setup(m => m.Map<Category>(categoryDto)).Returns(category);

            var categoryService = new CategoryService(_mockCategoryRepository.Object, _mockUnitOfWork.Object, _mockMapper.Object, _loggerMock.Object);

            // Act
            await categoryService.UpdateCategoryAsync(categoryDto);

            // Assert
            _mockCategoryRepository.Verify(repo => repo.UpdateCategoryAsync(It.Is<Category>(c => c.CategoryId == 1 && c.Name == "Updated Name")), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteCategoryAsync_Should_Delete_Category()
        {
            // Arrange
            int categoryId = 1;

            var categoryService = new CategoryService(_mockCategoryRepository.Object, _mockUnitOfWork.Object, _mockMapper.Object, _loggerMock.Object);

            // Act
            await categoryService.DeleteCategoryAsync(categoryId);

            // Assert
            _mockCategoryRepository.Verify(repo => repo.DeleteCategoryAsync(categoryId), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
        }
    }
}
