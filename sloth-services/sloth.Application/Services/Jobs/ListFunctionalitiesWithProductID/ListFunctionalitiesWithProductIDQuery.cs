using MediatR;
using sloth.Application.Models.Jobs;

namespace sloth.Application.Services.Jobs;
public class ListFunctionalitiesWithProductIDQuery(IEnumerable<int>? productIDs) : IRequest<IEnumerable<CacheFunctionalityItem>>
{
    public IEnumerable<int>? ProductIDs { get; set; } = productIDs;
}
