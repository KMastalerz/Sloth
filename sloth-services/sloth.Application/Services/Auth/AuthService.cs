using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using sloth.Application.Models.Auth;
using sloth.Application.Models.Configuration;
using sloth.Domain.Entities;
using sloth.Utilities.Constants;

namespace sloth.Application.Services.Auth;
internal class AuthService(IConfiguration configuration) : IAuthService
{
    public AccessTokenResponse GenerateAcesssTokenResponse(User user)
    {
        var tokenConfig = GetTokenConfiguration();

        var tokenExpiration = DateTime.UtcNow.AddMinutes(tokenConfig.TokenLifetime);
        var refreshTokenExpiration = DateTime.UtcNow.AddMinutes(tokenConfig.RefreshTokenLifetime);
        return new AccessTokenResponse()
        {
            TokenType = TokenScheme.TokenType,
            AccessToken = GenerateJwtToken(user, tokenExpiration),
            ExpiresAt = tokenExpiration,
            RefreshToken = GenerateToken(expiresAt: refreshTokenExpiration),
            RefreshExpiresAt = refreshTokenExpiration,
        };
    }
    public bool ValidateRefreshToken(string refreshToken)
    {
        var tokenConfig = GetTokenConfiguration();

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = tokenConfig.TokenIssuer,
            ValidAudience = tokenConfig.TokenIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfig.TokenKey)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken validatedToken;

        try
        {
            tokenHandler.ValidateToken(refreshToken, tokenValidationParameters, out validatedToken);
            return validatedToken is not null;
        }
        catch
        {
            return false;
        }
    }
    private string GenerateJwtToken(User user, DateTime expiresAt)
    {
        var claims = new List<Claim> {
            new(ClaimTypes.NameIdentifier, user.UserID.ToString()),
            new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new(SlothClaimTypes.FirstName, user.FirstName ?? ""),
            new(SlothClaimTypes.LastName, user.LastName ?? ""),
            new(SlothClaimTypes.UserName, user.UserName ?? ""),
            new(ClaimTypes.Email, user.Email),
        };

        foreach (var role in user.UserRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.RoleCode));
        }

        return GenerateToken(claims, expiresAt);
    }
    private TokenConfiguration GetTokenConfiguration()
    {
        var appConfig = configuration.GetSection(ConfigurationKeys.Configuration).Get<Configuration>()!;
        return appConfig.TokenConfiguration;
    }
    private string GenerateToken(List<Claim>? claims = null, DateTime expiresAt = new())
    {
        var tokenConfig = GetTokenConfiguration();

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfig.TokenKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            tokenConfig.TokenIssuer,
            tokenConfig.TokenIssuer,
            claims,
            expires: expiresAt,
            signingCredentials: cred
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}
