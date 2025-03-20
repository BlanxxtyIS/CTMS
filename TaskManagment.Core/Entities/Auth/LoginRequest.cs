using System.ComponentModel.DataAnnotations;

namespace TaskManagment.Core.Entities.Auth;

public class LoginRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    public bool RemeberMe { get; set; }
}
