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

        // GET: <api>/<version>/<HoatDongController>/danhsachbuoi
        [HttpGet]
        [Authorize]
        [Route("DanhSachBuoi")]
        public IActionResult GetDanhSachBuoi([FromQuery] int maHoatDong)
        {
            List<BuoiDiemDanhModel> danhSachBuoiDiemDanh = BuoiDiemDanhModel.GetDanhSachBuoi(maHoatDong);
            return Ok(JsonSerializer.Serialize<dynamic>(new
            {
                danh_sach_buoi = danhSachBuoiDiemDanh
            }));
        }

        // GET: <api>/<version>/<HoatDongController>/danhsachsinhVien
        [HttpGet]
        [Authorize]
        [Route("DanhSachSinhVien")]
        public IActionResult GetDanhSachSinhVien([FromQuery] int maBuoiHoatDong)
        {
            List<SinhVienModel> danhSachSinhVien = SinhVienModel.GetDanhSachSinhVien(maBuoiHoatDong);
            return Ok(JsonSerializer.Serialize<dynamic>(new
            {
                danh_sach_sinh_vien = danhSachSinhVien
            }));
        }
    }
}
