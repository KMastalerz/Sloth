using Sloth.Designer.Core;
using Sloth.Designer.Models;
using Sloth.Designer.Services;
using System.Collections.ObjectModel;
using static Sloth.Designer.Pages.WebPageListCommands;

namespace Sloth.Designer.Pages;
public class WebPageListViewModel : BaseViewModel
{
    public readonly IWebPageStateService webPageStateService;
    public readonly MainWindowViewModel mainWindowViewModel;
    public WebPageListViewModel(IWebPageStateService webPageStateService, MainWindowViewModel mainWindowViewModel)
    {
        this.webPageStateService = webPageStateService;
        this.mainWindowViewModel = mainWindowViewModel;
        webPages = new(webPageStateService.WebPages);
        EditPage = new EditPage(this);
    }
    private ListWebPageItem? selectedWebPage = null;
    public ListWebPageItem? SelectedWebPage
    {
        get => selectedWebPage;
        set => SetProperty(ref selectedWebPage, value);
    }

    private ObservableCollection<ListWebPageItem> webPages = [];
    public ObservableCollection<ListWebPageItem> WebPages
    {
        get => webPages;
        set => SetProperty(ref webPages, value);
    }

    public IAsyncCommand EditPage { get; private set; }

}
