namespace sloth.Domain.Entities;
/// <summary>
/// Job can be a bug, project, task, test or query
/// </summary>
public class Job
{
    public int JobID { get; set; }
    public string Header { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int? PriorityID { get; set; } = null;
    public int? StatusID { get; set; } = null;
    public Guid? ClientID { get; set; } = null;
    public string Type { get; set; } = default!;
    public Guid CreatedByID { get; set; }
    public DateTime CreatedDate { get; set; } 
    public Guid? UpdatedByID { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public Guid? ClosedByID { get; set; }
    public DateTime? ClosedDate { get; set; } = null;
    public bool IsClosed { get; set; } = false;

    /// <summary>
    /// External properties
    /// </summary>
    public User? CreatedBy { get; set; } = null;
    public User? ClosedBy { get; set; } = null;
    public Client? Client { get; set; } = null;
    public User? UpdatedBy { get; set; } = null;
    public Priority? Priority { get; set; } = null;
    public Status? Status { get; set; } = null;

    public ICollection<JobComment> Comments { get; set; } = [];
    public ICollection<JobAssignment> Assignments { get; set; } = [];
    public ICollection<JobFile> Files { get; set; } = [];
    public ICollection<Product> Products { get; set; } = [];
    public ICollection<ProductFunctionality> Functionalities { get; set; } = [];
    public ICollection<JobLink> ParentJobs { get; set; } = [];
    public ICollection<JobLink> ChildJobs { get; set; } = [];
    public ICollection<JobLinkHistory> ChildJobHistory { get; set; } = [];
    public ICollection<JobLinkHistory> ParentJobHistory { get; set; } = [];
    public ICollection<JobAssignmentHistory> AssignmentHistory { get; set; } = [];
    public ICollection<JobClientHistory> ClientHistory { get; set; } = [];
    public ICollection<JobDetailHistory> DetailHistory { get; set; } = [];
    public ICollection<JobFunctionalityHistory> FunctionalityHistory { get; set; } = [];
    public ICollection<JobProductHistory> ProductHistory { get; set; } = [];
    public ICollection<JobPriorityHistory> PriorityHistory { get; set; } = [];
    public ICollection<JobStatusHistory> StatusHistory { get; set; } = [];
}
