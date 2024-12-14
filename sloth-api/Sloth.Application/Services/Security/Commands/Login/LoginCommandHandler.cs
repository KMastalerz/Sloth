using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Sloth.Domain.Entities;
using Sloth.Domain.Exceptions;
using Sloth.Domain.Services;
using Sloth.Domain.Repositories;
using Sloth.Shared.DTO;

namespace Sloth.Application.Services.Security;
public class LoginCommandHandler(
    ILogger<LoginCommandHandler> logger, 
    IMapper mapper, 
    ISecurityRepository securityRepository, 
    IPasswordHasher<User> passwordHasher,
    ISecurityService securityService) : IRequestHandler<LoginCommand, AccessTokenResponse>
{
    public async Task<AccessTokenResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Get user by username or email
        var user = await securityRepository.GetUserAsync(request.Login);

        if (user == null)
            throw new InvalidLoginException();

        // Check if password is correct
        var checkPassword = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

        if (checkPassword != PasswordVerificationResult.Success)
            throw new InvalidLoginException();

        // TO DO: Check if Password is expired
        var accessToken = securityService.GenerateAcesssTokenResponse(user);
        accessToken.User = mapper.Map<AccessUser>(user);

        // Store Refresh Token
        await securityRepository.ReplaceRefreshTokenAsync(accessToken.RefreshToken, user.UserID, accessToken.RefreshExpiresAt);

        logger.LogInformation("User {UserID} Logged In", user.UserID);

        return accessToken;
    }
}
