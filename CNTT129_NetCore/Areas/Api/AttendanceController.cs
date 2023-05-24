using Microsoft.AspNetCore.Mvc;
using CNTT129_NetCore.Attributes;

namespace CNTT129_NetCore.Areas.Api
{
    [ApiRoute()]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        // GET: <api>/<version>/<AttendanceController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Attendance1", "Attendance2" };
        }
    }
}
