namespace sloth.Domain.Cache;

public interface IRoleCache
{
    bool RoleExists(string roleName);
}