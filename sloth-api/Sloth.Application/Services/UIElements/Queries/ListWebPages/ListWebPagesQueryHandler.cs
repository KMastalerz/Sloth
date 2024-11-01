using MediatR;
using Sloth.Domain.Repositories;
using Sloth.Shared.Models.Items;

namespace Sloth.Application.Services.UIElements;
public class ListWebPagesQueryHandler(IUIElementsRepository uIElementsRepository) : IRequestHandler<ListWebPagesQuery, ListWebPageFull>
{
    public Task<ListWebPageFull> Handle(ListWebPagesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
