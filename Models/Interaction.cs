using System;
using System.Collections.Generic;

namespace ERP.Models;

public partial class Interaction
{
    public int InteractionId { get; set; }

    public int? CustomerId { get; set; }

    public string Type { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? Timestamp { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Customer? Customer { get; set; }
}
