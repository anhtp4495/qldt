using System.Data.SqlClient;
using System.Data;
using CNTT129_NetCore.Settings;
using CNTT129_NetCore.Extensions;

namespace CNTT129_NetCore.Models.Api
{
    public class SinhVienModel
    {
        public string?         MaSinhVien     { get; set; } = string.Empty;
        public string?      TenSinhVien       { get; set; } = string.Empty;
        public List<string> DanhSachThietBi   { get; set; } = new List<string>();

        public static List<SinhVienModel> GetDanhSachSinhVien(int maHoatDong)
        {
            List<SinhVienModel> danhSachSinhVien = new List<SinhVienModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(AppSettings.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(@"
SELECT 
    SINHVIEN.MASV                                   MaSinhVien,
    SINHVIEN.TENSV                                  TenSinhVien,
    STRING_AGG(THIETBI_SINHVIEN.MATHIETBI, ', ') as DanhSachThietBi
FROM
SINHVIEN
LEFT JOIN 
THIETBI_SINHVIEN
ON SINHVIEN.MASV = THIETBI_SINHVIEN.MASV
GROUP BY SINHVIEN.MASV, SINHVIEN.TENSV
-- WHERE MAHD = @maHoatDong -> Tạo bảng rồi làm tiếp đi em
", con);
                    cmd.Parameters.Add(new SqlParameter("maHoatDong", maHoatDong));
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        danhSachSinhVien.Add(new SinhVienModel
                        {
                            MaSinhVien      = dr.GetStringOrDefault("MaSinhVien"),
                            TenSinhVien     = dr.GetStringOrDefault("TenSinhVien"),
                            DanhSachThietBi = dr.GetListStringOrDefault("DanhSachThietBi")
                        });
                    }
                }
            }
            catch
            {
            }
            return danhSachSinhVien;
        }
    }
}
