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

        var secConfig = configurationService.SecurityConfig;

        // Create Reponse 
        var result = new AccessTokenResponse()
        {
            TokenType = AuthScheme.Bearer,
            AccessToken = securityService.GenerateToken(user),
            ExpiresIn = TimeSpan.FromMinutes(secConfig!.TokenLifetime),
            RefreshToken = securityService.GenerateRefreshToken(user),
            User = mapper.Map<AccessUser>(user)
        };

        // TO DO: Check if Password is expired

        // TO DO: Add 2FA
        return result;

        //throw new NotImplementedException();
    }
}
