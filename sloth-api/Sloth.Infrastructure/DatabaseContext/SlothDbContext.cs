using Microsoft.EntityFrameworkCore;
using Sloth.Domain.Entities;

namespace Sloth.Infrastructure.DatabaseContext;
internal class SlothDbContext(DbContextOptions<SlothDbContext> options): DbContext(options)
{
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
    internal DbSet<WebControl> WebControl { get; set; }
    internal DbSet<WebPage> WebPage { get; set; }
    internal DbSet<WebPanel> WebPanel { get; set; }
    internal DbSet<WebOption> WebOption { get; set; }
    #endregion

    #region [Project] 
    internal DbSet<Project> Project { get; set; }
    internal DbSet<ProjectFunctionality> ProjectFunctionality { get; set; }
    internal DbSet<ProjectReleases> ProjectReleases { get; set; }
    internal DbSet<ProjectType> ProjectType { get; set; }
    internal DbSet<ProjectTechnologyStack> ProjectTechnologyStack { get; set; }
    internal DbSet<Technology> Technology { get; set; }
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

            entity.HasMany<WebPageSecurity>()
                  .WithOne()
                  .HasForeignKey(wps => wps.PageID);
        });

        // WebPanel Configuration
        builder.Entity<WebPanel>(entity =>
        {
            entity.HasKey(e => new { e.PageID, e.PanelID });

            entity.HasOne<WebPage>()
                  .WithMany()
                  .HasForeignKey(wp => wp.PageID)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // WebControl Configuration
        builder.Entity<WebControl>(entity =>
        {
            entity.HasKey(e => new { e.PageID, e.PanelID, e.ControlID });

            entity.HasOne<WebPanel>()
                .WithMany()
                .HasForeignKey(wc => new { wc.PageID, wc.PanelID })
                .OnDelete(DeleteBehavior.Cascade);

        });

        // WebOption Configuration
        builder.Entity<WebOption>(entity =>
        {
            entity.HasKey(e => new {e.Functionality, e.OptionKey});
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
            entity.Property(e => e.IsRequired).HasDefaultValue(false);
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

        #region [Project]

        // Project Configuration
        builder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectID);
        });

        // ProjectFunctionality Configuration
        builder.Entity<ProjectFunctionality>(entity =>
        {
            entity.HasKey(e => e.FunctionalityID);
        });

        // ProjectReleases Configuration
        builder.Entity<ProjectReleases>(entity =>
        {
            entity.HasKey(e => e.ReleaseID);
        });

        // ProjectType Configuration
        builder.Entity<ProjectType>(entity =>
        {
            entity.HasKey(e => e.Type);
        });

        // ProjectTechnologyStack Configuration
        builder.Entity<ProjectTechnologyStack>(entity =>
        {
            entity.HasKey(e => new { e.ProjectID, e.TechnologyID });

            entity.HasOne<Project>()
                  .WithMany()
                  .HasForeignKey(e => e.ProjectID)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne<Technology>()
                  .WithMany()
                  .HasForeignKey(e => e.TechnologyID)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Technology Configuration
        builder.Entity<Technology>(entity =>
        {
            entity.HasKey(e => e.TechnologyID);
        });

        #endregion
    }
}
