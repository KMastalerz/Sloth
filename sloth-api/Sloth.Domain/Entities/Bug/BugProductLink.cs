namespace Sloth.Domain.Entities;
public class BugProductLink
{
    public int BugID { get; set; }
    public int ProductID { get; set; }
    public DateTime LinkedDate { get; set; }
    public Guid LinkedBy { get; set; }
}
