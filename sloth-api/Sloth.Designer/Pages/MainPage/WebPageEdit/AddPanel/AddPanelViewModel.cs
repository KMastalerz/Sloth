using Sloth.Designer.Core;
using System.Windows.Input;

namespace Sloth.Designer.Pages;
public class AddPanelViewModel:BaseViewModel
{
    public AddPanelViewModel()
    {
        
    }

    private string? panelID = null;
    public string? PanelID
    {
        get => panelID;
        set => SetProperty(ref panelID, value);
    }

    private List<string> panelTypes = [];
    public List<string> PanelTypes
    {
        get => panelTypes;
        set => SetProperty(ref panelTypes, value);
    }

    public ICommand AddPanelCommand { get; }
}
