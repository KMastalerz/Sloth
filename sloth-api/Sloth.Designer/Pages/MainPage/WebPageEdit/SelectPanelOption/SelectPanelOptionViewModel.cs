using Sloth.Designer.Constants;
using Sloth.Designer.Core;
using Sloth.Designer.Services;
using System.Windows.Input;
using static Sloth.Designer.Pages.SelectPanelOptionCommands;

namespace Sloth.Designer.Pages;
public class SelectPanelOptionViewModel: BaseViewModel
{
    public SelectPanelOptionViewModel(IWindowService windowService, IWebPageStateService webPageStateService)
    {
        LoadSelectedOption = new LoadSelectedOption(windowService, webPageStateService);
    }

    private string? panelOption = null;
    public string? PanelOption
    {
        get => panelOption;
        set => SetProperty(ref panelOption, value);
    }

    private List<string> panelOptions = PanelConstants.AddPanelOptions;
    public List<string> PanelOptions
    {
        get => panelOptions;
        set => SetProperty(ref panelOptions, value);
    }

    public ICommand LoadSelectedOption { get; }
}
