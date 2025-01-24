namespace sloth.Domain.Entities;
public class JobAncestryLink
{
    public int ParentJobID { get; set; } = default;
    public int ChildJobID { get; set; } = default;
    public DateTime LinkDate { get; set; } = default!;
    public Guid LinkedByID { get; set; } = default!;

    public User LinkedBy { get; set; } = default!;
    public Job ParentJob { get; set; } = default!;
    public Job ChildJob { get; set; } = default!;
}
