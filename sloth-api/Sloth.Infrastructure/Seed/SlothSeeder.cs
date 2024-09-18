using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sloth.Domain.Constants;
using Sloth.Domain.Entities;
using Sloth.Infrastructure.DatabaseContext;
using Sloth.Shared.Helpers;

namespace Sloth.Infrastructure.Seed;

// TO DO: All data has to be seed from json, not static like right now. Including System Options
internal class SlothSeeder(ILogger<SlothSeeder> logger, SlothDbContext dbContext, IMapper mapper, IConfiguration configuration) : ISlothSeeder
{
    public async Task Seed()
    {
        bool userForcedSeed = configuration.GetValue<bool>(SeedConfig.UseForcedSeed);
        if(userForcedSeed)
        {
            await dbContext.Database.ExecuteSqlRawAsync("delete from [SystemOption]");
            await dbContext.SaveChangesAsync();
            await dbContext.Database.ExecuteSqlRawAsync("delete from [UserRoleLink]");
            await dbContext.SaveChangesAsync();
            await dbContext.Database.ExecuteSqlRawAsync("delete from [UserClaim]");
            await dbContext.SaveChangesAsync();
            await dbContext.Database.ExecuteSqlRawAsync("delete from [User]");
            await dbContext.SaveChangesAsync();
            await dbContext.Database.ExecuteSqlRawAsync("delete from [UserRole]");
            await dbContext.SaveChangesAsync();
            await dbContext.Database.ExecuteSqlRawAsync("delete from [Language]");
            await dbContext.SaveChangesAsync();
            await dbContext.Database.ExecuteSqlRawAsync("delete from [WebPage]");
            await dbContext.SaveChangesAsync();
            await dbContext.Database.ExecuteSqlRawAsync("delete from [WebControl]");
            await dbContext.SaveChangesAsync();
            await dbContext.Database.ExecuteSqlRawAsync("delete from [WebPageSecurity]");
            await dbContext.SaveChangesAsync();
        }

        if (await dbContext.Database.CanConnectAsync())
        {
            // needed to perform transactional insert
            var strategy = dbContext.Database.CreateExecutionStrategy();
            await SeedLanguage();
            await SeedSystemOptions();
            await SeedUserRoles();
            await SeedUser(strategy);
            await SeedWebPages();
            await SeedWebControls();
        }
    }

    private async Task SeedLanguage()
    {
        var languageSeed = GetData<LanguageSeed>(SeedFile.Language);

        if (languageSeed is not null)
        {
            if (!dbContext.Language.Any(l => l.LanguageCode == languageSeed.LanguageCode))
            {
                var language = mapper.Map<Language>(languageSeed);
                await dbContext.Language.AddAsync(language);
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Added {object}: {Value} to database", nameof(Language), language.LanguageName);
            }
        }
    }

    private async Task SeedSystemOptions()
    {
        var systemOptionsSeed = GetData<List<SystemOptionSeed>>(SeedFile.SystemOptions);

        // set systemOptionsFiltered to be list that does not exsit in database 
        var systemOptionsFiltered = systemOptionsSeed?.Where(so => !dbContext.SystemOption.Any(dbso => dbso.OptionID == so.OptionID));

        if (systemOptionsFiltered is not null && systemOptionsFiltered.Any())
        {
            var systemOptions = mapper.Map<IEnumerable<SystemOption>?>(systemOptionsFiltered);

            if(systemOptions is not null)
            {
                await dbContext.SystemOption.AddRangeAsync(systemOptions);
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Added {Count} {object} to database", systemOptions.Count(), nameof(SystemOption));
            }
        }
    }

    private async Task SeedUserRoles()
    {
        var userRolesSeed = GetData<List<UserRoleSeed>>(SeedFile.UserRoles);

        // set userRolesFiltered to be list that does not exsit in database
        var userRolesFiltered = userRolesSeed?.Where(ur => !dbContext.Roles.Any(dbur => dbur.Name == ur.Name));

        if (userRolesFiltered is not null && userRolesFiltered.Any())
        {
            var userRoles = mapper.Map<IEnumerable<UserRole>?>(userRolesFiltered);

            if (userRoles is not null)
            {
                await dbContext.Roles.AddRangeAsync(userRoles);
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Added {Count} {object} to database", userRoles.Count(), nameof(UserRole));
            }
        }
    }

    private async Task SeedUser(IExecutionStrategy strategy)
    {

        var userSeed = GetData<UserSeed>(SeedFile.User);

        if (userSeed is null) return;

        if (!dbContext.Users.Any(u => u.UserName == userSeed.UserName))
        {
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await dbContext.Database.BeginTransactionAsync();

                try
                {
                    var userRole = await dbContext.Roles.FirstOrDefaultAsync(r => r.Name == userSeed.RoleName);
                    if (userRole is null)
                    {
                        logger.LogWarning("Not added {object} to database, property {property} missing in {object}.", nameof(User), nameof(userSeed.RoleName), nameof(UserRole));
                        return;
                    }

                    var user = mapper.Map<User>(userSeed);

                    #region [Password Hashing]
                    if (userSeed.Password is null)
                    {
                        logger.LogWarning("Not added {object} to database, missing {property}", nameof(User), nameof(userSeed.Password));
                        return;
                    }

                    var passwordHasher = new PasswordHasher<User>();

                    user.PasswordHash = passwordHasher.HashPassword(user, userSeed.Password);
                    user.NormalizedEmail = user.Email!.ToUpper();
                    user.NormalizedUserName = user.UserName!.ToUpper();

                    await dbContext.Users.AddAsync(user);
                    await dbContext.SaveChangesAsync();
                    #endregion

                    #region[UserRoleLink]
                    var userRoleLink = new UserRoleLink
                    {
                        UserId = user.Id,
                        RoleId = userRole.Id
                    };

                    await dbContext.UserRoles.AddAsync(userRoleLink);

                    await dbContext.SaveChangesAsync();
                    #endregion

                    #region[UserClaim]

                    if(userSeed.UserGroup is not null)
                    {
                        var userClaim = new UserClaim
                        {
                            UserId = user.Id,
                            ClaimType = SlothClaims.UserGroup,
                            ClaimValue = userSeed.UserGroup
                        };
                        await dbContext.UserClaims.AddAsync(userClaim);
                        await dbContext.SaveChangesAsync();
                    }

                    #endregion

                    await transaction.CommitAsync();
                    logger.LogInformation("Added {object}: {Value} to database", nameof(User), user.NormalizedUserName);
                }
                catch
                {
                    logger.LogError("Failed to add {object} to database, due to an error during seeding", nameof(User));
                    await transaction.RollbackAsync();
                }
            }); // end strategy.ExecuteAsync
        }

    }

    private async Task SeedWebPages()
    {
        var webPageSeed = GetData<List<WebPageSeed>>(SeedFile.WebPages);

        if (webPageSeed is not null)
        {
            var webPages = mapper.Map<IEnumerable<WebPage>?>(webPageSeed);
            var webPageSecurities = mapper.Map<IEnumerable<WebPageSecurity>?>(webPageSeed);

            if (webPages is not null)
            {
                await dbContext.WebPage.AddRangeAsync(webPages);
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Added {Count} {object} to database", webPages.Count(), nameof(WebPage));
            }

            if (webPageSecurities is not null)
            {
                await dbContext.WebPageSecurity.AddRangeAsync(webPageSecurities);
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Added {Count} {object} to database", webPageSecurities.Count(), nameof(WebPageSecurity));
            }
        }
    }

    private async Task SeedWebControls()
    {
        var webControlSeed = GetData<List<WebControlSeed>>(SeedFile.WebControls);

        if (webControlSeed is not null)
        {
            var webControls = mapper.Map<IEnumerable<WebControl>?>(webControlSeed);

            if (webControls is not null)
            {
                await dbContext.WebControl.AddRangeAsync(webControls);
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Added {Count} {object} to database", webControls.Count(), nameof(WebControl));
            }
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
            case SeedFile.User:
                fileName = SeedPath.User;
                break;
            case SeedFile.UserRoles:
                fileName = SeedPath.UserRoles;
                break;
            case SeedFile.WebControls:
                fileName = SeedPath.WebControls;
                break;
            case SeedFile.WebPages:
                fileName = SeedPath.WebPages;
                break;
            default:
                fileName = null;
                break;
        }

        // Guard 1: if relative path not found, return default
        if (fileName is null) return default;

        var relativePath = Path.Combine("..", "Sloth.Infrastructure/Seed/Data", fileName);
        var fullPath = Path.GetFullPath(relativePath);

        // Guard 2: if file not found, return default
        if (!File.Exists(fullPath)) return default;

        var jsonString = File.ReadAllText(fullPath);

        // Return: Return deserialized object or if json string is empty or incorrect, return default 
        return jsonString.TryConvert<T>(default);
    }
}
