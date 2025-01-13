using Microsoft.AspNetCore.Mvc;
using webAPI.Models;
using webAPI.Services;

[ApiController]
[Route("api/[controller]")]
public class RegisterController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<RegisterController> _logger;

    public RegisterController(IUserService userService, ILogger<RegisterController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        _logger.LogInformation("Incoming registration request: {@request}", request);
        Console.WriteLine("Registering user"); // Debugging 
        if (request == null)
        {
            return BadRequest(new ApiResponse { Message = "Request data is null" });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponse { Message = "Invalid request data" });
        }

        try
        {
            await _userService.RegisterUser(request);
            return Ok(new ApiResponse { Message = "User registered successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while registering the user");
            return BadRequest(new ApiResponse { Message = ex.Message });
        }
    }
}
