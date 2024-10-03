using e_commerce_app.Server.APIs.DTOs.CheckoutDTO;
using e_commerce_app.Server.Core.Entities;

namespace e_commerce_app.Server.Core.Services.Interfaces
{
    public interface IPayPalService
    {
        Task<string> CreatePaymentAsync(Order order);
        Task<PayPalOrderResponseDTO> ExecutePaymentAsync(int orderId);
    }
}
