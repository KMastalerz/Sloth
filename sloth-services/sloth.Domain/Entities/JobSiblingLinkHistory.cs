namespace sloth.Domain.Entities;
public class JobSiblingLinkHistory
{
    public int PrimaryJobID { get; set; } = default;
    public int SecondaryJobID { get; set; } = default;
    public DateTime ChangeDate { get; set; }
    public Guid ChangedByID { get; set; }
    public string Action { get; set; } = default!;

    public User ChangedBy { get; set; } = default!;
    public Job PrimaryJob { get; set; } = default!;
    public Job SecondaryJob { get; set;} = default!;
}
