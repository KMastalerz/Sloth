using MediatR;
using sloth.Application.Models.Miscellaneous;

namespace sloth.Application.Services.Jobs;
public class ListFunctionalitiesWithProductIDQuery(IEnumerable<int>? productIDs) : IRequest<IEnumerable<ListItem>>
{
    public IEnumerable<int>? ProductIDs { get; set; } = productIDs;
}
