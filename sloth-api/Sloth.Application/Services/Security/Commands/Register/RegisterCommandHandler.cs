using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Sloth.Domain.Entities;
using Sloth.Domain.Repositories;
using Sloth.Shared.Helpers;

namespace Sloth.Application.Services.Security;
public class RegisterCommandHandler(ILogger<RegisterCommandHandler> logger, IMapper mapper, ISecurityRepository securityRepository, IPasswordHasher<User> passwordHasher) : IRequestHandler<RegisterCommand>
{
    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // Fix formatting
        request = FixRequest(request);
        
        // Create a new user
        var user = mapper.Map<User>(request);

        // Hash the password
        user.PasswordHash = passwordHasher.HashPassword(user, request.Password);

        // Add user to the database
        await securityRepository.AddUserAsync(user);

        logger.LogInformation("{UserName} registered successfully", request.UserName);
        // TO DO: Send email verification
    }

    private RegisterCommand FixRequest(RegisterCommand request)
    {
        return new()
        {
            UserName = request.UserName.ToUpper(),
            Email = request.Email.ToLower(),
            Password = request.Password,
            ConfirmPassword = request.ConfirmPassword,
            FirstName = request.FirstName.CapitalizeFirstLetter(),
            LastName = request.LastName.CapitalizeFirstLetter(),
        };
    }
}
