namespace sloth.Domain.Entities;
public class JobPriorityHistory
{
    public int JobID { get; set; }
    public int PreviousPriorityLevel { get; set; } 
    public int NewPriorityLevel { get; set; }
    public Guid ChangedByID { get; set; }
    public DateTime ChangeDate { get; set; }


    /// <summary>
    /// External properties
    /// </summary>

    public JobPriority PreviousPriority { get; set; } = default!;
    public JobPriority NewPriority { get; set; } = default!;
    public User ChangedBy { get; set; } = default!;
}
