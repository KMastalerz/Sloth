using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using sloth.Application.Models.Configuration;
using sloth.Application.UserIdentity;
using sloth.Domain.Entities;
using sloth.Domain.Exceptions;
using sloth.Domain.Repositories;
using sloth.Utilities.Constants;
using System.Transactions;

namespace sloth.Application.Services.Jobs;
public class CreateJobCommandHandler(
    ILogger<CreateJobCommandHandler> logger,
    IJobRepository jobRepository,
    IUserContext userContext,
    IConfiguration configuration) 
    : IRequestHandler<CreateJobCommand>
{
    private readonly string jobsPath = Path.Combine(Directory.GetCurrentDirectory(), "Jobs");
    public async Task Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        if (currentUser == null)
            throw new InvalidUserException();

        // get initial project status from IConfiguration
        var appConfig = configuration.GetSection(ConfigurationKeys.Configuration).Get<Configuration>()!;

        // if job status is not set get initial status for its type, or from all. 
        if (request.StatusID is null)
        {
            request.StatusID = await jobRepository.GetInitialStatusAsync(request.Type);
        }

        // generate creation time
        var newJobDate = DateTime.UtcNow;
        var jobID = 0;
        var id = 0;
        List<string> addedFiles = [];

        using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            try
            {
                switch (request.Type)
                {
                    case "Bug":
                        // create new Bug
                        var newBug = new Bug()
                        {
                            Header = request.Header,
                            Description = request.Description,
                            StatusID = request.StatusID,
                            Type = request.Type,
                            PriorityID = request.PriorityID,
                            CreatedByID = currentUser.UserGuid,
                            CreatedDate = newJobDate,
                            ClientID = request.ClientID,
                            RaisedDate = request.RaisedDate?.ToUniversalTime() ?? newJobDate
                        };

                        // add new bug
                        var bug = await jobRepository.CreateBugAsync(newBug);
                        jobID = bug.JobID;
                        id = bug.BugID;

                        break;
                    case "Query":
                        // creaate new Query
                        var newQuery = new Query()
                        {
                            Header = request.Header,
                            Description = request.Description,
                            StatusID = request.StatusID,
                            Type = request.Type,
                            PriorityID = request.PriorityID,
                            CreatedByID = currentUser.UserGuid,
                            CreatedDate = newJobDate,
                            ClientID = request.ClientID,
                            RaisedDate = request.RaisedDate?.ToUniversalTime() ?? newJobDate
                        };

                        // add new query
                        var query = await jobRepository.CreateQueryAsync(newQuery);
                        jobID = query.JobID;
                        id = query.QueryID;

                        break;
                    default:
                        throw new InvalidJobTypeException();

                }

                // link products to this job
                var jobProductLinks = request.Products.Select(item => new JobProductLink
                {
                    JobID = jobID,
                    ProductID = item
                });

                // link functionalities to this job
                var jobFunctionalityLinks = request.Functionalities?.Select(item => new JobFunctionalityLink
                {
                    JobID = jobID,
                    FunctionalityID = item
                });

                // link products to job
                await jobRepository.AddJobProductLinksAsync(jobProductLinks);

                if(jobFunctionalityLinks is not null)
                    await jobRepository.AddJobFunctionalityLinksAsync(jobFunctionalityLinks);

                // add files if such was requested for add
                if (request.Files is not null && request.Files.Any())
                {

                    var jobFiles = request.Files.Select(file => new JobFile()
                    {
                        JobID = jobID,
                        Name = file.FileName,
                        Size = file.Length,
                        Extension = Path.GetExtension(file.FileName),
                        AddedByID = currentUser.UserGuid,
                        AddedDate = newJobDate
                    });

                    await jobRepository.AddJobFilesAsync(jobFiles);

                    var typePath = Path.Combine(jobsPath, request.Type);
                    var jobPath = Path.Combine(jobsPath, jobID.ToString());

                    foreach (var item in request.Files)
                    {
                        var filePath = Path.Combine(jobPath, item.FileName);

                        // check if directory exists
                        if (!Directory.Exists(jobPath))
                            Directory.CreateDirectory(jobPath);
                        
                        // save file on server
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await item.CopyToAsync(stream, cancellationToken);
                        }
                        addedFiles.Add(filePath);
                        logger.LogInformation("New file {FileName} was added by: {UserName}", item.FileName, currentUser.UserName);
                    }
                }
                logger.LogInformation("New {Type}: {id} was created by: {UserName}", request.Type.ToLower(), id, currentUser.UserName);
                transactionScope.Complete();
            }
            catch
            {
                foreach (var item in addedFiles)
                {
                    if (File.Exists(item))
                    {
                        File.Delete(item);
                        logger.LogInformation("Deleted file {FileName} as part of rollback due to an exception.", Path.GetFileName(item));
                    }
                }

                throw new JobCreationException();
            }
        }
    }
}
