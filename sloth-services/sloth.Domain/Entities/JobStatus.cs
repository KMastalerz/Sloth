namespace sloth.Domain.Entities;
public class JobStatus
{
    public int JobStatusID {  get; set; } // PK
    public string Status { get; set; } = default!;
    public string? StatusDescription { get; set; } = null;
    public bool OwnerChange { get; set; } = true;
    public bool EndState { get; set; } = false;
}
