using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, "User")
        };

        var token = _tokenService.GenerateAccessToken(claims);
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
