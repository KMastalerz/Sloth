using AutoMapper;
using MediatR;
using sloth.Application.Models.Miscellaneous;
using sloth.Domain.Repositories;

namespace sloth.Application.Services.Jobs;
public class ListFunctionalitiesWithProductIDQueryHandler(
    IMapper mapper, 
    IJobRepository jobRepository
    ) : IRequestHandler<ListFunctionalitiesWithProductIDQuery, IEnumerable<ListItem>>
{
    public async Task<IEnumerable<ListItem>> Handle(ListFunctionalitiesWithProductIDQuery request, CancellationToken cancellationToken)
    {
        var functionalities = await jobRepository.ListFunctionalitiesWithProductIDAsync(request.ProductIDs);
        var results = mapper.Map<IEnumerable<ListItem>>(functionalities).OrderBy(p => p.Label).ToList();
        return results;
    }
}
