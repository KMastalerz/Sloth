using sloth.Domain.Entities;

namespace sloth.Domain.Repositories;
public interface IAuthRepository
{
    Task<User?> GetUserAsync(string login);
    Task<User?> GetUserAsync(Guid userID);
    Task<UserRole?> GetUserRoleAsync(string roleName);
    Task<User> RegisterUserAsync(User user, UserRole userRole);
    Task UpdateRefreshTokenAsync(string refreshToken, Guid userID, DateTime expirationDate);
    void AddUserFailedLoginAttemptsAsync(User user);
    void ResetUserFailedLoginAttemptsAsync(User user);
    Task<bool> IsUserLockedAsync(User user);
    Task LockUserAsync(User user, DateTime expirationDate);
    Task UnlockUserAsync(User user);
    Task <RefreshToken?> GetRefreshTokenAsync (Guid userID);
}