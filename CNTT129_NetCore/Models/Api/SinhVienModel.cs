﻿using System.Data.SqlClient;
using System.Data;
using CNTT129_NetCore.Settings;
using CNTT129_NetCore.Extensions;

namespace CNTT129_NetCore.Models.Api
{
    public class SinhVienModel
    {
        public string?      MaSinhVien        { get; set; } = string.Empty;
        public string?      TenSinhVien       { get; set; } = string.Empty;
        public List<string> DanhSachThietBi   { get; set; } = new List<string>();

        public static List<SinhVienModel> GetDanhSachSinhVien(int maBuoiHoatDong)
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
    STRING_AGG(TB.MATHIETBI, ', ') as               DanhSachThietBi
FROM (
	SELECT *
	FROM HOATDONGTHEONGAY
	WHERE IDBUOI = @maBuoiHoatDong
) HD
INNER JOIN DANGKY
ON DANGKY.IDBUOI = HD.IDBUOI
INNER JOIN SINHVIEN
ON DANGKY.MASV = SINHVIEN.MASV
LEFT JOIN THIETBI_SINHVIEN TB
ON SINHVIEN.MASV = TB.MASV
WHERE DANGKY.TRANGTHAI = 1
GROUP BY SINHVIEN.MASV, SINHVIEN.TENSV", con);
                    cmd.Parameters.Add(new SqlParameter("maBuoiHoatDong", maBuoiHoatDong));
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
        
        public static SinhVienModel UpdateThietBi(SinhVienModel model)
        {
            SinhVienModel sv = null;
            try
            {
                using (SqlConnection con = new SqlConnection(AppSettings.ConnectionString))
                {
                    SqlCommand cmdInsert = new SqlCommand(@"
INSERT INTO THIETBI_SINHVIEN(MASV, MATHIETBI)
SELECT
    SINHVIEN.MASV,
    @maThietBi
FROM SINHVIEN
WHERE RTRIM(SINHVIEN.MASV) = @maSV", con);
                    cmdInsert.CommandType = CommandType.Text;
                    cmdInsert.Parameters.Add(new SqlParameter("maThietBi", model.DanhSachThietBi[0]));
                    cmdInsert.Parameters.Add(new SqlParameter("maSV", model.MaSinhVien));

                    SqlCommand cmdSelect = new SqlCommand(@"
SELECT
	SINHVIEN.MASV                                   MaSinhVien,
    SINHVIEN.TENSV                                  TenSinhVien,
    STRING_AGG(TB.MATHIETBI, ', ') as               DanhSachThietBi
FROM SINHVIEN
LEFT JOIN THIETBI_SINHVIEN TB
ON SINHVIEN.MASV = TB.MASV
WHERE SINHVIEN.MASV = @maSV
GROUP BY SINHVIEN.MASV, SINHVIEN.TENSV", con);
                    cmdSelect.Parameters.Add(new SqlParameter("maSV", model.MaSinhVien));
                    cmdSelect.CommandType = CommandType.Text;

                    con.Open();

                    try
                    {
                        cmdInsert.ExecuteScalar();
                    }
                    catch { }

                    SqlDataReader dr = cmdSelect.ExecuteReader();
                    if (dr.Read())
                    {
                        sv = new SinhVienModel
                        {
                            MaSinhVien      = dr.GetStringOrDefault("MaSinhVien"),
                            TenSinhVien     = dr.GetStringOrDefault("TenSinhVien"),
                            DanhSachThietBi = dr.GetListStringOrDefault("DanhSachThietBi")
                        };
                    }
                }
            }
            catch
            {
            }
            return sv;
        }
    }
}
