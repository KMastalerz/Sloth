namespace Sloth.Domain.Entities;
public class UserGroup
{
    public string UserGroupID { get; set; } = default!;
    /// <summary>
    /// references UserRole
    /// </summary>
    public string UserRoleID { get; set; } = default!;
    public string Description { get; set; } = default!;
}
