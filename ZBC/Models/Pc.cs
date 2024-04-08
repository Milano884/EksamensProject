using System;
using System.Collections.Generic;

namespace ZBC.Models;

public partial class Pc
{
    public int ModelId { get; set; }

    public int? Speed { get; set; }

    public int? Ram { get; set; }

    public int? HardDisk { get; set; }

    public string? ReadDrive { get; set; }

    public int? Price { get; set; }

    public virtual Product Model { get; set; } = null!;
}
