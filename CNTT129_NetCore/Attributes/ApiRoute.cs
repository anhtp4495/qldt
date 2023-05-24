using CNTT129_NetCore.Settings;
using Microsoft.AspNetCore.Mvc;

namespace CNTT129_NetCore.Attributes
{
    public class ApiRoute : RouteAttribute
    {
        public ApiRoute() : base($"{AppSettings.ApiPrefix}/{AppSettings.ApiVersion}/[controller]")
        {
        }
        public ApiRoute(string template) : base($"{AppSettings.ApiPrefix}/{AppSettings.ApiVersion}/{template}")
        {
        }
    }
}
