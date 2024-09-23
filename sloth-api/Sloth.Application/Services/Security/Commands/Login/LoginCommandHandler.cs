using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Sloth.Application.DTO.Security;
using Sloth.Domain.Constants;
using Sloth.Domain.Entities;
using Sloth.Domain.Exceptions;
using Sloth.Domain.Services;
using Sloth.Domain.Repositories;

namespace Sloth.Application.Services.Security;
public class LoginCommandHandler(
    ILogger<LoginCommandHandler> logger, 
    IMapper mapper, 
    ISecurityRepository securityRepository, 
    IPasswordHasher<User> passwordHasher,
    IConfigurationService configurationService,
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

        // Create Reponse 
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

        logger.LogInformation("User {UserID} Logged In", user.UserID);

        return result;
    }
}
