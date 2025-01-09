namespace sloth.Domain.Entities;
public class Team
{
    public Guid TeamID { get; set; } // PK
    public string TeamName { get; set; } = default!;
    public string TeamDescription { get; set; } = default!;

    /// <summary>
    /// external property
    /// </summary>

    // Taken by link table (TeamProductLink): which links Product => ProductID and Team => TeamID
    public List<Product> Products { get; set; } = [];
}
