using sloth.Domain.Entities;

namespace sloth.Domain.Repositories;
public interface IJobRepository
{
    Task<IEnumerable<Priority>> ListPriorities();
    Task<IEnumerable<Product>> ListProducts();
    Task<Status?> GetStatus(int statusID);
    Task<Bug> CreateBug (Bug bug);
    Task AddJobProductLinks(IEnumerable<JobProductLink> productLinks);
    Task AddJobFile (JobFile jobFile);
    Task<Query> CreateQuery (Query query);
}