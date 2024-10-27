using Sloth.Designer.Core;
using Sloth.Designer.Services;
using System.Windows;

namespace Sloth.Designer.Pages;

public static class LoginCommands
{
    public class LoginCommand(IAuthService authService, IWindowService windowService) : AsyncCommand
    {
        public override bool CanExecute(object? parameter = null)
        {
            if (parameter is not LoginViewModel parameters) return false;
            return !string.IsNullOrEmpty(parameters.UserName) && !string.IsNullOrEmpty(parameters.Password);
        }
        public override async Task ExecuteAsync(object? parameter = null)
        {
            if (parameter is not LoginViewModel parameters) return;

            var loggedIn = await authService.Login(parameters.UserName, parameters.Password);

            if (loggedIn)
            {
                windowService.LoadPage(new MainPage());
                return;
            } else Application.Current.Dispatcher.Invoke(() => MessageBox.Show("Invalid username or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error));
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
