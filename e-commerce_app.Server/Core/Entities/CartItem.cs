using Microsoft.Identity.Client;

namespace e_commerce_app.Server.Core.Entities
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateAdded { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }

    }
}
