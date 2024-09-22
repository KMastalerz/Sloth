using Microsoft.AspNetCore.Http;
using Sloth.Domain.Constants;
using System.Security.Claims;

namespace Sloth.Application.UserIdentity;
public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        var user = httpContextAccessor.HttpContext?.User;
        if (user is null)
        {
            throw new InvalidOperationException("User context is not present");
        }

        if (user.Identity is null || !user.Identity.IsAuthenticated)
        {
            return null;
        }

        var userID = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
        var userName = user.FindFirst(c => c.Type == ClaimTypes.Name)!.Value;
        var userGroup = user.FindFirst(c => c.Type == SlothClaimTypes.Group)?.Value;
        var userRole = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).FirstOrDefault();

        return new CurrentUser(userID, email, userName, userGroup, userRole);
    }
}
