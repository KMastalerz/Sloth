using sloth.Application.Models.Auth;
using sloth.Domain.Entities;

namespace sloth.Application.Services.Auth;
public interface IAuthService
{
    AccessTokenResponse GenerateAcesssTokenResponse(User user);
    bool ValidateRefreshToken(string refreshToken);
}