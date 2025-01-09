namespace sloth.Domain.Entities;
public class JobAssignmentHistory
{
    public int JobID { get; set; } // FK
    public Guid PreviousOwnerID { get; set; } // FK
    public Guid CurrentOwnerID { get; set; } // FK
    public Guid TeamID { get; set; } // FK
    public Guid ChangedByID { get; set; } // FK
    public DateTime ChangeDate { get; set; }

    /// <summary>
    /// external property
    /// </summary>
    
    // FK: PreviousOwnerID
    public User PreviousOwner { get; set; } = default!;
    // FK: CurrentOwnerID
    public User CurrentOwner { get; set; } = default!;
    // FK: ChangedByID
    public User ChengedBy { get; set; } = default!;
    // FK: TeamID
    public Team Team { get; set; } = default!;
}
