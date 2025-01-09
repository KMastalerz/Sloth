namespace sloth.Domain.Entities;
public class JobPriority
{
    public int PriorityLevel { get; set; }
    public string Priority { get; set; } = default!;
    public string? PriorityDescription { get; set; } = null;
    public string? PriorityClass { get; set; } = null;
}
