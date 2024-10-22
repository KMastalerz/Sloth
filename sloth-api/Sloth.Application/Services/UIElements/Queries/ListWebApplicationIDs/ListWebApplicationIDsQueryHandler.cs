using MediatR;
using Sloth.Domain.Repositories;

namespace Sloth.Application.Services.UIElements;
public class ListWebApplicationIDsQueryHandler(IUIElementsRepository uIElementsRepository) : IRequestHandler<ListWebApplicationIDsQuery, IEnumerable<string>?>
{
    public async Task<IEnumerable<string>?> Handle(ListWebApplicationIDsQuery request, CancellationToken cancellationToken)
    {
        return await uIElementsRepository.ListWebApplicationIDsAsync();
    }
}
