using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webAPI.Models;
using webAPI.Services;

[ApiController]
[Route("api/[controller]")]
public class DietitianController : ControllerBase
{
    private readonly IUserService _userService;

    public DietitianController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDietitian()
    {
        try
        {
            var dietitians = await _userService.GetAllDietitians();
            return Ok(dietitians);
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse { Message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateDietitian(int id, Dietitian dietitian)
    {
        try
        {
            var result = await _userService.UpdateDietitian(id, dietitian);
            if (!result)
                return NotFound(new ApiResponse { Message = "Dietitian not found" });

            return Ok(new ApiResponse { Message = "Dietitian updated successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse { Message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDietitian(int id)
    {
        var result = await _userService.DeleteDietitian(id);
        if (result)
        {
            return NoContent();
        }
        return NotFound();
    }

}
