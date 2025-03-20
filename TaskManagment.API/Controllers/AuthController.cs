using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Interfaces.Services;
using TaskManagment.Core.Entities;
using TaskManagment.Core.Entities.Auth;
using TaskManagment.Infrastructure.Data;

namespace TaskManagment.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IJwtService _tokenService;
    private readonly ApplicationDbContext _context;

    public AuthController(IJwtService tokenService, ApplicationDbContext context)
    {
        _tokenService = tokenService;
        _context = context;
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

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegRequest request)
    {
        var newUser = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = request.Password,
            IsActive = true,
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Регистрация прошла успешно" });
    }
}
