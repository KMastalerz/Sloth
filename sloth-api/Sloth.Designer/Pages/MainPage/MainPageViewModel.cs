using Sloth.Designer.Core;
using Sloth.Designer.Services;
using System.Windows.Controls;
using System.Windows.Input;
using static Sloth.Designer.Pages.MainPageCommands;

namespace Sloth.Designer.Pages;

public class MainPageViewModel : BaseViewModel
{
    public MainPageViewModel(IDesignerService designerService,IUserSettingsService userSettingsService, IWindowService windowService)
    {
        Logoff = new Logoff(userSettingsService, windowService);
        OpenAccountSettings = new OpenAccountSettings();
        OpenDesigner = new OpenDesigner();
        ExportSeed = new ExportSeed(designerService, windowService, userSettingsService);
    }
    private UserControl? mainPageControl = null;
    public UserControl? MainPageControl
    {
        get => mainPageControl;
        set => SetProperty(ref mainPageControl, value);
    }

    public ICommand Logoff { get; }
    public ICommand OpenAccountSettings { get; }
    public ICommand OpenDesigner { get; }
    public ICommand ExportSeed { get; }
}
