using Microsoft.AspNetCore.Mvc;
using CNTT129_NetCore.Attributes;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using CNTT129_NetCore.Models.Api;
using CNTT129_NetCore.Models;

namespace CNTT129_NetCore.Areas.Api
{
    [ApiRoute()]
    [ApiController]
    public class DiemDanhController : ControllerBase
    {
        // POST: <api>/<version>/<DiemDanhController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] DiemDanhModel model)
        {
            if (DiemDanhModel.Save(model))
            {
                return Ok(JsonSerializer.Serialize<dynamic>(new
                {
                    trang_thai = "Điểm danh thành công!",
                }));
            }

            return BadRequest(JsonSerializer.Serialize<dynamic>(new
            {
                error_message = "Điểm danh thất bại!",
            }));
        }
    }
}
