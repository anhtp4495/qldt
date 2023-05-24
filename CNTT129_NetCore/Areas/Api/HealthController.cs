using CNTT129_NetCore.Extensions;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CNTT129_NetCore.Areas.Api
{
    [Route("health")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        // GET: <api>/<version>/<HealthController>
        [HttpGet]
        public string Get()
        {
            return "OK";
        }
    }
}
