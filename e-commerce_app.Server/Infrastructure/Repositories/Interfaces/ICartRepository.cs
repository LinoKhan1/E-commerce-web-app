using e_commerce_app.Server.Core.Entities;

namespace e_commerce_app.Server.Infrastructure.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<CartItem> GetCartItemByIdAsync(int cartItemId);
        Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync(string userId);
        Task AddCartItemAsync(CartItem cartItem);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task DeleteCartItemAsync(int cartItemId);
    }
}
