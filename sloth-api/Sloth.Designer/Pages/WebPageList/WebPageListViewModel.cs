using Sloth.Designer.Core;
using Sloth.Designer.Services;
using Sloth.Shared.Models;
using System.Collections.ObjectModel;
using static Sloth.Designer.Pages.WebPageListCommands;

namespace Sloth.Designer.Pages;
public class WebPageListViewModel(IWebPageStateService webPageStateService) : BaseViewModel
{
    private ListWebPageItem? selectedWebPage = null;
    public ListWebPageItem? SelectedWebPage
    {
        get => selectedWebPage;
        set => SetProperty(ref selectedWebPage, value);
    }

    private ObservableCollection<ListWebPageItem> webPages = new(webPageStateService.WebPages);
    public ObservableCollection<ListWebPageItem> WebPages
    {
        get => webPages;
        set => SetProperty(ref webPages, value);
    }

    public IAsyncCommand EditPage { get; private set; } = new EditPage();

}
