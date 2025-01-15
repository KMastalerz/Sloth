using System.Diagnostics;

namespace sloth.Infrastructure.Seed.Models;
public class BugSeed
{
    public string InquiryNumber { get; set; } = default!;
    public DateTime RaisedDate { get; set; } = default!;
    public string Header { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int PriorityID { get; set; }
    public DateTime CreatedDate { get; set; } = default!;
    public string ClientAlias { get; set; } = default!;
    public int StatusID { get; set; }
    public List<string> Products { get; set; } = [];
    public string Type { get; set; } = default!;
    public string CreatedByAlias { get; set; } = default!;
}
