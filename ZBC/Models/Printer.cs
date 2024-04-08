using System;
using System.Collections.Generic;

namespace ZBC.Models;

public partial class Printer
{
    public int ModelId { get; set; }

    public string? Color { get; set; }

    public string? PrinterType { get; set; }

    public int? Price { get; set; }

    public virtual Product Model { get; set; } = null!;
}
