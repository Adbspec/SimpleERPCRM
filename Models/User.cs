using System;
using System.Collections.Generic;

namespace ERP.Models;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool PeristantLogin { get; set; }
}
