namespace TaskManagement.Application.Interfaces.Services;

public interface IJwtService
{
    string GenerateToken(string username, IEnumerable<string> roles);
}