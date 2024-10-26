using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Sloth.Designer.Pages;

/// <summary>
/// Interaction logic for Login.xaml
/// </summary>
public partial class Login : UserControl
{
    public Login()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider.GetRequiredService<LoginViewModel>();
    }
}
