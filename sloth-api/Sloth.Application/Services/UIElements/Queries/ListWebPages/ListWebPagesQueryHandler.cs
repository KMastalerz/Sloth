using AutoMapper;
using MediatR;
using Sloth.Domain.Repositories;
using Sloth.Shared.Models;

namespace Sloth.Application.Services.UIElements;
public class ListWebPagesQueryHandler(IUIElementsRepository uIElementsRepository, IMapper mapper) : IRequestHandler<ListWebPagesQuery, ListWebPagesItem>
{
    public async Task<ListWebPagesItem> Handle(ListWebPagesQuery request, CancellationToken cancellationToken)
    {
        var webPages = await uIElementsRepository.ListAllWebPageAsync();
        var webPanels = await uIElementsRepository.ListAllWebPanelAsync();
        var webSections = await uIElementsRepository.ListAllWebSectionAsync();
        var webControls = await uIElementsRepository.ListAllWebControlAsync();

        //return mapper.Map<IEnumerable<WebPageItem>>(webPages);
        return new ListWebPagesItem
        {
            WebPages = mapper.Map<IEnumerable<WebPageItem>>(webPages),
            WebPanels = mapper.Map<IEnumerable<WebPanelItem>>(webPanels),
            WebSections = mapper.Map<IEnumerable<WebSectionItem>>(webSections),
            WebControls = mapper.Map<IEnumerable<WebControlItem>>(webControls)
        };
    }
}
