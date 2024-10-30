using Sloth.Designer.Constants;
using Sloth.Designer.Core;
using Sloth.Designer.Services;
using Sloth.Shared.Models;
using System.Windows.Controls;

namespace Sloth.Designer.Pages;
public class BasePageViewModel: BaseViewModel
{
    public BasePageViewModel(IWebPageStateService webPageStateService)
    {
        WebPage = webPageStateService.WebPage!;
        Orientations = PageConstants.Orientation;
        Positions = PageConstants.Position;
        Backgrounds = PageConstants.Background;
    }

    private WebPageItem webPage = default!;
    public WebPageItem WebPage
    {
        get => webPage;
        set => SetProperty(ref webPage, value);
    }

    private Dictionary<string, string?> orientations;
    public Dictionary<string, string?> Orientations
    {
        get => orientations;
        set => SetProperty(ref orientations, value);
    }

    private Dictionary<string, string?> positions;
    public Dictionary<string, string?> Positions
    {
        get => positions;
        set => SetProperty(ref positions, value);
    }

    private Dictionary<string, string?> backgrounds;
    public Dictionary<string, string?> Backgrounds
    {
        get => backgrounds;
        set => SetProperty(ref backgrounds, value);
    }

    private UserControl? metadataPage = null;

    public UserControl? MetadataPage
    {
        get => metadataPage;
        set => SetProperty(ref metadataPage, value);
    }

}
