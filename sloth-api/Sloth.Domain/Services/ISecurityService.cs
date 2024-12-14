using Sloth.Shared.DTO;
using Sloth.Domain.Entities;

namespace Sloth.Domain.Services;
public interface ISecurityService
{
    string GenerateRefreshToken(User user);
    string GenerateToken(User user);
    AccessTokenResponse GenerateAcesssTokenResponse(User user);
}