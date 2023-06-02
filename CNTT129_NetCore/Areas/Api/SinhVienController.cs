using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CNTT129_NetCore.Attributes;
using CNTT129_NetCore.Models.Api;
using CNTT129_NetCore.Models;

namespace CNTT129_NetCore.Areas.Api
{
    [ApiRoute()]
    [ApiController]
    public class SinhVienController : ControllerBase
    {

        // POST: <api>/<version>/<SinhVienController>/thietbi
        [HttpPost]
        [Authorize]
        [Route("ThietBi")]
        public IActionResult Post([FromBody] SinhVienModel model)
        {
            SinhVienModel sinhVien = SinhVienModel.UpdateThietBi(model);
            if (sinhVien != null)
            {
                return Ok(JsonSerializer.Serialize<dynamic>(new
                {
                    sinh_vien = sinhVien
                }));
            }

            return BadRequest(JsonSerializer.Serialize<dynamic>(new
            {
                error_message = "Không tìm thấy thông tin sinh viên!"
            }));
        }
    }
}
