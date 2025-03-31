﻿namespace TaskManagment.Core.Entities;

public class RefreshToken
{
    public Guid Id { get; set; }
    public string Token { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public DateTime Expires { get; set; }
    public bool IsRevoked { get; set; } = false;
    public User User { get; set; } = null!;
}
