using System;
using System.Collections.Generic;
using System.Linq;
using Betting_Aggregator.Business.Database;
using Betting_Aggregator.Business.Models;
using Betting_Aggregator.Utils;
using Microsoft.Extensions.Configuration;

namespace Betting_Aggregator.Business
{
    public class LeagueService : ILeagueService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _appDbContext;

        public LeagueService(IConfiguration configuration, AppDbContext appDbContext)
        {
            _configuration = configuration;
            _appDbContext = appDbContext;
        }

        public List<League> FetchAll()
        {
            return _appDbContext.League.Select(item => new League { Name = item.Name, SportType = 0 }).ToList();
        }

        public int Save(League league)
        {
            var existingLeague = _appDbContext.League
                .Select(item => item.Name == league.Name)
                .ToList();
            if (existingLeague.Any())
            {
                var details = new Dictionary<string, string>();
                details.Add("Name", "League should be unique in a season");
                throw new BusinessException(details);
            }
            var createdLeague = new Database.DbModels.League { Name = league.Name };
            _appDbContext.Add(createdLeague);
            _appDbContext.SaveChanges();

            return createdLeague.Id;
        }
    }

    public interface ILeagueService
    {
        int Save(League league);

        List<League> FetchAll();
    }
    
}
