using Sloth.Designer.Core;
using Sloth.Designer.Services;
using System.Windows.Controls;
using System.Windows.Input;
using static Sloth.Designer.Pages.MainPageCommands;

namespace Sloth.Designer.Pages;

public class MainPageViewModel : BaseViewModel
{
    public MainPageViewModel(IAuthService authService, IMainWindowService mainWindowService)
    {
        UserControl = new WebPageSearch();
        Logoff = new Logoff(authService, mainWindowService);
    }
    private UserControl? userControl = null;
    public UserControl? UserControl
    {
        get => userControl;
        set => SetProperty(ref userControl, value);
    }

    public ICommand Logoff { get; }
}
