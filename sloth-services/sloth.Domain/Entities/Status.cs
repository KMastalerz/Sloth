namespace sloth.Domain.Entities;
public class Status
{
    public int StatusID {  get; set; }
    public string Type { get; set; } = default!;
    public string Tag { get; set; } = default!;
    public string? TagColor { get; set; } = null;
    public string? Description { get; set; } = null;
    public bool OwnerChange { get; set; } = true;
    public bool EndState { get; set; } = false;
    public bool IsInitial { get; set; } = false;
}
