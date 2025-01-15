namespace sloth.Domain.Entities;
public class JobPriorityHistory
{
    public int JobID { get; set; }
    public int PreviousPriorityID { get; set; } = default!;
    public int NewPriorityID { get; set; } = default!;
    public Guid ChangedByID { get; set; }
    public DateTime ChangedDate { get; set; }


    /// <summary>
    /// External properties
    /// </summary>
    public User? ChangedBy { get; set; } = null;
    public Priority? PreviousPriority { get; set; } = null;
    public Priority? NewPriority { get; set; } = null;
}
