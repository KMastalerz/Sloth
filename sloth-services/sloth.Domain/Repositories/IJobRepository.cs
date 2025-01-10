using sloth.Domain.Entities;

namespace sloth.Domain.Repositories;
public interface IJobRepository
{
    Task<IEnumerable<JobPriority>> ListJobPriorities();
    Task<IEnumerable<Product>> ListProducts();
}