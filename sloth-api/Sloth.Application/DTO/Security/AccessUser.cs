namespace Sloth.Application.DTO.Security;
public class AccessUser
{
    public string UserName { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? PreviousKey { get; set; }
    public string? CurrentKey { get; set; }
    public string UserRole { get; set; } = default!;
    public string UserGroup { get; set; } = default!;
}
