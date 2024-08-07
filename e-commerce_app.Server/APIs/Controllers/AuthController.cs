using e_commerce_app.Server.APIs.DTOs.AuthenticationDTOs;
using e_commerce_app.Server.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_app.Server.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="userService">Service for handling user-related operations.</param>
        /// <param name="logger">Logger for logging information, warnings, and errors.</param>
        public AuthController(IUserService userService, ILogger<AuthController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid registration attempt.");
                return BadRequest(ModelState);
            }

            try
            {
                var userResponse = await _userService.RegisterAsync(registerDTO);
                if (userResponse == null)
                {
                    _logger.LogWarning("Registration failed for email: {Email}", registerDTO.Email);
                    return BadRequest("User registration failed.");
                }

                _logger.LogInformation("User registered successfully with email: {Email}", registerDTO.Email);
                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during registration for email: {Email}", registerDTO.Email);
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid login attempt.");
                return BadRequest(ModelState);
            }
            try
            {
                var userResponse = await _userService.LoginAsync(loginDTO);
                if (userResponse == null)
                {
                    _logger.LogWarning("Authentication failed for email: {Email}", loginDTO.Email);
                    return Unauthorized("Invalid credentials.");
                }
                _logger.LogInformation("User authenticated successfully with email: {Email}", loginDTO.Email);
                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during authentication for email: {Email}", loginDTO.Email);
                return StatusCode(500, "Internal server error");
            }

        }
    }
}
