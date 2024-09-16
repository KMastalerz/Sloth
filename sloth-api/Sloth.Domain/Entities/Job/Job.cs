namespace Sloth.Domain.Entities;
/// <summary>
/// This table represents all jobs within Sloth: Tasks, Queries, Projects, Enhancements etc...
/// </summary>
public class Job
{
    public int JobID { get; set; }
    /// <summary>
    /// references Producr
    /// </summary>
    public int ProductID { get; set; }
    public string JobHeader { get; set; } = default!;
    public string JobDescription { get; set; } = default!;
    /// <summary>
    /// references JobStatus
    /// </summary>
    public int StatusID { get; set; }
    /// <summary>
    /// references JobPriority
    /// </summary>
    public int PriorityID { get; set; }
    /// <summary>
    /// references JobType
    /// </summary>
    public int JobTypeID { get; set; }
    /// <summary>
    /// references Client, from which job was received
    /// </summary>
    public int? ClientID { get; set; }
    /// <summary>
    /// references Team, from which job was received
    /// </summary>
    public int? ReportingTeamID { get; set; }
    /// <summary>
    /// references Team, from which job is Assigned
    /// </summary>
    public int? AssignedTeamID { get; set; }
    /// <summary>
    /// referenced User, points to person who reported the job!
    /// </summary>
    public Guid? ReportedBy { get; set; }
    /// <summary>
    /// referenced User, points to person who is responsible for job resolution!
    /// </summary>
    public Guid? ResolvedBy { get; set; }
    /// <summary>
    /// referenced User, points to person who tested the job!
    /// </summary>
    public Guid? TestedBy { get; set; }
    /// <summary>
    /// referenced User, points to person who closed the job!
    /// </summary>
    public Guid? ClosedBy { get; set; }
    /// <summary>
    /// referenced User, points to person who currently is assigned to job!
    /// </summary>
    public Guid? CurrentOwner { get; set; }
    public bool IsCompleted { get; set; } = false;
}
