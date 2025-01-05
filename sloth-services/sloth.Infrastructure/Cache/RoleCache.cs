using sloth.Domain.Cache;
using sloth.Domain.Repositories;

namespace sloth.Infrastructure.Cache;
internal class RoleCache : IRoleCache
{
    private readonly IEnumerable<string> _roles;

    public RoleCache(IUserSettingsRepository userSettingsRepository)
    {
        // Preload roles into memory
        _roles = userSettingsRepository.GetUserRolesAsync().Result.Select(ur => ur.RoleCode);
    }

    public bool RoleExists(string roleName)
    {
        return _roles.Contains(roleName.ToUpper());
    }
}
