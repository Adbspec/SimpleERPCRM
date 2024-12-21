﻿using System;
using System.Collections.Generic;

namespace ERP.Models;

public partial class Sale
{
    public int SaleId { get; set; }

    public int? CustomerId { get; set; }

    public int? ProductId { get; set; }

    public int QuantitySold { get; set; }

    public DateTime? SaleDate { get; set; }

    public decimal TotalPrice { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Product? Product { get; set; }
}