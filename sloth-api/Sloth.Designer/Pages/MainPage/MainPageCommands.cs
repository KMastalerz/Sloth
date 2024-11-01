using Sloth.Designer.Core;
using Sloth.Designer.Services;
using System.IO;
using System.Text.Json;
//using System.Windows.Forms;

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

    public class ExportSeed(IDesignerService designerService, IWindowService windowService, IUserSettingsService userSettingsService) : AsyncCommand
    {
        public override bool CanExecute(object? parameter = null) => true;

        public override async Task ExecuteAsync(object? parameter = null)
        {
            var response = await designerService.ListWebPages();

            if(!response!.Success)
            {
                await windowService.ShowErrorAsync(response.Error!);
                return;
            }

            var seedPath = userSettingsService.GetSeedPath();
            var seedData = response.Data!;

            if(string.IsNullOrEmpty(seedPath))
            {
                await windowService.ShowWarningAsync("Setup path for seeder in account settings");
                return;
            }

            if (seedData is null)
            {
                await windowService.ShowWarningAsync("Nothing to save");
                return;
            }

            try
            {
                // Define file paths
                var webPagesPath = Path.Combine(seedPath!, "WebPages.json");
                var webPanelsPath = Path.Combine(seedPath!, "WebPanels.json");
                var webSectionsPath = Path.Combine(seedPath!, "WebSections.json");
                var webControlsPath = Path.Combine(seedPath!, "WebControls.json");

                // Serialize each list to JSON and save to files
                await File.WriteAllTextAsync(webPagesPath, JsonSerializer.Serialize(seedData.WebPages));
                await File.WriteAllTextAsync(webPanelsPath, JsonSerializer.Serialize(seedData.WebPanels));
                await File.WriteAllTextAsync(webSectionsPath, JsonSerializer.Serialize(seedData.WebSections));
                await File.WriteAllTextAsync(webControlsPath, JsonSerializer.Serialize(seedData.WebControls));

                await windowService.ShowSuccessAsync("Seed data exported successfully!");
            }
            catch (Exception ex)
            {
                await windowService.ShowErrorAsync($"An error occurred while exporting data: {ex.Message}");
            }
        }
    }
}
