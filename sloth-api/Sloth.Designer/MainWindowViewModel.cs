using Sloth.Designer.Core;
using System.Windows.Controls;

namespace Sloth.Designer;
public class MainWindowViewModel : BaseViewModel
{
    private UserControl? userControl = null;
    public UserControl? UserControl
    {
        get => userControl;
        set => SetProperty(ref userControl, value);
    }

    private UserControl? dialog = null; 
    public UserControl? Dialog
    {
        get => dialog;
        set => SetProperty(ref dialog, value);
    }
}
