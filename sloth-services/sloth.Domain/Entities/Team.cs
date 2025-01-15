namespace sloth.Domain.Entities;
public class Team
{
    public Guid TeamID { get; set; }
    public string Alias { get; set; } = default!;
    public string Speciality { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    /// <summary>
    /// External property
    /// </summary>

    public List<Product> Products { get; set; } = [];
}
