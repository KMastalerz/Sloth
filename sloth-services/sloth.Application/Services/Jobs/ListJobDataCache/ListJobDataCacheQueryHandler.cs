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

        var priorityResults = mapper.Map<List<ToggleItem>>(priorities).OrderBy(p => p.Label).ToList();
        var productResults = mapper.Map<List<ListItem>>(products).OrderBy(p => p.Label).ToList();
        var clientResults = mapper.Map<List<ListItem>>(clients).OrderBy(p => p.Label).ToList();

        clientResults.Insert(0, new() { Label = "None", Value = null });

        var returnItem = new ListJobDataCacheItem()
        {
            Products = productResults,
            Priorities = priorityResults,
            Clients = clientResults
        };

        return returnItem;
    }
}
