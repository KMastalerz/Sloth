namespace Sloth.Domain.Entities;
/// <summary>
/// Table represents all changes in job statuses for specific job
/// </summary>
public class JobStatusHistory
{
    /// <summary>
    /// references Job
    /// </summary>
    public int JobID { get; set; }
    /// <summary>
    /// references JobStatus
    /// </summary>
    public int? PreviousStatusID { get; set; }
    /// <summary>
    /// references JobStatus
    /// </summary>
    public int? CurrentStatusID { get; set; }
    public DateTime ChangeDate { get; set; }
}
