using AutoMapper;
using MediatR;
using sloth.Application.Models.Jobs;
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
        var functionalities = await jobRepository.ListFunctionalitiesAsync();
        var statuses = await jobRepository.ListStatusesAsync();

        var priorityResults = mapper.Map<List<CachePriorityItem>>(priorities.OrderBy(p => p.PriorityLevel)).ToList();
        var productResults = mapper.Map<List<CacheProductItem>>(products).OrderBy(p => p.Name).ToList();
        var clientResults = mapper.Map<List<CacheClientItem>>(clients).OrderBy(p => p.Name).ToList();
        var functionalitiesResult = mapper.Map<List<CacheFunctionalityItem>>(functionalities).OrderBy(p => p.Name).ToList();
        var statusesResult = mapper.Map<List<CacheStatusItem>>(statuses).OrderBy(s => s.Tag).ToList();

        clientResults.Insert(0, new() { Name = "None", ClientID = null });

        var returnItem = new ListJobDataCacheItem()
        {
            Products = productResults,
            Priorities = priorityResults,
            Clients = clientResults,
            Functionalities = functionalitiesResult,
            Statuses = statusesResult
        };

        return returnItem;
    }
}
