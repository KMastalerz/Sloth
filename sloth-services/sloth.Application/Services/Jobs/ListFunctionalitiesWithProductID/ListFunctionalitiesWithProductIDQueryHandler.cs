using AutoMapper;
using MediatR;
using sloth.Application.Models.Jobs;
using sloth.Domain.Repositories;

namespace sloth.Application.Services.Jobs;
public class ListFunctionalitiesWithProductIDQueryHandler(
    IMapper mapper, 
    IJobRepository jobRepository
    ) : IRequestHandler<ListFunctionalitiesWithProductIDQuery, IEnumerable<CacheFunctionalityItem>>
{
    public async Task<IEnumerable<CacheFunctionalityItem>> Handle(ListFunctionalitiesWithProductIDQuery request, CancellationToken cancellationToken)
    {
        var functionalities = await jobRepository.ListFunctionalitiesWithProductIDAsync(request.ProductIDs);
        var results = mapper.Map<IEnumerable<CacheFunctionalityItem>>(functionalities).OrderBy(p => p.Name).ToList();
        return results;
    }
}
