using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Sloth.Designer.Pages;

/// <summary>
/// Interaction logic for AddElement.xaml
/// </summary>
public partial class AddElement : UserControl
{
    public AddElement()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider.GetRequiredService<AddElementViewModel>();
    }
}
