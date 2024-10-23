using Sloth.Designer.Core;
using Sloth.Designer.Services;
using Sloth.Shared.Models;
using System.Windows.Input;
using static Sloth.Designer.Pages.WebPageEditCommands;
using static Sloth.Designer.Pages.WebPageListCommands;

namespace Sloth.Designer.Pages;
public class WebPageEditViewModel: BaseViewModel
{
    public WebPageEditViewModel(IDesignerService designerService, IWebPageStateService webPageStateService, MainWindowViewModel mainWindowViewModel)
    {
        GoBack = new GoBack(webPageStateService, mainWindowViewModel);
        OnDrag = new OnDrag();
        OnDrop = new OnDrop(WebPage);
        WebPage = webPageStateService.WebPage;
    }

    private WebPageItem? webPage; 
    public WebPageItem? WebPage
    {
        get => webPage;
        set => SetProperty(ref webPage, value);
    }

    public ICommand GoBack { get; private set; }
    public ICommand OnDrag { get; private set; }
    public ICommand OnDrop { get; private set; }

}
