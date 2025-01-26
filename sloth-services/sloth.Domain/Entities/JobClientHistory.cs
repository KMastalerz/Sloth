namespace sloth.Domain.Entities;
public class JobClientHistory
{
    public int JobID { get; set; }
    public Guid? ClientID { get; set; } = null;
    public Guid ChangedByID { get; set; }
    public DateTime ChangedDate { get; set; }
    public string Action { get; set; } = default!;

    /// <summary>
    /// External properties
    /// </summary>
    public User ChangedBy { get; set; } = default!;
    public Client? Client { get; set; } = null;
}
