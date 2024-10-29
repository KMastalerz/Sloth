using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Sloth.Designer.Pages;
/// <summary>
/// Interaction logic for BasePage.xaml
/// </summary>
public partial class BasePage : UserControl
{
    public BasePage()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider.GetRequiredService<BasePageViewModel>();
    }
}
