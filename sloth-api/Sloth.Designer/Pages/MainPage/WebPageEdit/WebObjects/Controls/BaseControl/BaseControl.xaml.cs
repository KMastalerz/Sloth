using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Sloth.Designer.Pages;
/// <summary>
/// Interaction logic for BaseControl.xaml
/// </summary>
public partial class BaseControl : UserControl
{
    public BaseControl()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider.GetRequiredService<BaseControlViewModel>();
    }
}
