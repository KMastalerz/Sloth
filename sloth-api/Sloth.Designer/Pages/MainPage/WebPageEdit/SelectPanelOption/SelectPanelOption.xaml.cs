using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Sloth.Designer.Pages;
/// <summary>
/// Interaction logic for SelectPanelOption.xaml
/// </summary>
public partial class SelectPanelOption : UserControl
{
    public SelectPanelOption()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider.GetRequiredService<SelectPanelOptionViewModel>();
    }
}
