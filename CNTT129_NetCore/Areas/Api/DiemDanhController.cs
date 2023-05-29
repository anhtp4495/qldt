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
        // GET: <api>/<version>/<AttendanceController>
        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery] DiemDanhModel model)
        {
            DIEMDANH dd = new DIEMDANH();
            List<DIEMDANH> danhSachDiemDanh = dd.danhSachHD(model.MaHoatDong, model.TrangThai);
            return Ok(JsonSerializer.Serialize<dynamic>(new
            {
                danh_sach = danhSachDiemDanh
            }));
        }

        // POST: <api>/<version>/<AttendanceController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] List<DIEMDANH> danhSachDiemDanh)
        {
            return Ok(JsonSerializer.Serialize<dynamic>(new
            {
                trang_thai = "Điểm danh thành công"
            }));
        }
    }
}
