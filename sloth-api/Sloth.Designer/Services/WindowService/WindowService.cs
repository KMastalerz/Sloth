using System.Windows;
using System.Windows.Controls;

namespace Sloth.Designer.Services;

internal class WindowService(MainWindowViewModel mainWindowViewModel) : IWindowService
{
    public void ShowDialog(UserControl control)
    {
        mainWindowViewModel.Dialog = control;
    }

    public void CloseDialog()
    {
        mainWindowViewModel.Dialog = null;
    }

    public void LoadPage(UserControl control)
    {
        mainWindowViewModel.WindowControl = control;
    }

    public async Task LoadPageAsync(UserControl control)
    {
        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            mainWindowViewModel.WindowControl = control;
        });
    }

    public void ClosePage()
    {
        mainWindowViewModel.WindowControl = null;
    }

    private async Task ShowSnackbarAsync(string message, int durationInSeconds, Style style)
    {
        mainWindowViewModel.SnackbarMessage = message;
        mainWindowViewModel.SnackbarStyle = style;
        mainWindowViewModel.IsSnackbarActive = true;

        await Task.Delay(durationInSeconds * 1000);
        mainWindowViewModel.IsSnackbarActive = false;
    }

    private void ShowSnackbar(string message, Style style)
    {
        mainWindowViewModel.SnackbarMessage = message;
        mainWindowViewModel.SnackbarStyle = style;
        mainWindowViewModel.IsSnackbarActive = true;
    }

    public void ShowError(string message) => ShowSnackbar(message, (Application.Current.Resources["ErrorSnackbarStyle"] as Style)!);
    public void ShowInformation(string message) => ShowSnackbar(message, (Application.Current.Resources["InformationSnackbarStyle"] as Style)!);
    public void ShowSuccess(string message) => ShowSnackbar(message, (Application.Current.Resources["SuccessSnackbarStyle"] as Style)!);
    public void ShowWarning(string message) => ShowSnackbar(message, (Application.Current.Resources["WarningSnackbarStyle"] as Style)!);
    public async Task ShowErrorAsync(string message, int durationInSeconds = 3) => await ShowSnackbarAsync(message, durationInSeconds, (Application.Current.Resources["ErrorSnackbarStyle"] as Style)!);
    public async Task ShowInformationAsync(string message, int durationInSeconds = 3) => await ShowSnackbarAsync(message, durationInSeconds, (Application.Current.Resources["InformationSnackbarStyle"] as Style)!);
    public async Task ShowSuccessAsync(string message, int durationInSeconds = 3) => await ShowSnackbarAsync(message, durationInSeconds, (Application.Current.Resources["SuccessSnackbarStyle"] as Style)!);
    public async Task ShowWarningAsync(string message, int durationInSeconds = 3) => await ShowSnackbarAsync(message, durationInSeconds, (Application.Current.Resources["WarningSnackbarStyle"] as Style)!);
}
