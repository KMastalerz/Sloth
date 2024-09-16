using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sloth.Domain.Entities;

namespace Sloth.Infrastructure.DatabaseContext;
internal class SlothDbContext(DbContextOptions<SlothDbContext> options): IdentityDbContext<User>(options)
{
    /* !!! Important Note !!!
     * Team, User, Statuses, Priorities & Types cannot be removed when mapped to Job. They can be modified or deactivated. 
     * This is important, since Job entries rely havily on those objects when are beeing used.
     * 
     * When Product, Client or Service is removed all job related tables that it referenced will be removed, 
     * but they still can be deactivated.
     */
    #region [Security]
    internal DbSet<EndpointConfiguration> EndpointConfiguration { get; set; }
    internal DbSet<SecurityTable> SecurityTable { get; set; }
    internal DbSet<SystemOption> SystemOption { get; set; }
    internal DbSet<UserGroup> UserGroup { get; set; }
    internal DbSet<WebPageSecurity> WebControlSecurity { get; set; }
    internal DbSet<WebPageSecurity> WebControlValidation { get; set; }
    internal DbSet<WebPageSecurity> WebPageSecurity { get; set; }

    #endregion

    #region [Interface]

    internal DbSet<Language> Language { get; set; }
    internal DbSet<Theme> Theme { get; set; }
    internal DbSet<Translation> Translation { get; set; }
    internal DbSet<WebControl> WebControl { get; set; }
    internal DbSet<WebPage> WebPage { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region [Security]

        // Map other Identity tables to custom ones
        builder.Entity<User>().ToTable("User");
        builder.Entity<IdentityRole>().ToTable("UserRole"); // Map to your custom table
        builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
        builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
        builder.Entity<IdentityUserRole<string>>().ToTable("UserRoleLink");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("UserRoleClaimLink");
        builder.Entity<IdentityUserToken<string>>().ToTable("UserToken");


        builder.Entity<UserClaim>().ToTable("UserClaim");
        builder.Entity<UserLogin>().ToTable("UserLogin");
        builder.Entity<UserRoleLink>().ToTable("UserRoleLink");
        builder.Entity<UserRoleClaimLink>().ToTable("UserRoleClaimLink");
        builder.Entity<UserToken>().ToTable("UserToken");

        // User and Theme (one-to-one or many-to-one, optional relationship)
        builder.Entity<User>()
            .HasOne(u => u.Theme)
            .WithMany()  // Assuming Theme is shared by multiple users
            .HasForeignKey(u => u.ThemeID)
            .OnDelete(DeleteBehavior.SetNull);  // Set ThemeID to null if Theme is deleted

        // User and Language (many-to-one relationship)
        builder.Entity<User>()
            .HasOne(u => u.Language)
            .WithMany()  // One language can be linked to many users
            .HasForeignKey(u => u.LanguageCode)
            .OnDelete(DeleteBehavior.Restrict);  // Prevent deletion of Language if Users exist

        // User and UserGroup (many-to-one relationship)
        builder.Entity<User>()
            .HasOne(u => u.UserGroup)
            .WithMany()  // One UserGroup can have many users
            .HasForeignKey(u => u.UserGroupID)
            .OnDelete(DeleteBehavior.Restrict);  // Prevent deletion of UserGroup if Users exist

        builder.Entity<UserGroup>()
            .HasKey(ug => ug.UserGroupID);

        // UserGroup and UserRole (many-to-one relationship)
        builder.Entity<UserGroup>()
            .HasOne<UserRole>()  // A UserGroup is linked to one UserRole
            .WithMany()  // Assuming UserRole is linked to multiple UserGroups
            .HasForeignKey(ug => ug.UserRoleID)
            .OnDelete(DeleteBehavior.Restrict);  // Prevent deletion of UserRole if linked UserGroups exist

        builder.Entity<WebPageSecurity>()
            .HasKey(wp => new { wp.PageID, wp.UserGroupID });

        // WebPageSecurity and UserGroup (many-to-one relationship)
        builder.Entity<WebPageSecurity>()
            .HasOne<UserGroup>()  // WebPageSecurity references a single UserGroup
            .WithMany()  // One UserGroup can be linked to multiple WebPageSecurity
            .HasForeignKey(wps => wps.UserGroupID)
            .OnDelete(DeleteBehavior.Restrict);  // Prevent deletion of UserGroup if linked WebPageSecurity exists

        builder.Entity<WebControlSecurity>()
            .HasKey(wc => new { wc.PageID, wc.ControlID, wc.UserGroupID });

        // WebControlSecurity and UserGroup (many-to-one relationship)
        builder.Entity<WebControlSecurity>()
            .HasOne<UserGroup>()  // WebControlSecurity references a single UserGroup
            .WithMany()  // One UserGroup can be linked to multiple WebControlSecurity entries
            .HasForeignKey(wcs => wcs.UserGroupID)
            .OnDelete(DeleteBehavior.Restrict);  // Prevent deletion of UserGroup if linked WebControlSecurity exists

        builder.Entity<SecurityTable>()
            .HasKey(st => new {st.SecurityTableID, st.UserGroupID});

        // SecurityTable and UserGroup (many-to-one relationship)
        builder.Entity<SecurityTable>()
            .HasOne<UserGroup>()  // SecurityTable references a single UserGroup
            .WithMany()  // One UserGroup can be linked to multiple SecurityTable entries
            .HasForeignKey(st => st.UserGroupID)
            .OnDelete(DeleteBehavior.Restrict);  // Prevent deletion of UserGroup if linked SecurityTable exists

        builder.Entity<EndpointConfiguration>()
            .HasKey(e => e.EndpointID);

        #endregion

        #region [Interface] 

        builder.Entity<WebPage>()
            .HasKey(wc => wc.PageID)
            .IsClustered();

        // WebPage and WebControl (one-to-many relationship)
        builder.Entity<WebControl>()
            .HasOne<WebPage>()
            .WithMany() // One WebPage can be linked to multiple WebControl entries
            .HasForeignKey(wc => wc.PageID)
            .OnDelete(DeleteBehavior.Cascade);  // Cascade delete WebControls when WebPage is deleted

        builder.Entity<WebControl>()
            .HasKey(wc => new { wc.PageID, wc.ControlID })
            .IsClustered(); 

        builder.Entity<Language>()
            .HasKey(l => l.LanguageCode);

        builder.Entity<Language>()
            .HasMany<Translation>()  // One Language has many Translations
            .WithOne()  // Each Translation references one Language
            .HasForeignKey(t => t.LanguageCode)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Translation>()
            .HasKey(l => l.ENUText)
            .IsClustered(); 

        builder.Entity<SystemOption>()
            .HasKey(so => so.OptionID)
            .IsClustered();

        builder.Entity<Theme>()
            .HasKey(so => so.ThemeID)
            .IsClustered();

        builder.Entity<Theme>()
            .HasIndex(t => t.ThemeName)
            .IsUnique();

        #endregion
    }
}
