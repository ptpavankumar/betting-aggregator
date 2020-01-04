using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Betting_Aggregator.Api.Dtos;
using Betting_Aggregator.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Betting_Aggregator.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<HealthCheckController> _logger;

        public HealthCheckController(IMapper mapper, IConfiguration configuration, IHealthCheckService healthCheckService, ILogger<HealthCheckController> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
            _healthCheckService = healthCheckService;
        }

        public IConfiguration _configuration { get; }
        public IHealthCheckService _healthCheckService { get; }

        [HttpGet]
        public ActionResult<HealthCheckDTO> Get()
        {
            var dependents = new List<Dependent>();

            var dependent = _healthCheckService.GetDbVersion();
            var dependentDto = _mapper.Map<Dependent>(dependent);
            dependents.Add(dependentDto);

            return new HealthCheckDTO { 
                Version = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion, 
                DependsOn = dependents 
            };
        }
    }
}