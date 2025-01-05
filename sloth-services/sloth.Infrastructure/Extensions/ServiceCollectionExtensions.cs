using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using sloth.Domain.Cache;
using sloth.Domain.Entities;
using sloth.Domain.Repositories;
using sloth.Infrastructure.Cache;
using sloth.Infrastructure.DatabaseContext;
using sloth.Infrastructure.Repositories;
using sloth.Infrastructure.Seed;
using sloth.Utilities.Constants;

namespace sloth.Infrastructure.Extensions;
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

        // Register password hasher
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        // Register seeder
        services.AddScoped<ISeeder, Seeder>();

        // Register domain repositories
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IUserSettingsRepository, UserSettingsRepository>();

        // Register Caches
        services.AddScoped<IRoleCache, RoleCache>();
    }
}
