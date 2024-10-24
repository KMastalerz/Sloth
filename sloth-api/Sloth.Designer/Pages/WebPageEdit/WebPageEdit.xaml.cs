using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sloth.Designer.Pages;

/// <summary>
/// Interaction logic for WebPageEdit.xaml
/// </summary>
public partial class WebPageEdit : UserControl
{
    public WebPageEdit()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider.GetRequiredService<WebPageEditViewModel>();
    }
}
