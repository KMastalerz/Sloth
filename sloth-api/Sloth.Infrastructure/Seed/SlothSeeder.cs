using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sloth.Domain.Constants;
using Sloth.Domain.Entities;
using Sloth.Domain.Exceptions;
using Sloth.Infrastructure.DatabaseContext;

namespace Sloth.Infrastructure.Seed;

// TO DO: All data has to be seed from json, not static like right now. Including System Options
internal class SlothSeeder(ILogger<SlothSeeder> logger, SlothDbContext dbContext) : ISlothSeeder
{
    public async Task Seed()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.SystemOption.Any())
            {
                var systemOptions = GetSystemOptions();

                await dbContext.SystemOption.AddRangeAsync(systemOptions);
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Added {Count} {object} to database", systemOptions.Count(), nameof(SystemOption));
            }

            if (!dbContext.Roles.Any())
            {
                var roles = GetRoles();

                await dbContext.Roles.AddRangeAsync(roles);
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Added {Count} {object} to database", roles.Count(), nameof(UserRole));
            }

            if (!dbContext.UserGroup.Any())
            {
                var group = GetUserGroup();
                group.UserRoleID = await dbContext.Roles.Where(r => r.NormalizedName == group.UserRoleID).Select(r => r.Id).FirstOrDefaultAsync() ?? 
                    throw new NotFoundException(nameof(UserRole), group.UserRoleID);

                await dbContext.UserGroup.AddAsync(group);
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Added {object} to database", nameof(UserGroup));
            }

            if (!dbContext.Language.Any())
            {
                var language = GetLanguage();

                await dbContext.Language.AddAsync(language);
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Added {object} to database", nameof(Language));
            }

            if (!dbContext.Users.Any())
            {
                var user = GetUser();
                var passwordHasher = new PasswordHasher<User>();

                // TO DO: Replace static password with JSON that will be extracted from 
                user.PasswordHash = passwordHasher.HashPassword(user, user.PasswordHash ?? "master");
                user.NormalizedEmail = user.Email!.ToUpper();
                user.NormalizedUserName = user.UserName!.ToUpper();


                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Added {object} to database", nameof(User));
            }
        }
    }

    private IEnumerable<SystemOption> GetSystemOptions()
    {
        return [
            new SystemOption(){
                OptionID = ConfigSystemOptions.PasswordComplexity,
                OptionName = "Password Complexity",
                OptionValue = "{\"RequiredLength\":10,\"RequiredUniqueChars\":2,\"RequireNonAlphanumeric\":false,\"RequireLowercase\":true,\"RequireUppercase\":true,\"RequireDigit\":false}",
                Description = "This options allow to set values, that will determine the password complexity."
            },
            new SystemOption() {
                OptionID = ConfigSystemOptions.RefreshTokenLifetime,
                OptionName = "Refresh Token Lifetime",
                OptionValue = "720",
                Description = "Describes minutes, that Refresh Token is valid for."
            },
            new SystemOption() {
                OptionID = ConfigSystemOptions.TokenLifetime,
                OptionName = "Token Lifetime",
                OptionValue = "120",
                Description = "Describes minutes, that Bearer Token is valid for."
            }
        ];
    }

    private IEnumerable<UserRole> GetRoles()
    {
        return [
            new(){
                Name = "SYSADMIN",
                NormalizedName = "SYSADMIN",
                DisplayName = "System Admin",
            },
            new(){
                Name = "CLIENT",
                NormalizedName = "CLIENT",
                DisplayName = "Client"
            },
            new(){
                Name = "DEVSE",
                NormalizedName = "DEVSE",
                DisplayName = "Software Engineer"
            },
            new(){
                Name = "QAE",
                NormalizedName = "QAE",
                DisplayName = "QA Engineer"
            },
            new(){
                Name = "SUPE",
                NormalizedName = "SUPE",
                DisplayName = "Support Engineer"
            },
        ];
    }

    private UserGroup GetUserGroup()
    {
        return new()
        {
            UserGroupID = "ADMIN",
            UserRoleID = "SYSADMIN",
            Description = "System administators can setup and access whole application"
        };
    }

    private Language GetLanguage()
    {
        return new()
        {
            LanguageCode = "ENU",
            LanguageName = "English (USA)"
        };
    }

    private User GetUser()
    {
        return new()
        {
            UserName = "KRZMAS",
            FirstName = "krzysztof",
            LastName = "mastalerz",
            Email = "kmastalerz@test.com",
            UserGroupID = "ADMIN",
            PasswordHash = "master"
        };
    }
}
