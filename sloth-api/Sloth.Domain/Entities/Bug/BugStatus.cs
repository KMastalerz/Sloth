namespace Sloth.Domain.Entities;
public class BugStatus
{
    public int BugID { get; set; }
    public string Status { get; set; } = default!;
    public string StatusDate { get; set; } = default!;
    public Guid StatusSetBy { get; set; } = default!;
    public Guid StatusTeam { get; set; } = default!;
}
