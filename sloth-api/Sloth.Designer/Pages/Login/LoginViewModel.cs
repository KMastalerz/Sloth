using Sloth.Designer.Core;
using Sloth.Designer.Services;
using System.Windows.Input;
using static Sloth.Designer.Pages.LoginCommands;

namespace Sloth.Designer.Pages;

public class LoginViewModel : BaseViewModel
{
    private readonly IUserSettingsService userSettingsService;
    public LoginViewModel(IAuthService authService, IWindowService windowService, IUserSettingsService userSettingsService, MainPageViewModel mainPageViewModel)
    {
        this.userSettingsService = userSettingsService;
        rememberMe = userSettingsService.GetRememberMe();
        Login = new LoginCommand(authService, windowService, mainPageViewModel, userSettingsService);
        Close = new CloseCommand();
    }

    private string userName = string.Empty;
    public string UserName
    {
        get => userName;
        set => SetProperty(ref userName, value);
    }

    private string password = string.Empty;
    public string Password
    {
        get => password;
        set => SetProperty(ref password, value);
    }

    private bool rememberMe;
    public bool RememberMe
    {
        get => rememberMe;
        set {
            SetProperty(ref rememberMe, value);
            userSettingsService.SaveRememberMe(value);
        }
    }

    public IAsyncCommand Login { get; }
    public ICommand Close { get; }
}
