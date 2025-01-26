namespace sloth.Domain.Entities;
public class JobDetailHistory
{
    public int JobID { get; set; }
    public string Field { get; set; } = default!;
    public string Value { get; set; } = default!; 
    public Guid ChangedByID { get; set; }
    public DateTime ChangedDate { get; set; }

    /// <summary>
    /// External properties
    /// </summary>
    public User ChangedBy { get; set; } = default!;
}
