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

        // get initial status from repository
        var initialJobStatus = await jobRepository.GetStatus(appConfig.InitialJobStatus);

        // generate creation time
        var newJobDate = DateTime.UtcNow;
        
        // create new Job
        var newJob = new Job()
        {
            Header = request.Header,
            Description = request.Description,
            CurrentStatus = initialJobStatus?.Status ?? string.Empty,
            Type = request.Type,
            Priority = request.Priority,
            CreatedBy = currentUser.UserGuid,
            CreationDate = newJobDate,
            IsClient = request.IsClient,
            ClientID = request.ClientID,
        };

        using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            try
            {
                // add new job
                var job = await jobRepository.CreateJob(newJob);

                // link products to this job
                var jobProductLinks = request.Products.Select(item => new JobProductLink
                {
                    JobID = job.JobID,
                    ProductID = item
                });

                // link products to job
                await jobRepository.AddJobProductLinks(jobProductLinks);

                // add file if such was requested for add
                if (request.File is not null)
                {
                    var jobFile = new JobFile()
                    {
                        JobID = job.JobID,
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

                logger.LogInformation("New job {JobID} was created by: {UserName}", newJob.JobID, currentUser.UserName);
                transactionScope.Complete();
            }
            catch
            {
                throw new JobCreationException();
            }
        }
    }
}
