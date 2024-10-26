using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Sloth.Designer.Pages;

/// <summary>
/// Interaction logic for WebPageList.xaml
/// </summary>
public partial class WebPageList : UserControl
{
    public WebPageList()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider.GetRequiredService<WebPageListViewModel>();
    }
}
