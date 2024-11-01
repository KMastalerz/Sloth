using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Sloth.Designer.Pages;

/// <summary>
/// Interaction logic for AddPage.xaml
/// </summary>
public partial class AddPage : UserControl
{
    public AddPage()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider.GetRequiredService<AddPageViewModel>();
    }
}
