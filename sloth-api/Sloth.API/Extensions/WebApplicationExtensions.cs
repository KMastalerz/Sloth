using Sloth.Infrastructure.Seed;
using Sloth.Infrastructure.Services;

namespace Sloth.API.Extensions;
public static class WebApplicationExtensions
{

    public static async Task RunSeed(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<ISlothSeeder>();
        await seeder.Seed();
    }

    public static void AddCors(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var configurationService = scope.ServiceProvider.GetRequiredService<IConfigurationService>();
        var hosts = configurationService.GetAppSettings.AllowedHosts;

        if(hosts.Contains("*"))
            app.UseCors(builder =>
                builder.SetIsOriginAllowed(_ => true) 
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials());
        else 
            app.UseCors(builder =>
                builder.WithOrigins(hosts.Split(','))
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials());
    }
}
