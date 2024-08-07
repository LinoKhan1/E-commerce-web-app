using e_commerce_app.Server.APIs.DTOs.AuthenticationDTOs;
using e_commerce_app.Server.Core.Entities;
using e_commerce_app.Server.Core.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace e_commerce_app.Server.Core.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtAuthService _jwtAuthService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userManager">User manager for handling user-related operations.</param>
        /// <param name="jwtAuthService">JWT authentication service.</param>
        public UserService(UserManager<User> userManager, IJwtAuthService jwtAuthService)
        {
            _userManager = userManager;
            _jwtAuthService = jwtAuthService;
        }

        public async Task<UserResponseDTO> RegisterAsync(RegisterUserDTO registerDTO)
        {
            return await _jwtAuthService.RegisterAsync(registerDTO);
        }

        public async Task<UserResponseDTO> LoginAsync(LoginUserDTO loginDTO)
        {
            return await _jwtAuthService.AuthenticateAsync(loginDTO);
        }

    }
}
