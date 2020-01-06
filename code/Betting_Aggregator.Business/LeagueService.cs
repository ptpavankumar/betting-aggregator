using System;
using System.Collections.Generic;
using Betting_Aggregator.Business.Database;
using Betting_Aggregator.Business.Models;
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
            throw new NotImplementedException();
        }

        public Guid Save(League league)
        {
            throw new NotImplementedException();
        }
    }

    public interface ILeagueService
    {
        Guid Save(League league);

        List<League> FetchAll();
    }
    
}
