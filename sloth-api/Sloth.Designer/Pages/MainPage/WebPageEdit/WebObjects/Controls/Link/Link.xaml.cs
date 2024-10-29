using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Sloth.Designer.Pages;
/// <summary>
/// Interaction logic for Link.xaml
/// </summary>
public partial class Link : UserControl
{
    public Link()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider.GetRequiredService<LinkViewModel>();
    }
}
