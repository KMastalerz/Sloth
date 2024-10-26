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

        InitialRouting();

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
        services.AddSingleton<IUserSettingsService, UserSettingsService>();
        services.AddSingleton<IAuthService, AuthService>();
        services.AddSingleton<IDesignerService, DesignerService>();
        services.AddSingleton<IWebPageStateService, WebPageStateService>();
        services.AddSingleton<IMainWindowService, MainWindowService>();
    }
    private void ConfigureViewModels(ServiceCollection services)
    {
        // Register ViewModels
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<MainPageViewModel>();
        services.AddSingleton<LoginViewModel>();
        services.AddSingleton<WebPageSearchViewModel>();
        services.AddSingleton<WebPageListViewModel>();
        services.AddTransient<WebPageEditViewModel>();
        services.AddTransient<AddElementViewModel>();
    }
    private void ConfigureViews(ServiceCollection services)
    {
        // Register Views
        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainPage>();
        services.AddSingleton<Login>();
        services.AddSingleton<WebPageSearch>();
        services.AddSingleton<WebPageList>();
        services.AddTransient<WebPageEdit>();
        services.AddTransient<AddElement>();
    }
    private async Task InitialRouting()
    {
        var userSettingsService = ServiceProvider.GetRequiredService<IUserSettingsService>();
        var authService = ServiceProvider.GetRequiredService<IAuthService>();
        var mainWindowService = ServiceProvider.GetRequiredService<IMainWindowService>();

        if(userSettingsService.IsLoogedIn())
        {
            var token = userSettingsService.GetRefreshToken();
            var username = userSettingsService.GetUserName();

            if(token is null || username is null)
            {
                mainWindowService.LoadPage(new Login());
                return;
            }

            await authService.Refreshtoken(username, token);

            if (userSettingsService.IsLoogedIn())
                mainWindowService.LoadPage(new MainPage());
            else
                mainWindowService.LoadPage(new Login());
        }
        else
        {
            mainWindowService.LoadPage(new Login());
        }

    }
}

