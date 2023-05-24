using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CNTT129_NetCore.Settings;

namespace CNTT129_NetCore.Models
{
    public class HoatDongBuoi
    {
        public string conf = AppSettings.ConnectionString;
        public string IDBUOI { get; set; }
        public string IDHD { get; set; }
        public string MAHD { get; set; }
        public string LOAI_BUOI { get; set; }
        public Boolean LOAI_BUOI_S { get; set; }
        public Boolean LOAI_BUOI_C { get; set; }
        public Boolean LOAI_BUOI_T { get; set; }
        public string NGAYDIEMDANH { get; set; }
        public string TIEUDE { get; set; }
        public string name_khoa { get; set; }
        public string name_hocky { get; set; }

        public string disabled { get; set; }
        public string Trang_Thai { get; set; }
        public int save(int idhd, int loai, string ngaybatdaudd)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into HOATDONGTHEONGAY(IDHD,LOAI_BUOI,NGAYBATDAUDIEMDANH,NGAYKETTHUCDIENDANH) values (N'" + idhd + "',N'" + loai + "',N'" + ngaybatdaudd + "',N'" + ngaybatdaudd + "')", con);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteNonQuery();
            con.Close();

            return dr;
        }

        public List<HoatDongBuoi> findByID(string IDHD)
        {
            List<HoatDongBuoi> listHD = new List<HoatDongBuoi>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select HOATDONGTHEONGAY.* from HOATDONGTHEONGAY where IDHD='" + IDHD + "' and disabled = 0 order by NGAYBATDAUDIEMDANH ASC", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                HoatDongBuoi emp = new HoatDongBuoi();
                emp.IDBUOI = dr.GetValue(0).ToString();
                emp.IDHD = dr.GetValue(1).ToString();
                emp.LOAI_BUOI = dr.GetValue(2).ToString();
                emp.NGAYDIEMDANH = dr.GetValue(3).ToString();
                emp.disabled = dr.GetValue(5).ToString();
                emp.Trang_Thai = dr.GetValue(6).ToString();
                listHD.Add(emp);
            }
            con.Close();
            return listHD;
        }

        public List<HoatDongBuoi> findByIDBuoi(string IDBUOI)
        {
            List<HoatDongBuoi> listHD = new List<HoatDongBuoi>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select HOATDONGTHEONGAY.* from HOATDONGTHEONGAY where IDBUOI='" + IDBUOI + "' and disabled = 0 order by NGAYBATDAUDIEMDANH ASC", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                HoatDongBuoi emp = new HoatDongBuoi();
                emp.IDBUOI = dr.GetValue(0).ToString();
                emp.IDHD = dr.GetValue(1).ToString();
                emp.LOAI_BUOI = dr.GetValue(2).ToString();
                emp.NGAYDIEMDANH = dr.GetValue(3).ToString();
                emp.disabled = dr.GetValue(5).ToString();
                emp.Trang_Thai = dr.GetValue(6).ToString();
                listHD.Add(emp);
            }
            con.Close();
            return listHD;
        }

        public List<HoatDongBuoi> findByIDBuoi2(string IDBUOI)
        {
            List<HoatDongBuoi> listHD = new List<HoatDongBuoi>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select HOATDONGTHEONGAY.* from HOATDONGTHEONGAY where IDBUOI='" + IDBUOI + "' and trangthai = 4 order by NGAYBATDAUDIEMDANH ASC", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                HoatDongBuoi emp = new HoatDongBuoi();
                emp.IDBUOI = dr.GetValue(0).ToString();
                emp.IDHD = dr.GetValue(1).ToString();
                emp.LOAI_BUOI = dr.GetValue(2).ToString();
                emp.NGAYDIEMDANH = dr.GetValue(3).ToString();
                emp.disabled = dr.GetValue(5).ToString();
                emp.Trang_Thai = dr.GetValue(6).ToString();
                listHD.Add(emp);
            }
            con.Close();
            return listHD;
        }

        public List<HoatDongBuoi> findByIDGroupByDate(string IDHD)
        {
            List<HoatDongBuoi> listHD = new List<HoatDongBuoi>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select NGAYBATDAUDIEMDANH from HOATDONGTHEONGAY where IDHD='" + IDHD + "' and disabled = 0 group by NGAYBATDAUDIEMDANH", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                HoatDongBuoi emp = new HoatDongBuoi();
                emp.IDBUOI = "";
                emp.IDHD = "";
                emp.LOAI_BUOI_S = false;
                emp.LOAI_BUOI_C = false;
                emp.LOAI_BUOI_T = false;
                emp.NGAYDIEMDANH = dr.GetValue(0).ToString();
                emp.disabled = "";
                emp.Trang_Thai = "";
                listHD.Add(emp);
            }
            con.Close();
            return listHD;
        }

        public int updateTT(int idhd, int tt)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            SqlCommand cmd = new SqlCommand("update HOATDONGTHEONGAY set disabled = '" + tt + "' where idhd=" + idhd, con);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteNonQuery();
            con.Close();

            return dr;
        }

        public int KtraHD(int id, string ngay, int loai)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd2 = new SqlCommand("select count(*) from HOATDONGTHEONGAY where idhd='" + id + "' and NGAYBATDAUDIEMDANH = '" + ngay + "' and LOAI_BUOI = " + loai, con);
            cmd2.CommandType = CommandType.Text;
            con.Open();
            Object kq = cmd2.ExecuteScalar();
            con.Close();
            return int.Parse(kq.ToString());
        }

        public int laySlBuoiHdChuaHuy(int id)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd2 = new SqlCommand("select count(*) from HOATDONGTHEONGAY where idhd='" + id + "'and TRANGTHAI != 1 and disabled = 0", con);
            cmd2.CommandType = CommandType.Text;
            con.Open();
            Object kq = cmd2.ExecuteScalar();
            con.Close();
            return int.Parse(kq.ToString());
        }

        public int laySlBuoiHdChuaDongDangKy(int id)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd2 = new SqlCommand("select count(*) from HOATDONGTHEONGAY where IDHD=" + id + " and TRANGTHAI = 0 and disabled = 0", con);
            cmd2.CommandType = CommandType.Text;
            con.Open();
            Object kq = cmd2.ExecuteScalar();
            con.Close();
            return int.Parse(kq.ToString());
        }

        public int laySlBuoiHdChuaDongDiemDanh(int id)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd2 = new SqlCommand("select count(*) from HOATDONGTHEONGAY where IDHD=" + id + " and TRANGTHAI IN (3,1) and disabled = 0", con);
            cmd2.CommandType = CommandType.Text;
            con.Open();
            Object kq = cmd2.ExecuteScalar();
            con.Close();
            return int.Parse(kq.ToString());
        }

        public int countHd(int id)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd2 = new SqlCommand("select count(*) from HOATDONGTHEONGAY where IDHD=" + id + " and disabled = 0", con);
            cmd2.CommandType = CommandType.Text;
            con.Open();
            Object kq = cmd2.ExecuteScalar();
            con.Close();
            return int.Parse(kq.ToString());
        }

        public int updateHdb(int idhd, string ngay, int loai, int tt)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            SqlCommand cmd = new SqlCommand("update HOATDONGTHEONGAY set disabled = '" + tt + "'  where idhd='" + idhd + "' and NGAYBATDAUDIEMDANH = '" + ngay + "' and LOAI_BUOI = " + loai, con);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteNonQuery();
            con.Close();

            return dr;
        }

        public int updateTTHdb(int idbuoi, int tt)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            SqlCommand cmd = new SqlCommand("update HOATDONGTHEONGAY set TRANGTHAI = '" + tt + "'  where idbuoi=" + idbuoi, con);//1 hủy // 0 đang chờ // 2 là đã tạo mã  // 3 là form đã đóng điểm danh // 4 là đóng form đăng ký
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteNonQuery();
            con.Close();

            return dr;
        }

        public List<HoatDongBuoi> find(string CODE_HD, string TEN_HD, string ID_KHOA, string ngaybd, string ngaykt, string ID_HK, string loai)
        {
            string sql = " where NGAYBATDAUDIEMDANH >= '" + ngaybd + "' and NGAYBATDAUDIEMDANH <= '" + ngaykt + "' and HOATDONGTHEONGAY.disabled = 0 ";

            if (CODE_HD != "" && CODE_HD != null)
            {
                sql += "and MAHD = '" + CODE_HD + "' ";
            }

            if (TEN_HD != "" && TEN_HD != null)
            {
                sql += "and TIEUDE like '%" + TEN_HD + "%' ";
            }

            if (ID_KHOA != "0")
            {
                sql += "and HOATDONG.ID_KHOA = " + ID_KHOA + " ";
            }


            if (ID_HK != "0")
            {
                sql += "and HOATDONG.ID_HK = " + ID_HK + " ";
            }

            if (loai != "")
            {
                sql += "and HOATDONGTHEONGAY.TRANGTHAI = " + loai + " ";
            }
            else
            {
                sql += "and HOATDONGTHEONGAY.TRANGTHAI IN (3,6)";
            }

            List<HoatDongBuoi> listHD = new List<HoatDongBuoi>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select HOATDONG.IDHD,HOATDONGTHEONGAY.NGAYBATDAUDIEMDANH,HOATDONG.TIEUDE,HOATDONGTHEONGAY.TRANGTHAI,KHOA.TEN_KHOA,HOC_KI.TEN_HK,hoatdong.mahd,HOATDONGTHEONGAY.IDBUOI from HOATDONG INNER JOIN HOATDONGTHEONGAY ON HOATDONGTHEONGAY.IDHD = HOATDONG.IDHD LEFT JOIN KHOA ON KHOA.ID_KHOA = HOATDONG.ID_KHOA LEFT JOIN HOC_KI ON HOC_KI.ID_HK = HOATDONG.ID_HK " + sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                HoatDongBuoi emp = new HoatDongBuoi();
                emp.IDHD = dr.GetValue(0).ToString();
                emp.NGAYDIEMDANH = dr.GetValue(1).ToString();
                emp.TIEUDE = dr.GetValue(2).ToString();
                emp.Trang_Thai = dr.GetValue(3).ToString();
                emp.name_khoa = dr.GetValue(4).ToString();
                emp.name_hocky = dr.GetValue(5).ToString();
                emp.MAHD = dr.GetValue(6).ToString();
                emp.IDBUOI = dr.GetValue(7).ToString();
                listHD.Add(emp);
            }
            con.Close();
            return listHD;
        }
        public int updateTTHdb()
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            SqlCommand cmd = new SqlCommand("update HOATDONGTHEONGAY set TRANGTHAI = 5  where NGAYBATDAUDIEMDANH < FORMAT(GETDATE(),'yyyy-MM-dd') and trangthai = 4", con);//1 hủy // 0 đang chờ // 2 là đã tạo mã  // 3 là form đã đóng điểm danh // 4 là đóng form đăng ký
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteNonQuery();
            con.Close();

            return dr;
        }

        public int huyAllHdb(int idhd)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            SqlCommand cmd = new SqlCommand("update HOATDONGTHEONGAY set TRANGTHAI = 1 where idhd=" + idhd, con);//1 hủy // 0 đang chờ // 2 là đã tạo mã  // 3 là form đã đóng điểm danh // 4 là đóng form đăng ký
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteNonQuery();
            con.Close();

            return dr;
        }

    }

     
}