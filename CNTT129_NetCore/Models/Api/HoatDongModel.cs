using System.Data.SqlClient;
using System.Data;
using CNTT129_NetCore.Settings;
using CNTT129_NetCore.Extensions;
using System.Data.SqlTypes;

namespace CNTT129_NetCore.Models.Api
{

    public class SearchHoatDongModel
    {
        public string? NgayBatDau  { get; set; } = string.Empty;
        public string? NgayKetThuc { get; set; } = string.Empty;
    }

    public class HoatDongModel
    {
        public int    MaHoatDong { get; set; }
        public string TieuDe     { get; set; } = string.Empty;
        public string NoiDung    { get; set; } = string.Empty;
        public string NguoiTao   { get; set; } = string.Empty;
        public string Khoa       { get; set; } = string.Empty;
        public string DiaDiem    { get; set; } = string.Empty;
        public string ThoiGian   { get; set; } = string.Empty;


        public static List<HoatDongModel> GetDanhSachHoatDong(SearchHoatDongModel searchModel)
        {
            List<HoatDongModel> danhsachHoatDong = new List<HoatDongModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(AppSettings.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(@"
SELECT 
	HOATDONG.IDHD                                      MaHoatDong,
	HOATDONG.TIEUDE                                    TieuDe,
	HOATDONG.NOIDUNG                                   NoiDung,
	GIANG_VIEN.TENGV                                   NguoiTao,
	HOATDONG.DIADIEM                                   DiaDiem,
	KHOA.TEN_KHOA                                      Khoa,
	CONVERT(varchar(20), HDTN.NGAYBATDAUDIEMDANH, 105) ThoiGian
FROM (
	SELECT IDBUOI, IDHD, NGAYBATDAUDIEMDANH 
	FROM HoatDongTheoNgay
	WHERE
	disabled = 0 AND
	NGAYBATDAUDIEMDANH >= @ngayBatDau AND NGAYBATDAUDIEMDANH <= @ngayKetThuc
) HDTN
INNER JOIN HOATDONG
ON HDTN.IDHD = HOATDONG.IDHD
LEFT JOIN GIANG_VIEN
ON HOATDONG.NGUOITAO = GIANG_VIEN.ID_GV
LEFT JOIN KHOA
ON HOATDONG.ID_KHOA = KHOA.ID_KHOA
ORDER BY NGAYBATDAUDIEMDANH", con);
                    cmd.Parameters.Add(new SqlParameter("ngayBatDau", searchModel.NgayBatDau.ToSqlDateTime(SqlDateTime.MinValue.Value)));
                    cmd.Parameters.Add(new SqlParameter("ngayKetThuc", searchModel.NgayKetThuc.ToSqlDateTime(SqlDateTime.MaxValue.Value)));
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        danhsachHoatDong.Add(new HoatDongModel
                        {
                            MaHoatDong = dr.GetInt32OrDefault("MaHoatDong"),
                            TieuDe     = dr.GetStringOrDefault("TieuDe"),
                            NoiDung    = dr.GetStringOrDefault("NoiDung"),
                            NguoiTao   = dr.GetStringOrDefault("NguoiTao"),
                            DiaDiem    = dr.GetStringOrDefault("DiaDiem"),
                            Khoa       = dr.GetStringOrDefault("Khoa"),
                            ThoiGian   = dr.GetStringOrDefault("ThoiGian")
                        });
                    }
                }
            }
            catch
            {
            }
            return danhsachHoatDong;
        }
    }
}
