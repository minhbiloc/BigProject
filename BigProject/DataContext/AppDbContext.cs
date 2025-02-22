using BigProject.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace BigProject.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RewardDiscipline>()
                .HasOne(x => x.Proposer)
                .WithMany()
                .HasForeignKey(x => x.ProposerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RewardDiscipline>()
                .HasOne(x => x.Recipient)
                .WithMany()
                .HasForeignKey(x => x.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<EmailConfirm> emailConfirms { get; set; }
        public DbSet<Event> events { get; set; }
        public DbSet<EventJoint> eventJoints { get; set; }
        public DbSet<EventType> eventTypes { get; set; }
        public DbSet<RefreshToken> refreshTokens { get; set; }
        public DbSet<Report> reports { get; set; }
        public DbSet<ReportType> reportTypes { get; set; }
        public DbSet<RewardDiscipline> rewardDisciplines { get; set; }
        public DbSet<RewardDisciplineType> rewardDisciplineTypes { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<User> users { get; set; }
    }
}
