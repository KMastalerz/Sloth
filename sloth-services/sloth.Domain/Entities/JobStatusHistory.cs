namespace sloth.Domain.Entities;
public class JobStatusHistory
{
    public int JobID { get; set; }
    public Guid ChangedByID { get; set; }
    public int? PreviousStatusID { get; set; } = null;
    public int? NewStatusID { get; set; } = null;
    public DateTime ChangedDate { get; set; }


    /// <summary>
    /// External properties
    /// </summary>

    public User ChangedBy { get; set; } = default!;
    public Status? PreviousStatus { get; set; } = null;
    public Status? NewStatus { get; set; } = null;
}