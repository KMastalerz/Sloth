using Sloth.Designer.Constants;
using Sloth.Designer.Core;
using Sloth.Designer.Models;
using Sloth.Designer.Services;
using System.Windows.Input;
using static Sloth.Designer.Pages.BasePageCommands;

namespace Sloth.Designer.Pages;
public class BasePageViewModel: BaseViewModel
{
    public BasePageViewModel(IWebPageStateService webPageStateService, IWindowService windowService)
    {
        WebPage = webPageStateService.WebPage!;
        Backgrounds = PageConstants.Background;
        SetLayout = new SetLayout(windowService);
    }

    private WebPageItem webPage = default!;
    public WebPageItem WebPage
    {
        get => webPage;
        set => SetProperty(ref webPage, value);
    }

    private Dictionary<string, string?> backgrounds = default!;
    public Dictionary<string, string?> Backgrounds
    {
        get => backgrounds;
        set => SetProperty(ref backgrounds, value);
    }

    public ICommand SetLayout { get; }
}
