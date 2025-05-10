using System.ComponentModel.DataAnnotations;

namespace TaskManagment.Core.Entities.Auth;

public class RegRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Username { get; set; } = string.Empty;
    [Required]
    [StringLength(100, MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;
    [Compare("Password")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
