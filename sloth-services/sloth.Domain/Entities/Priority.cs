namespace sloth.Domain.Entities;
public class Priority
{
    public int PriorityID { get; set; }
    public int PriorityLevel { get; set; }
    public string PriorityValue { get; set; } = default!;
    public string? Description { get; set; } = null;
    public string? Class { get; set; } = null;
}
