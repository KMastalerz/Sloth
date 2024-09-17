using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sloth.Domain.Entities;
using System.Reflection.Emit;

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
    internal DbSet<WebPageSecurity> WebPageSecurity { get; set; }

    #endregion

    #region [UIElements]
    internal DbSet<Language> Language { get; set; }
    internal DbSet<Validation> Validation { get; set; }
    internal DbSet<Translation> Translation { get; set; }
    internal DbSet<WebControl> WebControl { get; set; }
    internal DbSet<WebControlValidation> WebControlValidation { get; set; }
    internal DbSet<WebPage> WebPage { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region[UIElements]
        // WebPage Configuration

        builder.Entity<WebPage>(entity =>
        {
            entity.HasKey(e => e.PageID);

            entity.Property(e => e.Title).IsRequired();
            entity.Property(e => e.SecurityTableID).IsRequired(false);
            entity.Property(e => e.Description).IsRequired(false);

            // Relationships
            entity.HasMany(e => e.SecurityTables)
                  .WithOne()
                  .HasForeignKey(st => st.SecurityTableID);

            entity.HasMany(e => e.WebControls)
                  .WithOne()
                  .HasForeignKey(wc => wc.PageID);

            entity.HasMany(e => e.WebPageSecurities)
                  .WithOne()
                  .HasForeignKey(wps => wps.PageID);
        });

        // WebControl Configuration
        builder.Entity<WebControl>(entity =>
        {
            entity.HasKey(e => new { e.PageID, e.ControlID });

            entity.Property(e => e.ControlType).IsRequired();
            entity.Property(e => e.ControlLabel).IsRequired();
            entity.Property(e => e.ControlPlaceholder).IsRequired();
            entity.Property(e => e.Route).IsRequired(false);
            entity.Property(e => e.RoutePageID).IsRequired(false);
            entity.Property(e => e.SecurityTableID).IsRequired(false);

            // Relationships
            entity.HasMany(e => e.WebControlSecurities)
                  .WithOne()
                  .HasForeignKey(wcs => new { wcs.PageID, wcs.ControlID });

            // Linking Validations with WebControl through WebControlValidation
            entity.HasMany(e => e.Validations)
                  .WithMany(v => v.WebControls)
                  .UsingEntity<WebControlValidation>(
                      j => j.HasOne<Validation>().WithMany().HasForeignKey(wcv => wcv.ValidationName),
                      j => j.HasOne<WebControl>().WithMany().HasForeignKey(wcv => new { wcv.PageID, wcv.ControlID }),
                      j => j.HasKey(wcv => new { wcv.PageID, wcv.ControlID, wcv.ValidationName })
                  );
        });

        // WebControlValidation Configuration
        builder.Entity<WebControlValidation>(entity =>
        {
            entity.HasKey(e => new { e.PageID, e.ControlID, e.ValidationName });
        });

        // Validation Configuration
        builder.Entity<Validation>(entity =>
        {
            entity.HasKey(e => e.ValidationName);
        });

        // Translation Configuration
        builder.Entity<Translation>(entity =>
        {
            entity.HasKey(e => new { e.ENUText, e.LanguageCode });
        });

        // Language Configuration
        builder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.LanguageCode);
        });
        #endregion

        #region [Security]

        // SystemOption entity 
        builder.Entity<SystemOption>(entity=>
        {
            entity.HasKey(e => e.OptionID);
        });

        // User entity
        builder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasOne(u => u.Theme)
                  .WithMany()
                  .HasForeignKey(u => u.ThemeID)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(u => u.Language)
                  .WithMany()
                  .HasForeignKey(u => u.LanguageCode)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(u => u.UserGroup)
                  .WithMany()
                  .HasForeignKey(u => u.UserGroupID)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // UserGroup entity
        builder.Entity<UserGroup>(entity =>
        {
            entity.HasKey(ug => ug.UserGroupID);

            entity.HasOne<UserRole>()
                  .WithMany()
                  .HasForeignKey(ug => ug.UserRoleID)
                  .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of UserRole if linked UserGroups exist
        });

        // WebPageSecurity entity
        builder.Entity<WebPageSecurity>(entity =>
        {
            entity.HasKey(wp => new { wp.PageID, wp.UserGroupID });

            entity.HasOne<UserGroup>()
                  .WithMany()
                  .HasForeignKey(wp => wp.UserGroupID)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.Property(e => e.CanAccess).HasDefaultValue(true);
            entity.Property(e => e.CanAdd).HasDefaultValue(true);
            entity.Property(e => e.CanDelete).HasDefaultValue(true);
        });

        // WebControlSecurity entity
        builder.Entity<WebControlSecurity>(entity =>
        {
            entity.HasKey(wc => new { wc.PageID, wc.ControlID, wc.UserGroupID });

            entity.HasOne<UserGroup>()
                  .WithMany()
                  .HasForeignKey(wc => wc.UserGroupID)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // SecurityTable entity
        builder.Entity<SecurityTable>(entity =>
        {
            entity.HasKey(st => new { st.SecurityTableID, st.UserGroupID });

            entity.HasOne<UserGroup>()
                  .WithMany()
                  .HasForeignKey(st => st.UserGroupID)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(e => new { e.SecurityTableID, e.ControlName });
            entity.Property(e => e.IsHidden).HasDefaultValue(false);
            entity.Property(e => e.IsReadOnly).HasDefaultValue(false);
            entity.Property(e => e.IsDisabled).HasDefaultValue(false);
        });

        // EndpointConfiguration entity
        builder.Entity<EndpointConfiguration>(entity =>
        {
            entity.HasKey(e => e.EndpointID);
        });

        // Name overrites only
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


        #endregion
    }
}
