namespace sloth.Domain.Entities;
public class JobAncestryLinkHistory
{
    public int ParentJobID { get; set; } = default;
    public int ChildJobID { get; set; } = default;
    public DateTime ChangeDate { get; set; } = default!;
    public Guid ChangedByID { get; set; } = default!;
    public string Action { get; set; } = default!;

    public User ChangedBy { get; set; } = default!;
    public Job ParentJob { get; set; } = default!;
    public Job ChildJob { get; set; } = default!;
}
