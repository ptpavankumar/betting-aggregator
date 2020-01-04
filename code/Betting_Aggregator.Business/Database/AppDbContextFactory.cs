using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Betting_Aggregator.Business.Database
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext Create()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var basePath = AppContext.BaseDirectory;

            return Create(basePath, environmentName);
        }

        public AppDbContext CreateDbContext(string[] args)
        {
            return Create(
                Directory.GetCurrentDirectory(),
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
        }

        private AppDbContext Create(string basePath, string environmentName)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true);

            var config = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = config["PostgreSql:ConnectionString"];
            var dbPassword = config["PostgreSql:DbPassword"];

            var ngBuilder = new NpgsqlConnectionStringBuilder(connectionString)
            {
                Password = dbPassword
            };
            optionsBuilder.UseNpgsql(ngBuilder.ConnectionString);

            return new AppDbContext(optionsBuilder.Options);
            
        }
    }

}
