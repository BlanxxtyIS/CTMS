using System.Security.Claims;

namespace TaskManagment.Applications.Services;

public interface IJwtService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
}