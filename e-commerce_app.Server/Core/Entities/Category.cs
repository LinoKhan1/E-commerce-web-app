using System.ComponentModel.DataAnnotations;

namespace e_commerce_app.Server.Core.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(100)] // Adjust the length as needed
        public string Name { get; set; } = string.Empty;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
