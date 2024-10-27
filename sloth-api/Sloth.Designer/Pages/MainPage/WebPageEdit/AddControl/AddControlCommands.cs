
using Sloth.Designer.Core;
using Sloth.Designer.Enums;
using Sloth.Designer.Models;
using Sloth.Designer.Services;

namespace Sloth.Designer.Pages;
public static class AddControlCommands
{
    public class AddControlCommand(IWebPageStateService webPageStateService) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null)
        {
            if(parameter is AddControlViewModel addControlViewModel)
            {
                return !string.IsNullOrEmpty(addControlViewModel.ControlName) && !string.IsNullOrEmpty(addControlViewModel.ControlType);
            }
            return false;
        }

        protected override void ExecuteSync(object? parameter = null)
        {
            if (parameter is not AddControlViewModel addControlViewModel) return;

            var control = new NewControl(addControlViewModel.ControlName!, addControlViewModel.ControlType!);

            switch (webPageStateService.AddElementType)
            {
                case AddElementType.SectionControl:
                    webPageStateService.AddSectionControl(control);
                    break;
                case AddElementType.PanelControl:
                    webPageStateService.AddPanelControl(control);
                    break;
            }
        }
    }
}
