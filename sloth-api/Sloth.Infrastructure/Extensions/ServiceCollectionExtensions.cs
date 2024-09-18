using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sloth.Domain.Constants;
using Sloth.Domain.Entities;
using Sloth.Domain.Repositories;
using Sloth.Domain.Repositories.Configuration;
using Sloth.Infrastructure.DatabaseContext;
using Sloth.Infrastructure.DTO;
using Sloth.Infrastructure.Repositories;
using Sloth.Infrastructure.Repositories.Configuration;
using Sloth.Infrastructure.Seed;

namespace Sloth.Infrastructure.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddInfractructure(this IServiceCollection services, IConfiguration configuration)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        var connectionString = configuration.GetConnectionString("Sloth");

        services.AddDbContext<SlothDbContext>(options => 
            options.UseSqlServer(connectionString, 
                options => options.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null))
            .EnableSensitiveDataLogging());

        services.AddIdentityApiEndpoints<User>()
            .AddRoles<UserRole>()
            .AddEntityFrameworkStores<SlothDbContext>();

        services.AddAutoMapper(applicationAssembly);

        // Build Config Repository
        services.AddScoped<IConfigRepository, ConfigRepository>();

        // Seed Repositories
        services.AddScoped<ISlothSeeder, SlothSeeder>();

        // Domain Repositories
        services.AddScoped<IUIElementsRepository, UIElementsRepository>();

    }

    public static async Task ConfigureAppConfig(this IServiceCollection services)
    {
        // Create a temporary service provider to retrieve DbContext and ConfigRepository
        using (var tempServiceProvider = services.BuildServiceProvider())
        {
            using (var scope = tempServiceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<SlothDbContext>();

                if (await dbContext.Database.CanConnectAsync())
                {
                    var configRepository = scope.ServiceProvider.GetRequiredService<IConfigRepository>();
                    var sysOptions = await configRepository.ListConfigSystemOptionAsync(); // You can make this async if needed

                    var tokenLifetime = DefualtConfigSystemOptions.TokenLifetime;
                    var refreshTokenLifetime = DefualtConfigSystemOptions.RefreshTokenLifetime;

                    int.TryParse(sysOptions?.FirstOrDefault(o => o.OptionID == ConfigSystemOptions.TokenLifetime)?.OptionValue, out tokenLifetime);
                    int.TryParse(sysOptions?.FirstOrDefault(o => o.OptionID == ConfigSystemOptions.RefreshTokenLifetime)?.OptionValue, out refreshTokenLifetime);

                    var passwordComplexity = sysOptions?.FirstOrDefault(o => o.OptionID == ConfigSystemOptions.PasswordComplexity)?.OptionValue ?? DefualtConfigSystemOptions.PasswordComplexity;

                    var activeEndpoints = await configRepository.ListConfigEndpointAsync();

                    var appConfig = new AppConfig
                    {
                        TokenLifetime = tokenLifetime,
                        RefreshTokenLifetime = refreshTokenLifetime,
                        PasswordComplexity = passwordComplexity,
                        ActiveEndpoints = activeEndpoints ?? new List<string>()
                    };

                    // Register AppConfig globally in the DI container as a singleton
                    services.AddSingleton(appConfig);
                }
                else
                {
                    // Log the issue (optional, add a logger if needed)
                    Console.WriteLine("Database does not exist or is inaccessible. Skipping AppConfig setup.");

                    // Use default AppConfig values as fallback
                    var appConfig = new AppConfig
                    {
                        TokenLifetime = DefualtConfigSystemOptions.TokenLifetime,
                        RefreshTokenLifetime = DefualtConfigSystemOptions.RefreshTokenLifetime,
                        PasswordComplexity = DefualtConfigSystemOptions.PasswordComplexity,
                        ActiveEndpoints = new List<string>() // Default empty list
                    };

                    // Register AppConfig globally in the DI container as a singleton
                    services.AddSingleton(appConfig);
                }
            }
        }
    }
}
