using System;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Betting_Aggregator.Api.Dtos;
using Betting_Aggregator.Business;
using Betting_Aggregator.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        public ActionResult<HealthCheckDTO> Get([FromQuery] bool throwInternalServerError, [FromQuery] bool throwBadRequestError)
        {

            var businessExceptionDetails = new Dictionary<string, string>();
            businessExceptionDetails.Add("CollectionSet", "Probability Invalid to empty set");
            if (throwBadRequestError)
            {
                ModelState.AddModelError("", "Do not use this in production");

                throw new BusinessException(businessExceptionDetails);
            }
            if (throwInternalServerError)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Exception("sample error"));
            }

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