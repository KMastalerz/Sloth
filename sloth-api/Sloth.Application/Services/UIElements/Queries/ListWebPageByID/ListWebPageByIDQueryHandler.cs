using AutoMapper;
using MediatR;
using Sloth.Application.DTO;
using Sloth.Domain.Repositories;

namespace Sloth.Application.Services.UIElements;
public class ListWebPageByIDQueryHandler(IUIElementsRepository uIElementsRepository, IMapper mapper) : IRequestHandler<ListWebPageByIDQuery, IEnumerable<DesignerListWebPageItem>?>
{
    public async Task<IEnumerable<DesignerListWebPageItem>?> Handle(ListWebPageByIDQuery request, CancellationToken cancellationToken)
    {
        var webPages = await uIElementsRepository.ListWebPageByIDAsync(request.AppID, request.PageID);

        var result = mapper.Map<IEnumerable<DesignerListWebPageItem>>(webPages);
        return result;
    }
}
