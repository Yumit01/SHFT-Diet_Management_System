using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using webAPI.Controllers;
using webAPI.Models;
using webAPI.Services;
using Xunit;

namespace webAPI.Tests.Controllers
{
    public class ClientControllerTest
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly ClientController _controller;

        public ClientControllerTest()
        {
            _mockUserService = new Mock<IUserService>();
            _controller = new ClientController(_mockUserService.Object);
        }

        [Fact]
        public async Task CreateClient_ReturnsOkResult_WhenClientIsCreated()
        {
            // Arrange
            var request = new RegisterRequest();
            _mockUserService.Setup(service => service.RegisterUser(request)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateClient(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ApiResponse>(okResult.Value);
            Assert.Equal("Client created successfully", response.Message);
        }

        [Fact]
        public async Task CreateClient_ReturnsBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            var request = new RegisterRequest();
            _mockUserService.Setup(service => service.RegisterUser(request)).Throws(new Exception("Error"));

            // Act
            var result = await _controller.CreateClient(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<ApiResponse>(badRequestResult.Value);
            Assert.Equal("Error", response.Message);
        }

        [Fact]
        public async Task GetAllClients_ReturnsOkResult_WithClients()
        {
            // Arrange
            var clients = new List<Client> { new Client(), new Client() };
            _mockUserService.Setup(service => service.GetAllClients()).ReturnsAsync(clients);

            // Act
            var result = await _controller.GetAllClients();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(clients, okResult.Value);
        }

        [Fact]
        public async Task GetAllClients_ReturnsBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            _mockUserService.Setup(service => service.GetAllClients()).Throws(new Exception("Error"));

            // Act
            var result = await _controller.GetAllClients();

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<ApiResponse>(badRequestResult.Value);
            Assert.Equal("Error", response.Message);
        }

        [Fact]
        public async Task UpdateClient_ReturnsOkResult_WhenClientIsUpdated()
        {
            // Arrange
            var client = new Client();
            _mockUserService.Setup(service => service.UpdateClient(1, client)).ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateClient(1, client);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ApiResponse>(okResult.Value);
            Assert.Equal("Client updated successfully", response.Message);
        }

        [Fact]
        public async Task UpdateClient_ReturnsNotFound_WhenClientIsNotFound()
        {
            // Arrange
            var client = new Client();
            _mockUserService.Setup(service => service.UpdateClient(1, client)).ReturnsAsync(false);

            // Act
            var result = await _controller.UpdateClient(1, client);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var response = Assert.IsType<ApiResponse>(notFoundResult.Value);
            Assert.Equal("Client not found", response.Message);
        }

        [Fact]
        public async Task UpdateClient_ReturnsBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            var client = new Client();
            _mockUserService.Setup(service => service.UpdateClient(1, client)).Throws(new Exception("Error"));

            // Act
            var result = await _controller.UpdateClient(1, client);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<ApiResponse>(badRequestResult.Value);
            Assert.Equal("Error", response.Message);
        }

        [Fact]
        public async Task DeleteClient_ReturnsOkResult_WhenClientIsDeleted()
        {
            // Arrange
            _mockUserService.Setup(service => service.DeleteClient(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteClient(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ApiResponse>(okResult.Value);
            Assert.Equal("Client deleted successfully", response.Message);
        }

        [Fact]
        public async Task DeleteClient_ReturnsNotFound_WhenClientIsNotFound()
        {
            // Arrange
            _mockUserService.Setup(service => service.DeleteClient(1)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteClient(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var response = Assert.IsType<ApiResponse>(notFoundResult.Value);
            Assert.Equal("Client not found", response.Message);
        }

        [Fact]
        public async Task DeleteClient_ReturnsBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            _mockUserService.Setup(service => service.DeleteClient(1)).Throws(new Exception("Error"));

            // Act
            var result = await _controller.DeleteClient(1);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<ApiResponse>(badRequestResult.Value);
            Assert.Equal("Error", response.Message);
        }
    }
}