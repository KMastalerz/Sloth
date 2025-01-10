using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using sloth.Domain.Entities;
using sloth.Infrastructure.DatabaseContext;
using System.Text.Json;

namespace sloth.Infrastructure.Seed;
internal class Seeder(ILogger<Seeder> logger, SlothDbContext dbContext) : ISeeder
{
    public async Task Seed()
    {
        string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string seedDataDirectory = Path.Combine(appDirectory, "DataSeed");

        await SeedUserRoles(seedDataDirectory);
        await SeedJobPriorities(seedDataDirectory);
        await SeedJobStatuses(seedDataDirectory);
        await SeedProducts(seedDataDirectory);
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
    private async Task SeedJobPriorities(string seedDataDirectory)
    {
        string prioritesDirectory = Path.Combine(seedDataDirectory, "JobPriority.json");

        if (!File.Exists(prioritesDirectory))
        {
            logger.LogInformation("There are no job priorities to seed");
            return;
        };

        string prioritesJson = await File.ReadAllTextAsync(prioritesDirectory);
        var priorites = JsonSerializer.Deserialize<List<JobPriority>>(prioritesJson);

        // remove priorites that already exist in the database
        var existingPriorites = await dbContext.JobPriority.ToListAsync();
        priorites = priorites?.Where(
            ur => !existingPriorites.Any(eur => eur.PriorityLevel == ur.PriorityLevel)).ToList();

        if (!priorites?.Any() ?? true)
        {
            logger.LogInformation("There are no job priorities to seed");
            return;
        }

        await dbContext.JobPriority.AddRangeAsync(priorites!);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Added: {Count} job priorites", priorites!.Count);
    }
    private async Task SeedJobStatuses(string seedDataDirectory)
    {
        string statusesDirectory = Path.Combine(seedDataDirectory, "JobStatus.json");

        if (!File.Exists(statusesDirectory))
        {
            logger.LogInformation("There are no job statuses to seed");
            return;
        };

        string statusesJson = await File.ReadAllTextAsync(statusesDirectory);
        var statuses = JsonSerializer.Deserialize<List<JobStatus>>(statusesJson);

        // remove statuses that already exist in the database
        var existingStatuses = await dbContext.JobStatus.ToListAsync();
        statuses = statuses?.Where(
            ur => !existingStatuses.Any(eur => eur.Status == ur.Status)).ToList();

        if (!statuses?.Any() ?? true)
        {
            logger.LogInformation("There are no job statuses to seed");
            return;
        }

        await dbContext.JobStatus.AddRangeAsync(statuses!);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Added: {Count} job statuses", statuses!.Count);
    }
    private async Task SeedProducts(string seedDataDirectory)
    {
        string productsDirectory = Path.Combine(seedDataDirectory, "Products.json");

        if (!File.Exists(productsDirectory))
        {
            logger.LogInformation("There are no products to seed");
            return;
        };

        string productsJson = await File.ReadAllTextAsync(productsDirectory);
        var products = JsonSerializer.Deserialize<List<Product>>(productsJson);

        // remove products that already exist in the database
        var existingProducts = await dbContext.Product.ToListAsync();
        products = products?.Where(
            ur => !existingProducts.Any(eur => eur.ProductName == ur.ProductName)).ToList();

        if (!products?.Any() ?? true)
        {
            logger.LogInformation("There are no products to seed");
            return;
        }

        await dbContext.Product.AddRangeAsync(products!);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Added: {Count} products", products!.Count);
    }
}
