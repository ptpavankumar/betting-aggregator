using System;
using System.Linq;
using Betting_Aggregator.Business.Database;
using Betting_Aggregator.Business.Models;
using Microsoft.Extensions.Configuration;

namespace Betting_Aggregator.Business
{
    public class HealthCheckService : IHealthCheckService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _appDbContext;

        public HealthCheckService(IConfiguration configuration, AppDbContext appDbContext)
        {
            _configuration = configuration;
            _appDbContext = appDbContext;
        }

        public Dependent GetDbVersion()
        {
            try
            {
                var versionRecord = _appDbContext.__EFMigrationsHistory
                                    .OrderByDescending(item => item.Id)
                                    .FirstOrDefault();

                var healthCheck = new Dependent();
                healthCheck.Name = "Datastore connectivity";
                healthCheck.Status = "success";
                healthCheck.Version = versionRecord.MigrationId;
                healthCheck.Type = "database";
                return healthCheck;
            }
            catch (Exception ex)
            {
                var healthCheck = new Dependent();
                healthCheck.Name = "Datastore connectivity";
                healthCheck.Status = "failure";
                healthCheck.Type = "database";
                Console.WriteLine(ex);
                return healthCheck;
            }
        }
    }

    public interface IHealthCheckService
    {
        Dependent GetDbVersion();
    }
}
