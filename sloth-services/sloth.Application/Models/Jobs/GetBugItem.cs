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
    public string PreviousOwner { get; set; } = default!;
    public string PreviousOwnerFullName { get; set; } = default!;
    public string PreviousOwnerEmail { get; set; } = default!;
    public string CurrentOwner { get; set; } = default!;
    public string CurrentOwnerFullName { get; set; } = default!;
    public string CurrentOwnerEmail { get; set; } = default!;
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
    public int NewPriorityID { get; set; }
    public string NewPriorityTag { get; set; } = default!;
    public string? NewPriorityTagColor { get; set; } = default!;
    public string? NewPriorityDescription { get; set; } = default!;
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
    public int NewStatusID { get; set; } = default!;
    public string NewStatusTag { get; set; } = default!;
    public string? NewStatusTagColor { get; set; } = default!;
    public string? NewStatusDescription { get; set; } = default!;
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
