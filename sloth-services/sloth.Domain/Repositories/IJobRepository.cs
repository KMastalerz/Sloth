using sloth.Domain.Entities;
using sloth.Domain.Models.Jobs;

namespace sloth.Domain.Repositories;
public interface IJobRepository
{
    Task<IEnumerable<Priority>> ListPrioritiesAsync();
    Task<IEnumerable<Product>> ListProductsAsync();
    Task<IEnumerable<Client>> ListClientsAsync();
    Task<IEnumerable<ProductFunctionality>> ListFunctionalitiesAsync();
    Task<Status?> GetStatusAsync(int statusID);
    Task<Bug> CreateBugAsync(Bug bug);
    Task AddJobProductLinksAsync(IEnumerable<JobProductLink> productLinks);
    Task AddJobFunctionalityLinksAsync(IEnumerable<JobFunctionalityLink> functionalityLinks);
    Task AddJobFilesAsync(IEnumerable<JobFile> jobFiles);
    Task<Query> CreateQueryAsync(Query query);
    Task<IEnumerable<Product>?> ListProductsWithClientIDAsync(Guid? clientID = null);
    Task<IEnumerable<ProductFunctionality>?> ListFunctionalitiesWithProductIDAsync(IEnumerable<int>? productIDs);
    Task<ListBugItemRepositoryResponse> ListBugsAsync(ListBugFilters filters);
    Task<int> GetInitialStatusAsync(string type);
    Task<Bug?> GetBugAsync (int bugID);
    Task AddJobCommentAsync(JobComment jobComment);
    Task<IEnumerable<JobComment>> ListJobCommentsAsync(int jobID);
    Task<IEnumerable<Status>> ListStatusesAsync();
}