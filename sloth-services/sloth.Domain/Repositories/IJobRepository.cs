﻿using sloth.Domain.Entities;
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
    Task<ListBugItemRepositoryResponse> ListBugsAsync(ListBugFilters filters);
    Task<int> GetInitialStatusAsync(string type);
    Task<Bug?> GetBugAsync (int bugID);
    Task AddJobCommentAsync(JobComment jobComment);
    Task<IEnumerable<JobComment>> ListJobCommentsAsync(int jobID);
    Task<IEnumerable<Status>> ListStatusesAsync();
    Task DeleteBugAsync(Bug bug);
    Task AddJobAssignmentAsync(JobAssignment assignment);
    Task<IEnumerable<JobAssignment>> ListAssignmentsAsync(int jobID);
    Task<JobAssignment> GetJobAssignment(int jobID, Guid userID);
    Task RemoveJobAssignmentAsync(JobAssignment assignment);
    Task<Priority?> GetPriorityAsync(int priorityID);
    Task AddJobHistory(JobHistory jobHistory);
    Task AddJobHistories(IEnumerable<JobHistory> jobHistories);
    Task UpdateBugAsync(Bug bug);
    Task<int> GetBugCountByUserAsync(Guid userID);
    Task<Job?> GetJobAsync (int jobID);
    Task<Bug?> GetBugByJobIDAsync(int jobID);
    Task DeleteLinkedJobsAsync (int jobID);
    Task<Client?> GetClientByIDAsync (Guid clientID);
}