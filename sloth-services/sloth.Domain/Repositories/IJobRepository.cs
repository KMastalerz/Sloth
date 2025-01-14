using sloth.Domain.Entities;

namespace sloth.Domain.Repositories;
public interface IJobRepository
{
    Task<IEnumerable<Priority>> ListPrioritiesAsync();
    Task<IEnumerable<Product>> ListProductsAsync();
    Task<IEnumerable<Client>> ListClientsAsync();
    Task<Status?> GetStatusAsync(int statusID);
    Task<Bug> CreateBugAsync(Bug bug);
    Task AddJobProductLinksAsync(IEnumerable<JobProductLink> productLinks);
    Task AddJobFilesAsync(IEnumerable<JobFile> jobFiles);
    Task<Query> CreateQueryAsync(Query query);
    Task<IEnumerable<Product>?> ListProductsWithClientIDAsync(Guid? clientID);
}