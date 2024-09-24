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
        if(!request.ByPassSecurity)
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

        var webPage = await uIElementsRepository.GetWebPageAsync(request.PageID) ??
            throw new NotFoundException(nameof(WebPage), request.PageID);

        var webPanels = await uIElementsRepository.ListWebPanelAsync(request.PageID) ??
            throw new NotFoundException(nameof(List<WebPanel>), request.PageID); 

        var webSection = await uIElementsRepository.ListWebSectionAsync(request.PageID) ??
            throw new NotFoundException(nameof(List<WebSection>), request.PageID); 

        var webControls = await uIElementsRepository.ListWebControlAsync(request.PageID) ??
            throw new NotFoundException(nameof(List<WebControl>), request.PageID);

        var neededSecTables = webControls.Where(wp => wp.SecurityTableID is not null).Select(wc => wc.SecurityTableID).Distinct()
            .Union(webPanels.Where(wp => wp.SecurityTableID is not null).Select(wp => wp.SecurityTableID).Distinct())
            .Union(webSection.Where(wp => wp.SecurityTableID is not null).Select(ws => ws.SecurityTableID).Distinct());

        if(webPage.SecurityTableID is not null)
            neededSecTables.Union([webPage.SecurityTableID]);
        
        var secTables = await securityRepository.ListControlSecurityAsync(WebSecurity.Default, neededSecTables as IEnumerable<string>) ?? [];

        var dto = mapper.Map<GetWebPage>(webPage);

        dto.WebPanels = mapper.Map<IEnumerable<GetWebPanel>>(webPanels).ToList();

        dto.WebPanels.ToList().ForEach(
            wp => wp.WebSections = mapper.Map<IEnumerable<GetWebSection>>(webSection.Where(ws => ws.PanelID == wp.PanelID)).ToList());

        dto.WebPanels.ToList().ForEach(
            wp => wp.WebSections.ToList().ForEach(
                ws => ws.WebControls = mapper.Map<IEnumerable<GetWebControl>>(webControls.Where(wc => wc.SectionID == ws.SectionID && wc.PanelID == wp.PanelID)).ToList()));

        // Create dictionaries for faster lookups
        var wpDict = webPanels.ToDictionary(w => w.PanelID, w => w.SecurityTableID);
        var wsDict = webSection.ToDictionary(w => new { w.SectionID, w.PanelID }, w => w.SecurityTableID);
        var wcDict = webControls.ToDictionary(w => new { w.ControlID, w.SectionID, w.PanelID }, w => w.SecurityTableID);

        dto.WebPanels.ForEach(wp =>
        {
            wp.WebSections.ForEach(ws =>
            {
                ws.WebControls.ForEach(wc =>
                {
                    // Look up security table IDs using dictionaries for fast access
                    var wcSecTable = wcDict.TryGetValue(new { wc.ControlID, wc.SectionID, wc.PanelID }, out var wcSec) ? wcSec: null;
                    var wsSecTable = wsDict.TryGetValue(new { ws.SectionID, wp.PanelID }, out var wsSec) ? wsSec: null;
                    var wpSecTable = wpDict.TryGetValue(wp.PanelID, out var wpSec) ? wpSec: null;

                    // Determine the effective security table ID
                    var searchedSecTable = wcSecTable ?? wsSecTable ?? wpSecTable ?? webPage.SecurityTableID;

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
        });

        // TO DO: Add Validators 

        // TO DO: Add Translations

        return dto;
    }
}
