using System.IO;
using Betting_Aggregator.Business.Database.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Betting_Aggregator.Business.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseNpgsql(string.Concat(_configuration["PostgreSql:ConnectionString"], "password=", _configuration["PostgreSql:DbPassword"]));
                //server=localhost;port=5432;userid=postgres;database=bettingAggregator;
                //server=localhost;port=5432;userid=postgres;database=bettingAggregator;password=sa;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");
            modelBuilder.ForNpgsqlUseIdentityAlwaysColumns();
        }

        public DbSet<__EFMigrationsHistory> __EFMigrationsHistory { get; set; }
        public DbSet<League> League { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}