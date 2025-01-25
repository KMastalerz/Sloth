namespace sloth.Application.Models.Jobs;
public class ListBugItemResponse
{
    public int TotalCount { get; set; }
    public IEnumerable<ListBugItem> Bugs { get; set; } = [];
}
public class ListBugItem
{
    public int BugID { get; set; }
    public string InquiryNumber { get; set; } = default!;
    public string Header { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int PriorityID { get; set; }
    public ListBugPriorityItem? Priority { get; set; } = null;
    public ListBugStatusItem? Status { get; set; } = null;
    public string Type { get; set; } = default!;
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? ClosedDate { get; set; } = null;
    public bool IsClosed { get; set; } = false;
    public Guid? UpdatedByID { get; set; } = null;
    public string? UpdatedBy { get; set; } = null;
    public Guid? ClosedByID { get; set; } = null;
    public string? ClosedBy { get; set; } = null;
    public Guid? ClientID { get; set; } = null;
    public string? Client { get; set; } = null;
    public bool IsBlocker { get; set; } = false;
    public List<ListBugProductItem>? Products { get; set; } = null;
    public List<ListBugFunctionalityItem>? Functionalities { get; set; } = null;
}

public class ListBugProductItem()
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}

public class ListBugFunctionalityItem()
{
    public string Tag { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string TagColor { get; set; } = default!;
}

public class ListBugPriorityItem()
{
    public string Tag { get; set; } = default!;
    public string TagColor { get; set; } = default!;
}

public class ListBugStatusItem()
{
    public string Tag { get; set; } = default!;
    public string TagColor { get; set; } = default!;
}
