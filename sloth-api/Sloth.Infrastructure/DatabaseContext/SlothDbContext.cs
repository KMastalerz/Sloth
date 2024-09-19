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
    internal DbSet<WebPageSecurity> WebPageSecurity { get; set; }

    #endregion

    #region [UIElements]
    internal DbSet<Language> Language { get; set; }
    internal DbSet<Translation> Translation { get; set; }
    internal DbSet<UserGroup> UserGroup { get; set; }
    internal DbSet<Validation> Validation { get; set; }
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
            entity.HasMany<SecurityTable>()
                  .WithOne()
                  .HasForeignKey(st => st.SecurityTableID);

            entity.HasMany<WebControl>()
                  .WithOne()
                  .HasForeignKey(wc => wc.PageID);

            entity.HasMany<WebPageSecurity>()
                  .WithOne()
                  .HasForeignKey(wps => wps.PageID);
        });

        // WebControl Configuration
        builder.Entity<WebControl>(entity =>
        {
            entity.HasKey(e => new { e.PageID, e.ControlID });

            entity.Property(e => e.ControlType).IsRequired();
            entity.Property(e => e.ControlLabel).IsRequired(false);
            entity.Property(e => e.ControlPlaceholder).IsRequired(false);
            entity.Property(e => e.ControlTooltip).IsRequired(false);
            entity.Property(e => e.Route).IsRequired(false);
            entity.Property(e => e.RoutePageID).IsRequired(false);
            entity.Property(e => e.SecurityTableID).IsRequired(false);

            entity.HasMany<WebControlValidation>()
                  .WithOne()
                  .HasForeignKey(wcv => new { wcv.PageID, wcv.ControlID })
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // WebControlValidation Configuration
        builder.Entity<WebControlValidation>(entity =>
        {
            entity.HasKey(e => new { e.PageID, e.ControlID });
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

            entity.Property(e => e.Id).HasColumnName("UserID");

            entity.HasOne<Theme>()
                  .WithMany()
                  .HasForeignKey(u => u.ThemeID)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne<Language>()
                  .WithMany()
                  .HasForeignKey(u => u.LanguageCode)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne<UserRoleLink>()               
                  .WithOne()                            
                  .HasForeignKey<UserRoleLink>(url => url.UserId) 
                  .OnDelete(DeleteBehavior.Cascade);    
        });

        // UserRoleLink entity 
        builder.Entity<UserRoleLink>(entity =>
        {
            entity.HasOne<UserRole>()
                  .WithMany()
                  .HasForeignKey(url => url.RoleId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Adding unique constraint to ensure a user can only have one role
            entity.HasIndex(url => new { url.UserId, url.RoleId }).IsUnique();
        });

        // WebPageSecurity entity
        builder.Entity<WebPageSecurity>(entity =>
        {
            entity.HasKey(wp => new { wp.PageID, wp.UserGroup });

            entity.Property(e => e.CanAccess).HasDefaultValue(true);
            entity.Property(e => e.CanAdd).HasDefaultValue(true);
            entity.Property(e => e.CanDelete).HasDefaultValue(true);
        });

        // SecurityTable entity
        builder.Entity<SecurityTable>(entity =>
        {
            entity.HasKey(st => new { st.SecurityTableID, st.ControlID, st.UserGroup });
            entity.HasIndex(e => new { e.SecurityTableID, e.ControlID });
            entity.Property(e => e.IsHidden).HasDefaultValue(false);
            entity.Property(e => e.IsReadOnly).HasDefaultValue(false);
            entity.Property(e => e.IsDisabled).HasDefaultValue(false);
        });

        // EndpointConfiguration entity
        builder.Entity<EndpointConfiguration>(entity =>
        {
            entity.HasKey(e => e.EndpointID);
        });

        // UserRole entity
        builder.Entity<IdentityRole>(entity =>
        {
            entity.ToTable("UserRole"); 

            entity.Property(e => e.Id).HasColumnName("UserRoleID");
        });

        // UserClaim entity
        builder.Entity<IdentityUserClaim<string>>(entity =>
        {
            entity.ToTable("UserClaim");

            entity.Property(e => e.Id).HasColumnName("UserClaimID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        // UserLogin entity
        builder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.ToTable("UserLogin");

            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        // UserRoleLink entity
        builder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.ToTable("UserRoleLink");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.RoleId).HasColumnName("UserRoleID");
        });

        // RoleClaimLink entity
        builder.Entity<IdentityRoleClaim<string>>(entity =>

        {
            entity.ToTable("RoleClaimLink");

            entity.Property(e => e.RoleId).HasColumnName("UserRoleID");
            entity.Property(e => e.Id).HasColumnName("RoleClaimID");
        });

        // UserToken entity
        builder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.ToTable("UserToken");

            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        builder.Entity<UserClaim>().ToTable("UserClaim");
        builder.Entity<UserLogin>().ToTable("UserLogin");
        builder.Entity<UserRoleLink>().ToTable("UserRoleLink");
        builder.Entity<RoleClaimLink>().ToTable("RoleClaimLink");
        builder.Entity<UserToken>().ToTable("UserToken");

        #endregion
    }
}
