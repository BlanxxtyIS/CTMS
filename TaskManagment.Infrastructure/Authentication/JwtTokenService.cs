using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TaskManagment.Applications.Services;

namespace TaskManagment.Infrastructure.Authentication;

public class JwtTokenService : IJwtService
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    /// <summary>
    /// Создает JWT на основе переданных claims
    /// </summary>
    /// <param name="claims"></param>
    /// <returns></returns>
    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {

        //Создание JWT-токена
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var credentials = new SigningCredentials
                    (key, SecurityAlgorithms.HmacSha256);

        var jwt = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer, // кто создал токен
            audience: _jwtSettings.Audience, // для кого токен
            claims: claims, // данные пользователя
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenLifetime), // время истечения
            signingCredentials: credentials // подпись создается с помощью серктеного ключа
        ); 

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    /// <summary>
    /// Генерирует случайную стркоу для Refresh Token
    /// </summary>
    /// <returns></returns>
    public string GenerateRefreshToken()
    {
        var randomBytes = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
}
