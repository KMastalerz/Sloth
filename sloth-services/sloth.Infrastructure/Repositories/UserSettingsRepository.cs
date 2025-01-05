using Microsoft.EntityFrameworkCore;
using sloth.Domain.Entities;
using sloth.Domain.Repositories;
using sloth.Infrastructure.DatabaseContext;

namespace sloth.Infrastructure.Repositories;
internal class UserSettingsRepository(SlothDbContext dbContext) : IUserSettingsRepository
{
    public async Task<List<UserRole>> GetUserRolesAsync()
    {
        return await dbContext.UserRole.ToListAsync();
    }
    public async Task UpdateUserInfoAsync(User user)
    {
        dbContext.User.Update(user);
        await dbContext.SaveChangesAsync();
    }
    public async Task UpdateUserPasswordAsync(User user)
    {
        dbContext.User.Update(user);
        await dbContext.SaveChangesAsync();
    }
}
