namespace sloth.Domain.Entities;
public class Priority
{
    public int PriorityID { get; set; }
    public int PriorityLevel { get; set; }
    public string Tag { get; set; } = default!;
    public string? TagColor { get; set; } = default;
    public string? Description { get; set; } = null;
}
