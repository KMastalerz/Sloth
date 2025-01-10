using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using sloth.Application.Models.Job;
using sloth.Application.Models.Miscellaneous;
using sloth.Domain.Repositories;

namespace sloth.Application.Services.Job.ListJobDataCache;
public class ListJobDataCacheQueryHandler(
    ILogger<ListJobDataCacheQueryHandler> logger,
    IMapper mapper,
    IJobRepository jobRepository
    ) : IRequestHandler<ListJobDataCacheQuery, ListJobDataCacheItem>
{
    public async Task<ListJobDataCacheItem> Handle(ListJobDataCacheQuery request, CancellationToken cancellationToken)
    {
        var priorities = await jobRepository.ListJobPriorities();
        var products = await jobRepository.ListProducts();

        var returnItem = new ListJobDataCacheItem()
        {
            Products = mapper.Map<List<ListItem>>(products),
            JobPriorities = mapper.Map<List<ToggleItem>>(priorities)
        };

        return returnItem;
    }
}
