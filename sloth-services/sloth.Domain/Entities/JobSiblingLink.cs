namespace sloth.Domain.Entities;
public class JobSiblingLink
{
    public int PrimaryJobID { get; set; } = default;
    public int SecondaryJobID { get; set; } = default;
    public DateTime LinkDate { get; set; } = default!;
    public Guid LinkedByID { get; set; } = default!;

    public User LinkedBy { get; set; } = default!;
    public Job PrimaryJob { get; set; } = default!;
    public Job SecondaryJob { get; set; } = default!;
}