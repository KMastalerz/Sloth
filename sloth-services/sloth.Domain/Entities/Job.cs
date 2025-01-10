namespace sloth.Domain.Entities;
/// <summary>
/// Job can be a bug, project, task, test or query
/// </summary>
public class Job
{
    public int JobID { get; set; }
    public string JobHeader { get; set; } = default!;
    public string JobDescription { get; set; } = default!;
    public int CurrentJobStatusID { get; set; } = default!; 
    public Guid? CurrentOwnerID { get;set; }
    public Guid? CurrentTeamID {  get; set; } 
    public string JobType { get; set; } = default!;
    public int PriorityLevel { get; set; } = default!; 
    public DateTime CreationDate { get; set; } 
    public DateTime LastModifiedDate { get; set; }
    public DateTime? CloseDate { get; set; } = null;
    public bool IsClient { get; set; } = false;
    public Guid? ClientID { get; set; }

    /// <summary>
    /// External properties
    /// </summary>

    public User? CurrentOwner { get; set; } = null;
    public Team? CurrentTeam { get; set; } = null;
    public JobStatus JobStatus { get; set; } = default!;
    public JobPriority JobPriority {  get; set; } = default!;
    public Client? Client { get; set; } = null;
    public List<JobComment> JobComments { get; set; } = [];
    public List<JobStatusHistory> JobStatusHistory { get; set; } = [];
    public List<JobAssignmentHistory> JobAssignmentHistory { get; set; } = [];
    public List<JobPriorityHistory> JobPriorityHistory { get; set; } = [];
    public List<Product> Products { get; set; } = [];
    public List<JobAssignment> JobAssignments { get; set; } = [];   

}
