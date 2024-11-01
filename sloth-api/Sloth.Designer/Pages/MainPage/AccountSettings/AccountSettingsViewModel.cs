using Sloth.Designer.Core;
using Sloth.Designer.Services;
using System.Windows.Input;
using static Sloth.Designer.Pages.AccountSettingsCommands;

namespace Sloth.Designer.Pages;

public class AccountSettingsViewModel : BaseViewModel
{
    public AccountSettingsViewModel(IUserSettingsService userSettingsService, IWindowService windowService)
    {
        SeedPath = userSettingsService.GetSeedPath() ?? string.Empty;
        SaveCommand = new SaveCommand(userSettingsService, windowService);
    }

    private string seedPath = string.Empty;
    public string SeedPath
    {
        get => seedPath;
        set => SetProperty(ref seedPath, value);
    }

    public ICommand SaveCommand { get; }
}
