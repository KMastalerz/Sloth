using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Sloth.Application.DTO.Security;
using Sloth.Domain.Constants;
using Sloth.Domain.Entities;
using Sloth.Domain.Exceptions;
using Sloth.Domain.Repositories;
using Sloth.Domain.Services;

namespace Sloth.Application.Services.Security;
public class RefreshTokenCommandHandler(
    ILogger<RefreshTokenCommandHandler> logger,
    IMapper mapper,
    ISecurityRepository securityRepository,
    IConfigurationService configurationService,
    ISecurityService securityService) : IRequestHandler<RefreshTokenCommand, AccessTokenResponse>
{
    public async Task<AccessTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        // Get user by username
        var user = await securityRepository.GetUserAsync(request.UserName) ?? 
            throw new NotFoundException(nameof(User), request.UserName);

        // Get refresh token
        var refreshToken = await securityRepository.GetRefreshTokenAsync(user.UserID, request.RefreshToken) ??
            throw new InvalidPropertyException(nameof(RefreshToken));

        // Now Token is valid, generate new token
        var result = new AccessTokenResponse()
        {
            TokenType = AuthScheme.Bearer,
            AccessToken = securityService.GenerateToken(user),
            ExpiresAt = DateTime.Now.AddMinutes(configurationService.SecurityConfig!.TokenLifetime),
            RefreshToken = securityService.GenerateRefreshToken(user),
            RefreshExpiresAt = DateTime.Now.AddMinutes(configurationService.SecurityConfig!.RefreshTokenLifetime),
            User = mapper.Map<AccessUser>(user)
        };

        // Store Refresh Token
        await securityRepository.AddRefreshTokenAsync(result.RefreshToken, user.UserID, result.RefreshExpiresAt);

        // Removed old token as it was used
        await securityRepository.RemoveRefreshTokenAsync(refreshToken);

        logger.LogInformation("User {UserID} Refreshed Token", user.UserID);

        return result;
    }
}
