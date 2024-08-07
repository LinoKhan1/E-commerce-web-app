using e_commerce_app.Server.APIs.DTOs.CartDTOs;
using e_commerce_app.Server.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace e_commerce_app.Server.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ILogger<CartController> _logger;

        public CartController(ICartService cartService, ILogger<CartController> logger)
        {
            _cartService = cartService;
            _logger = logger;
        }

        /// <summary>
        /// Gets the cart items for the authenticated user.
        /// </summary>
        /// <returns>A list of cart items.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItemDTO>>> GetCartItems()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                _logger.LogWarning("User ID not found in claims.");
                return BadRequest("User ID not found in claims.");
            }

            try
            {
                var cartItems = await _cartService.GetCartItemsAsync(userId);
                return Ok(cartItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving cart items for user {UserId}.", userId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving cart items.");
            }
        }

        /// <summary>
        /// Adds a new item to the cart for the authenticated user.
        /// </summary>
        /// <param name="addToCartDto">The data transfer object containing the details of the item to add.</param>
        [HttpPost]
        public async Task<IActionResult> AddCart([FromBody] AddToCartDTO addToCartDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                _logger.LogWarning("User ID not found in claims.");
                return BadRequest("User ID not found in claims.");
            }

            try
            {
                await _cartService.AddCartItemAsync(userId, addToCartDto);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new item to the cart for user {UserId}.", userId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the item to the cart.");
            }
        }

        /// <summary>
        /// Updates the quantity of an existing cart item.
        /// </summary>
        /// <param name="id">The unique identifier for the cart item.</param>
        /// <param name="quantity">The new quantity for the cart item.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCartItem(int id, [FromBody] int quantity)
        {
            if (quantity <= 0)
            {
                _logger.LogWarning("Invalid quantity {Quantity} for cart item {CartItemId}.", quantity, id);
                return BadRequest("Quantity must be greater than zero.");
            }

            try
            {
                await _cartService.UpdateCartItemAsync(id, quantity);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Cart item with ID {CartItemId} not found.", id);
                return NotFound($"Cart item with ID {id} not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating cart item with ID {CartItemId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the cart item.");
            }
        }

        /// <summary>
        /// Removes a cart item.
        /// </summary>
        /// <param name="id">The unique identifier for the cart item.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCartItem(int id)
        {
            try
            {
                await _cartService.RemoveCartItemAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while removing cart item with ID {CartItemId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while removing the cart item.");
            }
        }
    }
}
