namespace Sloth.Application.UserIdentity;
public record class CurrentUser(string ID, string Email, string UserName, IEnumerable<string> Roles)
{
    public bool IsInRole(string role) => Roles.Contains(role);
}
