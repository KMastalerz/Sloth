namespace sloth.Domain.Entities;
public class Product
{
    public int ProductID { get; set; }
    public string Alias { get; set; } = default!;
    public string? Name { get; set; } = null;
    public string Description { get; set;} = default!;
}
