using e_commerce_app.Server.APIs.DTOs.CartDTOs;

namespace e_commerce_app.Server.APIs.DTOs.CheckoutDTO
{
    public class CheckoutDTO
    {
        public List<CartItemDTO> CartItems { get; set; }
        public string ShippingAddress { get; set; } 
    }
}
