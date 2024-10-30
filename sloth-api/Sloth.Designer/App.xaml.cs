using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sloth.Designer.Core;
using Sloth.Designer.Pages;
using Sloth.Designer.Services;
using System.Configuration;
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
        services.AddSingleton<IWindowService, WindowService>();
        services.AddSingleton<IWebPageEditService, WebPageEditService>();
    }
    private void ConfigureViewModels(ServiceCollection services)
    {
        // Register ViewModels
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<MainPageViewModel>();
        services.AddSingleton<LoginViewModel>();
        services.AddSingleton<WebPageSearchViewModel>();
        services.AddTransient<WebPageEditViewModel>();
        services.AddTransient<AddPanelViewModel>();
        services.AddTransient<AddSectionViewModel>();
        services.AddTransient<AddControlViewModel>();
        services.AddTransient<SelectPanelOptionViewModel>();
        services.AddTransient<BasePageViewModel>();
        services.AddTransient<BasePanelViewModel>();
        services.AddTransient<BaseSectionViewModel>();
        services.AddTransient<BaseControlViewModel>();
        services.AddTransient<ButtonViewModel>();
        services.AddTransient<LinkViewModel>();
    }

    private void ConfigureViews(ServiceCollection services)
    {
        // Register Views
        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainPage>();
        services.AddSingleton<Login>();
        services.AddSingleton<WebPageSearch>();
        services.AddTransient<WebPageEdit>();
        services.AddTransient<AddPanel>();
        services.AddTransient<AddSection>();
        services.AddTransient<AddControl>();
        services.AddTransient<SelectPanelOption>();
        services.AddTransient<BasePage>();
        services.AddTransient<BasePanel>();
        services.AddTransient<BaseSection>();
        services.AddTransient<BaseControl>();
        services.AddTransient<Button>();
        services.AddTransient<Link>();
    }
    private void InitialRouting()
    {
        var userSettingsService = ServiceProvider.GetRequiredService<IUserSettingsService>();
        var authService = ServiceProvider.GetRequiredService<IAuthService>();
        var windowService = ServiceProvider.GetRequiredService<IWindowService>();
        var mainPageViewModel = ServiceProvider.GetRequiredService<MainPageViewModel>();

        if (userSettingsService.IsLoogedIn())
        {
            var token = userSettingsService.GetRefreshToken();
            var username = userSettingsService.GetUserName();

            if(token is null || username is null)
            {
                windowService.LoadPage(new Login());
                return;
            }

            Task.Run( async () => await authService.Refreshtoken(username, token));

            // initialize main page, and main page service
            var initMainPage = new MainPage();
            var initPageSearch = new WebPageSearch();

            if (userSettingsService.IsLoogedIn())
            {
                windowService.LoadPage(initMainPage);
                mainPageViewModel.MainPageControl = initPageSearch;
            }
            else
                windowService.LoadPage(new Login());
        }
        else
        {
            windowService.LoadPage(new Login());
        }

    }
}

