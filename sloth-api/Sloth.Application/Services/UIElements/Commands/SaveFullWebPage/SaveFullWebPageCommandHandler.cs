using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Sloth.Application.UserIdentity;
using Sloth.Domain.Entities;
using Sloth.Domain.Repositories;
using Sloth.Shared.Helpers;

namespace Sloth.Application.Services.UIElements;
public class SaveFullWebPageCommandHandler(ILogger<SaveFullWebPageCommandHandler> logger, IUIElementsRepository uIElementsRepository, IMapper mapper, IUserContext userContext) : IRequestHandler<SaveFullWebPageCommand>
{
    public async Task Handle(SaveFullWebPageCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser()!;
        logger.LogInformation("{UserID} attempts to save changes for page: {PageID}", user.UserID, request.WebPage.PageID);

        var originalWebPage = await uIElementsRepository.GetFullWebPageAsync(request.WebPage.AppID, request.WebPage.PageID);

        var webPage = mapper.Map<WebPage>(request.WebPage);

        if (originalWebPage is null)
        {
            await uIElementsRepository.AddWebPageAsync(webPage);
            logger.LogInformation("Page: {PageID} was successfully changed!", request.WebPage.PageID);
            return;
        }

        if (webPage.HasChanges(originalWebPage, ["WebPanels", "ChangeUser", "ChangeDate"]))
        {
            webPage.ChangeUser = user.UserGuid;
            webPage.ChangeDate = DateTime.UtcNow;
        }

        foreach (var panel in webPage.WebPanels)
        {
            var originalPanel = originalWebPage.WebPanels.FirstOrDefault(x => x.PanelID == panel.PanelID);

            if(originalPanel is null || panel.HasChanges(originalPanel, ["WebSections", "ChangeUser", "ChangeDate"]))
            {
                panel.ChangeUser = user.UserGuid;
                panel.ChangeDate = DateTime.UtcNow;
            }

            foreach (var section in panel.WebSections)
            {
                var originalSection = originalPanel?.WebSections.FirstOrDefault(x => x.SectionID == section.SectionID);

                if (originalSection is null || section.HasChanges(originalSection, ["WebControls", "ChangeUser", "ChangeDate"]))
                {
                    section.ChangeUser = user.UserGuid;
                    section.ChangeDate = DateTime.UtcNow;
                }

                foreach (var control in section.WebControls)
                {
                    var originalControl = originalSection?.WebControls.FirstOrDefault(x => x.ControlID == control.ControlID);

                    if (originalControl is null || control.HasChanges(originalControl, ["ChangeUser", "ChangeDate"]))
                    {
                        control.ChangeUser = user.UserGuid;
                        control.ChangeDate = DateTime.UtcNow;
                    }
                }
            }
        }

        webPage.CopyProperties(originalWebPage);
        await uIElementsRepository.SaveWebPageAsync(originalWebPage);
    }
}
