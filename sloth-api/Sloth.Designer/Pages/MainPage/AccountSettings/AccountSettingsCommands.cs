using Sloth.Designer.Core;
using Sloth.Designer.Services;

namespace Sloth.Designer.Pages;
public static class AccountSettingsCommands
{
    public class SaveCommand(IUserSettingsService userSettingsService, IWindowService windowService) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {
            if(parameter is not AccountSettingsViewModel accountSettingsViewModel) return;

            userSettingsService.SaveSeedPath(accountSettingsViewModel.SeedPath);

            windowService.ShowInformation("Seed path saved successfully.");
        }
    }
}