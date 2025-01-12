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
    public async Task<IEnumerable<Priority>> ListPriorities()
    {
        return await dbContext.Priority.ToListAsync();
    }

    public async Task<Status?> GetStatus(int statusID)
    {
        return await dbContext.Status.SingleOrDefaultAsync(js => js.StatusID == statusID);
    }

    public async Task<Bug> CreateBug(Bug bug)
    {
        await dbContext.Bug.AddAsync(bug);
        await dbContext.SaveChangesAsync();
        return bug;
    }

    public async Task AddJobProductLinks(IEnumerable<JobProductLink> productLinks)
    {
        await dbContext.JobProductLink.AddRangeAsync(productLinks);
        await dbContext.SaveChangesAsync();
    }

    public async Task AddJobFile(JobFile jobFile)
    {
        await dbContext.JobFile.AddAsync(jobFile);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Query> CreateQuery(Query query)
    {
        await dbContext.Query.AddAsync(query);
        await dbContext.SaveChangesAsync();
        return query;
    }
}
