using Sloth.Designer.Core;
using Sloth.Designer.Services;
using System.Windows.Controls;
using System.Windows.Input;
using static Sloth.Designer.Pages.MainPageCommands;

namespace Sloth.Designer.Pages;

public class MainPageViewModel : BaseViewModel
{
    public MainPageViewModel(IUserSettingsService userSettingsService, IWindowService windowService)
    {
        Logoff = new Logoff(userSettingsService, windowService);
    }
    private UserControl? mainPageControl = null;
    public UserControl? MainPageControl
    {
        get => mainPageControl;
        set => SetProperty(ref mainPageControl, value);
    }

    public ICommand Logoff { get; }
}
