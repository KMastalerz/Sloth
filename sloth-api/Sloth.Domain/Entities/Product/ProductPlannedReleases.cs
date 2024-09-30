namespace Sloth.Domain.Entities;
/// <summary>
/// This table represents all planned releases of applications
/// </summary>
public class ProductPlannedReleases
{
    public int PlannedReleaseID { get; set; }
    /// <summary>
    /// references Product
    /// </summary>
    public Guid ProductID { get; set; }
    public DateOnly ReleaseDate { get; set; }
}
