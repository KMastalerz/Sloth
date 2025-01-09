namespace sloth.Domain.Entities;
/// <summary>
/// Job can be a bug, project, task, test or query
/// </summary>
public class Job
{
    public int JobID { get; set; } // PK
    public string JobHeader { get; set; } = default!;
    public string JobDescription { get; set; } = default!;
    public int CurrentJobStatusID { get; set; } = default!; // FK
    public Guid? CurrentOwnerID { get;set; } // FK
    public Guid? CurrentTeamID {  get; set; } // FK
    public string JobType { get; set; } = default!;
    public int PriorityLevel { get; set; } = default!; // FK
    public DateTime CreationDate { get; set; } 
    public DateTime LastModifiedDate { get; set; }
    public DateTime? CloseDate { get; set; } = null;

    /// <summary>
    /// External properties
    /// </summary>

    // FK: CurrentOwnerID
    public User? CurrentOwner { get; set; } = null;
    // FK: CurrentTeamID 
    public Team? CurrentTeam { get; set; } = null;
    // FK: CurrentJobStatusID 
    public JobStatus JobStatus { get; set; } = default!;
    // FK: JobPriority
    public JobPriority JobPriority {  get; set; } = default!; 
    // Taken by PK: JobID
    public List<JobComment> JobComments { get; set; } = [];
    // Taken by PK: JobID
    public List<JobStatusHistory> JobStatusHistory { get; set; } = [];
    // Taken by PK: JobID
    public List<JobAssignmentHistory> JobAssignmentHistory { get; set; } = [];
    // Taken by PK: JobID
    public List<JobPriorityHistory> JobPriorityHistory { get; set; } = [];
    // Taken by link table (JobProductLink): which links Product => ProductID and Job => JobID
    public List<Product> Products { get; set; } = [];
    // Taken by PK: JobID
    public List<JobAssignment> JobAssignments { get; set; } = [];   
}
