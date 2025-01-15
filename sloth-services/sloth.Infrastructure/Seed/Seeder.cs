using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using sloth.Domain.Entities;
using sloth.Infrastructure.DatabaseContext;
using sloth.Infrastructure.Seed.Models;

namespace sloth.Infrastructure.Seed;
internal class Seeder(ILogger<Seeder> logger, SlothDbContext dbContext) : ISeeder
{
    private string seedDataDirectory = string.Empty;
    public async Task Seed()
    {
        string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
        seedDataDirectory = Path.Combine(appDirectory, "DataSeed");

        await SeedUsers();
        await SeedUserRoles();
        await SeedUserRoleLinks();
        await SeedPriorities();
        await SeedStatuses();
        await SeedProducts();
        await SeedProductFunctionalities();
        await SeedTeams();
        await SeedTeamProducts();
        await SeedTeamUsers();
        await SeedClients();
        await SeedClientProducts();
        await SeedBugs();
        await SeedJobProjectLinks();
        await SeedJobFunctionalitiesLinks();
    }

    private async Task SeedUsers()
    {
        string usersDirectory = Path.Combine(seedDataDirectory, "Users.json");

        if (!File.Exists(usersDirectory))
        {
            logger.LogInformation("There are no users to seed");
            return;
        };

        string usersJson = await File.ReadAllTextAsync(usersDirectory);
        var users = JsonSerializer.Deserialize<List<User>>(usersJson);

        // remove users that already exist in the database
        var existingUsers = await dbContext.User.ToListAsync();
        users = users?.Where(
            ur => !existingUsers.Any(eur => eur.UserName == ur.UserName)).ToList();

        var passwordHasher = new PasswordHasher<User>();
        users?.ForEach(u =>
        {
            u.PasswordHash = passwordHasher.HashPassword(u, u.PasswordHash);
        });

        if (!users?.Any() ?? true)
        {
            logger.LogInformation("There are no users to seed");
            return;
        }

        await dbContext.User.AddRangeAsync(users!);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Added: {Count} users", users!.Count);
    }
    private async Task SeedUserRoles()
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
    private async Task SeedUserRoleLinks()
    {
        string userRoleLinksDirectory = Path.Combine(seedDataDirectory, "UserRoleLinks.json");

        if (!File.Exists(userRoleLinksDirectory))
        {
            logger.LogInformation("There are no user role links to seed");
            return;
        };

        string userRoleLinksJson = await File.ReadAllTextAsync(userRoleLinksDirectory);
        var userRoleLinks = JsonSerializer.Deserialize<List<UserRoleLinkSeed>>(userRoleLinksJson);

        // remove userRoleLinks that already exist in the database, that clients do not exists, or products that do not exist
        var existingUserRoleLinks = await dbContext.UserRoleLink.ToListAsync();
        var existingUserRoles = await dbContext.UserRole.ToListAsync();
        var existingUsers = await dbContext.User.ToListAsync();


        var newList = (
            from s in userRoleLinks
            join u in existingUsers on s.UserName equals u.UserName
            join r in existingUserRoles on s.RoleCode equals r.RoleCode
            join l in existingUserRoleLinks
                on new { u.UserID, r.RoleID } equals new { l.UserID, l.RoleID } into linksGroup
            from existingLink in linksGroup.DefaultIfEmpty()
            where existingLink is null
            select new UserRoleLink
            {
                UserID = u.UserID,
                RoleID = r.RoleID
            }
        ).ToList();

        if (!newList?.Any() ?? true)
        {
            logger.LogInformation("There are no user role links to seed");
            return;
        }

        await dbContext.UserRoleLink.AddRangeAsync(newList!);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Added: {Count} user role links", newList!.Count);
    }
    private async Task SeedPriorities()
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
    private async Task SeedStatuses()
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
    private async Task SeedProducts()
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
    private async Task SeedProductFunctionalities()
    {
        string productFunctionalitiesDirectory = Path.Combine(seedDataDirectory, "productFunctionalities.json");

        if (!File.Exists(productFunctionalitiesDirectory))
        {
            logger.LogInformation("There are no product functionalities to seed");
            return;
        };

        string productFunctionalitiesJson = await File.ReadAllTextAsync(productFunctionalitiesDirectory);
        var productFunctionalities = JsonSerializer.Deserialize<List<ProductFunctionalitiesSeed>>(productFunctionalitiesJson);
        var products = await dbContext.Product.ToListAsync();
        var newProductLinks = productFunctionalities?.Select(pf => new ProductFunctionality()
        {
            ProductID = products.First(p => p.Alias == pf.ProductAlias).ProductID,
            Name = pf.Name,
            Description = pf.Description,
            Tag = pf.Tag,
            TagColor = pf.TagColor
        }) ?? [];

        // remove productFunctionalities that already exist in the database
        var existingProductFunctionalities = await dbContext.ProductFunctionality.ToListAsync();

        newProductLinks = newProductLinks?.Where(
            pf => !existingProductFunctionalities.Any(epf => epf.Name == pf.Name && epf.ProductID == pf.ProductID)).ToList();

        if (!newProductLinks?.Any() ?? true)
        {
            logger.LogInformation("There are no product functionalities to seed");
            return;
        }

        await dbContext.ProductFunctionality.AddRangeAsync(newProductLinks!);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Added: {Count} product functionalities", newProductLinks!.Count());
    }
    private async Task SeedTeams()
    {
        string teamsDirectory = Path.Combine(seedDataDirectory, "Teams.json");

        if (!File.Exists(teamsDirectory))
        {
            logger.LogInformation("There are no teams to seed");
            return;
        };

        string teamsJson = await File.ReadAllTextAsync(teamsDirectory);
        var teams = JsonSerializer.Deserialize<List<Team>>(teamsJson);

        // remove teams that already exist in the database
        var existingTeams = await dbContext.Team.ToListAsync();
        teams = teams?.Where(
            t => !existingTeams.Any(et => et.Alias == t.Alias)).ToList();

        if (!teams?.Any() ?? true)
        {
            logger.LogInformation("There are no teams to seed");
            return;
        }

        await dbContext.Team.AddRangeAsync(teams!);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Added: {Count} teams", teams!.Count);
    }
    private async Task SeedTeamProducts()
    {
        string teamProductLinksDirectory = Path.Combine(seedDataDirectory, "TeamProducts.json");

        if (!File.Exists(teamProductLinksDirectory))
        {
            logger.LogInformation("There are no team product links to seed");
            return;
        };

        string teamProductLinksJson = await File.ReadAllTextAsync(teamProductLinksDirectory);
        var teamProductLinks = JsonSerializer.Deserialize<List<TeamProductLinkSeed>>(teamProductLinksJson);

        // remove teamProductLinks that already exist in the database, that clients do not exists, or products that do not exist
        var existingTeamProductLinks = await dbContext.TeamProductLink.ToListAsync();
        var existingProducts = await dbContext.Product.ToListAsync();
        var existingTeams = await dbContext.Team.ToListAsync();


        var newList = (
            from s in teamProductLinks
            join t in existingTeams on s.TeamAlias equals t.Alias
            join p in existingProducts on s.ProductAlias equals p.Alias
            join l in existingTeamProductLinks
                on new { t.TeamID, p.ProductID } equals new { l.TeamID, l.ProductID } into linksGroup
            from existingLink in linksGroup.DefaultIfEmpty()
            where existingLink is null
            select new TeamProductLink
            {
                TeamID = t.TeamID,
                ProductID = p.ProductID
            }
        ).ToList();

        if (!newList?.Any() ?? true)
        {
            logger.LogInformation("There are no team product links to seed");
            return;
        }

        await dbContext.TeamProductLink.AddRangeAsync(newList!);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Added: {Count} team product links", teamProductLinks!.Count);
    }
    private async Task SeedTeamUsers()
    {
        string teamUserLinksDirectory = Path.Combine(seedDataDirectory, "teamUsers.json");

        if (!File.Exists(teamUserLinksDirectory))
        {
            logger.LogInformation("There are no team user links to seed");
            return;
        };

        string teamUserLinksJson = await File.ReadAllTextAsync(teamUserLinksDirectory);
        var teamUserLinks = JsonSerializer.Deserialize<List<TeamUserLinkSeed>>(teamUserLinksJson);

        // remove teamUserLinks that already exist in the database, that clients do not exists, or products that do not exist
        var existingTeamUserLinks = await dbContext.TeamUserLink.ToListAsync();
        var existingUsers = await dbContext.User.ToListAsync();
        var existingTeams = await dbContext.Team.ToListAsync();


        var newList = (
            from s in teamUserLinks
            join t in existingTeams on s.TeamAlias equals t.Alias
            join u in existingUsers on s.UserName equals u.UserName
            join l in existingTeamUserLinks
                on new { t.TeamID, u.UserID } equals new { l.TeamID, l.UserID } into linksGroup
            from existingLink in linksGroup.DefaultIfEmpty()
            where existingLink is null
            select new TeamUserLink
            {
                TeamID = t.TeamID,
                UserID = u.UserID
            }
        ).ToList();

        if (!newList?.Any() ?? true)
        {
            logger.LogInformation("There are no team user links to seed");
            return;
        }

        await dbContext.TeamUserLink.AddRangeAsync(newList!);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Added: {Count} team user links", teamUserLinks!.Count);
    }
    private async Task SeedClients()
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
    private async Task SeedClientProducts()
    {
        string clientProductLinksDirectory = Path.Combine(seedDataDirectory, "ClientProducts.json");

        if (!File.Exists(clientProductLinksDirectory))
        {
            logger.LogInformation("There are no client product links to seed");
            return;
        };

        string clientProductLinksJson = await File.ReadAllTextAsync(clientProductLinksDirectory);
        var clientProductLinks = JsonSerializer.Deserialize<List<ClientProductLinkSeed>>(clientProductLinksJson);

        // remove clientProductLinks that already exist in the database, that clients do not exists, or products that do not exist
        var existingClientProductLinks = await dbContext.ClientProductLink.ToListAsync();
        var existingProducts = await dbContext.Product.ToListAsync();
        var existingClients = await dbContext.Client.ToListAsync();


        var newList = (
            from s in clientProductLinks
            join c in existingClients on s.ClientAlias equals c.Alias
            join p in existingProducts on s.ProductAlias equals p.Alias
            join l in existingClientProductLinks
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
            logger.LogInformation("There are no client product links to seed");
            return;
        }

        await dbContext.ClientProductLink.AddRangeAsync(newList!);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Added: {Count} client product links", newList!.Count);
    }
    private async Task SeedBugs()
    {
        string bugsDirectory = Path.Combine(seedDataDirectory, "Bugs.json");

        if (!File.Exists(bugsDirectory))
        {
            logger.LogInformation("There are no bugs to seed");
            return;
        };

        string bugsJson = await File.ReadAllTextAsync(bugsDirectory);
        var bugs = JsonSerializer.Deserialize<List<BugSeed>>(bugsJson);

        // remove bugs that already exist in the database
        var existingBugs = await dbContext.Bug.ToListAsync();
        var clients = await dbContext.Client.ToListAsync();
        var users = await dbContext.User.ToListAsync();

        var newList = (
            from s in bugs
            join c in clients on s.ClientAlias equals c.Alias
            join u in users on s.CreatedByAlias equals u.UserName
            join b in existingBugs 
                on s.Header equals b.Header into linksGroup
            from existingLink in linksGroup.DefaultIfEmpty()
            where existingLink is null
            select new Bug
            {
                InquiryNumber = s.InquiryNumber,
                RaisedDate = s.RaisedDate,
                Header = s.Header,
                Description = s.Description,
                PriorityID = s.PriorityID,
                CreatedDate = s.CreatedDate,
                StatusID = s.StatusID,
                Type = s.Type,
                CreatedByID = u.UserID,
                ClientID = c.ClientID,
            }
        ).ToList();

        if (!newList?.Any() ?? true)
        {
            logger.LogInformation("There are no bugs to seed");
            return;
        }

        await dbContext.Bug.AddRangeAsync(newList!);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Added: {Count} bugs", newList!.Count);
    }
    private async Task SeedJobProjectLinks()
    {
        string jobProductLinksDirectory = Path.Combine(seedDataDirectory, "JobProductLinks.json");

        if (!File.Exists(jobProductLinksDirectory))
        {
            logger.LogInformation("There are no job product links to seed");
            return;
        };

        string jobProductLinksJson = await File.ReadAllTextAsync(jobProductLinksDirectory);
        var jobProductLinks = JsonSerializer.Deserialize<List<JobProductLinkSeed>>(jobProductLinksJson);

        // remove jobProductLinks that already exist in the database, that jobs do not exists, or products that do not exist
        var existingJobProductLinks = await dbContext.JobProductLink.ToListAsync();
        var existingProducts = await dbContext.Product.ToListAsync();
        var existingjobs = await dbContext.Job.ToListAsync();


        var newList = (
            from s in jobProductLinks
            join j in existingjobs on s.JobHeader equals j.Header
            join p in existingProducts on s.ProductAlias equals p.Alias
            join l in existingJobProductLinks
                on new { j.JobID, p.ProductID } equals new { l.JobID, l.ProductID } into linksGroup
            from existingLink in linksGroup.DefaultIfEmpty()
            where existingLink is null
            select new JobProductLink
            {
                JobID = j.JobID,
                ProductID = p.ProductID
            }
        ).ToList();

        if (!newList?.Any() ?? true)
        {
            logger.LogInformation("There are no job product links to seed");
            return;
        }

        await dbContext.JobProductLink.AddRangeAsync(newList!);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Added: {Count} job product links", newList!.Count);
    }
    private async Task SeedJobFunctionalitiesLinks()
    {
        string jobFunctionalityLinksDirectory = Path.Combine(seedDataDirectory, "JobfunctionalityLinks.json");

        if (!File.Exists(jobFunctionalityLinksDirectory))
        {
            logger.LogInformation("There are no job functionality links to seed");
            return;
        };

        string jobFunctionalityLinksJson = await File.ReadAllTextAsync(jobFunctionalityLinksDirectory);
        var jobFunctionalityLinks = JsonSerializer.Deserialize<List<JobFunctionalityLinkSeed>>(jobFunctionalityLinksJson);

        // remove jobFunctionalityLinks that already exist in the database, that jobs do not exists, or functionalitys that do not exist
        var existingJobFunctionalityLinks = await dbContext.JobFunctionalityLink.ToListAsync();
        var existingFunctionalities = await dbContext.ProductFunctionality.ToListAsync();
        var existingjobs = await dbContext.Job.ToListAsync();


        var newList = (
            from s in jobFunctionalityLinks
            join j in existingjobs on s.JobHeader equals j.Header
            join f in existingFunctionalities on s.FunctionalityName equals f.Name
            join l in existingJobFunctionalityLinks
                on new { j.JobID, f.FunctionalityID } equals new { l.JobID, l.FunctionalityID } into linksGroup
            from existingLink in linksGroup.DefaultIfEmpty()
            where existingLink is null
            select new JobFunctionalityLink
            {
                JobID = j.JobID,
                FunctionalityID = f.FunctionalityID
            }
        ).ToList();

        if (!newList?.Any() ?? true)
        {
            logger.LogInformation("There are no job functionality links to seed");
            return;
        }

        await dbContext.JobFunctionalityLink.AddRangeAsync(newList!);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Added: {Count} job functionality links", newList!.Count);
    }
}
