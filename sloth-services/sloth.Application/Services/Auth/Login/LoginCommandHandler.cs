using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using sloth.Application.Models.Auth;
using sloth.Application.Models.Configuration;
using sloth.Domain.Entities;
using sloth.Domain.Exceptions;
using sloth.Domain.Repositories;
using sloth.Utilities.Constants;

namespace sloth.Application.Services.Auth;
public class LoginCommandHandler (
    ILogger<LoginCommandHandler> logger,
    IMapper mapper,
    IPasswordHasher<User> passwordHasher,
    IAuthRepository authRepository,
    IAuthService authService,
    IConfiguration configuration) 
    : IRequestHandler<LoginCommand, AccessTokenResponse>
{
    public async Task<AccessTokenResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // get user by username or email
        var user = await authRepository.GetUserAsync(request.Login)
            ?? throw new InvalidLoginException();

        // check if password is correct
        var checkPassword = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

        var appConfig = configuration.GetSection(ConfigurationKeys.Configuration).Get<Configuration>()!;

        // check if user is already locked
        var isUserLocked = await authRepository.IsUserLockedAsync(user) || !user.IsActive;

        if(isUserLocked)
            throw new LockedUserException();
        else 
            await authRepository.UnlockUserAsync(user);

        if (checkPassword != PasswordVerificationResult.Success)
        {
            // check if max failed login attempts is set
            if (appConfig.MaxFailedLoginAttempts != 0)
            {
                // bump invalid login attempts per user
                authRepository.AddUserFailedLoginAttemptsAsync(user);

                // check max attempts
                if (appConfig.MaxFailedLoginAttempts <= user.FailedLoginAttempts)
                {
                    var lockExpirationDate = DateTime.UtcNow.AddMinutes(appConfig.FailedLoginTimeout);

                    await authRepository.LockUserAsync(user, lockExpirationDate);

                    throw new LockedUserException();
                }
                else throw new InvalidLoginException();
            }
            else throw new InvalidLoginException();
        }
        else
        {
            if(user.FailedLoginAttempts > 0)
                authRepository.ResetUserFailedLoginAttemptsAsync(user);
            
        }

        // generate access token
        var accessToken = authService.GenerateAcesssTokenResponse(user);
        accessToken.User = mapper.Map<AccessUser>(user);

        // refresh, refresh token
        await authRepository.UpdateRefreshTokenAsync(accessToken.RefreshToken, user.UserID, accessToken.ExpiresAt);

        logger.LogInformation("User: {UserName} logged in", user.UserName);
        // return access token
        return accessToken;
    }
}
