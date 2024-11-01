using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sloth.Designer.Core;
using Sloth.Designer.Pages;
using Sloth.Designer.Services;
using System.Configuration;
using System.IO;
using System.Net.Http;
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

        // Call base OnStartup synchronously to ensure WPF initializes properly
        base.OnStartup(e);

        // Schedule InitialRouting to run on the UI thread after startup
        Dispatcher.InvokeAsync(async () =>
        {
            await InitialRouting();
        });
    }

    // This method is used to register services in the DI container
    private void ConfigureServices(ServiceCollection services)
    {
        // Register HttpClient with the API base address from appsettings
        services.AddTransient<AuthenticatedHttpClientHandler>();
        services.AddHttpClient("ApiClient", client =>
        {
            var apiBaseAddress = Configuration.GetValue<string>("ApiBaseAddress");
            if (!string.IsNullOrEmpty(apiBaseAddress))
                client.BaseAddress = new Uri(apiBaseAddress);
        }).AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

        services.AddHttpClient("ApiBaseClient", client =>
        {
            var apiBaseAddress = Configuration.GetValue<string>("ApiBaseAddress");
            if (!string.IsNullOrEmpty(apiBaseAddress))
                client.BaseAddress = new Uri(apiBaseAddress);
        });

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
        services.AddTransient<AccountSettingsViewModel>();
        services.AddTransient<AddPageViewModel>();
        services.AddTransient<AddPanelViewModel>();
        services.AddTransient<AddSectionViewModel>();
        services.AddTransient<AddControlViewModel>();
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
        services.AddTransient<AccountSettings>();
        services.AddTransient<AddPage>();
        services.AddTransient<AddPanel>();
        services.AddTransient<AddSection>();
        services.AddTransient<AddControl>();
        services.AddTransient<BasePage>();
        services.AddTransient<BasePanel>();
        services.AddTransient<BaseSection>();
        services.AddTransient<BaseControl>();
        services.AddTransient<Button>();
        services.AddTransient<Link>();
    }
    private async Task InitialRouting()
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
                await windowService.LoadPageAsync(new Login());
                return;
            }

            var response = await authService.Refreshtoken(username, token);

            // initialize main page, and main page service
            if (response?.Success ?? false)
            {
                userSettingsService.SaveToken(response.Data!);
                await windowService.LoadPageAsync(new MainPage());
                Dispatcher.Invoke(() =>
                {
                    mainPageViewModel.MainPageControl = new WebPageSearch();
                });
                return;
            }
            else
            {
                //windowService.LoadPageFromAsync(new Login());
                await windowService.LoadPageAsync(new Login());
                return;
            }
        }
        else
        {
            //Dispatcher.Invoke(() =>
            //{
            //    windowService.LoadPage(new Login());
            //});
            await windowService.LoadPageAsync(new Login());
        }
    }
}

