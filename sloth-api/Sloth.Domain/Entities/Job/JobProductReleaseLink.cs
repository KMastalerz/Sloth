namespace Sloth.Domain.Entities;
/// <summary>
/// This table allows to link job, to specific product versions one to many
/// </summary>
public class JobProductReleaseLink
{
    /// <summary>
    /// references Job
    /// </summary>
    public int JobID { get; set; }
    /// <summary>
    /// references ProductReleases
    /// </summary>
    public int ReleaseID { get; set; }
}
