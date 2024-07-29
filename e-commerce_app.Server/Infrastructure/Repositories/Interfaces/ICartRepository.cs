using e_commerce_app.Server.Core.Entities;

namespace e_commerce_app.Server.Infrastructure.Repositories.Interfaces
{
    public interface ICartRepository
    
    {
        Task<IEnumerable<CartItem>> GetCartItemsAsync(string userId);

        /// <summary>
        /// Gets a cart item by its unique identifier.
        /// </summary>
        /// <param name="cartItemId">The unique identifier for the cart item.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the CartItem if found; otherwise, null.</returns>
        Task<CartItem> GetCartItemByIdAsync(int cartItemId);

        Task AddCartItemAsync(CartItem cartItem);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task DeleteCartItemAsync(int cartItemId);

    }
}
