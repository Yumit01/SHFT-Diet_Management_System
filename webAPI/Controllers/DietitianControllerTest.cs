using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using webAPI.Controllers;
using webAPI.Models;
using webAPI.Services;
using Xunit;

namespace webAPI.Controllers.Tests
{
    public class DietitianControllerTest
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly DietitianController _controller;

        public DietitianControllerTest()
        {
            _mockUserService = new Mock<IUserService>();
            _controller = new DietitianController(_mockUserService.Object);
        }

        [Fact]
        public async Task GetAllDietitian_ReturnsOkResult_WithListOfDietitians()
        {
            // Arrange
            _mockUserService.Setup(service => service.GetAllDietitians())
                .ReturnsAsync(new List<Dietitian> { new Dietitian(), new Dietitian() });

            // Act
            var result = await _controller.GetAllDietitian();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var dietitians = Assert.IsType<List<Dietitian>>(okResult.Value);
            Assert.Equal(2, dietitians.Count);
        }

        [Fact]
        public async Task GetAllDietitian_ReturnsBadRequest_OnException()
        {
            // Arrange
            _mockUserService.Setup(service => service.GetAllDietitians())
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.GetAllDietitian();

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponse>(badRequestResult.Value);
            Assert.Equal("Test exception", apiResponse.Message);
        }

        [Fact]
        public async Task UpdateDietitian_ReturnsOkResult_WhenUpdateIsSuccessful()
        {
            // Arrange
            var dietitian = new Dietitian();
            _mockUserService.Setup(service => service.UpdateDietitian(It.IsAny<int>(), It.IsAny<Dietitian>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateDietitian(1, dietitian);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponse>(okResult.Value);
            Assert.Equal("Dietitian updated successfully", apiResponse.Message);
        }

        [Fact]
        public async Task UpdateDietitian_ReturnsNotFound_WhenDietitianNotFound()
        {
            // Arrange
            var dietitian = new Dietitian();
            _mockUserService.Setup(service => service.UpdateDietitian(It.IsAny<int>(), It.IsAny<Dietitian>()))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.UpdateDietitian(1, dietitian);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponse>(notFoundResult.Value);
            Assert.Equal("Dietitian not found", apiResponse.Message);
        }

        [Fact]
        public async Task UpdateDietitian_ReturnsBadRequest_OnException()
        {
            // Arrange
            var dietitian = new Dietitian();
            _mockUserService.Setup(service => service.UpdateDietitian(It.IsAny<int>(), It.IsAny<Dietitian>()))
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.UpdateDietitian(1, dietitian);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponse>(badRequestResult.Value);
            Assert.Equal("Test exception", apiResponse.Message);
        }

        [Fact]
        public async Task DeleteDietitian_ReturnsNoContent_WhenDeleteIsSuccessful()
        {
            // Arrange
            _mockUserService.Setup(service => service.DeleteDietitian(It.IsAny<int>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteDietitian(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteDietitian_ReturnsNotFound_WhenDietitianNotFound()
        {
            // Arrange
            _mockUserService.Setup(service => service.DeleteDietitian(It.IsAny<int>()))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteDietitian(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}