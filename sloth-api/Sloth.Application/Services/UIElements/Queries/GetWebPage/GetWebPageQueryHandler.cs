using MediatR;
using Microsoft.Extensions.Logging;
using Sloth.Application.UserIdentity;
using Sloth.Domain.Constants;
using Sloth.Shared.DTO;
using Sloth.Domain.Entities;
using Sloth.Domain.Exceptions;
using Sloth.Domain.Repositories;
using AutoMapper;
using System.Security.Claims;

namespace Sloth.Application.Services.UIElements;
public class GetWebPageQueryHandler(ILogger<GetWebPageQueryHandler> logger, IUserContext userContext, IUIElementsRepository uIElementsRepository, ISecurityRepository securityRepository, IMapper mapper) : IRequestHandler<GetWebPageQuery, GetWebPage>
{
    public async Task<GetWebPage> Handle(GetWebPageQuery request, CancellationToken cancellationToken)
    {
        if(!request.BypassSecurity)
        {
            var currentUser = userContext.GetCurrentUser();

            logger.LogInformation("User: {UserID} is trying to access the page {PageID}", currentUser!.UserID, request.PageID);
            // Check if user can access this page, as WebPage has to co-exist with WebPageSecurity this is a valid check
            if (currentUser?.UserGroup is null)
                throw new NotFoundException(nameof(Claim), SlothClaimTypes.Group);

            // Get page security table for user
            var webPageSecurity = await securityRepository.GetWebPageSecurityAsync(request.PageID, currentUser.UserGroup);
            if (webPageSecurity is null)
                throw new MissingAccessException(nameof(WebPage), request.PageID);

            if (currentUser?.UserGroup is null)
                throw new NotFoundException(nameof(Claim), SlothClaimTypes.Group);
        }

        var webPage = await uIElementsRepository.GetWebPageAsync(request.AppID, request.PageID) ??
            throw new NotFoundException(nameof(WebPage), request.PageID);

        var webPanels = await uIElementsRepository.ListWebPanelAsync(request.AppID, request.PageID) ?? [];

        var webSections = await uIElementsRepository.ListWebSectionAsync(request.AppID, request.PageID) ?? [];

        var webControls = await uIElementsRepository.ListWebControlAsync(request.AppID, request.PageID) ?? [];


        // Build list of needed security tables
        var neededSecTables = webControls.Where(control => control.SecurityTableID is not null).Select(control => control.SecurityTableID).Distinct()
            .Union(webPanels.Where(panel => panel.SecurityTableID is not null).Select(panel => panel.SecurityTableID).Distinct());

        if (webPage.SecurityTableID is not null)
            neededSecTables.Union([webPage.SecurityTableID]);

        // Get security tables
        var secTables = await securityRepository.ListControlSecurityAsync(WebSecurity.Default, neededSecTables as IEnumerable<string>) ?? [];

        var dto = mapper.Map<GetWebPage>(webPage);
        // Map the raw panels to DTOs
        var dtoPanels = mapper.Map<IEnumerable<GetWebPanel>>(webPanels).ToList();

        // Split the panelsOrder to get the order array
        var panelsOrder = dto.Panels?.Split(',');

        // Sort dtoPanels based on the order in panelsOrder and create a list of GetWebPanel
        if(panelsOrder is not null)
            dto.WebPanels = panelsOrder
                .Select(order => dtoPanels.FirstOrDefault(panel => panel.PanelID == order.Trim())) 
                .Where(panel => panel != null)
                .Cast<GetWebPanel>()
                .ToList();



        // Create dictionaries for faster lookups
        var wpDict = webPanels.ToDictionary(panel => panel.PanelID, panel => panel.SecurityTableID);
        var wcDict = webControls.ToDictionary(control => new { control.ControlID, control.SectionID, control.PanelID }, control => control.SecurityTableID);

        // Build the WebSections
        dto.WebPanels?.ForEach(panel =>
        {
            var orderedSections = panel.Sections?.Split(',');
            if(orderedSections is not null)
            {
                panel.WebSections = orderedSections
                    .Select(order => webSections.FirstOrDefault(section => section.SectionID == order.Trim() && section.PanelID == panel.PanelID))
                    .Where(section => section != null)
                    .Select(section => mapper.Map<GetWebSection>(section))
                    .ToList();
            }

            panel.WebSections?.ForEach(section =>
            {
                var controlsOrdered = section.Controls?.Split(',');

                if(controlsOrdered is not null)
                    section.WebControls = controlsOrdered
                        .Select(order => webControls.FirstOrDefault(control => control.SectionID == section.SectionID && control.ControlID == order.Trim() && section.PanelID == panel.PanelID))
                        .Where(section => section != null)
                        .Select(section => mapper.Map<GetWebControl>(section))
                        .ToList();
            });
        });


        // TODO: Add Translations

        return dto;
    }
}
