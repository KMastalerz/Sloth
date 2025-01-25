namespace sloth.Application.Models.Jobs;
public class GetBugItem
{
    public int JobID { get; set; }
    public string Header { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Type { get; set; } = default!;
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? ClosedDate { get; set; } = null;
    public bool IsClosed { get; set; } = false;
    public bool IsBlocker { get; set; } = false;
    public GetUserBugItem? CreatedBy { get; set; } = null;
    public GetUserBugItem? ClosedBy { get; set; } = null;
    public GetClientBugItem? Client { get; set; } = null;
    public GetUserBugItem? UpdatedBy { get; set; } = null;
    public GetPriorityBugItem? Priority { get; set; } = null;
    public GetStatusBugItem? Status { get; set; } = null;
    public List<GetCommentBugItem> Comments { get; set; } = [];
    public List<GetAssignmentHistoryBugItem> AssignmentHistory { get; set; } = [];
    public List<GetAssignmentBugItem> Assignments { get; set; } = [];
    public List<GetFileBugItem> Files { get; set; } = [];
    public List<GetPriorityHistoryBugItem> PriorityHistory { get; set; } = [];
    public List<GetStatusHistoryBugItem> StatusHistory { get; set; } = [];
    public List<GetProductBugItem> Products { get; set; } = [];
    public List<GetFunctionalityBugItem> Functionalities { get; set; } = [];
    public List<GetChildJobLinkBugItem> ChildJobs { get; set; } = [];
    public List<GetParentJobLinkBugItem> ParentJobs { get; set; } = [];
    public List<GetParentJobLinkHistoryBugItem> ParentJobHistory { get; set; } = [];
    public List<GetChildJobLinkHistoryBugItem> ChildJobHistory { get; set; } = [];
}
public class GetAssignmentBugItem
{
    public DateTime AssignedDate { get; set; }
    public string AssignedTo { get; set; } = default!;
    public string AssignedToFullName { get; set; } = default!;
    public string AssignedToEmail { get; set; } = default!;
    public string AssignedBy { get; set; } = default!;
    public string AssignedByFullName { get; set; } = default!;
    public string AssignedByEmail { get; set; } = default!;
}
public class GetAssignmentHistoryBugItem
{
    public DateTime ChangedDate { get; set; }
    public string UserName { get; set; } = default!;
    public string UserFullName { get; set; } = default!;
    public string UserEmail { get; set; } = default!;
    public string Action { get; set; } = default!;
    public string ChangedBy { get; set; } = default!;
    public string ChangedByFullName { get; set; } = default!;
    public string ChangedByEmail { get; set; } = default!;
}
public class GetClientBugItem
{
    public Guid ClientID { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Alias { get; set; } = default!;
}
public class GetCommentBugItem
{
    public int CommentID { get; set; }
    public string Comment { get; set; } = default!;
    public DateTime CommentDate { get; set; } = default!;
    public bool IsEdited { get; set; } = false;
    public string CommentedBy { get; set; } = default!;
    public string CommentedByEmail { get; set; } = default!;
    public string CommentedByFullName { get; set; } = default!;
}
public class GetFileBugItem
{
    public Guid FileID { get; set; }
    public string Name { get; set; } = default!;
    public long Size { get; set; }
    public string Extension { get; set; } = default!;
    public DateTime AddedDate { get; set; }
    public string AddedBy { get; set; } = default!;
    public string AddedByEmail { get; set; } = default!;
    public string AddedByFullName { get; set; } = default!;
}
public class GetFunctionalityBugItem
{
    public int FunctionalityID { get; set; }
    public string Name { get; set; } = default!;
    public string Tag { get; set; } = default!;
    public string? TagColor { get; set; } = default;
    public string Description { get; set; } = default!;
}
public class GetPriorityBugItem
{
    public int PriorityID { get; set; }
    public string Tag { get; set; } = default!;
    public string? TagColor { get; set; } = default;
    public string? Description { get; set; } = null;
}
public class GetPriorityHistoryBugItem
{
    public DateTime ChangedDate { get; set; }
    public string ChangedBy { get; set; } = default!;
    public string ChangedByEmail { get; set; } = default!;
    public string ChangedByFullName { get; set; } = default!;
    public int? NewPriorityID { get; set; } = null;
    public string? NewPriorityTag { get; set; } = null;
    public string? NewPriorityTagColor { get; set; } = null;
    public string? NewPriorityDescription { get; set; } = null;
}
public class GetProductBugItem
{
    public int ProductID { get; set; }
    public string Alias { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; } = null;
}
public class GetStatusBugItem
{
    public int StatusID { get; set; }
    public string Tag { get; set; } = default!;
    public string? TagColor { get; set; } = default;
    public string? Description { get; set; } = null;
}
public class GetStatusHistoryBugItem
{
    public DateTime ChangedDate { get; set; }
    public string ChangedBy { get; set; } = default!;
    public string ChangedByEmail { get; set; } = default!;
    public string ChangedByFullName { get; set; } = default!;
    public int? NewStatusID { get; set; } = null;
    public string? NewStatusTag { get; set; } = null;
    public string? NewStatusTagColor { get; set; } = null;
    public string? NewStatusDescription { get; set; } = null;
}
public class GetTeamBugItem
{
    public string Alias { get; set; } = default!;
    public string Speciality { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}
public class GetUserBugItem
{
    public string UserName { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
}
public class GetJobLinkBugItem
{
    public int JobID { get; set; } = default;
    public string JobHeader { get; set; } = default!;
    public string JobDescription { get; set; } = default!;
    public DateTime LinkDate { get; set; } = default!;
    public string LinkedBy { get; set; } = default!;
    public string LinkedByFullName { get; set; } = default!;
    public string LinkedByEmail { get; set; } = default!;
}
public class GetParentJobLinkBugItem : GetJobLinkBugItem;
public class GetChildJobLinkBugItem : GetJobLinkBugItem;
public class GetJobLinkHistoryBugItem
{
    public int JobID { get; set; } = default;
    public string JobHeader { get; set; } = default!;
    public string JobDescription { get; set; } = default!;
    public DateTime ChangeDate { get; set; } = default!;
    public string Action { get; set; } = default!;
    public string ChangedBy { get; set; } = default!;
    public string ChangedByFullName { get; set; } = default!;
    public string ChangedByEmail { get; set; } = default!;
}
public class GetParentJobLinkHistoryBugItem : GetJobLinkHistoryBugItem;
public class GetChildJobLinkHistoryBugItem : GetJobLinkHistoryBugItem;