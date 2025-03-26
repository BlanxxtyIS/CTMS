using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TaskManagment.Applications.Services;
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
    public IActionResult Login([FromBody] LogRequest request)
    {
        var user = _context.Users.FirstOrDefault(u =>
            u.Email == request.Email || u.PasswordHash == request.Password);

        if (user == null)
        {
            return Unauthorized();
        }

        var token = _tokenService.GenerateToken(user.Username, new[] { "User" });
        return Ok(new { token });
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
