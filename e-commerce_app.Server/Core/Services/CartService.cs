using AutoMapper;
using e_commerce_app.Server.APIs.DTOs.CartDTOs;
using e_commerce_app.Server.Core.Entities;
using e_commerce_app.Server.Core.Services.Interfaces;
using e_commerce_app.Server.Infrastructure.Repositories.Interfaces;
using e_commerce_app.Server.Infrastructure.unitOfWork;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace e_commerce_app.Server.Core.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CartService> _logger;

        public CartService(ICartRepository cartRepository, IUnitOfWork unitOfWork, IMapper mapper, ILogger<CartService> logger)
        {
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<CartItemDTO>> GetCartItemsAsync(string userId)
        {
            try
            {
                var cartItems = await _cartRepository.GetCartItemsAsync(userId);
                return _mapper.Map<IEnumerable<CartItemDTO>>(cartItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving cart items for user {UserId}.", userId);
                throw;
            }
        }

        public async Task AddCartItemAsync(string userId, AddToCartDTO addToCartDto)
        {
            try
            {
                if (addToCartDto.Quantity <= 0)
                {
                    throw new ArgumentException("Quantity must be greater than zero.", nameof(addToCartDto.Quantity));
                }

                var cartItem = new CartItem
                {
                    ProductId = addToCartDto.ProductId,
                    UserId = userId,
                    Quantity = addToCartDto.Quantity,
                    DateAdded = DateTime.UtcNow,
                };

                await _cartRepository.AddCartItemAsync(cartItem);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new item to the cart for user {UserId}.", userId);
                throw;
            }
        }

        public async Task UpdateCartItemAsync(int id, int quantity)
        {
            try
            {
                if (quantity <= 0)
                {
                    throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
                }

                var cartItem = await _cartRepository.GetCartItemByIdAsync(id);
                if (cartItem != null)
                {
                    cartItem.Quantity = quantity;
                    await _cartRepository.UpdateCartItemAsync(cartItem);
                    await _unitOfWork.CompleteAsync();
                }
                else
                {
                    throw new KeyNotFoundException($"Cart item with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating cart item with ID {CartItemId}.", id);
                throw;
            }
        }

        public async Task RemoveCartItemAsync(int id)
        {
            try
            {
                await _cartRepository.DeleteCartItemAsync(id);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while removing cart item with ID {CartItemId}.", id);
                throw;
            }
        }
    }
}
