using AutoMapper;
using MediatR;
using sloth.Application.Models.Jobs;
using sloth.Application.Models.Miscellaneous;
using sloth.Domain.Repositories;

namespace sloth.Application.Services.Jobs;
internal class ListProductsWithClientIDCommandHandler(
    IMapper mapper,
    IJobRepository jobRepository
    )
    : IRequestHandler<ListProductsWithClientIDCommand, ListProductsWithClientIDItem>
{
    public async Task<ListProductsWithClientIDItem> Handle(ListProductsWithClientIDCommand request, CancellationToken cancellationToken)
    {
        var products = await jobRepository.ListProductsWithClientIDAsync(request.ClientID);
        var results = mapper.Map<IEnumerable<ListItem>>(products);
        return new(results);
    }
}
