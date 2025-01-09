using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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

    #region[Tracker]
    internal DbSet<Job> Job { get; set; }
    internal DbSet<JobAssignment> JobAssignment { get; set; }
    internal DbSet<JobAssignmentHistory> JobAssignmentHistory { get; set; }
    internal DbSet<JobComment> JobComment { get; set; }
    internal DbSet<JobPriority> JobPriority { get; set; }
    internal DbSet<JobPriorityHistory> JobPriorityHistory { get; set; }
    internal DbSet<JobProductLink> JobProductLink { get; set; }
    internal DbSet<JobStatus> JobStatus { get; set; }
    internal DbSet<JobStatusHistory> JobStatusHistory { get; set; }
    internal DbSet<OwnerStatusMap> OwnerStatusMap { get; set; }
    internal DbSet<Product> Product { get; set; }
    internal DbSet<Team> Team { get; set; }
    internal DbSet<TeamProductLink> TeamProductLink { get; set; }
    internal DbSet<TeamStatusMap> TeamStatusMap { get; set; }
    internal DbSet<UserTeamLink> UserTeamLink { get; set; }

    #endregion
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        #region [Login]
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
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            link.HasOne<UserRole>()
                .WithMany()
                .HasForeignKey(l => l.RoleID)
                .IsRequired()
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
        #endregion

        #region [Tracker]
        builder.Entity<Job>(job =>
        {
            job.HasKey(j => j.JobID);

            job.HasOne(j => j.CurrentOwner)
                .WithMany()
                .HasForeignKey(j => j.CurrentOwnerID)
                .OnDelete(DeleteBehavior.Restrict);

            job.HasOne(j => j.CurrentTeam)
                .WithMany()
                .HasForeignKey(j => j.CurrentTeamID)
                .OnDelete(DeleteBehavior.Restrict);

            job.HasOne(j => j.JobStatus)
                .WithMany()
                .HasForeignKey(j => j.CurrentJobStatusID)
                .OnDelete(DeleteBehavior.Cascade);

            job.HasOne(j => j.JobPriority)
                .WithMany()
                .HasForeignKey(j => j.PriorityLevel)
                .OnDelete(DeleteBehavior.Restrict);

            job.HasMany(j => j.JobComments)
                .WithOne()
                .HasForeignKey(c => c.JobID)
                .OnDelete(DeleteBehavior.Cascade);

            job.HasMany(j => j.JobStatusHistory)
                .WithOne()
                .HasForeignKey(h => h.JobID)
                .OnDelete(DeleteBehavior.Cascade);

            job.HasMany(j => j.JobAssignmentHistory)
                .WithOne()
                .HasForeignKey(h => h.JobID)
                .OnDelete(DeleteBehavior.Cascade);

            job.HasMany(j => j.JobPriorityHistory)
                .WithOne()
                .HasForeignKey(h => h.JobID)
                .OnDelete(DeleteBehavior.Cascade);

            job.HasMany(j => j.Products)
                .WithMany()
                .UsingEntity<JobProductLink>(
                    link => link.HasOne<Product>().WithMany().HasForeignKey(l => l.ProductID),
                    link => link.HasOne<Job>().WithMany().HasForeignKey(l => l.JobID),
                    link =>
                    {
                        link.HasKey(l => new { l.JobID, l.ProductID }); // Composite Key
                    });
        });

        builder.Entity<Team>(team =>
        {
            team.HasKey(t => t.TeamID);

            team.HasMany(t => t.Products)
                .WithMany()
                .UsingEntity<TeamProductLink>(
                    link => link.HasOne<Product>().WithMany().HasForeignKey(l => l.ProductID),
                    link => link.HasOne<Team>().WithMany().HasForeignKey(l => l.TeamID),
                    link =>
                    {
                        link.HasKey(l => new { l.TeamID, l.ProductID }); // Composite Key
                    });
        });

        builder.Entity<UserTeamLink>(link =>
        {
            // Composite Key
            link.HasKey(l => new { l.TeamID, l.UserID });

            // Configuring relationships
            link.HasOne<Team>()
                .WithMany()
                .HasForeignKey(l => l.TeamID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            link.HasOne<User>()
                .WithMany()
                .HasForeignKey(l => l.UserID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<JobStatus>(status =>
        {
            status.HasKey(s => s.JobStatusID);
        });

        builder.Entity<JobPriority>(priority =>
        {
            priority.HasKey(p => p.PriorityLevel);

            priority.Property(p => p.PriorityLevel)
                .ValueGeneratedNever(); // Identity is turned off; values are manually inserted.
        });

        builder.Entity<JobComment>(comment =>
        {
            comment.HasKey(c => c.CommentID);

            comment.HasOne(c => c.CommentedBy)
                .WithMany()
                .HasForeignKey(c => c.CommentedByID)
                .OnDelete(DeleteBehavior.Restrict);

            comment.HasMany(c => c.PreviousEdits)
                .WithOne()
                .HasForeignKey(c => c.OriginalCommentID)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<JobStatusHistory>(history =>
        {
            history.HasKey(h => new { h.JobID, h.ChangeDate });

            history.HasOne(h => h.ChangedBy)
                .WithMany()
                .HasForeignKey(h => h.ChangedByID)
                .OnDelete(DeleteBehavior.Restrict);

            history.HasOne(h => h.PreviousStatus)
                .WithMany()
                .HasForeignKey(h => h.PreviousStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            history.HasOne(h => h.NewStatus)
                .WithMany()
                .HasForeignKey(h => h.NewStatusID)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<JobAssignmentHistory>(history =>
        {
            history.HasKey(h => new { h.JobID, h.ChangeDate });

            history.HasOne(h => h.PreviousOwner)
                .WithMany()
                .HasForeignKey(h => h.PreviousOwnerID)
                .OnDelete(DeleteBehavior.NoAction);

            history.HasOne(h => h.CurrentOwner)
                .WithMany()
                .HasForeignKey(h => h.CurrentOwnerID)
                .OnDelete(DeleteBehavior.NoAction);

            history.HasOne(h => h.ChengedBy)
                .WithMany()
                .HasForeignKey(h => h.ChangedByID)
                .OnDelete(DeleteBehavior.NoAction);

            history.HasOne(h => h.Team)
                .WithMany()
                .HasForeignKey(h => h.TeamID)
                .OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<JobPriorityHistory>(history =>
        {
            history.HasKey(h => new { h.JobID, h.ChangeDate });

            history.HasOne(h => h.PreviousPriority)
                .WithMany()
                .HasForeignKey(h => h.PreviousPriorityLevel)
                .OnDelete(DeleteBehavior.Restrict);

            history.HasOne(h => h.NewPriority)
                .WithMany()
                .HasForeignKey(h => h.NewPriorityLevel)
                .OnDelete(DeleteBehavior.Restrict);

            history.HasOne(h => h.ChangedBy)
                .WithMany()
                .HasForeignKey(h => h.ChangedByID)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Product>(product =>
        {
            product.HasKey(p => p.ProductID);
        });

        builder.Entity<JobProductLink>(link => {
            // Composite Key
            link.HasKey(l => new { l.JobID, l.ProductID });

            // Configuring relationships
            link.HasOne<Job>()
                .WithMany()
                .HasForeignKey(l => l.JobID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            link.HasOne<Product>()
                .WithMany()
                .HasForeignKey(l => l.ProductID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<TeamProductLink>(link => {
            // Composite Key
            link.HasKey(l => new { l.TeamID, l.ProductID });

            // Configuring relationships
            link.HasOne<Team>()
                .WithMany()
                .HasForeignKey(l => l.TeamID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            link.HasOne<Product>()
                .WithMany()
                .HasForeignKey(l => l.ProductID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<JobAssignment>(assignment =>
        {
            assignment.HasKey(a => new { a.JobID, a.TeamID });

            assignment.HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserID)
                .OnDelete(DeleteBehavior.NoAction);

            assignment.HasOne(a => a.Team)
                .WithMany()
                .HasForeignKey(a => a.TeamID)
                .OnDelete(DeleteBehavior.NoAction);

            assignment.HasOne(a => a.AssignedBy)
                .WithMany()
                .HasForeignKey(a => a.AssignedByID)
                .OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<OwnerStatusMap>(statusMap =>
        {
            // Composite Key
            statusMap.HasKey(m => new { m.TeamID, m.JobStatusID });

            // Configuring relationships
            statusMap.HasOne<Team>()
                .WithMany()
                .HasForeignKey(m => m.TeamID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            statusMap.HasOne<JobStatus>()
                .WithMany()
                .HasForeignKey(m => m.JobStatusID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<TeamStatusMap>(statusMap =>
        {
            // Composite Key
            statusMap.HasKey(m => new { m.TeamID, m.JobStatusID });

            // Configuring relationships
            statusMap.HasOne<Team>()
                .WithMany()
                .HasForeignKey(m => m.TeamID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            statusMap.HasOne<JobStatus>()
                .WithMany()
                .HasForeignKey(m => m.JobStatusID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });
        #endregion

    }
}
