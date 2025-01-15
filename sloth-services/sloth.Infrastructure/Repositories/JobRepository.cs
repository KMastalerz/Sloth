using Microsoft.EntityFrameworkCore;
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

    public async Task<IEnumerable<Bug>?> ListBugsAsync(ListBugFilters filters)
    {
        var filteredQuery = dbContext.Bug
            .Include(b => b.CurrentOwner)
            .Include(b => b.CreatedBy)
            .Include(b => b.UpdatedBy)
            .AsQueryable();

        if (filters.BugID is not null)
        {
            filteredQuery = filteredQuery.Where(b =>
                b.BugID == filters.BugID);
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

        if (!string.IsNullOrEmpty(filters.CurrentOwner))
        {
            string currentOwner = filters.CurrentOwner.ToLower();

            filteredQuery = filteredQuery.Where(b =>
                b.CurrentOwner != null &&
                b.CurrentOwner.FullName != null &&
                b.CurrentOwner.FullName.ToLower().Contains(currentOwner));
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
            filteredQuery = filteredQuery.Include(b => b.Products);

            filteredQuery = filteredQuery.Where(b =>
            b.Products.Any(p => filters.Products.Contains(p.ProductID)));
        }

        if (filters.Functionalities != null && filters.Functionalities.Any())
        {
            filteredQuery = filteredQuery.Include(b => b.Functionalities);

            filteredQuery = filteredQuery.Where(b =>
            b.Functionalities.Any(p => filters.Functionalities.Contains(p.FunctionalityID)));
        }

        if (filters.Clients != null && filters.Clients.Any())
        {
            filteredQuery = filteredQuery.Include(b => b.Client);

            filteredQuery = filteredQuery.Where(b =>
            b.Client != null &&
            filters.Clients.Contains(b.Client.ClientID));
        }

        var results = await filteredQuery
            .Skip((filters.PageID - 1) * filters.TakeCount)
            .Take(filters.TakeCount)
            .ToListAsync();

        return results;
    }
}
