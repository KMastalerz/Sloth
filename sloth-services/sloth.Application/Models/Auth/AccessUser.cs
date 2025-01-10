namespace sloth.Application.Models.Auth;
public class AccessUser
{
    public Guid UserID { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public List<AccessRole> UserRoles { get; set; } = [];
}