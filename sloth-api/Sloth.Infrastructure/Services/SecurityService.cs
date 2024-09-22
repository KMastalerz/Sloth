using Microsoft.IdentityModel.Tokens;
using Sloth.Domain.Constants;
using Sloth.Domain.Entities;
using Sloth.Domain.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sloth.Infrastructure.Services;
internal class SecurityService(IConfigurationService configurationService) : ISecurityService
{
    public string GenerateToken(User user)
    {
        var secConfig = configurationService.SecurityConfig!;

        //TO DO: Add Claims about what team does user belongs to.
        var claims = new List<Claim> {
            new(ClaimTypes.NameIdentifier, user.UserID.ToString()),
            new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role?.RoleName.ToString() ?? ""),
            new(SlothClaimTypes.Group, user.Group?.GroupName.ToString()?? "")
        };

        return TokenGenerator(secConfig.TokenKey, secConfig.TokenLifetime, secConfig.TokenIssuer, claims);
    }

    public string GenerateRefreshToken(User user)
    {
        var secConfig = configurationService.SecurityConfig!;

        return TokenGenerator(secConfig.TokenKey, secConfig.RefreshTokenLifetime, secConfig.TokenIssuer);
    }

    private string TokenGenerator(string tokenKey, int tokenLifetime, string tokenIssuer, List<Claim>? claims = null)

    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddMinutes(tokenLifetime);

        var token = new JwtSecurityToken(
            tokenIssuer,
            tokenIssuer,
            claims,
            expires: expires,
            signingCredentials: cred
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);

    }
}
