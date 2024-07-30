using e_commerce_app.Server.APIs.Controllers;
using e_commerce_app.Server.APIs.DTOs.AuthenticationDTOs;
using e_commerce_app.Server.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_app.tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<ILogger<AuthController>> _mockLogger;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _mockLogger = new Mock<ILogger<AuthController>>();
            _controller = new AuthController(_mockUserService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Register_Should_Return_BadRequest_When_ModelState_Is_Invalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Error", "Model state is invalid.");

            var registerDTO = new RegisterUserDTO
            {
                UserName = "testuser",
                Email = "test@example.com",
                Password = "Test@123"
            };

            // Act
            var result = await _controller.Register(registerDTO);

            // Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, actionResult.StatusCode);
        }

        [Fact]
        public async Task Register_Should_Return_BadRequest_When_Registration_Fails()
        {
            // Arrange
            var registerDTO = new RegisterUserDTO
            {
                UserName = "testuser",
                Email = "test@example.com",
                Password = "Test@123"
            };

            _mockUserService.Setup(service => service.RegisterAsync(registerDTO))
                            .ReturnsAsync((UserResponseDTO)null);

            // Act
            var result = await _controller.Register(registerDTO);

            // Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, actionResult.StatusCode);
        }

        [Fact]
        public async Task Register_Should_Return_Ok_When_Registration_Succeeds()
        {
            // Arrange
            var registerDTO = new RegisterUserDTO
            {
                UserName = "testuser",
                Email = "test@example.com",
                Password = "Test@123"
            };

            var userResponse = new UserResponseDTO
            {
                UserName = "testuser",
                Email = "test@example.com",
                Token = "some_jwt_token"
            };

            _mockUserService.Setup(service => service.RegisterAsync(registerDTO))
                            .ReturnsAsync(userResponse);

            // Act
            var result = await _controller.Register(registerDTO);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<UserResponseDTO>(actionResult.Value);
            Assert.Equal(userResponse.UserName, response.UserName);
            Assert.Equal(userResponse.Email, response.Email);
            Assert.Equal(userResponse.Token, response.Token);
        }

        [Fact]
        public async Task Login_Should_Return_BadRequest_When_ModelState_Is_Invalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Error", "Model state is invalid.");

            var loginDTO = new LoginUserDTO
            {
                Email = "test@example.com",
                Password = "Test@123"
            };

            // Act
            var result = await _controller.Login(loginDTO);

            // Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, actionResult.StatusCode);
        }

        [Fact]
        public async Task Login_Should_Return_Unauthorized_When_Authentication_Fails()
        {
            // Arrange
            var loginDTO = new LoginUserDTO
            {
                Email = "test@example.com",
                Password = "Test@123"
            };

            _mockUserService.Setup(service => service.LoginAsync(loginDTO))
                            .ReturnsAsync((UserResponseDTO)null);

            // Act
            var result = await _controller.Login(loginDTO);

            // Assert
            var actionResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal(StatusCodes.Status401Unauthorized, actionResult.StatusCode);
        }

        [Fact]
        public async Task Login_Should_Return_Ok_When_Authentication_Succeeds()
        {
            // Arrange
            var loginDTO = new LoginUserDTO
            {
                Email = "test@example.com",
                Password = "Test@123"
            };

            var userResponse = new UserResponseDTO
            {
                UserName = "testuser",
                Email = "test@example.com",
                Token = "some_jwt_token"
            };

            _mockUserService.Setup(service => service.LoginAsync(loginDTO))
                            .ReturnsAsync(userResponse);

            // Act
            var result = await _controller.Login(loginDTO);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<UserResponseDTO>(actionResult.Value);
            Assert.Equal(userResponse.UserName, response.UserName);
            Assert.Equal(userResponse.Email, response.Email);
            Assert.Equal(userResponse.Token, response.Token);
        }
    }
}
