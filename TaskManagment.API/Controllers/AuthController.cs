using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
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

    [HttpGet("testAuth")]
    public IActionResult TestAuth()
    {
        return Ok(new
        {
            User.Identity.IsAuthenticated,
            User.Identity.Name,
            Claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList()
        });
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LogRequest request)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);
        
        if (user == null)
        {
            return Unauthorized(new {message = "Неверный логин или пароль"});
        }

        var passwordHasher = new PasswordHasher<User>();
        var passwordVerification = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

        if (passwordVerification == PasswordVerificationResult.Failed)
        {
            return Unauthorized(new { message = "Неверный логин или пароль" });
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Username.ToString())
        };

        if (user.UserRoles != null && user.UserRoles.Any())
        {
            foreach (var userRole in user.UserRoles)
            {
                if (userRole.Role != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
                }
            }
        }

        var accessToken = _tokenService.GenerateAccessToken(claims);
        var refreshToken = _tokenService.GenerateRefreshToken();

        var existingTokens = _context.RefreshTokens.Where(rt => rt.UserId == user.Id).ToList();
        _context.RefreshTokens.RemoveRange(existingTokens);

        var newRefreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = refreshToken,
            UserId = user.Id,
            Expires = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };

        _context.RefreshTokens.Add(newRefreshToken);
        _context.SaveChanges();

        Response.Cookies.Append("accessToken", accessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddMinutes(15)
        });

        return Ok(new
        {
            accessToken = accessToken,
            refreshToken = newRefreshToken.Token
        });
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegRequest request)
    {
        if (await _context.Users.AnyAsync(u => u.Username == request.Username))
        {
            return BadRequest(new
            {
                message = "Пользователь с такой почтой уже зарегистрирован"
            });
        }

        var user = new User();

        var hashedPassword = new PasswordHasher<User>()
            .HashPassword(user, request.Password);

        var newUser = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = hashedPassword,
            IsActive = true,
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return Ok(new { 
            message = "Регистрация прошла успешно" 
        });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
    {
        var refreshToken = _context.RefreshTokens
            .FirstOrDefault(rt => rt.Token == request.RefreshToken && !rt.IsRevoked);

        if (refreshToken == null || refreshToken.Expires < DateTime.UtcNow)
        {
            return Unauthorized(new { message = "Недействительный refresh-токен" });
        }

        var user = _context.Users.FirstOrDefault(u => u.Id == refreshToken.UserId);
        if (user == null)
        {
            return Unauthorized(new { message = "Пользователь не найден" });
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, "User")
        };

        var newAccesToken = _tokenService.GenerateAccessToken(claims);

        Response.Cookies.Append("accessToken", newAccesToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddMinutes(15)
        });

        return Ok();
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
        if (Request.Cookies.ContainsKey("accessToken"))
        {
            Response.Cookies.Delete("accessToken");
        }

        return Ok(new { message = "Вы успешно вышли из системы" });
    }
}
