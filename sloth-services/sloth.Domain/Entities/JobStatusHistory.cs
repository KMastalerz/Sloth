namespace sloth.Domain.Entities;
public class JobStatusHistory
{
    public int JobID { get; set; }
    public int? StatusID { get; set; } = null;
    public DateTime ChangedDate { get; set; }
    public Guid ChangedByID { get; set; }
    public string Action { get; set; } = default!;


    /// <summary>
    /// External properties
    /// </summary>

    public User ChangedBy { get; set; } = default!;
    public Status? Status { get; set; } = null;
}