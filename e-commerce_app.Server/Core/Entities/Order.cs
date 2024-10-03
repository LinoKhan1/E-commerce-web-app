using PayPal.Api;

namespace e_commerce_app.Server.Core.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public string ShippingAddress { get; set; }

        // New properties for transaction details
        public string PaymentId { get; set; } // Store the PayPal payment ID
        public string PayerInfo { get; set; } // Store payer information

        // Navigation Property
        public List<OrderItem> OrderItems { get; set; }
    }

    public enum OrderStatus
    {
        Pending,
        Completed,
        Shipped,
        Canceled
    }

    public enum PaymentStatus
    {
        Pending,
        Paid,
        Failed
    }
}
