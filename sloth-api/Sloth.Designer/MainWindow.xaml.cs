using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Sloth.Designer;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        try
        {
            var dataContext = App.ServiceProvider.GetRequiredService<MainWindowViewModel>();
            DataContext = dataContext;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        
     
    }
}