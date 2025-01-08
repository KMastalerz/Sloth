namespace sloth.Domain.Entities;
public class Priority
{
    public string Value { get; set; } = default!;
    public string? Display { get; set; } = null;
    public string? Class { get; set; } = null;
}
