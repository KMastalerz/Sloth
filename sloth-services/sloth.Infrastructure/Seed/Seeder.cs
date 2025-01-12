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
        await SeedPriorities(seedDataDirectory);
        await SeedStatuses(seedDataDirectory);
        await SeedProducts(seedDataDirectory);
        await SeedClients(seedDataDirectory);
        await SeedClientProducts(seedDataDirectory);
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
    private async Task SeedPriorities(string seedDataDirectory)
    {
        string prioritesDirectory = Path.Combine(seedDataDirectory, "Priority.json");

        if (!File.Exists(prioritesDirectory))
        {
            logger.LogInformation("There are no priorities to seed");
            return;
        };

        string prioritesJson = await File.ReadAllTextAsync(prioritesDirectory);
        var priorites = JsonSerializer.Deserialize<List<Priority>>(prioritesJson);

        // remove priorites that already exist in the database
        var existingPriorites = await dbContext.Priority.ToListAsync();
        priorites = priorites?.Where(
            ur => !existingPriorites.Any(eur => eur.PriorityLevel == ur.PriorityLevel)).ToList();

        if (!priorites?.Any() ?? true)
        {
            logger.LogInformation("There are no priorities to seed");
            return;
        }

        await dbContext.Priority.AddRangeAsync(priorites!);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Added: {Count} priorites", priorites!.Count);
    }
    private async Task SeedStatuses(string seedDataDirectory)
    {
        string statusesDirectory = Path.Combine(seedDataDirectory, "Status.json");

        if (!File.Exists(statusesDirectory))
        {
            logger.LogInformation("There are no statuses to seed");
            return;
        };

        string statusesJson = await File.ReadAllTextAsync(statusesDirectory);
        var statuses = JsonSerializer.Deserialize<List<Status>>(statusesJson);

        // remove statuses that already exist in the database
        var existingStatuses = await dbContext.Status.ToListAsync();
        statuses = statuses?.Where(
            s => !existingStatuses.Any(es => es.StatusValue == s.StatusValue && es.Type == s.Type)).ToList();

        if (!statuses?.Any() ?? true)
        {
            logger.LogInformation("There are no statuses to seed");
            return;
        }

        await dbContext.Status.AddRangeAsync(statuses!);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Added: {Count} statuses", statuses!.Count);
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
            ur => !existingProducts.Any(eur => eur.Name == ur.Name)).ToList();

        if (!products?.Any() ?? true)
        {
            logger.LogInformation("There are no products to seed");
            return;
        }

        await dbContext.Product.AddRangeAsync(products!);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Added: {Count} products", products!.Count);
    }
    private async Task SeedClients(string seedDataDirectory)
    {
        string clientsDirectory = Path.Combine(seedDataDirectory, "Clients.json");

        if (!File.Exists(clientsDirectory))
        {
            logger.LogInformation("There are no clients to seed");
            return;
        };

        string clientsJson = await File.ReadAllTextAsync(clientsDirectory);
        var clients = JsonSerializer.Deserialize<List<Client>>(clientsJson);

        // remove clients that already exist in the database
        var existingclients = await dbContext.Client.ToListAsync();
        clients = clients?.Where(
            ur => !existingclients.Any(eur => eur.Alias == ur.Alias)).ToList();

        if (!clients?.Any() ?? true)
        {
            logger.LogInformation("There are no clients to seed");
            return;
        }

        await dbContext.Client.AddRangeAsync(clients!);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Added: {Count} clients", clients!.Count);
    }
    private async Task SeedClientProducts(string seedDataDirectory)
    {
        string clientProduckLinksDirectory = Path.Combine(seedDataDirectory, "ClientProducts.json");

        if (!File.Exists(clientProduckLinksDirectory))
        {
            logger.LogInformation("There are no client product links to seed");
            return;
        };

        string clientProduckLinksJson = await File.ReadAllTextAsync(clientProduckLinksDirectory);
        var clientProduckLinks = JsonSerializer.Deserialize<List<SeedKeyValueModel>>(clientProduckLinksJson);

        // remove clientProduckLinks that already exist in the database, that clients do not exists, or products that do not exist
        var existingclientProduckLinks = await dbContext.ClientProductLink.ToListAsync();
        var existingProducts = await dbContext.Product.ToListAsync();
        var existingClients = await dbContext.Client.ToListAsync();


        var newList = (
            from kv in clientProduckLinks
            join c in existingClients on kv.Key equals c.Alias
            join p in existingProducts on kv.Value equals p.Alias
            join l in existingclientProduckLinks
                on new { c.ClientID, p.ProductID } equals new { l.ClientID, l.ProductID } into linksGroup
            from existingLink in linksGroup.DefaultIfEmpty()
            where existingLink is null
            select new ClientProductLink
            {
                ClientID = c.ClientID,
                ProductID = p.ProductID
            }
        ).ToList();

        if (!newList?.Any() ?? true)
        {
            logger.LogInformation("There are no clientProduckLinks to seed");
            return;
        }

        await dbContext.ClientProductLink.AddRangeAsync(newList!);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Added: {Count} clientProduckLinks", clientProduckLinks!.Count);
    }
}
