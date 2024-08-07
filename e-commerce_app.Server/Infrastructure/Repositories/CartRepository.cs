using e_commerce_app.Server.Core.Entities;
using e_commerce_app.Server.Infrastructure.Data;
using e_commerce_app.Server.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce_app.Server.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CartRepository> _logger;

        public CartRepository(ApplicationDbContext context, ILogger<CartRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsAsync(string userId)
        {
            try
            {
                return await _context.CartItems
                    .Include(ci => ci.Product)
                    .Where(ci => ci.UserId == userId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving cart items.");
                throw;
            }
        }

        public async Task<CartItem> GetCartItemByIdAsync(int cartItemId)
        {
            try
            {
                return await _context.CartItems.FirstOrDefaultAsync(ci => ci.CartItemId == cartItemId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving cart item with ID {cartItemId}.");
                throw;
            }
        }

        public async Task AddCartItemAsync(CartItem cartItem)
        {
            try
            {
                await _context.CartItems.AddAsync(cartItem);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new cart item.");
                throw;
            }
        }

        public async Task UpdateCartItemAsync(CartItem cartItem)
        {
            try
            {
                _context.Entry(cartItem).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating cart item with ID {cartItem.CartItemId}.");
                throw;
            }
        }

        public async Task DeleteCartItemAsync(int cartItemId)
        {
            try
            {
                var cartItem = await _context.CartItems.FindAsync(cartItemId);
                if (cartItem == null) throw new KeyNotFoundException($"Cart item with ID {cartItemId} not found.");

                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting cart item with ID {cartItemId}.");
                throw;
            }
        }
    }
}
