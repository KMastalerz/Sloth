using Sloth.Infrastructure.Seed;

namespace Sloth.API.Extensions;
public static class WebApplicationExtensions
{

    public static async Task RunSeed(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<ISlothSeeder>();
        await seeder.Seed();
    }
}
