using Sloth.Designer.Core;
using Sloth.Designer.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Sloth.Designer.Pages.WebPageSearchCommands;

namespace Sloth.Designer.Pages;

public class WebPageSearchViewModel: BaseViewModel
{
    public WebPageSearchViewModel(IDesignerService designerService, IWebPageStateService webPageStateService)
    {
        webPageStateService.RegisterCallback<IEnumerable<string>>(OnWebApplicationsUpdated);
        SearchPages = new SearchPages(designerService, webPageStateService);
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
        }
    }

    private string? appID = null;
    public string? AppID
    {
        get => appID;
        set
        {
            SetProperty(ref appID, value);
        }
    }

    private UserControl? userControl = null;
    public UserControl? UserControl
    {
        get => userControl;
        set => SetProperty(ref userControl, value);
    }

    public IAsyncCommand SearchPages { get; }
}
