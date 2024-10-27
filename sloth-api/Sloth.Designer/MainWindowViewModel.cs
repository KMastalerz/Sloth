using Sloth.Designer.Core;
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
}
