namespace sloth.Domain.Entities;
public class ProductFunctionality
{
    public int FunctionalityID { get; set; }
    public int ProductID { get; set; }
    public string Name { get; set; } = default!;
    public string Tag { get; set; } = default!;
    public string? TagColor { get; set; } = default;
    public string Description { get; set; } = default!;

    public Product? Product { get; set; } = null;
}
