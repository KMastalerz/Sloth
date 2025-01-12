namespace sloth.Domain.Entities;
/// <summary>
/// Job can be a bug, project, task, test or query
/// </summary>
public class Job
{
    public int JobID { get; set; }
    public string Header { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int PriorityID { get; set; }
    public int? StatusID { get; set; } = null;
    public bool IsClient { get; set; }
    public Guid? ClientID { get; set; } = null;
    public Guid? CurrentOwnerID { get;set; }
    public Guid? CurrentTeamID {  get; set; } 
    public string Type { get; set; } = default!;
    public Guid CreatedByID { get; set; }
    public DateTime CreatedDate { get; set; } 
    public Guid? LastModifiedByID { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public Guid? ClosedByID { get; set; }
    public DateTime? ClosedDate { get; set; } = null;
    public bool IsClosed { get; set; } = false;

    /// <summary>
    /// External properties
    /// </summary>

    public User? CurrentOwner { get; set; } = null;
    public Team? CurrentTeam { get; set; } = null;
    public User? CreatedBy { get; set; } = null;
    public User? ClosedBy { get; set; } = null;
    public Client? Client { get; set; } = null;
    public User? LastModifiedBy { get; set; } = null;
    public Priority? Priority { get; set; } = null;

    public Status? Status { get; set; } = null;
    public List<JobComment> Comments { get; set; } = [];
    public List<JobAssignmentHistory> AssignmentHistory { get; set; } = [];
    public List<JobAssignment> Assignments { get; set; } = [];
    public List<JobFile> Files { get; set; } = [];
    public List<JobPriorityHistory> PriorityHistory { get; set; } = [];
    public List<JobStatusHistory> StatusHistory { get; set; } = [];
    public List<Product> Products { get; set; } = [];
}
