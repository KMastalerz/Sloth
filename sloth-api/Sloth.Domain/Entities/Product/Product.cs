namespace Sloth.Domain.Entities;
/// <summary>
/// This table holds all product in Sloth
/// </summary>
public class Product
{
    public Guid ProductID { get; set; } 
    public string ProductName { get; set; } = default!;
    public string ProductType { get; set; } = default!;
    public string? ProductImageUrl { get; set; }
    public string ShortDescription { get; set; } = default!;
    public string Description { get; set; } = default!;
}
