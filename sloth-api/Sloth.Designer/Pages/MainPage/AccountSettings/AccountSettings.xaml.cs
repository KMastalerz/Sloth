using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Sloth.Designer.Pages;

/// <summary>
/// Interaction logic for AccountSettings.xaml
/// </summary>
public partial class AccountSettings : UserControl
{
    public AccountSettings()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider.GetRequiredService<AccountSettingsViewModel>();
    }
}
