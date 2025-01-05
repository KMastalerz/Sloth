using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using sloth.Domain.Entities;
using sloth.Infrastructure.DatabaseContext;
using System.Text.Json;

namespace sloth.Infrastructure.Seed;
internal class Seeder(ILogger<Seeder> logger, SlothDbContext dbContext, IMapper mapper) : ISeeder
{
    public async Task Seed()
    {
        string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string seedDataDirectory = Path.Combine(appDirectory, "DataSeed");

        await SeedUserRoles(seedDataDirectory);
    }

    private async Task SeedUserRoles(string seedDataDirectory)
    {
        string userRolesDirectory = Path.Combine(seedDataDirectory, "UserRoles.json");

        if (!File.Exists(userRolesDirectory))
        {
            logger.LogInformation("There are no user roles to seed");
            return;
        };

        string userRolesJson = await File.ReadAllTextAsync(userRolesDirectory);
        var userRoles = JsonSerializer.Deserialize<List<UserRole>>(userRolesJson);

        // remove userRoles that already exist in the database
        var existingUserRoles = await dbContext.UserRole.ToListAsync();
        userRoles = userRoles?.Where(
            ur => !existingUserRoles.Any(eur => eur.RoleCode == ur.RoleCode)).ToList();

        if (!userRoles?.Any() ?? true)
        {
            logger.LogInformation("There are no user roles to seed");
            return;
        }

        await dbContext.UserRole.AddRangeAsync(userRoles!);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Added: {Count} user roles", userRoles!.Count);
    }
}
