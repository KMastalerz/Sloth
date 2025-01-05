using Microsoft.EntityFrameworkCore;
using sloth.Domain.Entities;
using sloth.Domain.Repositories;
using sloth.Infrastructure.DatabaseContext;

namespace sloth.Infrastructure.Repositories;
internal class AuthRepository(SlothDbContext dbContext) : IAuthRepository
{
    public async Task<bool> IsEmailAsync(string email)
    {
        return await dbContext.User.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> IsUserNameAsync(string userName)
    {
        return await dbContext.User.AnyAsync(u => u.UserName == userName);
    }

    public async Task<UserRole?> GetUserRoleByNameAsync(string roleName)
    {
        return await dbContext.UserRole.FirstOrDefaultAsync(r => r.RoleName == roleName);
    }

    public async Task<User> RegisterUserAsync(User user, UserRole userRole)
    {
        await dbContext.User.AddAsync(user);
        await dbContext.UserRoleLink.AddAsync(new()
        {
            UserID = user.UserID,
            RoleID = userRole.RoleID
        });
        await dbContext.SaveChangesAsync();
        return user;
    }
}
