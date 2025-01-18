namespace sloth.Domain.Entities;
public class JobAssignmentHistory
{
    public int JobID { get; set; }
    public Guid PreviousOwnerID { get; set; }
    public Guid CurrentOwnerID { get; set; }
    public Guid TeamID { get; set; } 
    public Guid ChangedByID { get; set; }
    public DateTime ChangedDate { get; set; }

    /// <summary>
    /// External properties
    /// </summary>
    
    public User PreviousOwner { get; set; } = default!;
    public User CurrentOwner { get; set; } = default!;
    public User ChangedBy { get; set; } = default!;
    public Team Team { get; set; } = default!;
}
