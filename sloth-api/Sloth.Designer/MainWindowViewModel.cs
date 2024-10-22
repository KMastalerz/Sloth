using Sloth.Designer.Core;
using Sloth.Designer.Models;
using Sloth.Designer.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using static Sloth.Designer.MainWindowCommands;

namespace Sloth.Designer;
public class MainWindowViewModel : BaseViewModel
{
    public MainWindowViewModel(IDesignerService designerService, IWebPageStateService webPageStateService)
    {
        webPageStateService.RegisterCallback<IEnumerable<string>>(OnWebApplicationsUpdated);
        SearchPages = new SearchPages(designerService, webPageStateService, this);
    }

    private void OnWebApplicationsUpdated(IEnumerable<string> webApplications)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            WebApps = new(webApplications);
        });
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
        set
        {
            SetProperty(ref pageID, value);
            SearchPageParams.PageID = pageID;
        }
    }

    private string? appID = null;
    public string? AppID
    {
        get => appID;
        set
        {
            SetProperty(ref appID, value);
            SearchPageParams.AppID = appID;
        }
    }

    private UserControl? userControl = null;
    public UserControl? UserControl
    {
        get => userControl;
        set => SetProperty(ref userControl, value);
    }

    private ListWebPageParam searchPageParams = new();
    public ListWebPageParam SearchPageParams
    {
        get => searchPageParams;
        set => SetProperty(ref searchPageParams, value);
    }

    public IAsyncCommand SearchPages { get; private set; }
}
