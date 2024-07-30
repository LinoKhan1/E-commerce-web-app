using e_commerce_app.Server.APIs.DTOs.AuthenticationDTOs;
using e_commerce_app.Server.Core.Entities;
using e_commerce_app.Server.Core.Services.Interfaces;
using e_commerce_app.Server.Core.Services;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_app.tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly Mock<IJwtAuthService> _mockJwtAuthService;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mockUserManager = new Mock<UserManager<User>>(
                new Mock<IUserStore<User>>().Object,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null
            );

            _mockJwtAuthService = new Mock<IJwtAuthService>();
            _userService = new UserService(_mockUserManager.Object, _mockJwtAuthService.Object);
        }

        [Fact]
        public async Task RegisterAsync_Should_Return_UserResponseDTO_On_Successful_Registration()
        {
            // Arrange
            var registerDTO = new RegisterUserDTO
            {
                UserName = "testuser",
                Email = "test@example.com",
                Password = "Test@123"
            };

            var expectedResponse = new UserResponseDTO
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
                Token = "some_jwt_token"
            };

            _mockJwtAuthService.Setup(service => service.RegisterAsync(registerDTO))
                               .ReturnsAsync(expectedResponse);

            // Act
            var result = await _userService.RegisterAsync(registerDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.UserName, result.UserName);
            Assert.Equal(expectedResponse.Email, result.Email);
            Assert.Equal(expectedResponse.Token, result.Token);
        }

        [Fact]
        public async Task LoginAsync_Should_Return_UserResponseDTO_On_Successful_Login()
        {
            // Arrange
            var loginDTO = new LoginUserDTO
            {
                Email = "test@example.com",
                Password = "Test@123"
            };

            var expectedResponse = new UserResponseDTO
            {
                UserName = "testuser",
                Email = loginDTO.Email,
                Token = "some_jwt_token"
            };

            _mockJwtAuthService.Setup(service => service.AuthenticateAsync(loginDTO))
                               .ReturnsAsync(expectedResponse);

            // Act
            var result = await _userService.LoginAsync(loginDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.UserName, result.UserName);
            Assert.Equal(expectedResponse.Email, result.Email);
            Assert.Equal(expectedResponse.Token, result.Token);
        }

    }
  }
