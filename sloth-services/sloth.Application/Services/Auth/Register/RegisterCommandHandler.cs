using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using sloth.Domain.Entities;
using sloth.Domain.Exceptions;
using sloth.Domain.Repositories;
using sloth.Utilities.Extensions;

namespace sloth.Application.Services.Auth;
internal class RegisterCommandHandler(
    ILogger<RegisterCommandHandler> logger,
    IMapper mapper,
    IPasswordHasher<User> passwordHasher,
    IAuthRepository authRepository)
    : IRequestHandler<RegisterCommand>
{
    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // Fix strings formattings
        request = FixRequest(request);

        var user = await authRepository.GetUserAsync(request.UserName)
            ?? await authRepository.GetUserAsync(request.Email);

        if (user is not null)
            throw new UserAlreadyExistsException();

        // Create a new user
        user = mapper.Map<User>(request);

        // Hash the password
        user.PasswordHash = passwordHasher.HashPassword(user, request.Password);

        // Set user as active
        user.IsActive = true;

        // Invalid user role should not happen, it's checked in validation
        var userRole = await authRepository.GetUserRoleAsync(request.RoleCode)
            ?? throw new InvalidUserRoleException();

        await authRepository.RegisterUserAsync(user, userRole);

        logger.LogInformation("User: {UserName} registered", user.UserName);
    }
    private RegisterCommand FixRequest(RegisterCommand request)
    {
        return new()
        {
            UserName = request.UserName.ToLower(),
            Email = request.Email.ToLower(),
            Password = request.Password,
            ConfirmPassword = request.ConfirmPassword,
            FirstName = request.FirstName.CapitalizeFirstLetter(),
            LastName = request.LastName.CapitalizeFirstLetter(),
            RoleCode = request.RoleCode.ToUpper()
        };
    }
}
