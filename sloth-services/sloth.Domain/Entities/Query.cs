namespace sloth.Domain.Entities;
public class Query: Job
{
    public int QueryID { get; set; }
    public string? InquiryNumber { get; set; } = null;
    public DateTime RaisedDate { get; set; }
}
