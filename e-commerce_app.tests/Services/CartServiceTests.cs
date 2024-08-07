using AutoMapper;
using e_commerce_app.Server.APIs.DTOs.CartDTOs;
using e_commerce_app.Server.Core.Entities;
using e_commerce_app.Server.Core.Services;
using e_commerce_app.Server.Infrastructure.Repositories.Interfaces;
using e_commerce_app.Server.Infrastructure.unitOfWork;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_app.tests.Services
{
    public class CartServiceTests
    {

        private readonly Mock<ICartRepository> _mockCartRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<CartService>> _mockLogger;
        private readonly CartService _cartService;

        public CartServiceTests()
        {
            _mockCartRepository = new Mock<ICartRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<CartService>>();
            _cartService = new CartService(_mockCartRepository.Object, _mockUnitOfWork.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetCartItemsAsync_ReturnsCartItems()
        {
            // Arrange
            var userId = "user123";
            var cartItems = new List<CartItem> { new CartItem { CartItemId = 1, ProductId = 1, UserId = userId, Quantity = 2, DateAdded = DateTime.UtcNow } };
            var cartItemDtos = new List<CartItemDTO> { new CartItemDTO { Id = 1, ProductId = 1, Quantity = 2 } };

            _mockCartRepository.Setup(repo => repo.GetCartItemsAsync(userId)).ReturnsAsync(cartItems);
            _mockMapper.Setup(m => m.Map<IEnumerable<CartItemDTO>>(cartItems)).Returns(cartItemDtos);

            // Act
            var result = await _cartService.GetCartItemsAsync(userId);

            // Assert
            Assert.Equal(cartItemDtos, result);
        }

        [Fact]
        public async Task AddCartItemAsync_AddsCartItem()
        {
            // Arrange
            var userId = "user123";
            var addToCartDto = new AddToCartDTO { ProductId = 1, Quantity = 2 };

            // Act
            await _cartService.AddCartItemAsync(userId, addToCartDto);

            // Assert
            _mockCartRepository.Verify(repo => repo.AddCartItemAsync(It.IsAny<CartItem>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task AddCartItemAsync_ThrowsArgumentException_WhenQuantityIsZero()
        {
            // Arrange
            var userId = "user123";
            var addToCartDto = new AddToCartDTO { ProductId = 1, Quantity = 0 };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _cartService.AddCartItemAsync(userId, addToCartDto));
        }

        [Fact]
        public async Task UpdateCartItemAsync_UpdatesCartItem()
        {
            // Arrange
            var cartItemId = 1;
            var quantity = 5;
            var existingCartItem = new CartItem { CartItemId = cartItemId, Quantity = 2 };
            var updatedCartItem = new CartItem { CartItemId = cartItemId, Quantity = quantity };

            _mockCartRepository.Setup(repo => repo.GetCartItemByIdAsync(cartItemId)).ReturnsAsync(existingCartItem);

            // Act
            await _cartService.UpdateCartItemAsync(cartItemId, quantity);

            // Assert
            _mockCartRepository.Verify(repo => repo.UpdateCartItemAsync(It.Is<CartItem>(c => c.Quantity == quantity)), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateCartItemAsync_ThrowsArgumentException_WhenQuantityIsZero()
        {
            // Arrange
            var cartItemId = 1;
            var quantity = 0;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _cartService.UpdateCartItemAsync(cartItemId, quantity));
        }

        [Fact]
        public async Task UpdateCartItemAsync_ThrowsKeyNotFoundException_WhenCartItemDoesNotExist()
        {
            // Arrange
            var cartItemId = 1;
            var quantity = 5;
            _mockCartRepository.Setup(repo => repo.GetCartItemByIdAsync(cartItemId)).ReturnsAsync((CartItem)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _cartService.UpdateCartItemAsync(cartItemId, quantity));
        }

        [Fact]
        public async Task RemoveCartItemAsync_RemovesCartItem()
        {
            // Arrange
            var cartItemId = 1;

            // Act
            await _cartService.RemoveCartItemAsync(cartItemId);

            // Assert
            _mockCartRepository.Verify(repo => repo.DeleteCartItemAsync(cartItemId), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
        }
    }
}
