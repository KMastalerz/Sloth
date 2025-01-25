using MediatR;
using Microsoft.Extensions.Logging;
using sloth.Application.UserIdentity;
using sloth.Domain.Entities;
using sloth.Domain.Exceptions;
using sloth.Domain.Repositories;

namespace sloth.Application.Services.Jobs;
public class DeleteJobCommandHandler(
    ILogger<DeleteJobCommandHandler> logger,
    IJobRepository jobRepository,
    IUserContext userContext
    ) : IRequestHandler<DeleteJobCommand>
{
    public async Task Handle(DeleteJobCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser() ?? 
            throw new InvalidUserException();

        var job = await jobRepository.GetJobAsync(request.JobID) ??
            throw new MissingEntryException(nameof(Job));

        await jobRepository.DeleteLinkedJobsAsync(request.JobID);

        switch (job.Type)
        {
            case "Bug":
                var bug = await jobRepository.GetBugByJobIDAsync(request.JobID) ??
                    throw new MissingEntryException(nameof(Bug));
                await jobRepository.DeleteBugAsync(bug);
                break;
            //case "Query":

            //    break;
            default:
                throw new InvalidJobTypeException();
        }

        logger.LogWarning("{UserName} attempts to remove bug: {jobID}", user.UserName, request.JobID);
    }
}
