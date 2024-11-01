using AutoMapper;
using MediatR;
using Sloth.Domain.Repositories;
using Sloth.Shared.Helpers;
using Sloth.Shared.Models;

namespace Sloth.Application.Services.UIElements;
public class GetFullWebPageQueryHandler(IUIElementsRepository uIElementsRepository, IMapper mapper) : IRequestHandler<GetFullWebPageQuery, WebPageItem?>
{
    public async Task<WebPageItem?> Handle(GetFullWebPageQuery request, CancellationToken cancellationToken)
    {
        var webPage = await uIElementsRepository.GetFullWebPageAsync(request.AppID, request.PageID);

        var result = mapper.Map<WebPageItem>(webPage);

        if (result is null) return null;

        var orderedPanels = result.Panels?.Split(',');

        if(orderedPanels is not null) 
            if (ListHelpers.HasElements(orderedPanels))
            {
                result.WebPanels = new(orderedPanels
                    .Select(order => result.WebPanels
                    .FirstOrDefault(panel => panel.PanelID == order.Trim()))
                       .Where(panel => panel != null)
                       .Cast<WebPanelItem>());

                result.WebPanels.ToList().ForEach(panel =>
                {
                    // check if panel has sections, as section can be null
                    if (!string.IsNullOrEmpty(panel.Sections))
                    {
                        var orderedSections = panel.Sections.Split(',');
                        if (ListHelpers.HasElements(orderedSections))
                        {
                            panel.WebSections = new(orderedSections
                                .Select(order => panel.WebSections
                                .FirstOrDefault(section => section.SectionID == order.Trim()))
                                   .Where(section => section != null)
                                   .Cast<WebSectionItem>());
                        }

                        panel.WebSections.ToList().ForEach(section =>
                        {
                            var orderedControls = section.Controls?.Split(',');

                            if(orderedControls is not null)
                                if (ListHelpers.HasElements(orderedControls))
                                {
                                    section.WebControls = new(orderedControls
                                        .Select(order => section.WebControls
                                        .FirstOrDefault(control => control.ControlID == order.Trim()))
                                           .Where(control => control != null)
                                           .Cast<WebControlItem>());
                                }
                        });
                    }
                });
            }

        return result;
    }
}
