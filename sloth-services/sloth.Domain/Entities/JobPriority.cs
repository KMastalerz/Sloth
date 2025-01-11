namespace sloth.Domain.Entities;
public class JobPriority
{
    public int PriorityLevel { get; set; }
    public string Priority { get; set; } = default!;
    public string? Description { get; set; } = null;
    public string? Class { get; set; } = null;
}
