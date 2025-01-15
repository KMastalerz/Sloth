namespace sloth.Domain.Entities;
public class Product
{
    public int ProductID { get; set; }
    public string Alias { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set;} = null;

    /// <summary>
    /// External properties
    /// </summary>
    public List<ProductFunctionality> Functionalities { get; set; } = [];
}
