using Sloth.Domain.Entities;

namespace Sloth.Domain.Repositories;
public interface ISecurityRepository
{
    Task AddUserAsync(User user);
    Task<bool> ValidateEmailAsync(string email);
    Task<bool> ValidateUserNameAsync(string userName);
    Task<User?> GetUserAsync(string login);
    Task<UserRole?> GetUserRoleAsync(Guid roleID);
    Task<UserGroup?> GetUserGroupAsync(Guid groupID);
    Task AddRefreshTokenAsync(string refreshToken, Guid userID, DateTime expirationDate);
    Task ReplaceRefreshTokenAsync(string refreshToken, Guid userID, DateTime expirationDate);
    Task<RefreshToken?> GetRefreshTokenAsync(Guid userID, string refreshToken);
    Task RemoveRefreshTokenAsync(RefreshToken refreshToken);
    Task<WebPageSecurity?> GetWebPageSecurityAsync(string pageID, string userGroup);
    Task<IEnumerable<SecurityTable>?> ListControlSecurityAsync(string userRole, IEnumerable<string>? securityTables);
}