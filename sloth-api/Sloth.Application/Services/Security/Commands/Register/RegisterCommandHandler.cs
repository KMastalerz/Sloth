using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Sloth.Domain.Entities;

namespace Sloth.Application.Services.Security;
public class RegisterCommandHandler(ILogger<RegisterCommandHandler> logger, UserManager<User> userManager, IMapper mapper) : IRequestHandler<RegisterCommand>
{
    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Registering new user: {UserEmail}", request.Email);

        var user = mapper.Map<User>(request);

        await userManager.CreateAsync(user, request.Password);
    }
}
