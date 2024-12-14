using AutoMapper;
using MediatR;
using Sloth.Shared.DTO;
using Sloth.Domain.Repositories;

namespace Sloth.Application.Services.UIElements;
public class ListWebPageByIDQueryHandler(IUIElementsRepository uIElementsRepository, IMapper mapper) : IRequestHandler<ListWebPageByIDQuery, IEnumerable<ListWebPageItem>?>
{
    public async Task<IEnumerable<ListWebPageItem>?> Handle(ListWebPageByIDQuery request, CancellationToken cancellationToken)
    {
        var webPages = await uIElementsRepository.ListWebPageByIDAsync(request.AppID, request.PageID);

        var result = mapper.Map<IEnumerable<ListWebPageItem>>(webPages);
        return result;
    }
}
