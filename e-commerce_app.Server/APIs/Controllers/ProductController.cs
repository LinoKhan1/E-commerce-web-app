using e_commerce_app.Server.APIs.DTOs.ProductDTOs;
using e_commerce_app.Server.Core.Entities;
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
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts([FromQuery] int? limit)
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                if (limit.HasValue && limit.Value > 0)
                {
                    products = products.Take(limit.Value).ToList(); // Limit the number of products
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving products.");
                return StatusCode(500, "Internal server error");
            }
        }

       

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                return Ok(product);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Product with ID {id} not found.");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving product with ID {id}.");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByCategory(int categoryId)
        {
            var products = await _productService.GetProductsByCategoryAsync(categoryId);
            return Ok(products);
        }


        [HttpPost]
        public async Task<ActionResult> CreateProduct(ProductDTO productDto)
        {
            try
            {
                if (productDto == null)
                {
                    _logger.LogWarning("CreateProduct: ProductDTO is null.");
                    return BadRequest("Product data is null.");
                }

                await _productService.AddProductAsync(productDto);
                return CreatedAtAction(nameof(GetProduct), new { id = productDto.ProductId }, productDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a new product.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, ProductDTO productDto)
        {
            try
            {
                if (productDto == null)
                {
                    _logger.LogWarning("UpdateProduct: ProductDTO is null.");
                    return BadRequest("Product data is null.");
                }

                await _productService.UpdateProductAsync(id, productDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Product with ID {id} not found.");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating product with ID {id}.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Product with ID {id} not found.");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting product with ID {id}.");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
