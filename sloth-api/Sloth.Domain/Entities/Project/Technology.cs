namespace Sloth.Domain.Entities;
public class Technology
{
    public Guid TechnologyID { get; set; } = default!;
    public string TechnologyName { get; set; } = default!;
    public string TechnologyIconUrl { get; set; } = default!;
}
