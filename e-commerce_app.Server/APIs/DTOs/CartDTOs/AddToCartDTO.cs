namespace e_commerce_app.Server.APIs.DTOs.CartDTOs
{
    public class AddToCartDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product to be added to the cart.
        /// </summary>
        public int Quantity { get; set; }
    }
}
