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
                var statusHistory = new List<JobStatusHistory> {
                    new()
                    {
                        JobID = bug.JobID,
                        ChangedByID = user.UserGuid,
                        ChangedDate = DateTime.UtcNow,
                        StatusID = bug.StatusID,
                        Action = "Delete"
                    },
                    new()
                    {
                        JobID = bug.JobID,
                        ChangedByID = user.UserGuid,
                        ChangedDate = DateTime.UtcNow,
                        StatusID = request.NewStatusID,
                        Action = "Add"
                    }
                };
                bug.StatusID = request.NewStatusID;
                await jobRepository.AddJobStatusHistoryAsync(statusHistory);
            }

            if (request.PriorityChanged)
            {
                var priorityHistory = new List<JobPriorityHistory> {
                    new()
                    {
                        JobID = bug.JobID,
                        ChangedByID = user.UserGuid,
                        ChangedDate = DateTime.UtcNow,
                        PriorityID = bug.PriorityID,
                        Action = "Delete"
                    },
                    new()
                    {
                        JobID = bug.JobID,
                        ChangedByID = user.UserGuid,
                        ChangedDate = DateTime.UtcNow,
                        PriorityID = request.NewPriorityID,
                        Action = "Add"
                    }
                };
                bug.PriorityID = (int)request.NewPriorityID!;
                await jobRepository.AddJobPriorityHistoryAsync(priorityHistory);
            }

            if (request.ClientChanged)
            {
                var clientHistory = new List<JobClientHistory> {
                    new()
                    {
                        JobID = bug.JobID,
                        ChangedByID = user.UserGuid,
                        ChangedDate = DateTime.UtcNow,
                        ClientID = bug.ClientID,
                        Action = "Delete"
                    },
                    new()
                    {
                        JobID = bug.JobID,
                        ChangedByID = user.UserGuid,
                        ChangedDate = DateTime.UtcNow,
                        ClientID = request.NewClientID,
                        Action = "Add"
                    }
                };
                bug.ClientID = request.NewClientID;
                await jobRepository.AddJobClientHistoryAsync(clientHistory);
            }

            var detailHistory = new List<JobDetailHistory>();
            if (request.NewHeader is not null)
            {
                bug.Header = request.NewHeader;
                detailHistory.Add(
                    new()
                    {
                        JobID = bug.JobID,
                        Field = "Header",
                        Value = request.NewHeader,
                        ChangedDate = DateTime.UtcNow,
                        ChangedByID = user.UserGuid
                    }
                );
            }

            if (request.NewDescription is not null)
            {
                bug.Description = request.NewDescription;
                detailHistory.Add(
                    new()
                    {
                        JobID = bug.JobID,
                        Field = "Description",
                        Value = request.NewDescription,
                        ChangedDate = DateTime.UtcNow,
                        ChangedByID = user.UserGuid
                    }
                );
            }

            if(detailHistory.Any())
            {
                await jobRepository.AddJobDetailsHistoryAsync(detailHistory);
            }

            var productHistory = new List<JobProductHistory>();
            if (request.RemovedProductIDs.Any()) 
            {
                productHistory.AddRange(request.RemovedProductIDs.Select(p => new JobProductHistory()
                {
                    JobID = bug.JobID,
                    ProductID = p,
                    ChangedByID = user.UserGuid,
                    ChangedDate = DateTime.UtcNow,
                    Action = "Delete"
                }));
                bug.Products = bug.Products.Where(p => !request.RemovedProductIDs.Any(rp => rp == p.ProductID)).ToList();
            }
           
            if(request.NewProductIDs.Any())
            {
                productHistory.AddRange(request.NewProductIDs.Select(p => new JobProductHistory()
                {
                    JobID = bug.JobID,
                    ProductID = p,
                    ChangedByID = user.UserGuid,
                    ChangedDate = DateTime.UtcNow,
                    Action = "Add"
                }));
                var products = await jobRepository.ListProductsAsync();
                var newProducts = products.Where(p => request.NewProductIDs.Any(np => np == p.ProductID)).ToList();
                bug.Products.AddRange(newProducts);
            }

            if (productHistory.Any())
            {
                await jobRepository.AddJobProductHistoryAsync(productHistory);
            }

            var functionalityHistory = new List<JobFunctionalityHistory>();
            if (request.RemovedFunctionalityIDs.Any())
            {
                functionalityHistory.AddRange(request.RemovedFunctionalityIDs.Select(p => new JobFunctionalityHistory()
                {
                    JobID = bug.JobID,
                    FunctionalityID = p,
                    ChangedByID = user.UserGuid,
                    ChangedDate = DateTime.UtcNow,
                    Action = "Delete"
                }));
                bug.Functionalities = bug.Functionalities.Where(p => !request.RemovedFunctionalityIDs.Any(rp => rp == p.FunctionalityID)).ToList();
            }

            if (request.NewFunctionalityIDs.Any())
            {
                functionalityHistory.AddRange(request.NewFunctionalityIDs.Select(p => new JobFunctionalityHistory()
                {
                    JobID = bug.JobID,
                    FunctionalityID = p,
                    ChangedByID = user.UserGuid,
                    ChangedDate = DateTime.UtcNow,
                    Action = "Add"
                }));
                var functionalities = await jobRepository.ListFunctionalitiesAsync();
                var newFunctionalities = functionalities.Where(f => request.NewFunctionalityIDs.Any(np => np == f.FunctionalityID)).ToList();
                bug.Functionalities.AddRange(newFunctionalities);
            }

            if (functionalityHistory.Any())
            {
                await jobRepository.AddJobFunctionalityHistoryAsync(functionalityHistory);
            }

            await jobRepository.UpdateBugAsync(bug);
            scope.Complete();
        }

        var result = mapper.Map<GetBugItem>(bug);
        return result;
    }
}
