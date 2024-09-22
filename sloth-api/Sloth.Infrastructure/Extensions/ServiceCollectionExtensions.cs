using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Sloth.Domain.Constants;
using Sloth.Domain.Entities;
using Sloth.Domain.Services;
using Sloth.Domain.Repositories;
using Sloth.Infrastructure.DatabaseContext;
using Sloth.Infrastructure.Services;
using Sloth.Infrastructure.Repositories;
using Sloth.Infrastructure.Seed;
using System.Text;

namespace Sloth.Infrastructure.Extensions;
public static class ServiceCollectionExtensions
{
    public static IConfigurationService InitializeInfrastucture(this IServiceCollection services, IConfiguration configuration)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        var connectionString = configuration.GetConnectionString(ConfigurationKeys.ConnectionString);

        services.AddDbContext<SlothDbContext>(options =>
            options.UseSqlServer(connectionString)
            .EnableSensitiveDataLogging());

        // Register singleton configuration service
        var configurationService = new ConfigurationService(services, configuration);
        services.AddSingleton<IConfigurationService>(configurationService);

        return configurationService;
    }
    public static void AddInfractructure(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddAutoMapper(applicationAssembly);

        // Register services
        services.AddScoped<ISecurityService, SecurityService>();

        // Register password hasher
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        // Register seeder
        services.AddScoped<ISlothSeeder, SlothSeeder>();

        // Register domain repositories
        services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
        services.AddScoped<ISecurityRepository, SecurityRepository>();
        services.AddScoped<IUIElementsRepository, UIElementsRepository>();
    }

    public static void AddConfiguredAuthentication(this IServiceCollection services, IConfigurationService configurationService)
    {
        var secConfig = configurationService.SecurityConfig;

        services.AddAuthentication(option =>
        {
            // Setup authentication options
            option.DefaultAuthenticateScheme = AuthScheme.Bearer;
            option.DefaultScheme = AuthScheme.Bearer;
            option.DefaultChallengeScheme = AuthScheme.Bearer;
        }).AddJwtBearer(config =>
        {
            config.RequireHttpsMetadata = false;
            config.SaveToken = true;
            config.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = secConfig.TokenIssuer,
                ValidAudience = secConfig.TokenIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secConfig.TokenKey))
            };
        });
    }
}
