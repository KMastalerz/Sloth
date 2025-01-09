namespace sloth.Domain.Entities;
public class Product
{
    public int ProductID { get; set; } // PK
    public string ProductName { get; set; } = default!;
    public string? ProductDisplayName { get; set; } = null;
    public string ProductDescription { get; set;} = default!;
}
