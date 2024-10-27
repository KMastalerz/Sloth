using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sloth.Domain.Constants;
using Sloth.Domain.Entities;
using Sloth.Domain.Services;
using Sloth.Domain.Repositories;
using Sloth.Infrastructure.DatabaseContext;
using Sloth.Infrastructure.Services;
using Sloth.Infrastructure.Repositories;
using Sloth.Infrastructure.Seed;

namespace Sloth.Infrastructure.Extensions;
public static class ServiceCollectionExtensions
{ 
    public static void AddInfractructure(this IServiceCollection services, IConfiguration configuration)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        var connectionString = configuration.GetConnectionString(ConfigurationKeys.ConnectionString);

        services.AddDbContext<SlothDbContext>(options =>
            options.UseSqlServer(connectionString)
            .EnableSensitiveDataLogging());

        services.AddAutoMapper(applicationAssembly);

        // Register services
        services.AddScoped<ISecurityService, SecurityService>();

        // Register password hasher
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        // Register seeder
        services.AddScoped<ISlothSeeder, SlothSeeder>();

        // Register domain repositories
        services.AddScoped<IConfigurationService, ConfigurationService>();
        services.AddScoped<ISecurityRepository, SecurityRepository>();
        services.AddScoped<IUIElementsRepository, UIElementsRepository>();
    }
}
