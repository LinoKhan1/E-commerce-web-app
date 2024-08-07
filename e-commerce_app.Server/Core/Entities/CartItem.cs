using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace e_commerce_app.Server.Core.Entities
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

    }
}
