namespace sloth.Domain.Entities;
public class JobAssignmentHistory
{
    public int JobID { get; set; }
    public Guid UserID { get; set; }
    public DateTime ChangedDate { get; set; }
    public Guid ChangedByID { get; set; }
    public string Action { get; set; } = default!;

    /// <summary>
    /// External properties
    /// </summary>

    public User User { get; set; } = default!;
    public User ChangedBy { get; set; } = default!;
}
