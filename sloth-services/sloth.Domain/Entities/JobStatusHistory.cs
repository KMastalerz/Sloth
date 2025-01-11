namespace sloth.Domain.Entities;
public class JobStatusHistory
{
    public int JobID { get; set; }
    public Guid ChangedByID { get; set; }
    public string PreviousStatus { get; set; } = default!;
    public string NewStatus { get; set; } = default!;
    public DateTime ChangeDate { get; set; }


    /// <summary>
    /// External properties
    /// </summary>

    public User ChangedBy { get; set; } = default!;
}