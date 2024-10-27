using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Sloth.Designer.Pages;
/// <summary>
/// Interaction logic for AddControl.xaml
/// </summary>
public partial class AddControl : UserControl
{
    public AddControl()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider.GetRequiredService<AddControlViewModel>();
    }
}
