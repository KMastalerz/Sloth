using System.Windows.Controls;

namespace Sloth.Designer.Services;

public class MainWindowService(MainWindowViewModel mainWindowViewModel) : IMainWindowService
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
        mainWindowViewModel.UserControl = control;
    }

    public void ClosePage()
    {
        mainWindowViewModel.UserControl = null;
    }
}
