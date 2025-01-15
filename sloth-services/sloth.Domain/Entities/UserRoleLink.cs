namespace sloth.Domain.Entities;
public class UserRoleLink
{
    public Guid UserID { get; set; } = default!;
    public Guid RoleID { get; set; } = default!;
}
