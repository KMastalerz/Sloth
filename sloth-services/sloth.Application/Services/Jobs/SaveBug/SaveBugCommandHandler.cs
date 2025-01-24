using AutoMapper;
using MediatR;
using sloth.Application.Models.Jobs;
using sloth.Application.UserIdentity;
using sloth.Domain.Entities;
using sloth.Domain.Exceptions;
using sloth.Domain.Repositories;
using sloth.Utilities.Extensions;
using System.Transactions;

namespace sloth.Application.Services.Jobs;
public class SaveBugCommandHandler(
    IMapper mapper,
    IJobRepository jobRepository,
    IUserContext userContext
    ) : IRequestHandler<SaveBugCommand, GetBugItem>
{
    public async Task<GetBugItem> Handle(SaveBugCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser()
            ?? throw new InvalidUserException();

        var bug = await jobRepository.GetBugAsync(request.BugID)
            ?? throw new MissingEntryException(nameof(Bug));

        var updateBug = request.StatusChanged ||
            request.ClientChanged ||
            request.PriorityChanged ||
            !string.IsNullOrEmpty(request.NewDescription) ||
            !string.IsNullOrEmpty(request.NewHeader) ||
            request.NewProductIDs.Any() ||
            request.RemovedProductIDs.Any() ||
            request.NewFunctionalityIDs.Any() ||
            request.RemovedFunctionalityIDs.Any();

        if(updateBug == false)
        {
            throw new NoChangesException();
        }

        // Define the TransactionScope options
        var transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TransactionManager.DefaultTimeout
        };

        using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
        {
            if (request.StatusChanged)
            {
                var statusHistory = new JobStatusHistory() {
                    JobID = bug.JobID,
                    ChangedByID = user.UserGuid,
                    ChangedDate = DateTime.UtcNow,
                    PreviousStatusID = bug.StatusID,
                    NewStatusID = request.NewStatusID
                };
                bug.StatusID = request.NewStatusID;
                await jobRepository.AddJobStatusHistoryAsync(statusHistory);

            }

            if (request.PriorityChanged)
            {
                var priorityHistory = new JobPriorityHistory()
                {
                    JobID = bug.JobID,
                    ChangedByID = user.UserGuid,
                    ChangedDate = DateTime.UtcNow,
                    PreviousPriorityID = bug.PriorityID,
                    NewPriorityID = request.NewPriorityID
                };
                bug.PriorityID = (int)request.NewPriorityID!;
                await jobRepository.AddJobPriorityHistoryAsync(priorityHistory);
            }

            if (request.ClientChanged)
            {
                bug.ClientID = request.NewClientID;
            }

            if (request.NewHeader is not null)
            {
                bug.Header = request.NewHeader;
            }

            if (request.NewDescription is not null)
            {
                bug.Description = request.NewDescription;
            }

            if (request.RemovedProductIDs.Any()) 
            {
                bug.Products = bug.Products.Where(p => !request.RemovedProductIDs.Any(rp => rp == p.ProductID)).ToList();
            }
           
            if(request.NewProductIDs.Any())
            {
                var products = await jobRepository.ListProductsAsync();
                var newProducts = products.Where(p => request.NewProductIDs.Any(np => np == p.ProductID)).ToList();
                bug.Products.AddRange(newProducts);
            }

            if (request.RemovedFunctionalityIDs.Any())
            {
                bug.Functionalities = bug.Functionalities.Where(p => !request.RemovedFunctionalityIDs.Any(rp => rp == p.FunctionalityID)).ToList();
            }

            if (request.NewFunctionalityIDs.Any())
            {
                var functionalities = await jobRepository.ListFunctionalitiesAsync();
                var newFunctionalities = functionalities.Where(f => request.NewFunctionalityIDs.Any(np => np == f.FunctionalityID)).ToList();
                bug.Functionalities.AddRange(newFunctionalities);
            }

            await jobRepository.UpdateBugAsync(bug);
            scope.Complete();
        }

        var result = mapper.Map<GetBugItem>(bug);
        return result;
    }
}
