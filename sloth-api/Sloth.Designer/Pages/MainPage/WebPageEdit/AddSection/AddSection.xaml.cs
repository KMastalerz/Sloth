using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Sloth.Designer.Pages;
/// <summary>
/// Interaction logic for AddSection.xaml
/// </summary>
public partial class AddSection : UserControl
{
    public AddSection()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider.GetRequiredService<AddSectionViewModel>();
    }
}
