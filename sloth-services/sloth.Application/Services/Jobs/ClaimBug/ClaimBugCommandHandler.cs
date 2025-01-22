using AutoMapper;
using MediatR;
using sloth.Application.Models.Jobs;
using sloth.Application.UserIdentity;
using sloth.Domain.Entities;
using sloth.Domain.Exceptions;
using sloth.Domain.Repositories;

namespace sloth.Application.Services.Jobs.ClaimBug;
public class ClaimBugCommandHandler(
    IMapper mapper,
    IUserContext userContext,
    IJobRepository jobRepository
    ) : IRequestHandler<ClaimBugCommand, IEnumerable<GetAssignmentBugItem>>
{
    public async Task<IEnumerable<GetAssignmentBugItem>> Handle(ClaimBugCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser()
            ?? throw new InvalidUserException();

        var bug = await jobRepository.GetBugAsync(request.BugID)
            ?? throw new MissingEntryException(nameof(Bug));

        var newJobAssignment = new JobAssignment()
        {
            UserID = user.UserGuid,
            JobID = bug.JobID,
            AssignedByID = user.UserGuid,
            AssignedDate = DateTime.UtcNow
        };

        await jobRepository.AddJobAssignmentAsync(newJobAssignment);

        var assignments = await jobRepository.ListAssignmentsAsync(bug.JobID);

        var result = mapper.Map<IEnumerable<GetAssignmentBugItem>>(assignments);

        return result;
    }
}
