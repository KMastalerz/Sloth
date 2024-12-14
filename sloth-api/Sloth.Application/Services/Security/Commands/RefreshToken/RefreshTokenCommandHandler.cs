using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Sloth.Shared.DTO;
using Sloth.Domain.Entities;
using Sloth.Domain.Exceptions;
using Sloth.Domain.Repositories;
using Sloth.Domain.Services;

namespace Sloth.Application.Services.Security;
public class RefreshTokenCommandHandler(
    ILogger<RefreshTokenCommandHandler> logger,
    IMapper mapper,
    ISecurityRepository securityRepository,
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
        var accessToken = securityService.GenerateAcesssTokenResponse(user);
        accessToken.User = mapper.Map<AccessUser>(user);

        // Removed old token as it was used
        await securityRepository.RemoveRefreshTokenAsync(refreshToken);

        // Store new refresh token
        await securityRepository.AddRefreshTokenAsync(accessToken.RefreshToken, user.UserID, accessToken.RefreshExpiresAt);

        logger.LogInformation("User {UserID} Refreshed Token", user.UserID);

        return accessToken;
    }
}
