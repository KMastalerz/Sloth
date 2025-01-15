namespace sloth.Domain.Entities;
public class Bug: Job
{
    public int BugID { get; set; }
    public string InquiryNumber { get; set; } = default!;
    public DateTime RaisedDate { get; set; }
}
