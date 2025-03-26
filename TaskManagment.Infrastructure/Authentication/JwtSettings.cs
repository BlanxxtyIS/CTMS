using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TaskManagment.Infrastructure.Authentication;

public class JwtSettings
{
    public string Secret { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int AccessTokenLifetime { get; set; }
    public int RefreshTokenLifetime { get; set; }
}
