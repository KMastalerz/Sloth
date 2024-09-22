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
}