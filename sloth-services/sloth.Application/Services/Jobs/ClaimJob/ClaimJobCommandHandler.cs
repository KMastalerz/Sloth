using AutoMapper;
using MediatR;
using sloth.Application.Models.Jobs;
using sloth.Application.UserIdentity;
using sloth.Domain.Entities;
using sloth.Domain.Exceptions;
using sloth.Domain.Repositories;
using System.Text.Json;
using System.Transactions;

namespace sloth.Application.Services.Jobs.ClaimBug;
public class ClaimBugCommandHandler(
    IMapper mapper,
    IUserContext userContext,
    IJobRepository jobRepository
    ) : IRequestHandler<ClaimJobCommand, IEnumerable<GetAssignmentBugItem>>
{
    public async Task<IEnumerable<GetAssignmentBugItem>> Handle(ClaimJobCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser()
            ?? throw new InvalidUserException();

        var bug = await jobRepository.GetJobAsync(request.JobID)
            ?? throw new MissingEntryException(nameof(Job));

        var newJobAssignment = new JobAssignment()
        {
            UserID = user.UserGuid,
            JobID = request.JobID,
            AssignedByID = user.UserGuid,
            AssignedDate = DateTime.UtcNow
        };

        var transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TransactionManager.DefaultTimeout
        };

        using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
        {
            await jobRepository.AddJobAssignmentAsync(newJobAssignment);

            var jobHistory = new JobHistory()
            {
                JobID = request.JobID,
                ChangedByID = user.UserGuid,
                ChangeDate = DateTime.UtcNow,
                Type = "Assignment",
                Action = "Add",
                Value = JsonSerializer.Serialize(newJobAssignment)
            };

            await jobRepository.AddJobHistory(jobHistory);

            scope.Complete();
        }

        var assignments = await jobRepository.ListAssignmentsAsync(bug.JobID);

        var result = mapper.Map<IEnumerable<GetAssignmentBugItem>>(assignments);

        return result;
    }
}
