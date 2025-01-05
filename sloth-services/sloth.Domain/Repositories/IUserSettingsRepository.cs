using sloth.Domain.Entities;

namespace sloth.Domain.Repositories;
public interface IUserSettingsRepository
{
    Task<List<UserRole>> GetUserRolesAsync();
    Task UpdateUserInfoAsync(User user);
    Task UpdateUserPasswordAsync(User user);
}