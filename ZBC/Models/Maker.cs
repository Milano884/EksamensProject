namespace ZBC.Models;

public partial class Maker
{
    public string MakerId { get; set; } = null!;

    public string? MakerColor { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
