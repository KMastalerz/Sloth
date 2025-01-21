using AutoMapper;
using MediatR;
using sloth.Application.Models.Jobs;
using sloth.Domain.Repositories;

namespace sloth.Application.Services.Jobs;
internal class ListProductsWithClientIDQueryHandler(
    IMapper mapper,
    IJobRepository jobRepository
    )
    : IRequestHandler<ListProductsWithClientIDQuery, IEnumerable<CacheProductItem>>
{
    public async Task<IEnumerable<CacheProductItem>> Handle(ListProductsWithClientIDQuery request, CancellationToken cancellationToken)
    {
        var products = await jobRepository.ListProductsWithClientIDAsync(request.ClientID);
        var results = mapper.Map<IEnumerable<CacheProductItem>>(products).OrderBy(p => p.Name).ToList();
        return results;
    }
}
