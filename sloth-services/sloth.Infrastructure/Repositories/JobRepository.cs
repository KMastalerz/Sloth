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

}
