namespace sloth.Domain.Entities;
public class UserRole
{
    public Guid RoleID { get; set; } = new Guid();
    public string RoleName { get; set; } = default!;
    public string RoleCode { get; set; } = default!;
    public string? RoleDescription { get; set; }
}
