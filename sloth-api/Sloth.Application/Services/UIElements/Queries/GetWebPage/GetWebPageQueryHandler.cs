using MediatR;
using Microsoft.Extensions.Logging;
using Sloth.Application.UserIdentity;
using Sloth.Domain.Constants;
using Sloth.Application.DTO;
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
        var neededSecTables = webControls.Where(wp => wp.SecurityTableID is not null).Select(wc => wc.SecurityTableID).Distinct()
            .Union(webPanels.Where(wp => wp.SecurityTableID is not null).Select(wp => wp.SecurityTableID).Distinct());

        if (webPage.SecurityTableID is not null)
            neededSecTables.Union([webPage.SecurityTableID]);

        // Get security tables
        var secTables = await securityRepository.ListControlSecurityAsync(WebSecurity.Default, neededSecTables as IEnumerable<string>) ?? [];

        var dto = mapper.Map<GetWebPage>(webPage);
        // Map the raw panels to DTOs
        var dtoPanels = mapper.Map<IEnumerable<GetWebPanel>>(webPanels).ToList();

        // Split the panelsOrder to get the order array
        var panelsOrder = dto.Panels.Split(',');

        // Sort dtoPanels based on the order in panelsOrder and create a list of GetWebPanel
        dto.WebPanels = panelsOrder
            .Select(order => dtoPanels.FirstOrDefault(panel => panel.PanelID == order.Trim())) 
            .Where(panel => panel != null)
            .Cast<GetWebPanel>()
            .ToList();


        // For each WebPanel, map WebSections and respect the order
        dto.WebPanels.ForEach(wp =>
        {
            // Assuming `sections` in WebPanel contains the order
            var controlOrder = wp.Controls.Split(',');

            // Map WebControls and order them based on `sectionOrder`
            wp.WebControls = controlOrder
                .Select(order => webControls.FirstOrDefault(wc => wc.ControlID == order.Trim() && wc.PanelID == wp.PanelID))
                .Where(wc => wc != null)
                .Select(wc => mapper.Map<GetWebControl>(wc))
                .ToList();
        });

        // Create dictionaries for faster lookups
        var wpDict = webPanels.ToDictionary(w => w.PanelID, w => w.SecurityTableID);
        var wcDict = webControls.ToDictionary(w => new { w.ControlID, w.SectionID, w.PanelID }, w => w.SecurityTableID);

        dto.WebPanels.ForEach(wp =>
        {
            wp.WebControls.ForEach(wc =>
            {
                // Look up security table IDs using dictionaries for fast access
                var wcSecTable = wcDict.TryGetValue(new { wc.ControlID, wc.SectionID, wc.PanelID }, out var wcSec) ? wcSec : null;
                var wpSecTable = wpDict.TryGetValue(wp.PanelID, out var wpSec) ? wpSec : null;

                // Determine the effective security table ID
                var searchedSecTable = wcSecTable ?? wpSecTable ?? webPage.SecurityTableID;

                if (searchedSecTable is not null)
                {
                    var secTable = secTables.FirstOrDefault(st => st.SecurityTableID == searchedSecTable && st.ControlID == wc.ControlID);

                    if (secTable is not null)
                    {
                        wc.IsDisabled = secTable.IsDisabled;
                        wc.IsReadOnly = secTable.IsReadOnly;
                        wc.IsRequired = secTable.IsRequired;
                        wc.IsHidden = secTable.IsHidden;
                    }
                }
            });
        });

        // Build the WebSections
        dto.WebPanels.ForEach(wp =>
        {
            var orderedSections = wp.Sections?.Split(',');
            if(orderedSections is not null)
            {
                wp.WebSections = orderedSections
                    .Select(order => webSections.FirstOrDefault(ws => ws.SectionID == order.Trim() && ws.PanelID == wp.PanelID))
                    .Where(ws => ws != null)
                    .Select(ws => mapper.Map<GetWebSection>(ws))
                    .ToList();
            }

            wp.WebSections.ForEach(ws =>
            {
                var controlsOrdered = ws.Controls.Split(',');

                ws.WebControls = controlsOrdered
                    .Select(order => webControls.FirstOrDefault(ws => ws.SectionID == ws.SectionID && ws.ControlID == order.Trim() && ws.PanelID == wp.PanelID))
                    .Where(ws => ws != null)
                    .Select(ws => mapper.Map<GetWebControl>(ws))
                    .ToList();
            });
        });


        // TODO: Add Translations

        return dto;
    }
}
