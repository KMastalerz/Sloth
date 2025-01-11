using sloth.Domain.Entities;

namespace sloth.Domain.Repositories;
public interface IJobRepository
{
    Task<IEnumerable<JobPriority>> ListJobPriorities();
    Task<IEnumerable<Product>> ListProducts();
    Task<JobStatus?> GetStatus(int statusID);
    Task<Job> CreateJob (Job job);
    Task AddJobProductLinks (IEnumerable<JobProductLink> productLinks);
    Task AddJobFile (JobFile jobFile);
}