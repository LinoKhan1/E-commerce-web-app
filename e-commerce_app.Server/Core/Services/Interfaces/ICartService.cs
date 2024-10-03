using e_commerce_app.Server.APIs.DTOs.CartDTOs;

namespace e_commerce_app.Server.Core.Services.Interfaces
{
    public interface ICartService
    {
        Task<CartItemDTO> GetCartItemByIdAsync(int cartItemId);
        Task<IEnumerable<CartItemDTO>> GetCartItemsByUserIdAsync(string userId);
        Task AddCartItemAsync(AddToCartDTO addToCartDto, string userId);
        Task UpdateCartItemAsync(CartItemDTO cartItemDto);
        Task DeleteCartItemAsync(int cartItemId);
    }
}
