using e_commerce_app.Server.APIs.DTOs.CheckoutDTO;
using e_commerce_app.Server.Core.Configuration;
using e_commerce_app.Server.Core.Entities;
using e_commerce_app.Server.Core.Services.Interfaces;
using e_commerce_app.Server.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace e_commerce_app.Server.Core.Services
{
    public class PayPalService : IPayPalService
    {
        private readonly HttpClient _httpClient;
        private readonly PayPalSettings _payPalSettings;
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<PayPalService> _logger;

        public PayPalService(IOptions<PayPalSettings> payPalSettings, IOrderRepository orderRepository, ILogger<PayPalService> logger)
        {
            _payPalSettings = payPalSettings.Value;
            _orderRepository = orderRepository;
            _logger = logger;

            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri("https://api.sandbox.paypal.com") // Change to live URL in production
            };
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_payPalSettings.ClientId}:{_payPalSettings.ClientSecret}")));
        }

        public async Task<string> CreatePaymentAsync(Order order)
        {
            var paymentRequest = new
            {
                intent = "CAPTURE",
                purchase_units = new[]
                {
                    new
                    {
                        amount = new
                        {
                            currency_code = "USD",
                            value = order.TotalAmount.ToString("F2")
                        }
                    }
                },
                application_context = new
                {
                    return_url = "https://yourdomain.com/return",
                    cancel_url = "https://yourdomain.com/cancel"
                }
            };

            var json = JsonConvert.SerializeObject(paymentRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/v2/checkout/orders", content);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                dynamic orderResponse = JsonConvert.DeserializeObject(responseData);

                // Extract approval link manually
                string approvalLink = null;
                foreach (var link in orderResponse.links)
                {
                    if (link.rel == "approve")
                    {
                        approvalLink = link.href;
                        break;
                    }
                }

                _logger.LogInformation("PayPal Order created successfully: {OrderId}", (object)orderResponse.id);

                // Optionally save order to your database
                await _orderRepository.AddOrderAsync(order);

                // Return the approval link for frontend
                return approvalLink;
            }
            else
            {
                _logger.LogError("Error creating PayPal Order: {StatusCode}", response.StatusCode);
                throw new Exception("Error creating PayPal order.");
            }
        }

        public async Task<PayPalOrderResponseDTO> ExecutePaymentAsync(int orderId)
        {
            var response = await _httpClient.PostAsync($"/v2/checkout/orders/{orderId}/capture", null);
            var result = new PayPalOrderResponseDTO();

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                dynamic capturedOrder = JsonConvert.DeserializeObject(responseData);
                _logger.LogInformation("PayPal Order captured successfully: {OrderId}", (object)capturedOrder.id);

                // Retrieve the original order from the database using the order ID
                var order = await _orderRepository.GetOrderByIdAsync(orderId); // Implement this method in your repository

                // Update local order record as necessary
                order.Status = OrderStatus.Completed; // Update order status to Completed
                order.PaymentStatus = PaymentStatus.Paid; // Update payment status to Paid
                order.PaymentId = capturedOrder.id; // Save the payment ID
                order.PayerInfo = capturedOrder.payer.name; // Adjust as per your data structure

                await _orderRepository.UpdateOrderAsync(order); // Save updated order details

                result.OrderId = capturedOrder.id;
                result.Status = "COMPLETED";
                result.Message = "Payment completed successfully.";
                return result;
            }
            else
            {
                _logger.LogError("Error capturing PayPal Order: {StatusCode}", response.StatusCode);
                result.Status = "ERROR";
                result.Message = "Error capturing PayPal order.";
                return result;
            }
        }

    }
}
