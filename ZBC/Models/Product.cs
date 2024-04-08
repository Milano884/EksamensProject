using System;
using System.Collections.Generic;

namespace ZBC.Models;

public partial class Product
{
    public int ModelId { get; set; }

    public string MakerId { get; set; } = null!;

    public string? ProductType { get; set; }

    public virtual Laptop? Laptop { get; set; }

    public virtual Maker Maker { get; set; } = null!;

    public virtual Pc? Pc { get; set; }

    public virtual Printer? Printer { get; set; }
}
