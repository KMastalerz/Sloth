using Sloth.Designer.Constants;
using Sloth.Designer.Core;
using Sloth.Designer.Services;
using Sloth.Shared.Models;

namespace Sloth.Designer.Pages;
public class BasePanelViewModel: BaseViewModel
{
    public BasePanelViewModel(IWebPageStateService webPageStateService)
    {
        webPanel = webPageStateService.WebPanel!;
        Types = PanelConstants.PanelTypes;
    }

    private WebPanelItem webPanel = default!;
    public WebPanelItem WebPanel
    {
        get => webPanel;
        set => SetProperty(ref webPanel, value);
    }

    #region [Type]
    private List<PanelElement> types = default!;
    public List<PanelElement> Types
    {
        get => types;
        set => SetProperty(ref types, value);
    }
    #endregion
}
