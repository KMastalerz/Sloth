using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Sloth.Designer.Pages;
/// <summary>
/// Interaction logic for BaseSection.xaml
/// </summary>
public partial class BaseSection : UserControl
{
    public BaseSection()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider.GetRequiredService<BaseSectionViewModel>();
    }
}
