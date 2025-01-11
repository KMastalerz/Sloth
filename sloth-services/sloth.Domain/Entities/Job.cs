namespace sloth.Domain.Entities;
/// <summary>
/// Job can be a bug, project, task, test or query
/// </summary>
public class Job
{
    public int JobID { get; set; }
    public string Header { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string CurrentStatus { get; set; } = default!; 
    public Guid? CurrentOwnerID { get;set; }
    public Guid? CurrentTeamID {  get; set; } 
    public string Type { get; set; } = default!;
    public string Priority { get; set; } = default!;
    public Guid CreatedBy { get; set; }
    public DateTime CreationDate { get; set; } 
    public DateTime? LastModifiedDate { get; set; }
    public DateTime? CloseDate { get; set; } = null;
    public bool IsClient { get; set; } = false;
    public Guid? ClientID { get; set; }

    /// <summary>
    /// External properties
    /// </summary>

    public User? CurrentOwner { get; set; } = null;
    public Team? CurrentTeam { get; set; } = null;
    public Client? Client { get; set; } = null;
    public List<JobComment> Comments { get; set; } = [];
    public List<JobStatusHistory> StatusHistory { get; set; } = [];
    public List<JobAssignmentHistory> AssignmentHistory { get; set; } = [];
    public List<JobPriorityHistory> PriorityHistory { get; set; } = [];
    public List<Product> Products { get; set; } = [];
    public List<JobAssignment> Assignments { get; set; } = [];
    public List<JobFile> Files { get; set; } = [];
}
