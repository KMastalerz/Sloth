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

            await SeedSystemOptions();
            await SeedWebPages();
            await SeedWebPanels();
            await SeedWebSections();
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
            var languageSeed = GetData<LanguageSeed>(SeedFile.Language);

            if (languageSeed is not null)
            {
                var language = mapper.Map<Language>(languageSeed);

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
            var systemOptionsSeed = GetData<List<SystemOptionSeed>>(SeedFile.SystemOptions);

            if (systemOptionsSeed is not null && systemOptionsSeed.Any())
            {
                var systemOptions = mapper.Map<IEnumerable<SystemOption>?>(systemOptionsSeed);

                // filter systemOptionsSeed by those that do not exist in database
                systemOptions = systemOptions!.Where(so => !dbContext.SystemOption.Any(dbso => dbso.OptionID == so.OptionID));

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
            var webPageSeed = GetData<List<WebPageSeed>>(SeedFile.WebPages);

            if (webPageSeed is not null && webPageSeed.Any())
            {
                var webPages = mapper.Map<IEnumerable<WebPage>>(webPageSeed);
                var webPageSecurities = mapper.Map<IEnumerable<WebPageSecurity>>(webPageSeed);

                // filter webPages by those that do not exist in database
                webPages = webPages!.Where(wp => !dbContext.WebPage.Any(dbwp => dbwp.PageID == wp.PageID));

                // filter webPageSecurities by those that do not exist in database
                webPageSecurities = webPageSecurities!.Where(wps => !dbContext.WebPageSecurity.Any(dbwps => dbwps.PageID == wps.PageID));
                webPageSecurities = webPageSecurities!.Where(wps => wps.UserGroup is not null);

                if (webPages.Any())
                {
                    var count = webPages.Count();
                    await dbContext.WebPage.AddRangeAsync(webPages);
                    await dbContext.SaveChangesAsync();
                    logger.LogInformation("Added {Count} {object} to database", count, nameof(WebPage));
                }

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
            logger.LogError("Error {Exception} during seeding {object}", ex.Message, nameof(WebPage));
        }
    }

    private async Task SeedWebPanels()
    {
        try
        {
            var webPanelSeed = GetData<List<WebPanelSeed>>(SeedFile.WebPanels);

            if (webPanelSeed is not null && webPanelSeed.Any())
            {
                var webPanels = mapper.Map<IEnumerable<WebPanel>>(webPanelSeed);

                // filter webControls by those that do not exist in database
                webPanels = webPanels!.Where(wc => !dbContext.WebPanel.Any(dbwc => dbwc.PageID == wc.PageID && dbwc.PanelID == wc.PanelID));

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

    private async Task SeedWebSections()
    {
        try
        {
            var webSectionsSeed = GetData<List<WebSectionSeed>>(SeedFile.WebSections);

            if (webSectionsSeed is not null && webSectionsSeed.Any())
            {
                var webSections = mapper.Map<IEnumerable<WebSection>>(webSectionsSeed);

                // filter webControls by those that do not exist in database
                webSections = webSections!.Where(wc => !dbContext.WebControl.Any(dbwc => dbwc.PageID == wc.PageID && dbwc.PanelID == wc.PanelID && dbwc.SectionID == wc.SectionID));

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
            var webControlSeed = GetData<List<WebControlSeed>>(SeedFile.WebControls);

            if (webControlSeed is not null && webControlSeed.Any())
            {
                var webControls = mapper.Map<IEnumerable<WebControl>>(webControlSeed);

                // filter webControls by those that do not exist in database
                webControls = webControls!.Where(wc => !dbContext.WebControl.Any(dbwc => dbwc.PageID == wc.PageID && dbwc.PanelID == wc.PanelID && dbwc.SectionID == wc.SectionID && dbwc.ControlID == wc.ControlID));

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
            var userRolesSeed = GetData<List<UserRoleSeed>>(SeedFile.UserRoles);

            if (userRolesSeed is not null && userRolesSeed.Any())
            {
                var userRoles = mapper.Map<IEnumerable<UserRole>>(userRolesSeed);

                // filter userRolesSeed by those that do not exist in database
                userRoles = userRoles!.Where(ur => !dbContext.UserRole.Any(dbur => dbur.RoleName == ur.RoleName));

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
            var userGroupSeed = GetData<UserGroupSeed>(SeedFile.UserGroup);

            if (userGroupSeed is not null)
            {
                var userGroup = mapper.Map<UserGroup>(userGroupSeed);

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
