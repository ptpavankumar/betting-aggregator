using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace Betting_Aggregator.Api.Controllers
{
    [Route("")]
    [Route("[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
        }
    }
}
