namespace e_commerce_app.Server.APIs.DTOs.ProductDTOs
{
    public class ProductDTO
    {

        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock {  get; set; }
        public int CategoryId { get; set; }

    }

}
