﻿using Microsoft.EntityFrameworkCore;
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
    internal DbSet<Bug> Bug { get; set; }
    internal DbSet<Client> Client { get; set; }
    internal DbSet<ClientProductLink> ClientProductLink { get; set; }
    internal DbSet<Job> Job { get; set; }
    internal DbSet<JobAssignment> JobAssignment { get; set; }
    internal DbSet<JobAssignmentHistory> JobAssignmentHistory { get; set; }
    internal DbSet<JobComment> JobComment { get; set; }
    internal DbSet<JobFile> JobFile { get; set; }
    internal DbSet<JobPriorityHistory> JobPriorityHistory { get; set; }
    internal DbSet<JobProductLink> JobProductLink { get; set; }
    internal DbSet<JobStatusHistory> JobStatusHistory { get; set; }
    internal DbSet<OwnerStatusMap> OwnerStatusMap { get; set; }
    internal DbSet<Priority> Priority { get; set; }
    internal DbSet<Product> Product { get; set; }
    internal DbSet<Query> Query { get; set; }
    internal DbSet<Status> Status { get; set; }
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

        builder.Entity<Bug>(bug =>
        {
            bug.ToTable("Bug");

            bug.Property(b => b.BugID)
                .ValueGeneratedOnAdd();

            bug.HasIndex(b => b.BugID)
                .IsUnique();

            bug.HasOne<Job>()
               .WithOne()
               .HasForeignKey<Bug>(b => b.JobID)
               .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Client>(client =>
        {
            client.HasKey(c => c.ClientID);

            client.HasIndex(c => c.Alias)
                .IsUnique();

            client.HasMany(t => t.Products)
                .WithMany()
                .UsingEntity<ClientProductLink>(
                    link => link.HasOne<Product>().WithMany().HasForeignKey(l => l.ProductID),
                    link => link.HasOne<Client>().WithMany().HasForeignKey(l => l.ClientID),
                    link =>
                    {
                        link.HasKey(l => new { l.ClientID, l.ProductID }); // Composite Key
                    });
        });

        builder.Entity<ClientProductLink>(link => {
            // Composite Key
            link.HasKey(l => new { l.ClientID, l.ProductID });

            // Configuring relationships
            link.HasOne<Client>()
                .WithMany()
                .HasForeignKey(l => l.ClientID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            link.HasOne<Product>()
                .WithMany()
                .HasForeignKey(l => l.ProductID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Job>(job =>
        {
            job.ToTable("Job");

            job.HasKey(j => j.JobID);

            job.HasOne(j => j.CurrentOwner)
                .WithMany()
                .HasForeignKey(j => j.CurrentOwnerID)
                .OnDelete(DeleteBehavior.NoAction);

            job.HasOne(j => j.CurrentTeam)
                .WithMany()
                .HasForeignKey(j => j.CurrentTeamID)
                .OnDelete(DeleteBehavior.NoAction);

            job.HasMany(j => j.Comments)
                .WithOne()
                .HasForeignKey(c => c.JobID)
                .OnDelete(DeleteBehavior.Cascade);

            job.HasMany(j => j.AssignmentHistory)
                .WithOne()
                .HasForeignKey(h => h.JobID)
                .OnDelete(DeleteBehavior.Cascade);

            job.HasMany(j => j.Files)
                .WithOne()
                .HasForeignKey(f => f.JobID)
                .OnDelete(DeleteBehavior.Cascade);

            job.HasMany(j => j.StatusHistory)
                .WithOne()
                .HasForeignKey(h => h.JobID)
                .OnDelete(DeleteBehavior.Cascade);

            job.HasOne(b => b.Priority)
                .WithMany()
                .HasForeignKey(b => b.PriorityID);

            job.HasOne(b => b.Status)
                .WithMany()
                .HasForeignKey(b => b.StatusID);

            job.HasOne(b => b.Client)
                .WithMany()
                .HasForeignKey(b => b.ClientID);

            job.HasMany(b => b.PriorityHistory)
                .WithOne()
                .HasForeignKey(h => h.JobID)
                .OnDelete(DeleteBehavior.Cascade);

            job.HasMany(b => b.Products)
                .WithMany()
                .UsingEntity<JobProductLink>(
                    link => link.HasOne<Product>().WithMany().HasForeignKey(l => l.ProductID),
                    link => link.HasOne<Job>().WithMany().HasForeignKey(l => l.JobID),
                    link =>
                    {
                        link.HasKey(l => new { l.JobID, l.ProductID }); // Composite Key
                    });
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

        builder.Entity<JobComment>(comment =>
        {
            comment.HasKey(c => c.CommentID);

            comment.HasOne(c => c.CommentedBy)
                .WithMany()
                .HasForeignKey(c => c.CommentedByID)
                .OnDelete(DeleteBehavior.NoAction);

            comment.HasMany(c => c.PreviousEdits)
                .WithOne()
                .HasForeignKey(c => c.OriginalCommentID)
                .OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<JobFile>(file =>
        {
            file.HasKey(f => f.FileID);

            file.HasOne<Job>()
                .WithMany(j => j.Files)
                .HasForeignKey(f => f.JobID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<JobPriorityHistory>(history =>
        {
            history.HasKey(h => new { h.JobID, h.ChangeDate });

            history.HasOne(h => h.NewPriority)
                .WithMany()
                .HasForeignKey(h => h.NewPriorityID)
                .OnDelete(DeleteBehavior.NoAction);

            history.HasOne(h => h.PreviousPriority)
                .WithMany()
                .HasForeignKey(h => h.PreviousPriorityID)
                .OnDelete(DeleteBehavior.NoAction);

            history.HasOne(h => h.ChangedBy)
                .WithMany()
                .HasForeignKey(h => h.ChangedByID)
                .OnDelete(DeleteBehavior.NoAction);
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

        builder.Entity<JobStatusHistory>(history =>
        {
            history.HasKey(h => new { h.JobID, h.ChangeDate });

            history.HasOne(h => h.PreviousStatus)
                .WithMany()
                .HasForeignKey(h => h.PreviousStatusID)
                .OnDelete(DeleteBehavior.NoAction);

            history.HasOne(h => h.NewStatus)
                .WithMany()
                .HasForeignKey(h => h.NewStatusID)
                .OnDelete(DeleteBehavior.NoAction);

            history.HasOne(h => h.ChangedBy)
                .WithMany()
                .HasForeignKey(h => h.ChangedByID)
                .OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<OwnerStatusMap>(statusMap =>
        {
            // Composite Key
            statusMap.HasKey(m => new { m.TeamID, m.StatusID });

            // Configuring relationships
            statusMap.HasOne<Team>()
                .WithMany()
                .HasForeignKey(m => m.TeamID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            statusMap.HasOne<Status>()
                .WithMany()
                .HasForeignKey(m => m.StatusID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Priority>(priority =>
        {
            priority.HasKey(p => p.PriorityID);

            priority.HasIndex(p => p.PriorityLevel)
                .IsUnique();
        });

        builder.Entity<Product>(product =>
        {
            product.HasKey(p => p.ProductID);
        });

        builder.Entity<Query>(query => 
        {
            query.ToTable("Query");

            query.Property(b => b.QueryID)
                .ValueGeneratedOnAdd();

            query.HasIndex(b => b.QueryID)
                .IsUnique();

            query.HasOne<Job>()
               .WithOne()
               .HasForeignKey<Query>(b => b.JobID)
               .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Status>(status =>
        {
            status.HasKey(s => s.StatusID);
            status.HasIndex(s => new { s.StatusID, s.Type })
                .IsUnique();
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

        builder.Entity<TeamStatusMap>(statusMap =>
        {
            // Composite Key
            statusMap.HasKey(m => new { m.TeamID, m.StatusID });

            // Configuring relationships
            statusMap.HasOne<Team>()
                .WithMany()
                .HasForeignKey(m => m.TeamID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            statusMap.HasOne<Status>()
                .WithMany()
                .HasForeignKey(m => m.StatusID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
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
        #endregion

    }
}
