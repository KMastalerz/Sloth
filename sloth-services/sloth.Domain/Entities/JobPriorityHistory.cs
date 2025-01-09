namespace sloth.Domain.Entities;
public class JobPriorityHistory
{
    public int JobID { get; set; } // FK
    public int PreviousPriorityLevel { get; set; } // FK
    public int NewPriorityLevel { get; set; } // FK
    public Guid ChangedByID { get; set; } // FK
    public DateTime ChangeDate { get; set; }


    /// <summary>
    /// external property
    /// </summary>

    // FK: PreviousPriorityLevel
    public JobPriority PreviousPriority { get; set; } = default!;
    // FK: NewPriorityLevel
    public JobPriority NewPriority { get; set; } = default!;
    // FK: ChangeDate
    public User ChangedBy { get; set; } = default!;
}
