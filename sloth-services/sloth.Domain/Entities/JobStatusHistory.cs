namespace sloth.Domain.Entities;
public class JobStatusHistory
{
    public int JobID { get; set; } // FK
    public Guid ChangedByID { get; set; } // FK
    public int PreviousStatusID { get; set; } = default!;
    public int NewStatusID { get; set; } = default!;
    public DateTime ChangeDate { get; set; }


    /// <summary>
    /// external property
    /// </summary>

    // FK: ChangedByID
    public User ChangedBy { get; set; } = default!;
    // FK: PreviousStatusID
    public JobStatus PreviousStatus { get; set; } = default!;
    // FK: NewStatusID
    public JobStatus NewStatus { get; set;} = default!;
}