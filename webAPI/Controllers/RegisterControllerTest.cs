using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using webAPI.Controllers;
using webAPI.Models;
using webAPI.Services;
using Xunit;

namespace webAPI.Controllers.Tests
{
    public class RegisterControllerTest
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<ILogger<RegisterController>> _mockLogger;
        private readonly RegisterController _controller;

        public RegisterControllerTest()
        {
            _mockUserService = new Mock<IUserService>();
            _mockLogger = new Mock<ILogger<RegisterController>>();
            _controller = new RegisterController(_mockUserService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Register_ReturnsBadRequest_WhenRequestIsNull()
        {
            // Act
            var result = await _controller.Register(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponse>(badRequestResult.Value);
            Assert.Equal("Request data is null", apiResponse.Message);
        }

        [Fact]
        public async Task Register_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Error", "Invalid model state");
            var request = new RegisterRequest();

            // Act
            var result = await _controller.Register(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponse>(badRequestResult.Value);
            Assert.Equal("Invalid request data", apiResponse.Message);
        }

        [Fact]
        public async Task Register_ReturnsOk_WhenUserIsRegisteredSuccessfully()
        {
            // Arrange
            var request = new RegisterRequest();
            _mockUserService.Setup(service => service.RegisterUser(request)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Register(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponse>(okResult.Value);
            Assert.Equal("User registered successfully", apiResponse.Message);
        }

        [Fact]
        public async Task Register_ReturnsBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            var request = new RegisterRequest();
            var exceptionMessage = "An error occurred";
            _mockUserService.Setup(service => service.RegisterUser(request)).Throws(new Exception(exceptionMessage));

            // Act
            var result = await _controller.Register(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponse>(badRequestResult.Value);
            Assert.Equal(exceptionMessage, apiResponse.Message);
        }
    }
}