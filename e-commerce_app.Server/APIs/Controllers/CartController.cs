using e_commerce_app.Server.APIs.DTOs.CartDTOs;
using e_commerce_app.Server.APIs.DTOs.CheckoutDTO;
using e_commerce_app.Server.Core.Services;
using e_commerce_app.Server.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace e_commerce_app.Server.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ILogger<CartController> _logger;
        private readonly IOrderService _orderService;
        private readonly IPayPalService _payPalService;

        public CartController(ICartService cartService, ILogger<CartController> logger)
        {
            _cartService = cartService;
            _logger = logger;
        }

        [HttpGet("{cartItemId}")]
        public async Task<IActionResult> GetCartItem(int cartItemId)
        {
            try
            {
                var cartItem = await _cartService.GetCartItemByIdAsync(cartItemId);
                return Ok(cartItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving cart item with ID {cartItemId}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetCartItemsByUserId(string userId)
        {
            try
            {
                var cartItems = await _cartService.GetCartItemsByUserIdAsync(userId);
                return Ok(cartItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving cart items for user {userId}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDTO addToCartDto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _cartService.AddCartItemAsync(addToCartDto, userId);
                return CreatedAtAction(nameof(GetCartItemsByUserId), new { userId }, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding item to cart");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCartItem([FromBody] CartItemDTO cartItemDto)
        {
            try
            {
                await _cartService.UpdateCartItemAsync(cartItemDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating cart item with ID {cartItemDto.Id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> DeleteCartItem(int cartItemId)
        {
            try
            {
                await _cartService.DeleteCartItemAsync(cartItemId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting cart item with ID {cartItemId}");
                return StatusCode(500, "Internal server error");
            }
        }

         [HttpPost("checkout")]
         public async Task<IActionResult> Checkout([FromBody] CheckoutDTO checkoutDto)
         {
             try
             {
                 var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                 // Create the order
                 var order = await _orderService.CreateOrderAsync(userId, checkoutDto.CartItems, checkoutDto.ShippingAddress);

                 // Proceed to generate PayPal payment link
                 var paymentUrl = await _payPalService.CreatePaymentAsync(order);

                 return Ok(new { paymentUrl });
             }
             catch (Exception ex)
             {
                 _logger.LogError(ex, "Error occurred during checkout");
                 return StatusCode(500, "Internal server error");
             }
         }
    }
}