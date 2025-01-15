namespace sloth.Domain.Models.Jobs;
public class ListBugFilters
{
    public int PageID { get; set; } = 0;
    public int TakeCount { get; set; } = 50;
    public string? CurrentOwner { get; set; } = null;
    public string? Header { get; set; } = null;
    public string? Description { get; set; } = null;
    public bool? IsClosed { get; set; } = null;
    public int? BugID { get; set; } = null;
    public string InquiryNumber { get; set; } = default!;
    public DateTime? CreatedDateStart { get; set; } = null;
    public DateTime? CreatedDateEnd { get; set; } = null;
    public DateTime? UpdatedDateStart { get; set; } = null;
    public DateTime? UpdatedDateEnd { get; set; } = null;
    public DateTime? ClosedDateStart { get; set; } = null;
    public DateTime? ClosedDateEnd { get; set; } = null;
    public DateTime? RaisedDateStart { get; set; } = null;
    public DateTime? RaisedDateEnd { get; set; } = null;
    public List<int>? Products { get; set; } = null;
    public List<int>? Functionalities { get; set; } = null;
    public List<Guid>? Clients { get; set; } = null;
}
