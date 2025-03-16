namespace TaskManagment.Core.Interfaces;

public interface ITokenService
{
    string GenerateToken(string username, IEnumerable<string> roles);
}
