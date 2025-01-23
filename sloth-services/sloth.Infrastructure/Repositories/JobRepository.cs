using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using sloth.Domain.Entities;
using sloth.Domain.Models.Jobs;
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
        return await dbContext.Client.Include(c => c.Products).ToListAsync();
    }
    public async Task<IEnumerable<ProductFunctionality>> ListFunctionalitiesAsync()
    {
        return await dbContext.ProductFunctionality
            .Include(f=>f.Product).ToListAsync();
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
    public async Task AddJobFunctionalityLinksAsync(IEnumerable<JobFunctionalityLink> functionalityLinks)
    {
        await dbContext.JobFunctionalityLink.AddRangeAsync(functionalityLinks);
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
    public async Task<ListBugItemRepositoryResponse> ListBugsAsync(ListBugFilters filters)
    {
        var filteredQuery = dbContext.Bug
            .Include(b => b.CreatedBy)
            .Include(b => b.UpdatedBy)
            .Include(b => b.Priority)
            .Include(b => b.Status)
            .Include(b => b.Products)
            .Include(b => b.Functionalities)
            .Include(b => b.Client)
            .AsQueryable();

        if (filters.BugID is not null)
        {
            filteredQuery = filteredQuery.Where(b =>
                b.BugID == filters.BugID);
        }

        if (filters.IsRTS is not null)
        {
            if((bool)filters.IsRTS)
            {
                filteredQuery = filteredQuery.Where(b => b.ClientID != null);
            }
            else
            {
                filteredQuery = filteredQuery.Where(b => b.ClientID == null);
            }
        }

        if (filters.IsBlocker is not null)
        {
            filteredQuery = filteredQuery.Where(b =>
            b.IsBlocker == filters.IsBlocker);
        }

        if (filters.IsClosed is not null)
        {
            filteredQuery = filteredQuery.Where(b =>
            b.IsClosed == filters.IsClosed);
        }

        if (filters.CreatedDateStart is not null)
        {
            filteredQuery = filteredQuery.Where(b =>
                b.CreatedDate >= filters.CreatedDateStart);
        }

        if (filters.CreatedDateEnd is not null)
        {
            filteredQuery = filteredQuery.Where(b =>
                b.CreatedDate <= filters.CreatedDateEnd);
        }

        if (filters.UpdatedDateStart is not null)
        {
            filteredQuery = filteredQuery.Where(b =>
                b.UpdatedDate >= filters.UpdatedDateStart);
        }

        if (filters.UpdatedDateEnd is not null)
        {
            filteredQuery = filteredQuery.Where(b =>
                b.UpdatedDate <= filters.UpdatedDateEnd);
        }

        if (filters.ClosedDateStart is not null)
        {
            filteredQuery = filteredQuery.Where(b =>
                b.ClosedDate <= filters.ClosedDateStart);
        }

        if (filters.ClosedDateEnd is not null)
        {
            filteredQuery = filteredQuery.Where(b =>
                b.ClosedDate <= filters.ClosedDateEnd);
        }

        if (filters.RaisedDateStart is not null)
        {
            filteredQuery = filteredQuery.Where(b =>
                b.RaisedDate <= filters.RaisedDateStart);
        }

        if (filters.RaisedDateEnd is not null)
        {
            filteredQuery = filteredQuery.Where(b =>
                b.RaisedDate <= filters.RaisedDateEnd);
        }

        if (!string.IsNullOrEmpty(filters.InquiryNumber))
        {
            string inquiryNumber = filters.InquiryNumber.ToLower();

            filteredQuery = filteredQuery.Where(b =>
                b.InquiryNumber != null &&
                b.InquiryNumber.ToLower().Contains(inquiryNumber));
        }

        if (!string.IsNullOrEmpty(filters.Header))
        {
            string header = filters.Header.ToLower();

            filteredQuery = filteredQuery.Where(b =>
                !string.IsNullOrEmpty(b.Header) &&
                b.Header.ToLower().Contains(header));
        }

        if (!string.IsNullOrEmpty(filters.Description))
        {
            string description = filters.Description.ToLower();

            filteredQuery = filteredQuery.Where(b =>
                string.IsNullOrEmpty(b.Description) &&
                b.Description.ToLower().Contains(description));
        }

        if (filters.Products != null && filters.Products.Any())
        {
            filteredQuery = filteredQuery.Where(b =>
            b.Products.Any(p => filters.Products.Contains(p.ProductID)));
        }

        if (filters.Functionalities != null && filters.Functionalities.Any())
        {
            filteredQuery = filteredQuery.Where(b =>
            b.Functionalities.Any(p => filters.Functionalities.Contains(p.FunctionalityID)));
        }

        if (filters.Clients != null && filters.Clients.Any())
        {
            filteredQuery = filteredQuery.Where(b =>
            b.Client != null &&
            filters.Clients.Contains(b.Client.ClientID));
        }

        var totalCount = await filteredQuery.CountAsync();

        if (filters.PageID < 1) filters.PageID = 1;

        var results = await filteredQuery
            .Skip((filters.PageID - 1) * filters.TakeCount)
            .Take(filters.TakeCount)
            .ToListAsync();

        return new(){
            TotalCount = totalCount,
            Bugs = results
        };
    }
    public async Task<int> GetInitialStatusAsync(string type)
    {
        var result = await dbContext.Status
            .Where(s => s.IsInitial &&
                       (s.Type.ToLower() == type.ToLower() || s.Type.ToLower() == "all"))
            .OrderByDescending(s => s.Type.ToLower() == type.ToLower()) // True values sort first
            .Select(s => s.StatusID)
            .FirstAsync();

        return result; 
    }
    public async Task<Bug?> GetBugAsync(int bugID)
    {
        var result = await dbContext.Bug
            .Include(b => b.CurrentTeam)
            .Include(b => b.CreatedBy)
            .Include(b => b.ClosedBy)
            .Include(b => b.Client)
            .Include(b => b.UpdatedBy)
            .Include(b => b.Priority)
            .Include(b => b.Status)
            .Include(b => b.Comments.OrderByDescending(c=>c.CommentDate))
                .ThenInclude(c => c.CommentedBy)
            .Include(b => b.Comments)
                .ThenInclude(c => c.PreviousEdits)
            .Include(b => b.AssignmentHistory)
                .ThenInclude(ah => ah.User)
            .Include(b => b.AssignmentHistory)
                .ThenInclude(ah => ah.ChangedBy)
            .Include(b => b.Assignments)
                .ThenInclude(ah => ah.User)
            .Include(b => b.Assignments)
                .ThenInclude(ah => ah.AssignedBy)
            .Include(b => b.Files)
                .ThenInclude(f => f.AddedBy)
            .Include(b => b.PriorityHistory)
                .ThenInclude(ph => ph.ChangedBy)
            .Include(b => b.PriorityHistory)
                .ThenInclude(ph => ph.NewPriority)
            .Include(b => b.StatusHistory)
                .ThenInclude(sh => sh.ChangedBy)
            .Include(b => b.StatusHistory)
                .ThenInclude(sh => sh.NewStatus)
            .Include(b => b.Products)
            .Include(b => b.Functionalities)
            .FirstOrDefaultAsync(b=> b.BugID == bugID);

        return result;
    }
    public async Task AddJobCommentAsync(JobComment jobComment)
    {
        await dbContext.JobComment.AddAsync(jobComment);
        await dbContext.SaveChangesAsync();
    }
    public async Task<IEnumerable<JobComment>> ListJobCommentsAsync(int jobID)
    {
        var results = await dbContext.JobComment
            .Include(c=>c.CommentedBy)
            .Where(c => c.JobID == jobID)
            .OrderByDescending(c => c.CommentDate)
            .ToListAsync();
        return results;
    }
    public async Task<IEnumerable<Status>> ListStatusesAsync()
    {
        var result = await dbContext.Status.ToListAsync();
        return result;
    }
    public async Task DeleteBugAsync(Bug bug)
    {
        dbContext.Bug.Remove(bug);
        await dbContext.SaveChangesAsync();
    }
    public async Task AddJobAssignmentAsync(JobAssignment assignment)
    {
        await dbContext.JobAssignment.AddAsync(assignment);
        await dbContext.SaveChangesAsync();
    }
    public async Task<IEnumerable<JobAssignment>> ListAssignmentsAsync(int jobID)
    {
        return await dbContext.JobAssignment
            .Include(a => a.AssignedBy)
            .Include(a => a.User)
            .Where(a => a.JobID == jobID)
            .ToListAsync();
    }
    public async Task<JobAssignment> GetJobAssignment(int jobID, Guid userID)
    {
        return await dbContext.JobAssignment
            .Include(a => a.AssignedBy)
            .Include(a => a.User)
            .Where(a => a.JobID == jobID && a.UserID == userID)
            .FirstAsync();
    }
    public async Task RemoveJobAssignmentAsync(JobAssignment assignment)
    {
        dbContext.JobAssignment.Remove(assignment);
        await dbContext.SaveChangesAsync();
    }
    public async Task AddJobAssignmentHistoryAsync(JobAssignmentHistory assignmentHistory)
    {
        await dbContext.JobAssignmentHistory.AddAsync(assignmentHistory);
        await dbContext.SaveChangesAsync();
    }
    public async Task<Priority?> GetPriorityAsync(int priorityID)
    {
        var result = await dbContext.Priority.FirstOrDefaultAsync(p => p.PriorityID == priorityID);
        return result;
    }
    public async Task AddJobStatusHistoryAsync(JobStatusHistory statusHistory)
    {
        await dbContext.JobStatusHistory.AddAsync(statusHistory);
        await dbContext.SaveChangesAsync();
    }
    public async Task AddJobPriorityHistoryAsync(JobPriorityHistory priorityHistory)
    {
        await dbContext.JobPriorityHistory.AddAsync(priorityHistory);
        await dbContext.SaveChangesAsync();
    }
    public async Task UpdateBugAsync(Bug bug)
    {
        await dbContext.SaveChangesAsync();
    }
    public async Task<int> GetBugCountByUserAsync(Guid userID)
    {
        var result = await dbContext.Bug
            .Include(b => b.Assignments)
            .Where(b => b.Assignments.Any(a => a.UserID == userID))
            .CountAsync();

        return result;
    }
}
