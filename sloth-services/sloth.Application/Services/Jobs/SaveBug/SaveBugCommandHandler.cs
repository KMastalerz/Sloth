using AutoMapper;
using MediatR;
using sloth.Application.Models.Jobs;
using sloth.Application.UserIdentity;
using sloth.Domain.Entities;
using sloth.Domain.Exceptions;
using sloth.Domain.Repositories;
using sloth.Utilities.Extensions;
using System.Text.Json;
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

            var jobHistories = new List<JobHistory>();
            if (request.StatusChanged)
            {
                Status? status = null;

                if(request.NewStatusID is not null)
                    status = await jobRepository.GetStatusAsync((int)request.NewStatusID);

                jobHistories.AddRange(
                    new()
                    {
                        JobID = bug.JobID,
                        ChangedByID = user.UserGuid,
                        ChangeDate = DateTime.UtcNow,
                        Type = "Status",
                        Action = "Delete",
                        Value = JsonSerializer.Serialize(bug.Status),
                    },
                    new()
                    {
                        JobID = bug.JobID,
                        ChangedByID = user.UserGuid,
                        ChangeDate = DateTime.UtcNow,
                        Type = "Status",
                        Action = "Add",
                        Value = status != null ? JsonSerializer.Serialize(status) : null,
                    }
                );

                bug.StatusID = request.NewStatusID;
            }

            if (request.PriorityChanged)
            {
                Priority? priority = null;

                if (request.NewPriorityID is not null)
                    priority = await jobRepository.GetPriorityAsync((int)request.NewPriorityID);

                jobHistories.AddRange(
                    new()
                    {
                        JobID = bug.JobID,
                        ChangedByID = user.UserGuid,
                        ChangeDate = DateTime.UtcNow,
                        Type = "Priority",
                        Action = "Delete",
                        Value = JsonSerializer.Serialize(bug.Priority),
                    },
                    new()
                    {
                        JobID = bug.JobID,
                        ChangedByID = user.UserGuid,
                        ChangeDate = DateTime.UtcNow,
                        Type = "Priority",
                        Action = "Add",
                        Value = priority != null ? JsonSerializer.Serialize(priority) : null,
                    }
                );

                bug.PriorityID = (int)request.NewPriorityID!;
            }

            if (request.ClientChanged)
            {
                Client? client = null;

                if (request.NewClientID is not null)
                    client = await jobRepository.GetClientByIDAsync((Guid)request.NewClientID);

                jobHistories.AddRange(
                    new()
                    {
                        JobID = bug.JobID,
                        ChangedByID = user.UserGuid,
                        ChangeDate = DateTime.UtcNow,
                        Type = "Client",
                        Action = "Delete",
                        Value = JsonSerializer.Serialize(bug.Priority),
                    },
                    new()
                    {
                        JobID = bug.JobID,
                        ChangedByID = user.UserGuid,
                        ChangeDate = DateTime.UtcNow,
                        Type = "Client",
                        Action = "Add",
                        Value = client != null ? JsonSerializer.Serialize(client) : null,
                    }
                );

                bug.ClientID = request.NewClientID;
            }

            if (request.NewHeader is not null)
            {
                bug.Header = request.NewHeader;
                jobHistories.Add(
                    new()
                    {
                        JobID = bug.JobID,
                        ChangedByID = user.UserGuid,
                        ChangeDate = DateTime.UtcNow,
                        Type = "Header",
                        Action = "Change",
                        Value = request.NewHeader,
                    }
                );
            }

            if (request.NewDescription is not null)
            {
                bug.Description = request.NewDescription;
                jobHistories.Add(
                    new()
                    {
                        JobID = bug.JobID,
                        ChangedByID = user.UserGuid,
                        ChangeDate = DateTime.UtcNow,
                        Type = "Description",
                        Action = "Change",
                        Value = request.NewDescription,
                    }
                );
            }

            var products = await jobRepository.ListProductsAsync();

            if (request.RemovedProductIDs.Any()) 
            {
                bug.Products = bug.Products.Where(p => !request.RemovedProductIDs.Any(rp => rp == p.ProductID)).ToList();
            }
           
            if (request.NewProductIDs.Any())
            {
                jobHistories.AddRange(request.NewProductIDs.Select(p => new JobHistory()
                {
                    JobID = bug.JobID,
                    ChangedByID = user.UserGuid,
                    ChangeDate = DateTime.UtcNow,
                    Type = "Product",
                    Action = "Add",
                    Value = JsonSerializer.Serialize(products.FirstOrDefault(pr => pr.ProductID == p))
                }));
                var newProducts = products.Where(p => request.NewProductIDs.Any(np => np == p.ProductID)).ToList();
                bug.Products.AddRange(newProducts);
            }

            if (request.RemovedProductIDs.Any())
            {
                jobHistories.AddRange(request.RemovedProductIDs.Select(p => new JobHistory()
                {
                    JobID = bug.JobID,
                    ChangedByID = user.UserGuid,
                    ChangeDate = DateTime.UtcNow,
                    Type = "Product",
                    Action = "Delete",
                    Value = JsonSerializer.Serialize(products.FirstOrDefault(pr => pr.ProductID == p))
                }));
            } 

            var functionalities = await jobRepository.ListFunctionalitiesAsync();
            if (request.RemovedFunctionalityIDs.Any())
            { 
                bug.Functionalities = bug.Functionalities.Where(p => !request.RemovedFunctionalityIDs.Any(rp => rp == p.FunctionalityID)).ToList();
            }

            if (request.NewFunctionalityIDs.Any())
            {
                jobHistories.AddRange(request.NewFunctionalityIDs.Select(f => new JobHistory()
                {
                    JobID = bug.JobID,
                    ChangedByID = user.UserGuid,
                    ChangeDate = DateTime.UtcNow,
                    Type = "Functionality",
                    Action = "Add",
                    Value = JsonSerializer.Serialize(functionalities.FirstOrDefault(fu => fu.ProductID == f))
                }));
                var newFunctionalities = functionalities.Where(f => request.NewFunctionalityIDs.Any(np => np == f.FunctionalityID)).ToList();
                bug.Functionalities.AddRange(newFunctionalities);
            }

            if (request.RemovedFunctionalityIDs.Any())
            {
                jobHistories.AddRange(request.RemovedFunctionalityIDs.Select(f => new JobHistory()
                {
                    JobID = bug.JobID,
                    ChangedByID = user.UserGuid,
                    ChangeDate = DateTime.UtcNow,
                    Type = "Functionality",
                    Action = "Delete",
                    Value = JsonSerializer.Serialize(functionalities.FirstOrDefault(fu => fu.ProductID == f))
                }));
            }

            if(jobHistories.Any()) 
                await jobRepository.AddJobHistories(jobHistories);

            await jobRepository.UpdateBugAsync(bug);
            scope.Complete();
        }

        var result = mapper.Map<GetBugItem>(bug);
        return result;
    }
}
