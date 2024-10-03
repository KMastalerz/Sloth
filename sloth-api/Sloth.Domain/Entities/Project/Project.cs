namespace Sloth.Domain.Entities;
/// <summary>
/// This table holds all projects in Sloth, project are the entities that are applications supported by Sloth.
/// </summary>
public class Project
{
    public Guid ProjectID { get; set; } 
    public string ProjectName { get; set; } = default!;
    public string ProjectType { get; set; } = default!;
    public string? ProjectImageUrl { get; set; }
    public string ShortDescription { get; set; } = default!;
    public string Description { get; set; } = default!;
}
