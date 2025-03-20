using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagement.Application.Interfaces.Services;

namespace TaskManagment.Infrastructure.Authentication;

public class JwtTokenService : IJwtService
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public string GenerateToken(string username, IEnumerable<string> roles)
    {
        var claims = new List<Claim> 
        { 
            new Claim(ClaimTypes.Name, username) 
        };

        //Создание JWT-токена
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var credentials = new SigningCredentials
                    (key, SecurityAlgorithms.HmacSha256);

        var jwt = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer, // кто создал токен
            audience: _jwtSettings.Audience, // для кого токен
            claims: claims, // данные пользователя
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes), // время истечения
            signingCredentials: credentials // подпись создается с помощью серктеного ключа
        ); 

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}
