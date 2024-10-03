using AutoMapper;
using e_commerce_app.Server.APIs.DTOs.CartDTOs;
using e_commerce_app.Server.Core.Entities;
using e_commerce_app.Server.Core.Services.Interfaces;
using e_commerce_app.Server.Infrastructure.Repositories;
using e_commerce_app.Server.Infrastructure.Repositories.Interfaces;
using e_commerce_app.Server.Infrastructure.unitOfWork;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace e_commerce_app.Server.Core.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartItemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CartService> _logger;

        public CartService(ICartRepository cartItemRepository, IMapper mapper, ILogger<CartService> logger)
        {
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CartItemDTO> GetCartItemByIdAsync(int cartItemId)
        {
            try
            {
                var cartItem = await _cartItemRepository.GetCartItemByIdAsync(cartItemId);
                return _mapper.Map<CartItemDTO>(cartItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving cart item with ID {cartItemId}");
                throw; // Rethrow the exception for further handling
            }
        }

        public async Task<IEnumerable<CartItemDTO>> GetCartItemsByUserIdAsync(string userId)
        {
            try
            {
                var cartItems = await _cartItemRepository.GetCartItemsByUserIdAsync(userId);
                return _mapper.Map<IEnumerable<CartItemDTO>>(cartItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving cart items for user {userId}");
                throw;
            }
        }

        public async Task AddCartItemAsync(AddToCartDTO addToCartDto, string userId)
        {
            try
            {
                var cartItem = new CartItem
                {
                    UserId = userId,
                    ProductId = addToCartDto.ProductId,
                    Quantity = addToCartDto.Quantity,
                    DateAdded = DateTime.UtcNow
                };
                await _cartItemRepository.AddCartItemAsync(cartItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding item to cart");
                throw;
            }
        }

        public async Task UpdateCartItemAsync(CartItemDTO cartItemDto)
        {
            try
            {
                var cartItem = _mapper.Map<CartItem>(cartItemDto);
                await _cartItemRepository.UpdateCartItemAsync(cartItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating cart item with ID {cartItemDto.Id}");
                throw;
            }
        }

        public async Task DeleteCartItemAsync(int cartItemId)
        {
            try
            {
                await _cartItemRepository.DeleteCartItemAsync(cartItemId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting cart item with ID {cartItemId}");
                throw;
            }
        }
    }
}
