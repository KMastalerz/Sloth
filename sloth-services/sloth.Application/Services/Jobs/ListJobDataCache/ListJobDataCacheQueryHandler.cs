using AutoMapper;
using MediatR;
using sloth.Application.Models.Jobs;
using sloth.Application.Models.Miscellaneous;
using sloth.Domain.Repositories;

namespace sloth.Application.Services.Jobs;
public class ListJobDataCacheQueryHandler(
    IMapper mapper,
    IJobRepository jobRepository
    ) : IRequestHandler<ListJobDataCacheQuery, ListJobDataCacheItem>
{
    public async Task<ListJobDataCacheItem> Handle(ListJobDataCacheQuery request, CancellationToken cancellationToken)
    {
        var priorities = await jobRepository.ListPrioritiesAsync();
        var products = await jobRepository.ListProductsAsync();
        var clients = await jobRepository.ListClientsAsync();

        var returnItem = new ListJobDataCacheItem()
        {
            Products = mapper.Map<List<ListItem>>(products),
            JobPriorities = mapper.Map<List<ToggleItem>>(priorities),
            Clients = mapper.Map<List<ListItem>>(clients)
        };

        return returnItem;
    }
}
