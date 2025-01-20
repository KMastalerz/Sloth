using AutoMapper;
using MediatR;
using sloth.Application.Models.Jobs;
using sloth.Application.UserIdentity;
using sloth.Domain.Repositories;

namespace sloth.Application.Services.Jobs.ListBugDataCache;
public class ListBugDataCacheQueryHandler(
    IMapper mapper,
    IJobRepository jobRepository,
    IUserContext userContext) : IRequestHandler<ListBugDataCacheQuery, ListJobDataCacheItem>
{
    public Task<ListJobDataCacheItem> Handle(ListBugDataCacheQuery request, CancellationToken cancellationToken)
    {
        // get client full list, 
        // get statuses available for user
        // get priorities
        // get functionalities

        throw new NotImplementedException();
    }
}
