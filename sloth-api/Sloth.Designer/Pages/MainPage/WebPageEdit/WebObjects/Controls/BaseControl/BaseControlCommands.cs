using Sloth.Designer.Constants;
using Sloth.Designer.Core;
using Sloth.Designer.Services;

namespace Sloth.Designer.Pages;

public static class BaseControlCommands
{
    public class SetMetadata(IWindowService windowService) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;
        protected override void ExecuteSync(object? parameter = null)
        {
            if (parameter is not BaseControlViewModel baseControlViewModel) return;
            switch (baseControlViewModel.Type)
            {
                case ControlTypes.Button:
                    windowService.ShowDialog(new Button());
                    break;
                case ControlTypes.Link:
                    windowService.ShowDialog(new Link());
                    break;
                default:
                    windowService.CloseDialog();
                    break;
            }
        }
    }
}
