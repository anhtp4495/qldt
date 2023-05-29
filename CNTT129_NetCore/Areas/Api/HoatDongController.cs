using Microsoft.AspNetCore.Mvc;
using CNTT129_NetCore.Attributes;
using Microsoft.AspNetCore.Authorization;
using CNTT129_NetCore.Models;
using System.Text.Json;
using CNTT129_NetCore.Models.Api;

namespace CNTT129_NetCore.Areas.Api
{
    [ApiRoute()]
    [ApiController]
    public class HoatDongController : ControllerBase
    {
        // GET: <api>/<version>/<HoatDongController>
        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery] SearchHoatDongModel searchModel)
        {
            List<HoatDongModel> danhSachHoatDong = HoatDongModel.GetDanhSachHoatDong(searchModel);
            return Ok(JsonSerializer.Serialize<dynamic>(new
            {
                danh_sach_hoat_dong = danhSachHoatDong
            }));
        }

        // GET: <api>/<version>/<HoatDongController>
        [HttpGet]
        [Authorize]
        [Route("DanhSachSinhVien")]
        public IActionResult GetDanhSachSinhVien([FromQuery] int maHoatDong)
        {
            List<SinhVienModel> danhSachSinhVien = SinhVienModel.GetDanhSachSinhVien(maHoatDong);
            return Ok(JsonSerializer.Serialize<dynamic>(new
            {
                danh_sach_sinh_vien = danhSachSinhVien
            }));
        }
    }
}
