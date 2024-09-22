namespace Sloth.Domain.Entities;
public class UserGroup
{
    public Guid GroupID { get; set; } = new Guid();
    public string GroupName { get; set; } = default!;
    public string? Description { get; set; }
}
