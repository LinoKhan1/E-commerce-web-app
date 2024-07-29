using AutoMapper;
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
    public class CategoryServiceTests
    { 
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<CategoryService>> _loggerMock;

        public CategoryServiceTests()
            {
        _mockCategoryRepository = new Mock<ICategoryRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _loggerMock = new Mock<ILogger<CategoryService>>();

        // AutoMapper configuration for tests
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AutoMapperProfile());
        });
        _mapper = mapperConfig.CreateMapper();
    }

    /// <summary>
    /// Tests GetAllCategoriesAsync method of CategoryService.
    /// </summary>
    [Fact]
    public async Task GetAllCategoriesAsync_Should_Return_All_Categories()
    {
        // Arrange
        var categories = new List<Category>
            {
                new Category { CategoryId = 1, Name = "Category 1" },
                new Category { CategoryId = 2, Name = "Category 2" }
            };
        _mockCategoryRepository.Setup(repo => repo.GetAllCategoriesAsync()).ReturnsAsync(categories);

        var categoryService = new CategoryService(_mockCategoryRepository.Object, _mockUnitOfWork.Object, _mapper, _loggerMock.Object);

        // Act
        var result = await categoryService.GetAllCategoriesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count()); // Assuming there are 2 categories in the mocked repository

        // Additional assertions to verify properties of CategoryDTO
        Assert.Collection(result,
            item =>
            {
                Assert.Equal(1, item.Id);
                Assert.Equal("Category 1", item.Name);
                // Add more assertions for other properties if needed
            },
            item =>
            {
                Assert.Equal(2, item.Id);
                Assert.Equal("Category 2", item.Name);
                // Add more assertions for other properties if needed
            });
    }

    /// <summary>
    /// Tests GetCategoryByIdAsync method of CategoryService.
    /// </summary>
    [Fact]
    public async Task GetCategoryByIdAsync_Should_Return_Correct_Category()
    {
        // Arrange
        int categoryId = 1;
        var category = new Category { CategoryId = categoryId, Name = "Category 1" };
        _mockCategoryRepository.Setup(repo => repo.GetCategoryByIdAsync(categoryId)).ReturnsAsync(category);

        var categoryService = new CategoryService(_mockCategoryRepository.Object, _mockUnitOfWork.Object, _mapper, _loggerMock.Object);

        // Act
        var result = await categoryService.GetCategoryByIdAsync(categoryId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(categoryId, result.Id);
        Assert.Equal("Category 1", result.Name);
        // Add more assertions for other properties of CategoryDTO if needed
    }
}

}
