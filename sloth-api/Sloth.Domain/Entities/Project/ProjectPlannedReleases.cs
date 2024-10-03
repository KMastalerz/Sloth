namespace Sloth.Domain.Entities;
/// <summary>
/// This table represents all planned releases of applications
/// </summary>
public class ProjectPlannedReleases
{
    public int PlannedReleaseID { get; set; }
    /// <summary>
    /// references Project
    /// </summary>
    public Guid ProjectID { get; set; }
    public DateOnly ReleaseDate { get; set; }
}
