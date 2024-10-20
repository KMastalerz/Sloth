using Sloth.Designer.Core;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using static Sloth.Designer.MainWindowCommands;

namespace Sloth.Designer;
public class MainWindowViewModel : BaseViewModel
{
    public MainWindowViewModel()
    {
        SearchPages = new SearchPages(this);
        webApps = ["sloth"];
    }
    private ObservableCollection<string> webApps = [];
    public ObservableCollection<string> WebApps
    {
        get => webApps;
        set => SetProperty(ref webApps, value);
    }

    private string? pageID = null;
    public string? PageID
    {
        get => pageID;
        set => SetProperty(ref pageID, value);
    }

    private string? appID = null;
    public string? AppID
    {
        get => appID;
        set => SetProperty(ref appID, value);
    }

    private UserControl? userControl = null;
    public UserControl? UserControl
    {
        get => userControl;
        set => SetProperty(ref userControl, value);
    }

    public IAsyncCommand SearchPages { get; private set; }
}
