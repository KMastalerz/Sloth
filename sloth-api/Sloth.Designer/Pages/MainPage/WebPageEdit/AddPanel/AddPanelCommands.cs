using Sloth.Designer.Constants;
using Sloth.Designer.Core;
using Sloth.Designer.Models;
using Sloth.Designer.Services;

namespace Sloth.Designer.Pages;
public static class AddPanelCommands
{
    public class AddPanelCommand(IWebPageStateService webPageStateService) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null)
        {
            if (parameter is not AddPanelViewModel addControlViewModel) return false;
            return !string.IsNullOrEmpty(addControlViewModel.PanelID) && !string.IsNullOrEmpty(addControlViewModel.PanelType);
        }

        protected override void ExecuteSync(object? parameter = null)
        {
            if (parameter is not AddPanelViewModel addControlViewModel) return;

            var panelType = PanelConstants.PanelTypes.FirstOrDefault(p => p.PanelName == addControlViewModel.PanelType!)!;

            var panel = new NewPanel(addControlViewModel.PanelID!, panelType.PanelType, panelType.PanelSections);
            webPageStateService.AddPanel(panel);
        }
    }
}
