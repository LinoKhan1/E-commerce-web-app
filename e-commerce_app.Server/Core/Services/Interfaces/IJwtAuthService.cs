using e_commerce_app.Server.APIs.DTOs.AuthenticationDTOs;
using e_commerce_app.Server.Core.Entities;

namespace e_commerce_app.Server.Core.Services.Interfaces
{

    /// <summary>
    /// Interface for JWT authentication service.
    /// </summary>
    public interface IJwtAuthService
    {
        Task<UserResponseDTO> AuthenticateAsync(LoginUserDTO loginDTO);
        Task<UserResponseDTO> RegisterAsync(RegisterUserDTO registerDTO);

        string GenerateJwtToken(User user);
    }
}
