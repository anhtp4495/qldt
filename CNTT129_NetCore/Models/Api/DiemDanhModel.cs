using System.Data.SqlClient;
using System.Data;
using CNTT129_NetCore.Settings;
using CNTT129_NetCore.Extensions;

namespace CNTT129_NetCore.Models.Api
{
    public class DiemDanhModel
    {
        public string Id { get; set; } = string.Empty;
        
        public List<SinhVienModel> DanhSachSinhVien { get; set; } = new List<SinhVienModel>();
        
        public static bool Save(DiemDanhModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Id))
            {
                return false;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(AppSettings.ConnectionString))
                {
                    try
                    {
                        foreach (SinhVienModel sv in model.DanhSachSinhVien)
                        {
                            SqlCommand cmdInsert = new SqlCommand(@"
INSERT INTO DIEMDANH(ID_SV, MASV, SDT, EMAIL, GHICHU, NGAYDIEMDANH, TRANGTHAI, IDBUOI)
SELECT
	SINHVIEN.ID_SV,
	SINHVIEN.MASV,
	SINHVIEN.SDT,
	SINHVIEN.EMAIL,
	@ghiChu,
	@ngayDiemDanh,
	0,
	@idBuoi
FROM SINHVIEN
WHERE SINHVIEN.MASV = @maSV", con);
                            cmdInsert.CommandType = CommandType.Text;
                            cmdInsert.Parameters.Add(new SqlParameter("idBuoi", model.Id));
                            cmdInsert.Parameters.Add(new SqlParameter("maSV", sv.MaSinhVien));
                            cmdInsert.Parameters.Add(new SqlParameter("ghiChu", string.Format("{0}", sv.DanhSachThietBi.Count > 0 ? sv.DanhSachThietBi[0]: "Không có thiết bị!")));
                            cmdInsert.ExecuteScalar();
                        }
                    }
                    catch { }
                }

                return true;
            }
            catch
            {
            }
            return false;
        }
    }
}
