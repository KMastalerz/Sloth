using MediatR;
using Microsoft.Extensions.Logging;
using sloth.Application.UserIdentity;
using sloth.Domain.Entities;
using sloth.Domain.Exceptions;
using sloth.Domain.Repositories;

namespace sloth.Application.Services.Jobs;
public class DeleteBugCommandHandler(
    ILogger<DeleteBugCommandHandler> logger,
    IJobRepository jobRepository,
    IUserContext userContext
    ) : IRequestHandler<DeleteBugCommand>
{
    public async Task Handle(DeleteBugCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser() ?? 
            throw new InvalidUserException();

        logger.LogWarning("{UserName} attempts to remove bug: {bugID}", user.UserName, request.BugID);
        
        var bug = await jobRepository.GetBugAsync(request.BugID) ??
            throw new MissingEntryException(nameof(Bug));

        // TODO: Add check on linked child and secondary places, befor bug removal those need to be removed before bug is deleted.

        await jobRepository.DeleteBugAsync(bug);
    }
}
