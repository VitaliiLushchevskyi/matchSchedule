using matchSchedule.Models;
using Microsoft.EntityFrameworkCore;

namespace matchSchedule.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<PlayerTeamHistory> PlayerTeamsHistory { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Team>().ToTable("teams");
            modelBuilder.Entity<Player>().ToTable("players");
            modelBuilder.Entity<Match>().ToTable("matches");
            modelBuilder.Entity<Tournament>().ToTable("tournaments");
            modelBuilder.Entity<Coach>().ToTable("coaches");
            modelBuilder.Entity<PlayerTeamHistory>().ToTable("playerTeamsHistory");

            modelBuilder.Entity<Match>()
            .HasOne(m => m.HomeTeam)
            .WithMany()
            .HasForeignKey(m => m.HomeTeamId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.AwayTeam)
                .WithMany()
                .HasForeignKey(m => m.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
