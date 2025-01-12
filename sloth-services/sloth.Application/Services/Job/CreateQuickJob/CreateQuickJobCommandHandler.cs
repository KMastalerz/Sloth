using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using sloth.Application.Models.Configuration;
using sloth.Application.UserIdentity;
using sloth.Domain.Entities;
using sloth.Domain.Exceptions;
using sloth.Domain.Repositories;
using sloth.Utilities.Constants;
using System.Diagnostics;
using System.Transactions;

namespace sloth.Application.Services.Jobs;
public class CreateQuickJobCommandHandler(
    ILogger<CreateQuickJobCommandHandler> logger,
    IMapper mapper,
    IJobRepository jobRepository,
    IUserContext userContext,
    IConfiguration configuration) 
    : IRequestHandler<CreateQuickJobCommand>
{
    private readonly string jobsPath = Path.Combine(Directory.GetCurrentDirectory(), "Jobs");
    public async Task Handle(CreateQuickJobCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        if (currentUser == null)
            throw new InvalidUserException();

        // get initial project status from IConfiguration
        var appConfig = configuration.GetSection(ConfigurationKeys.Configuration).Get<Configuration>()!;

        // generate creation time
        var newJobDate = DateTime.UtcNow;
        var jobID = 0;
        var id = 0;
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
                        IsClient = request.IsClient,
                        ClientID = request.ClientID,
                    };

                    // add new bug
                    var job = await jobRepository.CreateBug(newBug);
                    jobID = job.JobID;
                    id = job.BugID;
                    break;
                    case "Query":
                        throw new InvalidJobTypeException();
                    default:
                        throw new InvalidJobTypeException();

                }



                // link products to this job
                var jobProductLinks = request.Products.Select(item => new JobProductLink
                {
                    JobID = jobID,
                    ProductID = item
                });

                // link products to job
                await jobRepository.AddJobProductLinks(jobProductLinks);

                // add file if such was requested for add
                if (request.File is not null)
                {
                    var jobFile = new JobFile()
                    {
                        JobID = jobID,
                        Name = request.File.FileName,
                        Size = request.File.Length,
                        Extension = Path.GetExtension(request.File.FileName),
                        AddedByID = currentUser.UserGuid,
                        AddedDate = newJobDate
                    };

                    await jobRepository.AddJobFile(jobFile);

                    var typePath = Path.Combine(jobsPath, request.Type);
                    var filePath = Path.Combine(typePath, request.File.FileName);

                    // check if directory exists
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(typePath);
                    }

                    // save file on server
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.File.CopyToAsync(stream, cancellationToken);
                    }

                    logger.LogInformation("New file {FileName} was added by: {UserName}", request.File.FileName, currentUser.UserName);
                }

                logger.LogInformation("New {Type}: {id} was created by: {UserName}", request.Type.ToLower(), id, currentUser.UserName);

                transactionScope.Complete();
            }
            catch(Exception ex)
            {
                throw new JobCreationException();
            }
        }
    }
}
