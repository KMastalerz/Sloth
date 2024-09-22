using Microsoft.EntityFrameworkCore;
using Sloth.Domain.Entities;
using Sloth.Domain.Repositories;
using Sloth.Infrastructure.DatabaseContext;

namespace Sloth.Infrastructure.Repositories;
internal class SecurityRepository(SlothDbContext dbContext) : ISecurityRepository
{
    public async Task<bool> ValidateEmailAsync(string email)
    {
        return await dbContext.User.Where(u => u.Email == email || u.UserName == email).AnyAsync();
    }

    public async Task<bool> ValidateUserNameAsync(string userName)
    {
        return await dbContext.User.Where(u => u.UserName == userName || u.Email == userName).AnyAsync();
    }

    public async Task<User?> GetUserAsync(string login)
    {
        var user = await dbContext.User.SingleOrDefaultAsync(u => u.UserName == login || u.Email == login);

        if(user is null)
            return null;

        var userRole = await dbContext.UserRole.SingleOrDefaultAsync(r => r.RoleID == user!.RoleID);
        var userGroup = await dbContext.UserGroup.SingleOrDefaultAsync(g => g.GroupID == user!.GroupID);

        user.Role = userRole;
        user.Group = userGroup;

        return user;
    }

    public async Task<UserRole?> GetUserRoleAsync(Guid roleID)
    {
        return await dbContext.UserRole.SingleOrDefaultAsync(r => r.RoleID == roleID);
    }

    public async Task<UserGroup?> GetUserGroupAsync(Guid groupID)
    {
        return await dbContext.UserGroup.SingleOrDefaultAsync(g => g.GroupID == groupID);
    }

    public async Task AddUserAsync(User user)
    {
        await dbContext.User.AddAsync(user);
        await dbContext.SaveChangesAsync();
    }
}
