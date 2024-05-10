﻿namespace Models;

public partial class Laptop
{
    public int ModelId { get; set; }

    public int? Speed { get; set; }

    public int? Ram { get; set; }

    public int? HardDisk { get; set; }

    public decimal? Screen { get; set; }

    public int? Price { get; set; }

    public virtual Product Model { get; set; } = null!;
}
