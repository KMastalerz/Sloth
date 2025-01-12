using AutoMapper;
using MediatR;
using sloth.Application.Models.Jobs;
using sloth.Application.Models.Miscellaneous;
using sloth.Domain.Repositories;

namespace sloth.Application.Services.Jobs.ListJobDataCache;
public class ListJobDataCacheQueryHandler(
    IMapper mapper,
    IJobRepository jobRepository
    ) : IRequestHandler<ListJobDataCacheQuery, ListJobDataCacheItem>
{
    public async Task<ListJobDataCacheItem> Handle(ListJobDataCacheQuery request, CancellationToken cancellationToken)
    {
        var priorities = await jobRepository.ListPriorities();
        var products = await jobRepository.ListProducts();

        var returnItem = new ListJobDataCacheItem()
        {
            Products = mapper.Map<List<ListItem>>(products),
            JobPriorities = mapper.Map<List<ToggleItem>>(priorities)
        };

        return returnItem;
    }
}
