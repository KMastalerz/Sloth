namespace sloth.Application.Models.Jobs;
public class ListBugItem
{
    public int BugID { get; set; }
    public string ClientInquiryNumber { get; set; } = default!;
    public string Header { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int PriorityID { get; set; }
    public string? Priority { get; set; } = null;
    public string? Status { get; set; } = null;
    public string Type { get; set; } = default!;
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? ClosedDate { get; set; } = null;
    public bool IsClosed { get; set; } = false;
    public Guid? CurrentOwnerID { get; set; } = null;
    public string? CurrentOwner { get; set; } = null;
    public Guid? CurrentTeamID { get; set; } = null;
    public string? CurrentTeam { get; set; } = null;
    public Guid? UpdatedByID { get; set; } = null;
    public string? UpdatedBy { get; set; } = null;
    public Guid? ClosedByID { get; set; } = null;
    public string? ClosedBy { get; set; } = null;
    public Guid? ClientID { get; set; } = null;
    public string? Client { get; set; } = null;
    public List<string>? Products { get; set; } = null;
    public List<string>? Functionalities { get; set; } = null;
}
