using Sloth.Designer.Core;
using Sloth.Designer.Services;
using Sloth.Shared.Models;
using System.Collections.ObjectModel;
using static Sloth.Designer.Pages.WebPageListCommands;

namespace Sloth.Designer.Pages;
public class WebPageListViewModel : BaseViewModel
{
    public WebPageListViewModel(IWebPageStateService webPageStateService, IDesignerService designerService, MainPageViewModel mainPageViewModel)
    {
        EditPage = new EditPage(designerService, webPageStateService, mainPageViewModel);
        WebPages = new(webPageStateService.WebPages);
        
    }

    private ObservableCollection<ListWebPageItem> webPages = [];
    public ObservableCollection<ListWebPageItem> WebPages
    {
        get => webPages;
        set => SetProperty(ref webPages, value);
    }

    public IAsyncCommand EditPage { get; private set; }

}
