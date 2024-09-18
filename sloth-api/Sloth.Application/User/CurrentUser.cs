namespace Sloth.Application.UserIdentity;
public record class CurrentUser(string ID, string Email, string UserName, string? UserGroup, string? UserRole)
{
    public bool IsInRole(string role) => role == UserRole;
    public bool IsInUserGroup(string group) => group == UserGroup;
}
