using sloth.Application.Models.Configuration;
using sloth.Infrastructure.Seed;
using sloth.Utilities.Constants;

namespace sloth.API.Extensions;

public static class WebApplicationExtensions
{
    public static async Task RunSeed(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<ISeeder>();
        await seeder.Seed();
    }
    public static void AddCors(this WebApplication app, IConfiguration configuration)
    {
        var appConfig = configuration.GetSection(ConfigurationKeys.Configuration).Get<Configuration>()!;
        var hosts = appConfig.AllowedHosts;

        if (hosts.Contains("*"))
            app.UseCors(builder =>
                builder.SetIsOriginAllowed(_ => true)
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials());
        else
            app.UseCors(builder =>
                builder.WithOrigins(hosts.ToArray())
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials());
    }
}
