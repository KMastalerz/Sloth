namespace sloth.Domain.Entities;
public class Product
{
    public int ProductID { get; set; }
    public string ProductShortName { get; set; } = default!;
    public string? ProductName { get; set; } = null;
    public string ProductDescription { get; set;} = default!;
}
