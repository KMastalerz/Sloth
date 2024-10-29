using Sloth.Designer.Constants;
using Sloth.Designer.Core;
using Sloth.Designer.Services;
using System.Windows.Input;
using static Sloth.Designer.Pages.AddPanelCommands;

namespace Sloth.Designer.Pages;
public class AddPanelViewModel:BaseViewModel
{
    public AddPanelViewModel(IWebPageStateService webPageStateService)
    {
        AddPanelCommand = new AddPanelCommand(webPageStateService);
    }

    private string? panelID = null;
    public string? PanelID
    {
        get => panelID;
        set => SetProperty(ref panelID, value);
    }

    private List<string> panelTypes = PanelConstants.PanelTypes.Select(p => p.PanelName).ToList();
    public List<string> PanelTypes
    {
        get => panelTypes;
        set => SetProperty(ref panelTypes, value);
    }

    private string? panelType = null;
    public string? PanelType
    {
        get => panelType;
        set => SetProperty(ref panelType, value);
    }

    public ICommand AddPanelCommand { get; }
}
