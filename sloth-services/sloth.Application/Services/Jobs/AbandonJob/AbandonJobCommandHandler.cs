using AutoMapper;
using MediatR;
using sloth.Application.Models.Jobs;
using sloth.Application.UserIdentity;
using sloth.Domain.Entities;
using sloth.Domain.Exceptions;
using sloth.Domain.Repositories;
using System.Text.Json;
using System.Transactions;

namespace sloth.Application.Services.Jobs;
public class AbandonJobCommandHandler(
    IMapper mapper,
    IUserContext userContext,
    IJobRepository jobRepository
    ) : IRequestHandler<AbandonJobCommand, IEnumerable<GetAssignmentBugItem>>
{
    public async Task<IEnumerable<GetAssignmentBugItem>> Handle(AbandonJobCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser()
            ?? throw new InvalidUserException();

        var bug = await jobRepository.GetJobAsync(request.JobID)
            ?? throw new MissingEntryException(nameof(Job));

        var jobAssignment = await jobRepository.GetJobAssignment(request.JobID, user.UserGuid)
            ?? throw new MissingEntryException(nameof(JobAssignment));

        var jobHistory = new JobHistory()
        {
            JobID = request.JobID,
            ChangedByID = user.UserGuid,
            ChangeDate = DateTime.UtcNow,
            Type = "Assignment",
            Action = "Delete",
            Value = JsonSerializer.Serialize(jobAssignment)
        };
        var transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TransactionManager.DefaultTimeout
        };

        using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
        {
            await jobRepository.AddJobHistory(jobHistory);

            await jobRepository.RemoveJobAssignmentAsync(jobAssignment);
            scope.Complete();
        }

        var assignments = await jobRepository.ListAssignmentsAsync(bug.JobID);

        var result = mapper.Map<IEnumerable<GetAssignmentBugItem>>(assignments);

        return result;
    }
}
