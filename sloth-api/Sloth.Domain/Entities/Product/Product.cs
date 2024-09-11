namespace Sloth.Domain.Entities;
/// <summary>
/// This table holds all product in Sloth
/// </summary>
public class Product
{
    public int ProductID { get; set; } 
    public string ProductName { get; set; } = default!;
    public string? ProductImageUrl { get; set; }
    public string ProductDescription { get; set; } = default!;

}
