using sloth.Domain.Entities;

namespace sloth.Domain.Repositories;
public interface IAuthRepository
{
    Task<UserRole?> GetUserRoleByNameAsync(string roleName);
    Task<bool> IsEmailAsync(string email);
    Task<bool> IsUserNameAsync(string userName);
    Task<User> RegisterUserAsync(User user, UserRole userRole);
}