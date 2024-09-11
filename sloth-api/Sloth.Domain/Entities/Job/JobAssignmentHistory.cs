namespace Sloth.Domain.Entities;
/// <summary>
/// represents history of assignments on Job
/// </summary>
public class JobAssignmentHistory
{
    /// <summary>
    /// references Job
    /// </summary>
    public int JobID { get; set; }
    /// <summary>
    /// references User
    /// </summary>
    public Guid? PreviousOwner {  get; set; }
    /// <summary>
    /// references User
    /// </summary>
    public Guid? CurrentOwner { get; set; }
    public DateTime ChangeDate { get; set; }
}
