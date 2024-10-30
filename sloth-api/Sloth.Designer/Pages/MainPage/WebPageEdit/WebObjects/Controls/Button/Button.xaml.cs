using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Sloth.Designer.Pages;
/// <summary>
/// Interaction logic for Button.xaml
/// </summary>
public partial class Button : UserControl
{
    public Button()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider.GetRequiredService<ButtonViewModel>();
    }
}
