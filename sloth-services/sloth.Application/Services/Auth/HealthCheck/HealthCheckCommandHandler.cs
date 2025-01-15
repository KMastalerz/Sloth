using MediatR;
using sloth.Application.UserIdentity;
using sloth.Domain.Repositories;

namespace sloth.Application.Services.Auth;
public class HealthCheckCommandHandler(
    IUserContext userContext,
    IAuthRepository authRepository)
    : IRequestHandler<HealthCheckCommand, bool>
{
    public async Task<bool> Handle(HealthCheckCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        if (currentUser is null) return false;
        if (currentUser.UserName != request.Login) return false;

        var user = await authRepository.GetUserAsync(request.Login);
        if (user is null) return false;

        var isUserLocked = await authRepository.IsUserLockedAsync(user);

        return !isUserLocked;
    }
}
