using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Sloth.Designer.Pages;
/// <summary>
/// Interaction logic for TextInput.xaml
/// </summary>
public partial class TextInput : UserControl
{
    public TextInput()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider.GetRequiredService<TextInputViewModel>();
    }
}
