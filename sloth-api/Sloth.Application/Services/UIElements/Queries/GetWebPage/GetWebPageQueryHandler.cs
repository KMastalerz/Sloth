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

        var webControls = await uIElementsRepository.ListWebControlAsync(request.PageID) ??
            throw new NotFoundException(nameof(List<WebControl>), request.PageID);


        // Build list of needed security tables
        var neededSecTables = webControls.Where(wp => wp.SecurityTableID is not null).Select(wc => wc.SecurityTableID).Distinct();

        if(webPage.SecurityTableID is not null)
            neededSecTables.Union([webPage.SecurityTableID]);

        // Get security tables
        var secTables = await securityRepository.ListControlSecurityAsync(WebSecurity.Default, neededSecTables as IEnumerable<string>) ?? [];

        var wcDict = webControls.ToDictionary(wc => wc.ControlID, wc => wc.SecurityTableID);

        var dto = mapper.Map<GetWebPage>(webPage);
        dto.WebPanels = mapper.Map<IEnumerable<GetWebPanel>>(webPanels).ToList();
        dto.WebPanels.ForEach(wp =>
        {
            wp.WebControls = mapper.Map<IEnumerable<GetWebControl>>(webControls.Where(wc => wc.PanelID == wp.PanelID)).ToList();
            wp.WebControls.ForEach(wc =>
            {
                var wcSecTable = wcDict.TryGetValue(wc.ControlID, out var wcSec) ? wcSec : null;
                var searchedSecTable = wcSecTable ?? webPage.SecurityTableID;

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

        // TO DO: Add Validators 

        // TO DO: Add Translations

        return dto;
    }
}
