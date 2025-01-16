namespace sloth.Domain.Entities;
public class Bug: Job
{
    public int BugID { get; set; }
    public bool IsBlocker { get; set; } = false;
    public string? InquiryNumber { get; set; } = null;
    public DateTime RaisedDate { get; set; }
}
