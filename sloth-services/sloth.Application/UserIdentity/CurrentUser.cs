namespace sloth.Application.UserIdentity;
public record class CurrentUser(string UserID, string Email, string UserName, IEnumerable<string> UserRoles, Guid UserGuid)
{
    public bool IsInRole(string role) => UserRoles.Contains(role);
}
