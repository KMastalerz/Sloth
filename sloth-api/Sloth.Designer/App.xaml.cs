using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sloth.Designer.Core;
using Sloth.Designer.Pages;
using Sloth.Designer.Services;
using System.IO;
using System.Windows;

namespace Sloth.Designer;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    // Define the ServiceProvider for dependency injection
    public static IServiceProvider ServiceProvider { get; private set; } = default!;
    public static IConfiguration Configuration { get; private set; } = default!;

    protected override void OnStartup(StartupEventArgs e)
    {
        var environment = "Production";
        #if DEBUG
                environment = "Development";
        #endif

        // Initialize the dependency injection container
        var services = new ServiceCollection();


        // Load configuration based on the environment
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);

        Configuration = builder.Build();

        // Configure services
        ConfigureServices(services);
        ConfigureViewModels(services);
        ConfigureViews(services);

        ServiceProvider = services.BuildServiceProvider();

        base.OnStartup(e);
    }

    // This method is used to register services in the DI container
    private void ConfigureServices(ServiceCollection services)
    {
        // Register HttpClient with the API base address from appsettings
        services.AddHttpClient("ApiClient", client =>
        {
            var apiBaseAddress = Configuration!.GetValue<string>("ApiBaseAddress");
            if (apiBaseAddress is not null)
                client.BaseAddress = new Uri(apiBaseAddress);
        });

        // Register other services if needed
        services.AddSingleton<IHttpServices, HttpServices>();
        services.AddSingleton<IDesignerService, DesignerService>();
        services.AddSingleton<IWebPageStateService, WebPageStateService>();
    }
    private void ConfigureViewModels(ServiceCollection services)
    {
        // Register ViewModels
        services.AddSingleton<MainWindowViewModel>();
        services.AddTransient<WebPageListViewModel>();
        services.AddTransient<WebPageEditViewModel>();
    }
    private void ConfigureViews(ServiceCollection services)
    {
        // Register Views
        services.AddSingleton<MainWindow>();
        services.AddTransient<WebPageList>();
        services.AddTransient<WebPageEdit>();
    }
}

