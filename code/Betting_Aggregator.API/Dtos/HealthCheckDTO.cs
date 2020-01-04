using System.Collections.Generic;

namespace Betting_Aggregator.Api.Dtos
{
    public class HealthCheckDTO 
    {
        public string Version { get; set; } 
        public List<Dependent> DependsOn { get; set; }
    }
}