using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Sloth.Designer.Pages;
/// <summary>
/// Interaction logic for PageLayout.xaml
/// </summary>
public partial class PageLayout : UserControl
{
    public PageLayout()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider.GetRequiredService<PageLayoutViewModel>();    
    }
}
