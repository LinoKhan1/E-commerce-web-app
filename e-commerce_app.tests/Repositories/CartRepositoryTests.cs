using Castle.Core.Logging;
using e_commerce_app.Server.Core.Entities;
using e_commerce_app.Server.Infrastructure.Data;
using e_commerce_app.Server.Infrastructure.Repositories;
using e_commerce_app.Server.Infrastructure.Repositories.Interfaces;
using e_commerce_app.tests.Repositories.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_app.tests.Repositories
{
    public class CartRepositoryTests : IClassFixture<DbContextFixture>
    {
        private readonly ApplicationDbContext _context;
        private readonly CartRepository _cartRepository;
        private readonly Mock<ILogger<CartRepository>> _loggerMock;

        public CartRepositoryTests(DbContextFixture fixture)
        {
            _context = fixture.Context;
            _loggerMock = new Mock<ILogger<CartRepository>>();
            _cartRepository = new CartRepository(_context, _loggerMock.Object);

        }
        /// <summary>
        /// Tests that the GetCartItemsAsync method returns cart items for a specified user.
        /// </summary>

        [Fact]
        public async Task GetCartItemsAsync_ReturnsCartItemsForUser()
        {
            // Act
            var cartItems = await _cartRepository.GetCartItemsAsync("user1");

            // Assert
            Assert.NotNull(cartItems);
            var cartItemList = cartItems.ToList();
            Assert.Single(cartItemList); // Assuming one item for user1
            Assert.Equal(1, cartItemList[0].CartItemId);
            Assert.Equal("user1", cartItemList[0].UserId);
            Assert.Equal(1, cartItemList[0].ProductId);
            Assert.Equal(2, cartItemList[0].Quantity);
            Assert.NotNull(cartItemList[0].Product);
            Assert.Equal(1, cartItemList[0].Product.ProductId);
            Assert.Equal("Laptop", cartItemList[0].Product.Name);
            Assert.Equal(35099, cartItemList[0].Product.Price);
        }


        /// <summary>
        /// Tests that the GetCartItemByIdAsync method returns a cart item by its unique identifier.
        /// </summary>
        [Fact]
        public async Task GetCartItemByIdAsync_ReturnsCartItemById()
        {
            // Act
            var cartItem = await _cartRepository.GetCartItemByIdAsync(1);

            // Assert
            Assert.NotNull(cartItem);
            Assert.Equal(1, cartItem.CartItemId);
            Assert.Equal("user1", cartItem.UserId);
            Assert.Equal(1, cartItem.ProductId);
            Assert.Equal(2, cartItem.Quantity);
        }
        /// <summary>
        /// Tests that AddCartItemAsync adds a new cart item.
        /// </summary>
        [Fact]
        public async Task AddCartItemAsync_AddsNewItem()
        {
            // Arrange
            var product = await _context.Products.FirstAsync(p => p.ProductId == 3);
            var newCartItem = new CartItem
            {
                CartItemId = 3,
                UserId = "user3",
                ProductId = 3,
                Quantity = 1,
                DateAdded = DateTime.UtcNow,
                Product = product // Use the existing tracked product
            };

            // Act
            await _cartRepository.AddCartItemAsync(newCartItem);
            await _context.SaveChangesAsync();

            // Assert
            var addedCartItem = await _cartRepository.GetCartItemByIdAsync(3);
            Assert.NotNull(addedCartItem);
            Assert.Equal(newCartItem.CartItemId, addedCartItem.CartItemId);
            Assert.Equal(newCartItem.UserId, addedCartItem.UserId);
            Assert.Equal(newCartItem.ProductId, addedCartItem.ProductId);
            Assert.Equal(newCartItem.Quantity, addedCartItem.Quantity);
        }

        /// <summary>
        /// Tests that UpdateCartItemAsync updates an existing cart item.
        /// </summary>
        [Fact]
        public async Task UpdateCartItemAsync_UpdatesExistingItem()
        {
            // Arrange
            var cartItem = await _cartRepository.GetCartItemByIdAsync(1);
            cartItem.Quantity = 3;

            // Act
            await _cartRepository.UpdateCartItemAsync(cartItem);
            await _context.SaveChangesAsync();

            // Assert
            var updatedCartItem = await _cartRepository.GetCartItemByIdAsync(1);
            Assert.NotNull(updatedCartItem);
            Assert.Equal(3, updatedCartItem.Quantity);
        }

        /// <summary>
        /// Tests that DeleteCartItemAsync removes a cart item.
        /// </summary>
        [Fact]
        public async Task DeleteCartItemAsync_RemovesItem()
        {
            // Arrange
            var cartItemId = 1;

            // Act
            await _cartRepository.DeleteCartItemAsync(cartItemId);
            await _context.SaveChangesAsync();

            // Assert
            var deletedCartItem = await _cartRepository.GetCartItemByIdAsync(cartItemId);
            Assert.Null(deletedCartItem);
        }


    }
}
