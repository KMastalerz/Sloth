using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Sloth.Application.UserIdentity;
using Sloth.Domain.Entities;
using Sloth.Domain.Extensions;
using Sloth.Domain.Repositories;
using System.Transactions;

namespace Sloth.Application.Services.UIElements;
public class SaveFullWebPageCommandHandler(ILogger<SaveFullWebPageCommandHandler> logger, IUIElementsRepository uIElementsRepository, IMapper mapper, IUserContext userContext) : IRequestHandler<SaveFullWebPageCommand>
{
    public async Task Handle(SaveFullWebPageCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser()!;
        logger.LogInformation("{UserID} attempts to save changes for page: {PageID}", user.UserID, request.WebPage.PageID);

        using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TransactionManager.MaximumTimeout
        }, TransactionScopeAsyncFlowOption.Enabled))
        {
            try
            {
                var checkWebPage = await uIElementsRepository.GetFullWebPageAsync(request.WebPage.AppID, request.WebPage.PageID) ?? null;

                var webPage = mapper.Map<WebPage>(request.WebPage);

                if (checkWebPage is not null)
                    await uIElementsRepository.SaveWebPageAsync(checkWebPage.Update(webPage, user.UserGuid));
                else
                    await uIElementsRepository.AddWebPageAsync(webPage);

                // Update and Add
                foreach (var panel in webPage.WebPanels)
                {
                    var checkWebPanel = checkWebPage?.WebPanels.FirstOrDefault(p => p.PanelID == panel.PanelID) ?? null;

                    if (checkWebPanel is not null)
                        await uIElementsRepository.SaveWebPanelAsync(checkWebPanel.Update(panel, user.UserGuid));
                    else
                        await uIElementsRepository.AddWebPanelAsync(panel);


                    foreach (var section in panel.WebSections)
                    {
                        var checkWebSections = checkWebPanel?.WebSections.FirstOrDefault(s => s.SectionID == section.SectionID) ?? null;

                        if (checkWebSections is not null)
                            await uIElementsRepository.SaveWebSectionAsync(checkWebSections.Update(section, user.UserGuid));
                        else
                            await uIElementsRepository.AddWebSectionAsync(section);

                        foreach (var control in section.WebControls)
                        {
                            var checkWebControl = checkWebSections?.WebControls.FirstOrDefault(c => c.ControlID == control.ControlID) ?? null;

                            if (checkWebControl is not null)
                                await uIElementsRepository.SaveWebControlAsync(checkWebControl.Update(control, user.UserGuid));
                            else
                                await uIElementsRepository.AddWebControlAsync(control);
                        }
                    }
                    foreach (var control in panel.WebControls)
                    {
                        var checkWebControl = checkWebPanel?.WebControls.FirstOrDefault(c => c.ControlID == control.ControlID) ?? null;

                        if (checkWebControl is not null)
                            await uIElementsRepository.SaveWebControlAsync(checkWebControl.Update(control, user.UserGuid));
                        else
                            await uIElementsRepository.AddWebControlAsync(control);
                    }
                }

                // Remove elements missing in new model
                foreach (var panel in checkWebPage?.WebPanels ?? [])
                {
                    // Find corresponding panel in the new model
                    var checkWebPanel = webPage.WebPanels.FirstOrDefault(p => p.PanelID == panel.PanelID);

                    // If the panel does not exist in the new model, remove it
                    if (checkWebPanel is null)
                    {
                        await uIElementsRepository.RemoveWebPanelAsync(panel);
                        continue; // Skip further checks for this panel since it's removed
                    }

                    // Remove sections missing in the new model
                    foreach (var section in panel.WebSections ?? [])
                    {
                        // Find corresponding section in the new model
                        var checkWebSection = checkWebPanel.WebSections.FirstOrDefault(s => s.SectionID == section.SectionID);

                        // If the section does not exist in the new model, remove it
                        if (checkWebSection is null)
                        {
                            await uIElementsRepository.RemoveWebSectionAsync(section);
                            continue; // Skip further checks for this section since it's removed
                        }

                        // Remove section controls missing in the new model
                        foreach (var sectionControl in section.WebControls ?? [])
                        {
                            // Only remove if the control is missing in the new model's section controls
                            var checkWebControl = checkWebSection.WebControls.FirstOrDefault(c => c.ControlID == sectionControl.ControlID);
                            if (checkWebControl is null && sectionControl.SectionID is not null)
                            {
                                await uIElementsRepository.RemoveWebControlAsync(sectionControl);
                            }
                        }
                    }

                    // Remove panel controls missing in the new model
                    foreach (var panelControl in panel.WebControls ?? [])
                    {
                        // Only remove if the control is missing in the new model's panel controls (not section-bound)
                        var checkWebControl = checkWebPanel.WebControls.FirstOrDefault(c => c.ControlID == panelControl.ControlID);
                        if (checkWebControl is null && panelControl.SectionID is null)
                        {
                            await uIElementsRepository.RemoveWebControlAsync(panelControl);
                        }
                    }
                }

                transaction.Complete();
                logger.LogInformation("Page: {PageID} was successfully changed!", request.WebPage.PageID);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "An error occurred while saving changes for page {PageID}", request.WebPage.PageID);
                throw; 
            }
        }

    }
}
