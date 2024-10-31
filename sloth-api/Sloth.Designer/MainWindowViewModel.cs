using Sloth.Designer.Core;
using System.Windows;
using System.Windows.Controls;

namespace Sloth.Designer;
public class MainWindowViewModel : BaseViewModel
{
    private UserControl? windowControl = null;
    public UserControl? WindowControl
    {
        get => windowControl;
        set => SetProperty(ref windowControl, value);
    }

    private UserControl? dialog = null; 
    public UserControl? Dialog
    {
        get => dialog;
        set => SetProperty(ref dialog, value);
    }

    private string snackbarMessage = string.Empty;
    public string SnackbarMessage
    {
        get => snackbarMessage;
        set => SetProperty(ref snackbarMessage, value);
    }

    private bool isSnackbarActive = false;
    public bool IsSnackbarActive
    {
        get => isSnackbarActive;
        set => SetProperty(ref isSnackbarActive, value);
    }

    private Style? snackbarStyle;
    public Style? SnackbarStyle
    {
        get => snackbarStyle;
        set => SetProperty(ref snackbarStyle, value);
    }
}
