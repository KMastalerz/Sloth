using Sloth.Designer.Pages;
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

    public void ClosePage()
    {
        mainWindowViewModel.WindowControl = null;
    }
}
