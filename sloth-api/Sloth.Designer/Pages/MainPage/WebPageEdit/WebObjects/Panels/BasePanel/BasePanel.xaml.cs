using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Sloth.Designer.Pages;
/// <summary>
/// Interaction logic for BasePanel.xaml
/// </summary>
public partial class BasePanel : UserControl
{
    public BasePanel()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider.GetRequiredService<BasePanelViewModel>();
    }
}
