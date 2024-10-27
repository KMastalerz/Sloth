using Sloth.Designer.Constants;
using Sloth.Designer.Core;
using Sloth.Designer.Enums;
using Sloth.Designer.Services;

namespace Sloth.Designer.Pages;
public static class SelectPanelOptionCommands
{
    public class LoadSelectedOption(IWindowService windowService, IWebPageStateService webPageStateService) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {
            if (parameter is SelectPanelOptionViewModel selectPanelOptionViewModel)
            {
                if (selectPanelOptionViewModel.PanelOption == PanelOption.Section)
                {
                    webPageStateService.AddElementType = AddElementType.Section;
                    windowService.ShowDialog(new AddSection());
                }
                else
                {
                    webPageStateService.AddElementType = AddElementType.PanelControl;
                    windowService.ShowDialog(new AddControl());
                }
            }
        }
    }
}
