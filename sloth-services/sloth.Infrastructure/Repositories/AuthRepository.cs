using Microsoft.EntityFrameworkCore;
using sloth.Domain.Entities;
using sloth.Domain.Repositories;
using sloth.Infrastructure.DatabaseContext;

namespace sloth.Infrastructure.Repositories;
internal class AuthRepository(SlothDbContext dbContext) : IAuthRepository
{
    public async Task<User?> GetUserAsync(string login)
    {
        return await dbContext.User.FirstOrDefaultAsync(u => u.UserName == login || u.Email == login);
    }
    public async Task<User?> GetUserAsync(Guid userID)
    {
        return await dbContext.User.FirstOrDefaultAsync(u => u.UserID == userID);
    }
    public async Task<UserRole?> GetUserRoleAsync(string roleName)
    {
        return await dbContext.UserRole.FirstOrDefaultAsync(r => r.RoleName == roleName);
    }
    public async Task<User> RegisterUserAsync(User user, UserRole userRole)
    {
        await dbContext.User.AddAsync(user);
        await dbContext.UserRoleLink.AddAsync(new()
        {
            UserID = user.UserID,
            RoleID = userRole.RoleID,
            FromDate = DateTime.UtcNow,
            ExpirationDate = null
        });
        await dbContext.SaveChangesAsync();
        return user;
    }
    public async Task UpdateRefreshTokenAsync(string refreshToken, Guid userID, DateTime expirationDate)
    {
        var currentRefreshToken = await dbContext.RefreshToken.FirstOrDefaultAsync(rt => rt.UserID == userID);

        if (currentRefreshToken is not null)
        {
            currentRefreshToken.Token = refreshToken;
            currentRefreshToken.ExpirationDate = expirationDate;
            dbContext.RefreshToken.Update(currentRefreshToken);
        }
        else
        {
            await dbContext.RefreshToken.AddAsync(new()
            {
                Token = refreshToken,
                UserID = userID,
                ExpirationDate = expirationDate
            });
        }

        await dbContext.SaveChangesAsync();
    }
    public void AddUserFailedLoginAttemptsAsync(User user)
    {
        user.FailedLoginAttempts += 1;
        dbContext.User.Update(user);
        dbContext.SaveChanges();
    }
    public void ResetUserFailedLoginAttemptsAsync(User user)
    {
        user.FailedLoginAttempts = 0;
        dbContext.User.Update(user);
        dbContext.SaveChanges();
    }
    public async Task<bool> IsUserLockedAsync(User user)
    {
        return await dbContext.LockedUser.AnyAsync(lu => lu.UserID == user.UserID &&
            lu.ExpirationDate >= DateTime.UtcNow);
    }
    public async Task LockUserAsync(User user, DateTime expirationDate)
    {
        await dbContext.LockedUser.AddAsync(new()
        {
            UserID = user.UserID,
            ExpirationDate = expirationDate
        });
        await dbContext.SaveChangesAsync();
    }
    public async Task UnlockUserAsync(User user)
    {
        var lockedUser = await dbContext.LockedUser.FirstOrDefaultAsync(lu => lu.UserID == user.UserID);

        if (lockedUser is null) return;

        dbContext.LockedUser.Remove(lockedUser);
        await dbContext.SaveChangesAsync();
    }
    public async Task<RefreshToken?> GetRefreshTokenAsync(Guid userID)
    {
        return await dbContext.RefreshToken.FirstOrDefaultAsync(rt => rt.UserID == userID);
    }
}
