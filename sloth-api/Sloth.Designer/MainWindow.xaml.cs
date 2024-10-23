using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Markup;

namespace Sloth.Designer;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
        DataContext = App.ServiceProvider.GetRequiredService<MainWindowViewModel>();
    }
}