using System.Data;
using System.Data.SqlClient;
using CNTT129_NetCore.Settings;
using CNTT129_NetCore.Extensions;

namespace CNTT129_NetCore.Models.Api
{
    public class BuoiDiemDanhSearchModel
    {
        public int?    MaHoatDong     { get; set; } = 0;
        public string? ThoiGianBatDau { get; set; } = string.Empty;
    }
    public class BuoiDiemDanhModel
    {
        public string MaBuoi { get; set; }          = string.Empty;
        public int    LoaiBuoi { get; set; }        = 0;
        public string TenBuoi { get; set; }         = string.Empty;
        public string ThoiGianBatDau { get; set; }  = string.Empty;
        public string ThoiGianKetThuc { get; set; } = string.Empty;

        private static List<BuoiDiemDanhModel> GetDanhSachBuoiTheoNgay(string ngayBatDau)
        {
            List<BuoiDiemDanhModel> danhSachSinhVien = new List<BuoiDiemDanhModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(AppSettings.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(@"
SELECT
	IDBUOI                                                 MaBuoi,
	LOAI_BUOI                                              LoaiBuoi,
	CONVERT(varchar(20), HDTN.NGAYBATDAUDIEMDANH, 105)     ThoiGianBatDau,
	CONVERT(varchar(20), HDTN.NGAYKETTHUCDIEnDANH, 105)    ThoiGianKetThuc
FROM HOATDONGTHEONGAY HDTN
WHERE DATEDIFF(day, HDTN.NGAYBATDAUDIEMDANH, @ngayBatDau) = 0
ORDER BY NGAYBATDAUDIEMDANH, LOAI_BUOI", con);
                    cmd.Parameters.Add(new SqlParameter("ngayBatDau", ngayBatDau.ToSqlDateTime(DateTime.Now)));
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        danhSachSinhVien.Add(new BuoiDiemDanhModel
                        {
                            MaBuoi          = dr.GetStringOrDefault("MaBuoi"),
                            LoaiBuoi        = dr.GetInt32OrDefault("LoaiBuoi"),
                            TenBuoi         = dr.GetInt32OrDefault("LoaiBuoi").ConvertLoai2TenBuoi(),
                            ThoiGianBatDau  = dr.GetStringOrDefault("ThoiGianBatDau"),
                            ThoiGianKetThuc = dr.GetStringOrDefault("ThoiGianKetThuc"),
                        });
                    }
                }
            }
            catch
            {
            }
            return danhSachSinhVien;
        }

        public static List<BuoiDiemDanhModel> GetDanhSachBuoi(BuoiDiemDanhSearchModel searchModel)
        {
            if (searchModel.MaHoatDong <= 0) return GetDanhSachBuoiTheoNgay(searchModel.ThoiGianBatDau);

            List<BuoiDiemDanhModel> danhSachSinhVien = new List<BuoiDiemDanhModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(AppSettings.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(@"
SELECT
	IDBUOI                                                 MaBuoi,
	LOAI_BUOI                                              LoaiBuoi,
	CONVERT(varchar(20), HDTN.NGAYBATDAUDIEMDANH, 105)     ThoiGianBatDau,
	CONVERT(varchar(20), HDTN.NGAYKETTHUCDIEnDANH, 105)    ThoiGianKetThuc
FROM HOATDONGTHEONGAY HDTN
WHERE IDHD = @maHoatDong
ORDER BY NGAYBATDAUDIEMDANH, LOAI_BUOI", con);
                    cmd.Parameters.Add(new SqlParameter("maHoatDong", searchModel.MaHoatDong));
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        danhSachSinhVien.Add(new BuoiDiemDanhModel
                        {
                            MaBuoi          = dr.GetStringOrDefault("MaBuoi"),
                            LoaiBuoi        = dr.GetInt32OrDefault("LoaiBuoi"),
                            TenBuoi         = dr.GetInt32OrDefault("LoaiBuoi").ConvertLoai2TenBuoi(),
                            ThoiGianBatDau  = dr.GetStringOrDefault("ThoiGianBatDau"),
                            ThoiGianKetThuc = dr.GetStringOrDefault("ThoiGianKetThuc"),
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
