namespace Sloth.Domain.Entities;
/// <summary>
/// Table represents changes to job priority overtime
/// </summary>
public class JobPriorityHistory
{
    /// <summary>
    /// references Job
    /// </summary>
    public int JobID { get; set; }
    /// <summary>
    /// references JobPriority
    /// </summary>
    public int? PreviousPriorityID { get; set; }
    /// <summary>
    /// references JobPriority
    /// </summary>
    public int? CurrentPriorityID { get; set; }
    public DateTime ChangeDate { get; set; }
}
