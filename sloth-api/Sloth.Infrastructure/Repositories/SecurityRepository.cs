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
    public async Task AddRefreshTokenAsync(string refreshToken, Guid userID, DateTime expirationDate)
    {

        await dbContext.RefreshToken.AddAsync(new RefreshToken()
        {
            UserID = userID,
            Token = refreshToken,
            ExpirationDate = expirationDate
        });
        await dbContext.SaveChangesAsync();
    }
    public async Task ReplaceRefreshTokenAsync(string refreshToken, Guid userID, DateTime expirationDate)
    {
        var currentRefreshToken = dbContext.RefreshToken.FirstOrDefault(r => r.UserID == userID);
        if(currentRefreshToken is not null)
        {
            dbContext.RefreshToken.Remove(currentRefreshToken);
            await dbContext.SaveChangesAsync();
        }
        
        await dbContext.RefreshToken.AddAsync(new RefreshToken()
        {
            UserID = userID,
            Token = refreshToken,
            ExpirationDate = expirationDate
        });
        await dbContext.SaveChangesAsync();
    }
    public async Task AddUserAsync(User user)
    {
        await dbContext.User.AddAsync(user);
        await dbContext.SaveChangesAsync();
    }
    public async Task<RefreshToken?> GetRefreshTokenAsync(Guid userID, string refreshToken)
    {
        return await dbContext.RefreshToken.Where(rt =>
            rt.UserID == userID &&
            rt.ExpirationDate > DateTime.Now &&
            rt.Token == refreshToken 
        ).FirstOrDefaultAsync();
    }
    public async Task RemoveRefreshTokenAsync(RefreshToken refreshToken)
    {
        dbContext.RefreshToken.Remove(refreshToken);
        await dbContext.SaveChangesAsync();
    }
    public async Task<WebPageSecurity?> GetWebPageSecurityAsync(string pageID, string userGroup)
    {
        return await dbContext.WebPageSecurity.SingleOrDefaultAsync(wps => wps.PageID == pageID && wps.UserGroup == userGroup);
    }
    public async Task<IEnumerable<SecurityTable>?> ListControlSecurityAsync(string userGroup, IEnumerable<string>? securityTables)
    {
        if (securityTables == null || !securityTables.Any())
        {
            return Enumerable.Empty<SecurityTable>();
        }

        var secTables = await dbContext.SecurityTable.Where(st => st.UserGroup == userGroup && securityTables.Contains(st.SecurityTableID)).ToListAsync();

        return secTables;
    }
}
