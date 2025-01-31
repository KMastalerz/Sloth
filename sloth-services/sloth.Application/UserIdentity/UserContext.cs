﻿using Microsoft.AspNetCore.Http;
using sloth.Utilities.Constants;
using System.Security.Claims;

namespace sloth.Application.UserIdentity;
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
        var firstName = user.FindFirst(c => c.Type == SlothClaimTypes.FirstName)!.Value;
        var lastName = user.FindFirst(c => c.Type == SlothClaimTypes.LastName)!.Value;
        var userName = user.FindFirst(c => c.Type == ClaimTypes.Name)!.Value;
        var userRoles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
        var userTeamsIDs = user.Claims.Where(c => c.Type == SlothClaimTypes.Teams).Select(c => c.Value);
        var userGuid = Guid.Empty;

        if (Guid.TryParse(userID, out Guid guid))
        {
            userGuid = guid;
        }

        var userTeams = new List<Guid>();
        foreach (var item in userTeamsIDs)
        {
            if (Guid.TryParse(item, out Guid teamGuid))
            {
                 userTeams.Add(teamGuid);
            }
        }

        return new CurrentUser(userID, email, firstName, lastName, userName, userRoles, userGuid, userTeams);
    }
}
