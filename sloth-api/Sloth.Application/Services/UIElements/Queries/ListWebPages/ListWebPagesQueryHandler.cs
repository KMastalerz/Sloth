using AutoMapper;
using MediatR;
using Sloth.Shared.DTO;
using Sloth.Domain.Repositories;

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
            WebPages = mapper.Map<IEnumerable<GetWebPageFull>>(webPages),
            WebPanels = mapper.Map<IEnumerable<GetWebPanelFull>>(webPanels),
            WebSections = mapper.Map<IEnumerable<GetWebSectionFull>>(webSections),
            WebControls = mapper.Map<IEnumerable<GetWebControlFull>>(webControls)
        };
    }
}
