namespace Sloth.Domain.Entities;
public class BugPriority
{
    public int BugID { get; set; }
    public string Priority { get; set; } = default!;
    public DateTime PriorityDate { get; set; } = default!;
    public Guid PrioritySetBy { get; set; } = default!;
}
