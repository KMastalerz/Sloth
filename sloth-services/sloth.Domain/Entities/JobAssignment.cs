namespace sloth.Domain.Entities;
public class JobAssignment
{
    public Guid UserID { get; set; } // FK
    public Guid TeamID { get; set; } // FK
    public int JobID { get; set; } // FK
    public Guid AssignedByID { get; set; } // FK
    public DateTime AssignedDate { get; set; }

    /// <summary>
    /// External properties
    /// </summary>

    // FK: UserID
    public User User { get; set; } = default!;
    // FK: TeamID
    public Team Team { get; set; } = default!;
    // FK: AssignedByID
    public User AssignedBy { get; set; } = default!;
}
