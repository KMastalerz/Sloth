using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Sloth.Designer.Pages;

/// <summary>
/// Interaction logic for WebPageSearch.xaml
/// </summary>
public partial class WebPageSearch : UserControl
{
    public WebPageSearch()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider.GetRequiredService<WebPageSearchViewModel>();
    }
}
