namespace sloth.Domain.Entities;
public class JobPriorityHistory
{
    public int JobID { get; set; }
    public string PreviousPriority { get; set; } = default!;
    public string NewPriority { get; set; } = default!;
    public Guid ChangedByID { get; set; }
    public DateTime ChangeDate { get; set; }


    /// <summary>
    /// External properties
    /// </summary>
    public User ChangedBy { get; set; } = default!;
}
