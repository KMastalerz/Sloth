using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sloth.Domain.Entities;
using Sloth.Infrastructure.DatabaseContext;
using Sloth.Shared.Helpers;

namespace Sloth.Infrastructure.Seed;

internal class SlothSeeder(ILogger<SlothSeeder> logger, SlothDbContext dbContext, IMapper mapper, IConfiguration configuration) : ISlothSeeder
{
    public async Task Seed()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            var seedOptions = new SeederOptions();
            configuration.GetSection(SeedConfig.SeederOptions).Bind(seedOptions);

            if (seedOptions is not null)
            {
                if (seedOptions.ReplaceWebPages)
                {
                    await ClearWebPages();
                }
            }

            //await SeedSystemOptions(); // TODO: Add system options later
            await SeedWebPages();
            await SeedWebPageSecurities();
            await SeedWebPanels();
            await SeecWebSections();
            await SeedWebControls();
            await SeedLanguage();
            await SeedRoles();
            await SeedGroup();
            await SeedUser();
        }
    }

    private async Task SeedLanguage()
    {
        try
        {
            var language = GetData<Language>(SeedFile.Language);

            if (language is not null)
            {
                // check if language already exists in database
                if (!dbContext.Language.Any(l => l.LanguageCode == language.LanguageCode))
                {
                    await dbContext.Language.AddAsync(language);
                    await dbContext.SaveChangesAsync();
                    logger.LogInformation("Added {object}: {Value} to database", nameof(Language), language.LanguageName);
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError("Error {Exception} during seeding {object}", ex.Message, nameof(Language));
        }
    }

    private async Task SeedSystemOptions()
    {
        try
        {
            var systemOptions = GetData<List<SystemOption>>(SeedFile.SystemOptions);

            if (systemOptions is not null && systemOptions.Any())
            {
                // filter systemOptionsSeed by those that do not exist in database
                systemOptions = systemOptions!.Where(so => !dbContext.SystemOption.Any(dbso => dbso.OptionID == so.OptionID)).ToList();

                if (systemOptions.Any())
                {
                    var count = systemOptions.Count();
                    await dbContext.SystemOption.AddRangeAsync(systemOptions);
                    await dbContext.SaveChangesAsync();
                    logger.LogInformation("Added {Count} {object} to database", count, nameof(SystemOption));
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError("Error {Exception} during seeding {object}", ex.Message, nameof(SystemOption));
        }
    }

    private async Task SeedWebPages()
    {
        try
        {
            var webPages = GetData<List<WebPage>>(SeedFile.WebPages);

            if (webPages is not null && webPages.Any())
            {
                // filter webPages by those that do not exist in database
                webPages = webPages!.Where(wp => !dbContext.WebPage.Any(dbwp => dbwp.PageID == wp.PageID)).ToList();

                if (webPages.Any())
                {
                    var count = webPages.Count();
                    await dbContext.WebPage.AddRangeAsync(webPages);
                    await dbContext.SaveChangesAsync();
                    logger.LogInformation("Added {Count} {object} to database", count, nameof(WebPage));
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError("Error {Exception} during seeding {object}", ex.Message, nameof(WebPage));
        }
    }

    private async Task SeedWebPageSecurities()
    {
        try
        {
            var webPageSecurities = GetData<List<WebPageSecurity>>(SeedFile.WebPageSecurities);

            if (webPageSecurities is not null && webPageSecurities.Any())
            {
                // filter webPages by those that do not exist in database
                webPageSecurities = webPageSecurities!.Where(wps => !dbContext.WebPageSecurity.Any(dbwps => dbwps.PageID == wps.PageID && dbwps.UserGroup == wps.UserGroup)).ToList();

                if (webPageSecurities.Any())
                {
                    var count = webPageSecurities.Count();
                    await dbContext.WebPageSecurity.AddRangeAsync(webPageSecurities);
                    await dbContext.SaveChangesAsync();
                    logger.LogInformation("Added {Count} {object} to database", count, nameof(WebPageSecurity));
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError("Error {Exception} during seeding {object}", ex.Message, nameof(WebPageSecurity));
        }

    }

    private async Task SeedWebPanels()
    {
        try
        {
            var webPanels = GetData<List<WebPanel>>(SeedFile.WebPanels);

            if (webPanels is not null && webPanels.Any())
            {
                // filter webPages by those that do not exist in database
                webPanels = webPanels!.Where(wp => !dbContext.WebPanel.Any(dbwp => dbwp.PageID == wp.PageID && dbwp.PanelID == wp.PanelID)).ToList();

                if (webPanels.Any())
                {
                    var count = webPanels.Count();
                    await dbContext.WebPanel.AddRangeAsync(webPanels);
                    await dbContext.SaveChangesAsync();
                    logger.LogInformation("Added {Count} {object} to database", count, nameof(WebPanel));
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError("Error {Exception} during seeding {object}", ex.Message, nameof(WebPanel));
        }
    }

    private async Task SeecWebSections()
    {
        try
        {
            var webSections = GetData<List<WebSection>>(SeedFile.WebSections);

            if (webSections is not null && webSections.Any())
            {
                // filter webSections by those that do not exist in database
                webSections = webSections!.Where(ws => !dbContext.WebSection.Any(dbws => dbws.PageID == ws.PageID && dbws.PanelID == ws.PanelID && dbws.SectionID == ws.SectionID)).ToList();

                if (webSections.Any())
                {
                    var count = webSections.Count();
                    await dbContext.WebSection.AddRangeAsync(webSections);
                    await dbContext.SaveChangesAsync();
                    logger.LogInformation("Added {Count} {object} to database", count, nameof(WebSection));
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError("Error {Exception} during seeding {object}", ex.Message, nameof(WebSection));
        }
    }

    private async Task SeedWebControls()
    {
        try
        {
            var webControls = GetData<List<WebControl>>(SeedFile.WebControls);

            if (webControls is not null && webControls.Any())
            {

                // filter webControls by those that do not exist in database
                webControls = webControls!.Where(wc => !dbContext.WebControl.Any(dbwc => dbwc.PageID == wc.PageID && dbwc.PanelID == wc.PanelID && dbwc.ControlID == wc.ControlID)).ToList();

                if (webControls.Any())
                {
                    var count = webControls.Count();
                    await dbContext.WebControl.AddRangeAsync(webControls);
                    await dbContext.SaveChangesAsync();
                    logger.LogInformation("Added {Count} {object} to database", count, nameof(WebControl));
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError("Error {Exception} during seeding {object}", ex.Message, nameof(WebControl));
        }
    }

    private async Task SeedRoles()
    {
        try
        {
            var userRoles = GetData<List<UserRole>>(SeedFile.UserRoles);

            if (userRoles is not null && userRoles.Any())
            {
                // filter userRolesSeed by those that do not exist in database
                userRoles = userRoles!.Where(ur => !dbContext.UserRole.Any(dbur => dbur.RoleName == ur.RoleName)).ToList();

                if (userRoles.Any())
                {
                    var count = userRoles.Count();
                    await dbContext.UserRole.AddRangeAsync(userRoles);
                    await dbContext.SaveChangesAsync();
                    logger.LogInformation("Added {Count} {object} to database", count, nameof(UserRole));
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError("Error {Exception} during seeding {object}", ex.Message, nameof(UserRole));
        }
    }

    private async Task SeedGroup()
    {
        try
        {
            var userGroup = GetData<UserGroup>(SeedFile.UserGroup);

            if (userGroup is not null)
            {
                // check if userGroup already exists in database
                if (!dbContext.UserGroup.Any(ug => ug.GroupName == userGroup.GroupName))
                {
                    await dbContext.UserGroup.AddAsync(userGroup);
                    await dbContext.SaveChangesAsync();
                    logger.LogInformation("Added {object}: {Value} to database", nameof(UserGroup), userGroup.GroupName);
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError("Error {Exception} during seeding {object}", ex.Message, nameof(UserGroup));
        }
    }

    private async Task SeedUser()
    {
        try
        {
            var userSeed = GetData<UserSeed>(SeedFile.User);

            if (userSeed is not null)
            {
                var user = mapper.Map<User>(userSeed);

                // get user role && group from database
                var userRole = await dbContext.UserRole.SingleOrDefaultAsync(ur => ur.RoleName == userSeed.RoleName);
                var userGroup = await dbContext.UserGroup.SingleOrDefaultAsync(ug => ug.GroupName == userSeed.GroupName);

                if (userRole is null || userGroup is null) return;

                // assign user role and group
                user.RoleID = userRole.RoleID;
                user.GroupID = userGroup.GroupID;

                // check if user already exists in database
                if (!dbContext.User.Any(u => u.UserName == user.UserName))
                {
                    var passwordHasher = new PasswordHasher<User>();
                    user.PasswordHash = passwordHasher.HashPassword(user, userSeed.Password);
                    await dbContext.User.AddAsync(user);
                    await dbContext.SaveChangesAsync();
                    logger.LogInformation("Added {object}: {Value} to database", nameof(User), user.UserName);
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError("Error {Exception} during seeding {object}", ex.Message, nameof(User));
        }
    }

    private async Task ClearWebPages()
    {
        try
        {
            var webPages = await dbContext.WebPage.ToListAsync();
            dbContext.WebPage.RemoveRange(webPages);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError("Error {Exception} during clearing {object}", ex.Message, nameof(WebPage));
        }
    }

    private T? GetData<T>(string dataType) where T : class, new()
    {
        string? fileName = null;

        switch (dataType)
        {
            case SeedFile.Language:
                fileName = SeedPath.Language;
                break;
            case SeedFile.SystemOptions:
                fileName = SeedPath.SystemOptions;
                break;
            case SeedFile.WebControls:
                fileName = SeedPath.WebControls;
                break;
            case SeedFile.WebPages:
                fileName = SeedPath.WebPages;
                break;
            case SeedFile.WebPageSecurities:
                fileName = SeedPath.WebPageSecurities;
                break;
            case SeedFile.WebPanels:
                fileName = SeedPath.WebPanels;
                break;
            case SeedFile.WebSections:
                fileName = SeedPath.WebSections;
                break;
            case SeedFile.User:
                fileName = SeedPath.User;
                break;
            case SeedFile.UserGroup:
                fileName = SeedPath.UserGroup;
                break;
            case SeedFile.UserRoles:
                fileName = SeedPath.UserRoles;
                break;
            default:
                fileName = null;
                break;
        }

        // Guard 1: if relative path not found, return default
        if (fileName is null) return default;

        var relativePath = Path.Combine("..", SeedPath.Base, fileName);
        var fullPath = Path.GetFullPath(relativePath);

        // Guard 2: if file not found, return default
        if (!File.Exists(fullPath)) return default;

        var jsonString = File.ReadAllText(fullPath);

        // Return: Return deserialized object or if json string is empty or incorrect, return default 
        return jsonString.TryConvert<T>(default);
    }
}
