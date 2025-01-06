using Microsoft.EntityFrameworkCore;
using sloth.Domain.Entities;

namespace sloth.Infrastructure.DatabaseContext;
internal class SlothDbContext(DbContextOptions<SlothDbContext> options) : DbContext(options)
{
    #region [Login]
    internal DbSet<LockedPassword> LockedPassword { get; set; }
    internal DbSet<LockedUser> LockedUser { get; set; }
    internal DbSet<RefreshToken> RefreshToken { get; set; }
    internal DbSet<ResetSecurityCode> ResetSecurityCode { get; set; }
    internal DbSet<User> User { get; set; }
    internal DbSet<UserRole> UserRole { get; set; }
    internal DbSet<UserRoleLink> UserRoleLink { get; set; }
    #endregion
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>(user =>
        {
            user.HasKey(u => u.UserID);

            user.HasIndex(r => r.Email).IsUnique();

            user.HasIndex(r => r.UserName).IsUnique();

            user.HasMany(u => u.UserRoles)
                .WithMany()
                .UsingEntity<UserRoleLink>(
                    link => link.HasOne<UserRole>().WithMany().HasForeignKey(l => l.RoleID),
                    link => link.HasOne<User>().WithMany().HasForeignKey(l => l.UserID),
                    link =>
                    {
                        link.HasKey(l => new { l.UserID, l.RoleID }); // Composite Key
                    });
        });

        builder.Entity<UserRole>(role =>
        {
            role.HasKey(r => r.RoleID);

            role.HasIndex(r => r.RoleCode).IsUnique();

            role.HasMany<UserRoleLink>()
                .WithOne()
                .HasForeignKey(ul => ul.RoleID)
                .IsRequired();
        });

        builder.Entity<UserRoleLink>(link =>
        {
            // Composite Key
            link.HasKey(l => new { l.UserID, l.RoleID });

            // Configuring relationships
            link.HasOne<User>()
                .WithMany()
                .HasForeignKey(l => l.UserID)
                .IsRequired();

            link.HasOne<UserRole>()
                .WithMany()
                .HasForeignKey(l => l.RoleID)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<LockedUser>(lockedUser =>
        {
            lockedUser.HasKey(lu => lu.UserID);

            lockedUser.HasOne<User>()
                .WithOne()
                .HasForeignKey<LockedUser>(lu => lu.UserID)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<LockedPassword>(lockedPassword =>
        {
            lockedPassword.HasKey(lp => lp.UserID);

            lockedPassword.HasOne<User>()
                .WithOne()
                .HasForeignKey<LockedPassword>(lp => lp.UserID)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<RefreshToken>(refreshToken =>
        {
            refreshToken.HasKey(rt => rt.UserID);

            refreshToken.HasOne<User>()
                .WithOne()
                .HasForeignKey<RefreshToken>(rt => rt.UserID)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<ResetSecurityCode>(resetSecurityCode =>
        {
            resetSecurityCode.HasKey(rsc => rsc.UserID);
            resetSecurityCode.HasOne<User>()
                .WithOne()
                .HasForeignKey<ResetSecurityCode>(rsc => rsc.UserID)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
