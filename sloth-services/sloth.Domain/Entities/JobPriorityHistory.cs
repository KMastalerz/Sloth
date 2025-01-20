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
    public User ChangedBy { get; set; } = default!;
    public Priority PreviousPriority { get; set; } = default!;
    public Priority NewPriority { get; set; } = default!;
}
