using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Sloth.Designer.Pages;

/// <summary>
/// Interaction logic for MainPage.xaml
/// </summary>
public partial class MainPage : UserControl
{
    public MainPage()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider.GetRequiredService<MainPageViewModel>();
    }
}
