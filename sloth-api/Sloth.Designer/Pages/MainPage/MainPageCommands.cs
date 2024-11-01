using Sloth.Designer.Core;
using Sloth.Designer.Services;

namespace Sloth.Designer.Pages;

public static class MainPageCommands
{
    public class Logoff(IUserSettingsService userSettingsService, IWindowService windowService) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;
        protected override void ExecuteSync(object? parameter = null)
        {
            userSettingsService.ClearToken();
            windowService.LoadPage(new Login());
        }
    }

    public class OpenAccountSettings : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;
        protected override void ExecuteSync(object? parameter = null)
        {
            if (parameter is not MainPageViewModel mainPageViewModel) return;
         
            mainPageViewModel.MainPageControl = new AccountSettings();
        }
    }

    public class OpenDesigner : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {
            if (parameter is not MainPageViewModel mainPageViewModel) return;

            mainPageViewModel.MainPageControl = new WebPageSearch();
        }
    }
}
