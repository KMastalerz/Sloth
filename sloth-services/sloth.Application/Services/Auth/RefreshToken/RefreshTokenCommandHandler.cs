using MediatR;
using Microsoft.Extensions.Logging;
using sloth.Application.Models.Auth;
using sloth.Domain.Exceptions;
using sloth.Domain.Repositories;

namespace sloth.Application.Services.Auth;
public class RefreshTokenCommandHandler(
    ILogger<RefreshTokenCommandHandler> logger,
    IAuthRepository authRepository,
    IAuthService authService) 
    : IRequestHandler<RefreshTokenCommand, AccessTokenResponse>
{
    public async Task<AccessTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        // get user by userID
        var user = await authRepository.GetUserAsync(request.UserID)
            ?? throw new InvalidUserException();

        // get token from database
        var refreshToken = await authRepository.GetRefreshTokenAsync(request.UserID)
            ?? throw new InvalidTokenException();

        // check if refresh token is valid and corresponding to each other
        if(refreshToken.Token != request.RefreshToken)
            throw new InvalidTokenException();

        // validate token
        if (!authService.ValidateRefreshToken(request.RefreshToken))
            throw new InvalidTokenException();

        // generate new AccessTokenResponse
        var accessToken = authService.GenerateAcesssTokenResponse(user);

        // refresh, refresh token
        await authRepository.UpdateRefreshTokenAsync(accessToken.RefreshToken, user.UserID, accessToken.ExpiresAt);

        logger.LogInformation("User: {UserName} refreshed token", user.UserName);
        // return access token
        return accessToken;
    }
}
