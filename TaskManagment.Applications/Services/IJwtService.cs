using System.Security.Claims;

namespace TaskManagment.Applications.Services;

public interface IJwtService
{
    public string GenerateAccessToken(IEnumerable<Claim> claims);
    public string GenerateRefreshToken();
}