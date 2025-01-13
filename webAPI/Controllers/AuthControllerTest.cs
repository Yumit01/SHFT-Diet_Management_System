using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using System.Threading.Tasks;
using webAPI.Controllers;
using webAPI.Models;
using webAPI.Services;
using Xunit;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace webAPI.Controllers.Tests
{
    public class AuthControllerTest
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly AuthController _controller;

        public AuthControllerTest()
        {
            _mockUserService = new Mock<IUserService>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(
                new Mock<IUserStore<ApplicationUser>>().Object, null, null, null, null, null, null, null, null);
            _controller = new AuthController(_mockUserService.Object, _mockUserManager.Object);
        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_WhenUserIsNull()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "test@example.com", Password = "password" };
            _mockUserService.Setup(s => s.Authenticate(loginDto.Email, loginDto.Password)).ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Invalid email or password", ((dynamic)unauthorizedResult.Value).message);
        }

        [Fact]
        public async Task Login_ReturnsOk_WhenUserIsAuthenticated()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "test@example.com", Password = "password" };
            var user = new ApplicationUser { UserName = loginDto.Email, Email = loginDto.Email };
            _mockUserService.Setup(s => s.Authenticate(loginDto.Email, loginDto.Password)).ReturnsAsync(user);
            _mockUserManager.Setup(m => m.GetRolesAsync(user)).ReturnsAsync(new List<string> { "User" });

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Login successful", ((dynamic)okResult.Value).message);
            Assert.Equal("User", ((dynamic)okResult.Value).role);
        }

        [Fact]
        public async Task GetUserRole_ReturnsUnauthorized_WhenUserIdIsNull()
        {
            // Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity());
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Act
            var result = await _controller.GetUserRole();

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task GetUserRole_ReturnsNotFound_WhenUserIsNull()
        {
            // Arrange
            var userId = "test-user-id";
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, userId) }));
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
            _mockUserService.Setup(s => s.GetUserById(userId)).ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _controller.GetUserRole();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("User not found", ((dynamic)notFoundResult.Value).message);
        }

        [Fact]
        public async Task GetUserRole_ReturnsOk_WhenRoleIsFound()
        {
            // Arrange
            var userId = "test-user-id";
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, userId) }));
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
            var applicationUser = new ApplicationUser { Id = userId, UserName = "test@example.com" };
            _mockUserService.Setup(s => s.GetUserById(userId)).ReturnsAsync(applicationUser);
            _mockUserManager.Setup(m => m.GetRolesAsync(applicationUser)).ReturnsAsync(new List<string> { "Admin" });

            // Act
            var result = await _controller.GetUserRole();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Admin", ((dynamic)okResult.Value).role);
        }
    }
}