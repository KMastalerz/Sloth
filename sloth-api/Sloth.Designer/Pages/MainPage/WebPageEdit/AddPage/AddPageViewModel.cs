using Sloth.Designer.Core;
using Sloth.Designer.Services;
using System.Windows.Input;
using static Sloth.Designer.Pages.AddPageCommands;

namespace Sloth.Designer.Pages;

public class AddPageViewModel: BaseViewModel
{
    public AddPageViewModel(IWebPageStateService webPageStateService, MainPageViewModel mainPageViewModel, IWindowService windowService)
    {
        AddPageCommand = new AddPageCommand(webPageStateService, mainPageViewModel, windowService);
        Applications = webPageStateService.WebApplications.ToList();
    }

    private List<string> applications = [];
    public List<string> Applications
    {
        get => applications;
        set => SetProperty(ref applications, value);
    }

    private string? appID = null;
    public string? AppID
    {
        get => appID;
        set => SetProperty(ref appID, value);
    }

    private string? pageID = null;
    public string? PageID
    {
        get => pageID;
        set => SetProperty(ref pageID, value);
    }

    public ICommand AddPageCommand { get; }
}
