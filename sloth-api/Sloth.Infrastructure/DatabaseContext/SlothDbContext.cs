using Microsoft.EntityFrameworkCore;
using Sloth.Domain.Entities;

namespace Sloth.Infrastructure.DatabaseContext;
internal class SlothDbContext(DbContextOptions<SlothDbContext> options): DbContext(options)
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
    internal DbSet<FailedAttempt> FailedAttempt { get; set; }
    internal DbSet<CookieKey> CookieKey { get; set; }
    internal DbSet<LockedPassword> LockedPassword { get; set; }
    internal DbSet<LockedUser> LockedUser { get; set; }
    internal DbSet<RefreshToken> RefreshToken { get; set; }
    internal DbSet<SecurityTable> SecurityTable { get; set; }
    internal DbSet<SystemOption> SystemOption { get; set; }
    internal DbSet<User> User { get; set; }
    internal DbSet<UserGroup> UserGroup { get; set; }
    internal DbSet<UserRole> UserRole { get; set; }
    internal DbSet<WebPageSecurity> WebPageSecurity { get; set; }

    #endregion

    #region [UIElements]
    internal DbSet<Language> Language { get; set; }
    internal DbSet<Translation> Translation { get; set; }
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

        // SystemOption Configuration 
        builder.Entity<SystemOption>(entity=>
        {
            entity.HasKey(e => e.OptionID);
        });

        // User Configuration
        builder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserID);

            entity.HasOne<Theme>()
                  .WithMany()
                  .HasForeignKey(u => u.ThemeID)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne<Language>()
                  .WithMany()
                  .HasForeignKey(u => u.LanguageCode)
                  .OnDelete(DeleteBehavior.SetNull);  

            entity.HasOne(u => u.Role)
                  .WithMany()
                  .HasForeignKey(u => u.RoleID)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(u => u.Group)
                  .WithMany()
                  .HasForeignKey(u => u.GroupID)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasMany<LockedPassword>()
                  .WithOne()
                  .HasForeignKey(lp => lp.UserID);

            entity.HasMany<LockedUser>()
                  .WithOne()
                  .HasForeignKey(lp => lp.UserID);

            entity.HasMany<FailedAttempt>()
                  .WithOne()
                  .HasForeignKey(lp => lp.UserID);

            entity.HasMany<RefreshToken>()
                  .WithOne()
                  .HasForeignKey(lp => lp.UserID);

            entity.HasMany<CookieKey>()
                  .WithOne()
                  .HasForeignKey(lp => lp.UserID);
        });

        // Role Configuration 
        builder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleID);
        });

        // Group Configuration 
        builder.Entity<UserGroup>(entity =>
        {
            entity.HasKey(e => e.GroupID);
        });

        // WebPageSecurity Configuration
        builder.Entity<WebPageSecurity>(entity =>
        {
            entity.HasKey(wp => new { wp.PageID, wp.UserGroup });

            entity.Property(e => e.CanAccess).HasDefaultValue(true);
            entity.Property(e => e.CanAdd).HasDefaultValue(true);
            entity.Property(e => e.CanDelete).HasDefaultValue(true);
        });

        // SecurityTable Configuration
        builder.Entity<SecurityTable>(entity =>
        {
            entity.HasKey(st => new { st.SecurityTableID, st.ControlID, st.UserGroup });
            entity.HasIndex(e => new { e.SecurityTableID, e.ControlID });
            entity.Property(e => e.IsHidden).HasDefaultValue(false);
            entity.Property(e => e.IsReadOnly).HasDefaultValue(false);
            entity.Property(e => e.IsDisabled).HasDefaultValue(false);
        });

        // EndpointConfiguration Configuration
        builder.Entity<EndpointConfiguration>(entity =>
        {
            entity.HasKey(e => e.EndpointID);
        });

        // LockedPassword Configuration
        builder.Entity<LockedPassword>(entity =>
        {
            entity.HasKey(e => new { e.UserID, e.PasswordHash});

            entity.HasOne<User>()
                  .WithMany()
                  .HasForeignKey(e => e.UserID)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // LockedUser Configuration
        builder.Entity<LockedUser>(entity =>
        {
            entity.HasKey(e => e.UserID);

            entity.HasOne<User>()
                  .WithMany()
                  .HasForeignKey(e => e.UserID)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // FailedAttempt Configuration
        builder.Entity<FailedAttempt>(entity =>
        {
            entity.HasKey(e => e.UserID);

            entity.HasOne<User>()
                  .WithMany()
                  .HasForeignKey(e => e.UserID)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // RefreshToken Configuration
        builder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => new {e.UserID, e.Token, e.ExpirationDate});

            entity.HasOne<User>()
                  .WithMany()
                  .HasForeignKey(e => e.UserID)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // CookieKey Configuration
        builder.Entity<CookieKey>(entity =>
        {
            entity.HasKey(e => new { e.UserID, e.Key, e.ExpirationDate });

            entity.HasOne<User>()
                  .WithMany()
                  .HasForeignKey(e => e.UserID)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        #endregion
    }
}
