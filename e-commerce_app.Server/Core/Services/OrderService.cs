using e_commerce_app.Server.APIs.DTOs.CartDTOs;
using e_commerce_app.Server.Core.Entities;
using e_commerce_app.Server.Core.Services.Interfaces;
using e_commerce_app.Server.Infrastructure.Repositories.Interfaces;
using e_commerce_app.Server.Infrastructure.unitOfWork;

namespace e_commerce_app.Server.Core.Services
{
    public class OrderService: IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        private readonly ICartRepository _cartRepository; // Assuming you already have this
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Order> CreateOrderAsync(string userId, List<CartItemDTO> cartItems, string shippingAddress)
        {
            // Calculate total amount for the order
            decimal totalAmount = cartItems.Sum(item => item.Price * item.Quantity);

            // Create a new order
            var order = new Order
            {
                UserId = userId,
                TotalAmount = totalAmount,
                Status = OrderStatus.Pending,
                PaymentStatus = PaymentStatus.Pending,
                OrderDate = DateTime.UtcNow,
                ShippingAddress = shippingAddress,
                OrderItems = cartItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Price
                }).ToList()
            };

            // Add order using repository
            await _orderRepository.AddOrderAsync(order);

            // Commit the transaction via Unit of Work
            await _unitOfWork.CompleteAsync();

            return order;


        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId);
        }

    }
}
