namespace CNTT129_NetCore.Models.Api
{
    public class DiemDanhModel
    {
        public string MaHoatDong { get; set; } = string.Empty;
        public string TrangThai { get; set; } = string.Empty;

        public List<SinhVienModel> DanhSachSinhVien { get; set; } = new List<SinhVienModel>();

    }
}
