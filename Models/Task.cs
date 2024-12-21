using System;
using System.Collections.Generic;

namespace ERP.Models;

public partial class Task
{
    public int TaskId { get; set; }

    public int? EmployeeId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? Deadline { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Employee? Employee { get; set; }
}
