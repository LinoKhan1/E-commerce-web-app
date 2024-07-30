using e_commerce_app.Server.APIs.DTOs.AuthenticationDTOs;
using e_commerce_app.Server.Core.Configuration;
using e_commerce_app.Server.Core.Entities;
using e_commerce_app.Server.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace e_commerce_app.tests.Services
{
    public class JwtAuthServiceTests
    {
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly Mock<IOptions<JwtSettings>> _mockJwtSettings;
        private readonly Mock<ILogger<JwtAuthService>> _mockLogger;
        private readonly JwtAuthService _jwtAuthService;

        public JwtAuthServiceTests()
        {
            _mockUserManager = new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(),
                null, null, null, null, null, null, null, null);
            _mockJwtSettings = new Mock<IOptions<JwtSettings>>();
            _mockLogger = new Mock<ILogger<JwtAuthService>>();

            _mockJwtSettings.SetupGet(x => x.Value).Returns(new JwtSettings
            {
                Key = "3018cyREang9m7J+PTV19QJLIS9PFnabCdJKa0hZgj0=", // Use a valid key for real tests
                Issuer = "test_issuer",
                Audience = "test_audience",
                ExpireHours = 1
            });

            _jwtAuthService = new JwtAuthService(
                _mockUserManager.Object,
                _mockJwtSettings.Object,
                _mockLogger.Object);
        }

        [Fact]
        public async Task AuthenticateAsync_Should_Return_UserResponseDTO_On_Valid_Credentials()
        {
            // Arrange
            var loginDTO = new LoginUserDTO { Email = "test@example.com", Password = "password" };
            var user = new User { UserName = "TestUser", Email = "test@example.com", Id = "1" };
            _mockUserManager.Setup(x => x.FindByEmailAsync(loginDTO.Email)).ReturnsAsync(user);
            _mockUserManager.Setup(x => x.CheckPasswordAsync(user, loginDTO.Password)).ReturnsAsync(true);

            // Act
            var result = await _jwtAuthService.AuthenticateAsync(loginDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.UserName, result.UserName);
            Assert.Equal(user.Email, result.Email);
            Assert.NotNull(result.Token); // You may want to decode and validate the token here
        }

        [Fact]
        public async Task AuthenticateAsync_Should_Return_Null_On_Invalid_Credentials()
        {
            // Arrange
            var loginDTO = new LoginUserDTO { Email = "test@example.com", Password = "wrongpassword" };
            _mockUserManager.Setup(x => x.FindByEmailAsync(loginDTO.Email)).ReturnsAsync((User)null);

            // Act
            var result = await _jwtAuthService.AuthenticateAsync(loginDTO);

            // Assert
            Assert.Null(result);
        }

        /*[Fact]
        public async Task RegisterAsync_Should_Return_UserResponseDTO_On_Successful_Registration()
        {
            // Arrange
            var registerDTO = new RegisterUserDTO { UserName = "NewUser", Email = "newuser@example.com", Password = "password" };
            var user = new User { UserName = registerDTO.UserName, Email = registerDTO.Email, Id = "2" };
            _mockUserManager.Setup(x => x.FindByEmailAsync(registerDTO.Email)).ReturnsAsync((User)null);
            _mockUserManager.Setup(x => x.CreateAsync(It.Is<User>(u => u.Email == registerDTO.Email), registerDTO.Password))
                .ReturnsAsync(IdentityResult.Success);
            _mockUserManager.Setup(x => x.FindByEmailAsync(registerDTO.Email)).ReturnsAsync(user);

            // Act
            var result = await _jwtAuthService.RegisterAsync(registerDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.UserName, result.UserName);
            Assert.Equal(user.Email, result.Email);
            Assert.NotNull(result.Token); // You may want to decode and validate the token here
        }*/
        [Fact]
        public async Task RegisterAsync_Should_Return_Null_On_Existing_Email()
        {
            // Arrange
            var registerDTO = new RegisterUserDTO { UserName = "ExistingUser", Email = "existinguser@example.com", Password = "password" };
            var existingUser = new User { UserName = registerDTO.UserName, Email = registerDTO.Email, Id = "3" };
            _mockUserManager.Setup(x => x.FindByEmailAsync(registerDTO.Email)).ReturnsAsync(existingUser);

            // Act
            var result = await _jwtAuthService.RegisterAsync(registerDTO);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GenerateJwtToken_Should_Return_Token()
        {
            // Arrange
            var user = new User { UserName = "TestUser", Email = "test@example.com", Id = "1" };

            // Act
            var token = _jwtAuthService.GenerateJwtToken(user);

            // Assert
            Assert.NotNull(token);
            Assert.IsType<string>(token);
        }
    }
}
