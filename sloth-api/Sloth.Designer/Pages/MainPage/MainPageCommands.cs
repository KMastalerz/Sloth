using Sloth.Designer.Core;
using Sloth.Designer.Services;

namespace Sloth.Designer.Pages;

public static class MainPageCommands
{
    public class Logoff(IAuthService authService, IMainWindowService mainWindowService) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;
        protected override void ExecuteSync(object? parameter = null)
        {
            authService.Logoff();
            mainWindowService.LoadPage(new Login());
        }
    }
}
