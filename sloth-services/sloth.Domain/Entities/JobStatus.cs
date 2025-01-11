namespace sloth.Domain.Entities;
public class JobStatus
{
    public int JobStatusID {  get; set; }
    public string Status { get; set; } = default!;
    public string? Description { get; set; } = null;
    public bool OwnerChange { get; set; } = true;
    public bool EndState { get; set; } = false;
}
