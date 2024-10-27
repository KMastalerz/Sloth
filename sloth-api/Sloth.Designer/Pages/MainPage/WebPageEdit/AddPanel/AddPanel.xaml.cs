using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Sloth.Designer.Pages;
/// <summary>
/// Interaction logic for AddPanel.xaml
/// </summary>
public partial class AddPanel : UserControl
{
    public AddPanel()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider.GetRequiredService<AddPanelViewModel>();
    }
}
