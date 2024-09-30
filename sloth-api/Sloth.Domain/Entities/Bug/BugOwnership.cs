namespace Sloth.Domain.Entities;
public class BugOwnership
{
    public int BugID { get; set; }
    public Guid OwnerID { get; set; }
    public DateTime DateAssigned { get; set; }
    public DateTime? DateUnassigned { get; set; }
    public Guid AssignedBy { get; set; }
    public Guid? UnassignedBy { get; set; }
}
