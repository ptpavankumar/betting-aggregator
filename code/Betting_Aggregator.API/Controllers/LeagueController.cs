using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Betting_Aggregator.API.Dtos;
using Betting_Aggregator.Business;
using Betting_Aggregator.Business.Models;
using Betting_Aggregator.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Betting_Aggregator.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LeagueController : ControllerBase
    {
        private readonly ILogger<LeagueController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ILeagueService _leagueService;

        public LeagueController(IMapper mapper, IConfiguration configuration, ILeagueService leagueService, ILogger<LeagueController> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
            _leagueService = leagueService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var leagues = _leagueService.FetchAll().Select(item => new LeagueDTO { Name = item.Name, SportType = (SportType)item.SportType });
            return Ok(leagues);
        }

        [HttpPost]
        public IActionResult Post([FromBody] LeagueDTO leagueDTO)
        {
            var leagueId = _leagueService.Save(_mapper.Map<League>(leagueDTO));
            return CreatedAtAction(nameof(Post), new { leagueId }, string.Empty);
        }
    }
}
