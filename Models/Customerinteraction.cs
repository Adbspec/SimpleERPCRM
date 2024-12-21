using System;
using System.Collections.Generic;

namespace ERP.Models;

public partial class Customerinteraction
{
    public int InteractionId { get; set; }

    public int CustomerId { get; set; }

    public string InteractionType { get; set; } = null!;

    public string Details { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
