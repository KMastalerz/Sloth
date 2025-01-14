using Microsoft.EntityFrameworkCore;
using sloth.Domain.Entities;
using sloth.Domain.Repositories;
using sloth.Infrastructure.DatabaseContext;

namespace sloth.Infrastructure.Repositories;
internal class JobRepository(SlothDbContext dbContext) : IJobRepository
{
    public async Task<IEnumerable<Product>> ListProductsAsync()
    {
        return await dbContext.Product.ToListAsync();
    }
    public async Task<IEnumerable<Priority>> ListPrioritiesAsync()
    {
        return await dbContext.Priority.ToListAsync();
    }
    public async Task<IEnumerable<Client>> ListClientsAsync()
    {
        return await dbContext.Client.ToListAsync();
    }

    public async Task<Status?> GetStatusAsync(int statusID)
    {
        return await dbContext.Status.SingleOrDefaultAsync(js => js.StatusID == statusID);
    }

    public async Task<Bug> CreateBugAsync(Bug bug)
    {
        await dbContext.Bug.AddAsync(bug);
        await dbContext.SaveChangesAsync();
        return bug;
    }

    public async Task AddJobProductLinksAsync(IEnumerable<JobProductLink> productLinks)
    {
        await dbContext.JobProductLink.AddRangeAsync(productLinks);
        await dbContext.SaveChangesAsync();
    }

    public async Task AddJobFilesAsync(IEnumerable<JobFile> jobFiles)
    {
        await dbContext.JobFile.AddRangeAsync(jobFiles);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Query> CreateQueryAsync(Query query)
    {
        await dbContext.Query.AddAsync(query);
        await dbContext.SaveChangesAsync();
        return query;
    }

    public async Task<IEnumerable<Product>?> ListProductsWithClientIDAsync(Guid? clientID)
    {
        if (clientID is not null)
        {
            return await dbContext.Client.Include(c => c.Products).Where(c => c.ClientID == clientID).Select(c => c.Products.AsEnumerable()).FirstOrDefaultAsync();
        }
        else
            return await dbContext.Product.ToListAsync();
    }
}
