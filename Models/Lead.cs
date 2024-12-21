using System;
using System.Collections.Generic;

namespace ERP.Models;

public partial class Lead
{
    public int LeadId { get; set; }

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }
}
