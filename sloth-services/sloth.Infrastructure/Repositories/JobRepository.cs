using Microsoft.EntityFrameworkCore;
using sloth.Domain.Entities;
using sloth.Domain.Repositories;
using sloth.Infrastructure.DatabaseContext;

namespace sloth.Infrastructure.Repositories;
internal class JobRepository(SlothDbContext dbContext) : IJobRepository
{
    public async Task<IEnumerable<Product>> ListProducts()
    {
        return await dbContext.Product.ToListAsync();
    }
    public async Task<IEnumerable<JobPriority>> ListJobPriorities()
    {
        return await dbContext.JobPriority.ToListAsync();
    }

    public async Task<JobStatus?> GetStatus(int statusID)
    {
        return await dbContext.JobStatus.SingleOrDefaultAsync(js => js.JobStatusID == statusID);
    }

    public async Task<Job> CreateJob(Job job)
    {
        await dbContext.Job.AddAsync(job);
        return job;
    }

    public async Task AddJobProductLinks(IEnumerable<JobProductLink> productLinks)
    {
        await dbContext.JobProductLink.AddRangeAsync(productLinks);
    }

    public async Task AddJobFile(JobFile jobFile)
    {
        await dbContext.JobFile.AddAsync(jobFile);
    }
}
