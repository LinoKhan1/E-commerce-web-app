using e_commerce_app.Server.APIs.DTOs.CartDTOs;
using e_commerce_app.Server.Core.Entities;

namespace e_commerce_app.Server.Core.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string userId, List<CartItemDTO> cartItems, string shippingAddress);
        Task<Order> GetOrderByIdAsync(int orderId);
    }


}
