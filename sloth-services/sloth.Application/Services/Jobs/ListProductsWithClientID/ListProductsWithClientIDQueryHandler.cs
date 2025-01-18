using AutoMapper;
using MediatR;
using sloth.Application.Models.Miscellaneous;
using sloth.Domain.Repositories;

namespace sloth.Application.Services.Jobs;
internal class ListProductsWithClientIDQueryHandler(
    IMapper mapper,
    IJobRepository jobRepository
    )
    : IRequestHandler<ListProductsWithClientIDQuery, IEnumerable<ListItem>>
{
    public async Task<IEnumerable<ListItem>> Handle(ListProductsWithClientIDQuery request, CancellationToken cancellationToken)
    {
        var products = await jobRepository.ListProductsWithClientIDAsync(request.ClientID);
        var results = mapper.Map<IEnumerable<ListItem>>(products).OrderBy(p => p.Label).ToList();
        return results;
    }
}
