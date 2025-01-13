using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webAPI.Models;
using webAPI.Services;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IUserService _userService;

    public ClientController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Authorize(Roles = "Dietitian, Admin")]
    public async Task<IActionResult> CreateClient([FromBody] RegisterRequest request)
    {
        try
        {
            await _userService.RegisterUser(request);
            return Ok(new ApiResponse { Message = "Client created successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse { Message = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllClients()
    {
        try
        {
            var clients = await _userService.GetAllClients();
            return Ok(clients);
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse { Message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Dietitian, Admin")]
    public async Task<IActionResult> UpdateClient(int id, [FromBody] Client client)
    {
        try
        {
            var result = await _userService.UpdateClient(id, client);
            if (!result)
                return NotFound(new ApiResponse { Message = "Client not found" });

            return Ok(new ApiResponse { Message = "Client updated successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse { Message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Dietitian, Admin")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        try
        {
            var result = await _userService.DeleteClient(id);
            if (!result)
                return NotFound(new ApiResponse { Message = "Client not found" });

            return Ok(new ApiResponse { Message = "Client deleted successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse { Message = ex.Message });
        }
    }
}
