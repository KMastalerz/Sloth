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
    public GetUserBugItem? CurrentOwner { get; set; } = null;
    public GetTeamBugItem? CurrentTeam { get; set; } = null;
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
    public GetUserBugItem User { get; set; } = default!;
    public GetTeamBugItem Team { get; set; } = default!;
    public GetUserBugItem AssignedBy { get; set; } = default!;
}
public class GetAssignmentHistoryBugItem
{
    public DateTime ChangedDate { get; set; }
    public GetUserBugItem PreviousOwner { get; set; } = default!;
    public GetUserBugItem CurrentOwner { get; set; } = default!;
    public GetUserBugItem ChangedBy { get; set; } = default!;
    public GetTeamBugItem Team { get; set; } = default!;
}
public class GetClientBugItem
{
    public string Name { get; set; } = default!;
    public string Alias { get; set; } = default!;
}
public class GetCommentBugItem
{
    public int CommentID { get; set; }
    public string Comment { get; set; } = default!;
    public DateTime CommendDate { get; set; } = default!;
    public bool IsEdited { get; set; } = false;
    public GetUserBugItem CommentedBy { get; set; } = default!;
    public List<GetCommentBugItem>? PreviousEdits { get; set; } = null;
}
public class GetFileBugItem
{
    public Guid FileID { get; set; }
    public string Name { get; set; } = default!;
    public long Size { get; set; }
    public string Extension { get; set; } = default!;
    public DateTime AddedDate { get; set; }
    public GetUserBugItem AddedBy { get; set; } = default!;
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
    public string Tag { get; set; } = default!;
    public string? TagColor { get; set; } = default;
    public string? Description { get; set; } = null;
}
public class GetPriorityHistoryBugItem
{
    public DateTime ChangedDate { get; set; }
    public GetUserBugItem ChangedBy { get; set; } = default!;
    public GetPriorityBugItem NewPriority { get; set; } = default!;
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
    public string Tag { get; set; } = default!;
    public string? TagColor { get; set; } = default;
    public string? Description { get; set; } = null;
}
public class GetStatusHistoryBugItem
{
    public DateTime ChangedDate { get; set; }
    public GetUserBugItem ChangedBy { get; set; } = default!;
    public GetStatusBugItem NewStatus { get; set; } = default!;
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
