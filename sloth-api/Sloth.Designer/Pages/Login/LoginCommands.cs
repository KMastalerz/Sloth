using Sloth.Designer.Core;
using Sloth.Designer.Services;
using System.Windows;

namespace Sloth.Designer.Pages;

public static class LoginCommands
{
    public class LoginCommand(IAuthService authService, IWindowService windowService, MainPageViewModel mainPageViewModel, IUserSettingsService userSettingsService) : AsyncCommand
    {
        public override bool CanExecute(object? parameter = null)
        {
            if (parameter is not LoginViewModel parameters) return false;
            return !string.IsNullOrEmpty(parameters.UserName) && !string.IsNullOrEmpty(parameters.Password);
        }
        public override async Task ExecuteAsync(object? parameter = null)
        {
            if (parameter is not LoginViewModel parameters) return;

            var response = await authService.Login(parameters.UserName, parameters.Password);

            if (response?.Success ?? false)
            {
                windowService.LoadPage(new MainPage());
                mainPageViewModel.MainPageControl = new WebPageSearch();
                userSettingsService.SaveToken(response?.Data!);
                return;
            } 
            else
            {
                await windowService.ShowErrorAsync(response?.Error!);
            }
        }
    }

    public class CloseCommand() : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;
        protected override void ExecuteSync(object? parameter = null)
        {
            Application.Current.Shutdown();
        }
    }
}
