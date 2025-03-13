using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TaskManagment.Core.Application.Interfaces;
using TaskManagment.Infrastructure.Authentication;

namespace TaskManagment.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public AuthController(ITokenService tokenService)
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
