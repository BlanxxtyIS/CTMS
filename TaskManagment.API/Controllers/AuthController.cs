using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Interfaces.Services;
using TaskManagment.Infrastructure.Authentication;

namespace TaskManagment.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IJwtService _tokenService;

    public AuthController(IJwtService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")] 
    public IActionResult Login([FromBody] string request)
    {
        if (request == "test")
        {
            var token = _tokenService.GenerateToken(request, new[] { "User" });
            return Ok(new { token });
        }

        return Unauthorized();
    }
}
