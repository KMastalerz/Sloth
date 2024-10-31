using System.Windows.Controls;

namespace Sloth.Designer.Services;
public interface IWindowService
{
    void CloseDialog();
    void ShowDialog(UserControl control);
    void LoadPage(UserControl control);
    Task LoadPageAsync(UserControl control);
    void ClosePage();
    void ShowError(string message);
    void ShowInformation(string message);
    void ShowSuccess(string message);
    void ShowWarning(string message);
    Task ShowErrorAsync(string message, int durationInSeconds = 3);
    Task ShowInformationAsync(string message, int durationInSeconds = 3);
    Task ShowSuccessAsync(string message, int durationInSeconds = 3);
    Task ShowWarningAsync(string message, int durationInSeconds = 3);
}