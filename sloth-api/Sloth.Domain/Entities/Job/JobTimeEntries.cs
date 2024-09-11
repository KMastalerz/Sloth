namespace Sloth.Domain.Entities;
/// <summary>
/// This table holds all entries related to job
/// </summary>
public class JobTimeEntries
{
    public int JobID { get; set; }
    public Guid UserID { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
