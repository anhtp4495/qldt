using CNTT129_NetCore.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CNTT129_NetCore.Controllers
{
    public class BaseController : Controller
    {
        public CustomSession Session { get => new CustomSession(HttpContext.Session); }
    }
}
