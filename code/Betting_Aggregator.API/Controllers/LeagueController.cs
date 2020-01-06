using System;
using System.Collections.Generic;
using AutoMapper;
using Betting_Aggregator.API.Dtos;
using Betting_Aggregator.Business;
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
            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] LeagueDTO leagueDTO)
        {
            //var dic = new Dictionary<string, string>();
            //dic.Add("Name", "This name has already been registered");
            //throw new BusinessException(dic);

            //throw new Exception("Unhanddled exception");
            return Ok();
            //var type = await _templateService.CreateTemplateAsync(request);
            //return CreatedAtAction(nameof(Get), new { type }, string.Empty);
        }
    }
}
