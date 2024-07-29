using e_commerce_app.Server.APIs.DTOs.ProductDTOs;
using e_commerce_app.Server.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_app.Server.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor for ProductsController.
        /// </summary>
        /// <param name="productService">The product service instance.</param>
        public ProductController(IProductService productService, ILogger logger)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A list of all products.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            try
            {
                _logger.LogInformation("Retrieving all products.");
                var products = await _productService.GetProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving products.");
                return StatusCode(500, "Internal server error.");
            }
        }

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The product with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            try
            {
                _logger.LogInformation("Retrieving product with ID {ProductId}.", id);
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    _logger.LogWarning("Product with ID {ProductId} not found.", id);
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving product with ID {ProductId}.", id);
                return StatusCode(500, "Internal server error.");
            }

        }
        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="productDto">The product to add.</param>
        /// <returns>The newly created product.</returns>
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> PostProduct(ProductDTO productDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid product data provided.");
                    return BadRequest(ModelState);
                }

                _logger.LogInformation("Adding new product.");
                await _productService.AddProductAsync(productDto);
                return CreatedAtAction(nameof(GetProduct), new { id = productDto.Id }, productDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new product.");
                return StatusCode(500, "Internal server error.");
            }

        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="productDTO">The updated product information.</param>
        /// <returns>No content.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDTO productDTO)
        {
            try
            {
                if (id != productDTO.Id)
                {
                    _logger.LogWarning("Product ID mismatch. Provided ID: {ProvidedId}, Product ID: {ProductId}", id, productDTO.Id);
                    return BadRequest("ID mismatch.");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid product data provided for ID {ProductId}.", id);
                    return BadRequest(ModelState);
                }

                _logger.LogInformation("Updating product with ID {ProductId}.", id);
                await _productService.UpdateProductAsync(productDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating product with ID {ProductId}.", id);
                return StatusCode(500, "Internal server error.");
            }
        }

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                _logger.LogInformation("Deleting product with ID {ProductId}.", id);
                await _productService.DeleteProductAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting product with ID {ProductId}.", id);
                return StatusCode(500, "Internal server error.");
            }
        }

    }
}
