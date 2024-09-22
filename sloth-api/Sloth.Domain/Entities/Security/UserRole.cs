namespace Sloth.Domain.Entities;
public class UserRole
{
    public Guid RoleID { get; set; } = new Guid();
    public string RoleName { get; set; } = default!;
    public string? Description { get; set; }
}
