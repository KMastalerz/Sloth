namespace sloth.Domain.Entities;
public class Team
{
    public Guid TeamID { get; set; }
    public string TeamName { get; set; } = default!;
    public string TeamDescription { get; set; } = default!;

    /// <summary>
    /// External property
    /// </summary>

    public List<Product> Products { get; set; } = [];
}
