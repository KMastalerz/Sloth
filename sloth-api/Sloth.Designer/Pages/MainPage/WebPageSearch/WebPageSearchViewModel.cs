using Microsoft.Extensions.DependencyInjection;
using Sloth.Designer.Core;
using Sloth.Designer.Services;
using Sloth.Shared.Models;
using System.Collections.ObjectModel;
using System.Windows;
using static Sloth.Designer.Pages.WebPageSearchCommands;

namespace Sloth.Designer.Pages;

public class WebPageSearchViewModel: BaseViewModel
{
    public WebPageSearchViewModel(IDesignerService designerService, IWebPageStateService webPageStateService)
    {
        var mainPageViewModel = App.ServiceProvider.GetRequiredService<MainPageViewModel>();
        webPageStateService.RegisterCallback<IEnumerable<string>>(OnWebApplicationsUpdated);
        webPageStateService.RegisterCallback<IEnumerable<ListWebPageItem>>(OnWebPagesUpdated);
        SearchPages = new SearchPages(designerService, webPageStateService);
        EditPage = new EditPage(designerService, webPageStateService, mainPageViewModel);
    }
    private void OnWebApplicationsUpdated(IEnumerable<string> webApplications)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            WebApps = new(webApplications);
        });
    }

    private void OnWebPagesUpdated(IEnumerable<ListWebPageItem> webPages)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            WebPages = new(webPages);
        });
    }

    private ObservableCollection<string> webApps = [];
    public ObservableCollection<string> WebApps
    {
        get => webApps;
        set => SetProperty(ref webApps, value);
    }

    private ObservableCollection<ListWebPageItem> webPages = [];
    public ObservableCollection<ListWebPageItem> WebPages
    {
        get => webPages;
        set => SetProperty(ref webPages, value);
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

    public IAsyncCommand SearchPages { get; }
    public IAsyncCommand EditPage { get; }
}
