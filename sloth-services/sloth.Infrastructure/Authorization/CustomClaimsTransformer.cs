using Microsoft.AspNetCore.Authentication;
using sloth.Domain.Repositories;
using sloth.Utilities.Constants;
using System.Security.Claims;

namespace sloth.Infrastructure.Authorization;
public class CustomClaimsTransformer(IAuthRepository authRepository) : IClaimsTransformation
{
    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal.Identity?.IsAuthenticated != true)
        {
            return principal;
        }

        var identity = (ClaimsIdentity)principal.Identity;

        // Avoid adding duplicate team claims
        if (identity.HasClaim(c => c.Type == SlothClaimTypes.Teams))
        {
            return principal;
        }

        var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userGuid))
        {
            return principal;
        }

        // Fetch user teams using the TeamService
        var user = await authRepository.GetUserAsync(userGuid);

        foreach (var team in user!.Teams)
        {
            identity.AddClaim(new Claim(SlothClaimTypes.Teams, team.TeamID.ToString()));
        }

        return principal;
    }
}
