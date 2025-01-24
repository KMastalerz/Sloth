using AutoMapper;
using MediatR;
using sloth.Application.Models.Jobs;
using sloth.Application.UserIdentity;
using sloth.Domain.Entities;
using sloth.Domain.Exceptions;
using sloth.Domain.Repositories;
using System.Transactions;

namespace sloth.Application.Services.Jobs;
public class AbandonBugCommandHandler(
    IMapper mapper,
    IUserContext userContext,
    IJobRepository jobRepository
    ) : IRequestHandler<AbandonBugCommand, IEnumerable<GetAssignmentBugItem>>
{
    public async Task<IEnumerable<GetAssignmentBugItem>> Handle(AbandonBugCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser()
            ?? throw new InvalidUserException();

        var bug = await jobRepository.GetBugAsync(request.BugID)
            ?? throw new MissingEntryException(nameof(Bug));

        var jobAssignment = await jobRepository.GetJobAssignment(bug.JobID, user.UserGuid)
            ?? throw new MissingEntryException(nameof(JobAssignment));

        var jobAssignmentHistory = mapper.Map<JobAssignmentHistory>(jobAssignment);
        jobAssignmentHistory.Action = "Delete";
        jobAssignmentHistory.ChangedDate = DateTime.UtcNow;

        var transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TransactionManager.DefaultTimeout
        };

        using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
        {
            await jobRepository.AddJobAssignmentHistoryAsync(jobAssignmentHistory);

            await jobRepository.RemoveJobAssignmentAsync(jobAssignment);
            scope.Complete();
        }

        var assignments = await jobRepository.ListAssignmentsAsync(bug.JobID);

        var result = mapper.Map<IEnumerable<GetAssignmentBugItem>>(assignments);

        return result;
    }
}
