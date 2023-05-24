using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CNTT129_NetCore.Settings;

namespace CNTT129_NetCore.Models
{
    public class DANGKY
    {
        public string conf = AppSettings.ConnectionString;
        public string IDDK { get; set; }
        public string IDSV { get; set; }
        public string MASV { get; set; }
        public string TENSV { get; set; }
        public string SDT { get; set; }
        public string EMAIL { get; set; }
        public string GHICHU { get; set; }
        public string NGAYDANGKY { get; set; }
        public string NGAYKETTHUC { get; set; }
        public string TRANGTHAI { get; set; } // 2 là nhà trường hủy 1 là tham gia 3 là không tham gia 4 là tự hủy 5 trạng thái k cho sinh viên tham gia
        public string IDBUOI { get; set; }

        public string TIEUDE { get; set; }

        public string NGAYBATDAU { get; set; }

        public string NGAYHUY { get; set; }

        public string LOAI_BUOI { get; set; }

        public string MAHD { get; set; }
        public string TENHK { get; set; }

        public int DemSLNguoiDK(string id)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd2 = new SqlCommand("select count(*) from DANGKY where IDBUOI='" + id + "' and trangthai != 4", con);
            cmd2.CommandType = CommandType.Text;
            con.Open();
            Object kq = cmd2.ExecuteScalar();
            con.Close();
            return int.Parse(kq.ToString());
        }

        public int ktraDangKy(string idsv, string idbuoi)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd2 = new SqlCommand("select count(*) from DANGKY where IDBUOI='" + idbuoi + "' and trangthai = 0 and idsv = " + idsv, con);
            cmd2.CommandType = CommandType.Text;
            con.Open();
            Object kq = cmd2.ExecuteScalar();
            con.Close();
            return int.Parse(kq.ToString());
        }

        public int ktraAll(string idsv, string idbuoi)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd2 = new SqlCommand("select count(*) from DANGKY where IDBUOI='" + idbuoi + "' and idsv = " + idsv, con);
            cmd2.CommandType = CommandType.Text;
            con.Open();
            Object kq = cmd2.ExecuteScalar();
            con.Close();
            return int.Parse(kq.ToString());
        }

        public int layDanhSachHDSvDaDK(string idsv)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd2 = new SqlCommand("select count(*) from DANGKY where idsv = " + idsv, con);
            cmd2.CommandType = CommandType.Text;
            con.Open();
            Object kq = cmd2.ExecuteScalar();
            con.Close();
            return int.Parse(kq.ToString());
        }

        public int save(string idbuoi, string idsv, string masv, string sdt, string email, string ghichu, string ngaydangky)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into DANGKY(IDSV,MASV,SDT,EMAIL,GHICHU,NGAYDANGKY,IDBUOI) values (N'" + idsv + "',N'" + masv + "',N'" + sdt + "',N'" + email + "',N'" + ghichu + "',N'" + ngaydangky + "',N'" + idbuoi + "')", con);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteNonQuery();
            con.Close();

            return dr;
        }

        public int capnhat(string idbuoi, string idsv, string trang_thai)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            SqlCommand cmd = new SqlCommand("update dangky set trangthai = " + trang_thai + " where idbuoi = " + idbuoi + " and idsv =" + idsv, con);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteNonQuery();
            con.Close();

            return dr;
        }

        public int huyDangKySinhVien(string iddk, string trang_thai)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            SqlCommand cmd = new SqlCommand("update dangky set trangthai = " + trang_thai + ",NGAYHUY = GETDATE() where iddk = " + iddk + "", con);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteNonQuery();
            con.Close();

            return dr;
        }

        public int khongChoSinhVienThamGia(string iddk, string trang_thai)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            SqlCommand cmd = new SqlCommand("update dangky set trangthai = " + trang_thai + ",NGAYHUY = GETDATE() where iddk = " + iddk + "", con);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteNonQuery();
            con.Close();

            return dr;
        }

        public List<DANGKY> danhSachHDDK(string IDSV, string IDHK)
        {
            List<DANGKY> list = new List<DANGKY>();
            SqlConnection con = new SqlConnection(conf);
            var sql = "";
            var sql_where = "";
            if (IDHK != "")
            {
                sql_where = " and HOATDONG.ID_HK = " + IDHK;
            }
            sql = " inner join HOATDONGTHEONGAY ON HOATDONGTHEONGAY.IDBUOI = dangky.IDBUOI inner join HOATDONG ON HOATDONG.IDHD = HOATDONGTHEONGAY.IDHD ";
            sql += " inner join HOC_KI ON HOC_KI.ID_HK = HOATDONG.ID_HK ";
            SqlCommand cmd = new SqlCommand("select HOATDONG.TIEUDE,HOATDONGTHEONGAY.NGAYBATDAUDIEMDANH,HOATDONG.NGAYBATDAUDK,HOATDONGTHEONGAY.LOAI_BUOI,HOATDONG.MAHD,TEN_HK,HOATDONG.NGAYKETTHUCDK,dangky.TRANGTHAI,dangky.IDDK from dangky " + sql + " where IDSV='" + IDSV + "' and dangky.TRANGTHAI = 0 " + sql_where + " ORDER BY NGAYDANGKY", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                DANGKY emp = new DANGKY();
                emp.TIEUDE = dr.GetValue(0).ToString();
                emp.NGAYBATDAU = dr.GetValue(1).ToString();
                emp.NGAYDANGKY = dr.GetValue(2).ToString();
                emp.LOAI_BUOI = dr.GetValue(3).ToString();
                emp.MAHD = dr.GetValue(4).ToString();
                emp.TENHK = dr.GetValue(5).ToString();
                emp.NGAYKETTHUC = dr.GetValue(6).ToString();
                emp.TRANGTHAI = dr.GetValue(7).ToString();
                emp.IDDK = dr.GetValue(8).ToString();
                list.Add(emp);
            }
            con.Close();
            return list;
        }

        public List<DANGKY> danhSachHDHuy(string IDSV, string IDHK, string trang_thai)
        {
            List<DANGKY> list = new List<DANGKY>();
            SqlConnection con = new SqlConnection(conf);
            var sql = "";
            var sql_where = "";
            if (IDHK != "")
            {
                sql_where = " and HOATDONG.ID_HK = " + IDHK;
            }
            sql = " inner join HOATDONGTHEONGAY ON HOATDONGTHEONGAY.IDBUOI = dangky.IDBUOI inner join HOATDONG ON HOATDONG.IDHD = HOATDONGTHEONGAY.IDHD ";
            sql += " inner join HOC_KI ON HOC_KI.ID_HK = HOATDONG.ID_HK ";
            SqlCommand cmd = new SqlCommand("select HOATDONG.TIEUDE,dangky.ngayhuy,dangky.NGAYDANGKY,HOATDONGTHEONGAY.LOAI_BUOI,dangky.TRANGTHAI,HOATDONG.MAHD,TEN_HK,HOATDONG.NGAYKETTHUCDK,HOATDONGTHEONGAY.NGAYBATDAUDIEMDANH from dangky " + sql + " where IDSV='" + IDSV + "' and dangky.TRANGTHAI IN (" + trang_thai + ")  " + sql_where + " ORDER BY NGAYDANGKY", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                DANGKY emp = new DANGKY();
                emp.TIEUDE = dr.GetValue(0).ToString();
                emp.NGAYHUY = dr.GetValue(1).ToString();
                emp.NGAYDANGKY = dr.GetValue(2).ToString();
                emp.LOAI_BUOI = dr.GetValue(3).ToString();
                emp.TRANGTHAI = dr.GetValue(4).ToString();
                emp.MAHD = dr.GetValue(5).ToString();
                emp.TENHK = dr.GetValue(6).ToString();
                emp.NGAYKETTHUC = dr.GetValue(7).ToString();
                emp.NGAYBATDAU = dr.GetValue(8).ToString();
                list.Add(emp);
            }
            con.Close();
            return list;
        }

        public int capnhatKhongThamGia()
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            SqlCommand cmd = new SqlCommand("update DANGKY set TRANGTHAI = 3,NGAYHUY = GETDATE() from HOATDONGTHEONGAY where IDDK IN (select IDDK from HOATDONGTHEONGAY,DANGKY where HOATDONGTHEONGAY.IDBUOI = DANGKY.IDBUOI and  HOATDONGTHEONGAY.NGAYBATDAUDIEMDANH < FORMAT(GETDATE(),'yyyy-MM-dd') and IDDK NOT IN( select DANGKY.IDDK from DIEMDANH,DANGKY,HOATDONGTHEONGAY where DIEMDANH.IDBUOI = DANGKY.IDBUOI and DIEMDANH.IDSV = DANGKY.IDSV and HOATDONGTHEONGAY.IDBUOI = DANGKY.IDBUOI and  HOATDONGTHEONGAY.NGAYBATDAUDIEMDANH < FORMAT(GETDATE(),'yyyy-MM-dd'))) ", con);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteNonQuery();
            con.Close();

            return dr;
        }

        public int capnhatHuyTatCa(string idbuoi)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            SqlCommand cmd = new SqlCommand("update DANGKY set TRANGTHAI = 2,NGAYHUY = GETDATE() where IDBUOI = " + idbuoi, con);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteNonQuery();
            con.Close();

            return dr;
        }

        public int capnhatKhongThamGiaTatCa(int idbuoi)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            SqlCommand cmd = new SqlCommand("update DANGKY set TRANGTHAI = 3,NGAYHUY = GETDATE() from HOATDONGTHEONGAY where IDDK IN (select IDDK from HOATDONGTHEONGAY,DANGKY where HOATDONGTHEONGAY.IDBUOI = DANGKY.IDBUOI and  HOATDONGTHEONGAY.IDBUOI = " + idbuoi + " and IDDK NOT IN(  select DANGKY.IDDK from DIEMDANH,DANGKY,HOATDONGTHEONGAY  where DIEMDANH.IDBUOI = DANGKY.IDBUOI and DIEMDANH.IDSV = DANGKY.IDSV and HOATDONGTHEONGAY.IDBUOI = DANGKY.IDBUOI and  HOATDONGTHEONGAY.IDBUOI =  " + idbuoi + "))", con);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteNonQuery();
            con.Close();

            return dr;
        }

        public int capnhatPhieuDangKyDaThamGia(string idbuoi, string idsv)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            SqlCommand cmd = new SqlCommand("update DANGKY set TRANGTHAI = 1 where IDBUOI = " + idbuoi + " and idsv = " + idsv, con);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteNonQuery();
            con.Close();

            return dr;
        }

        public List<DANGKY> danhSachHD(string IDBUOI, string trang_thai)
        {
            List<DANGKY> list = new List<DANGKY>();
            SqlConnection con = new SqlConnection(conf);
            var sql = "";
            var sql_where = "";
            if (trang_thai != "")
            {
                sql_where = " and DANGKY.TRANGTHAI = " + trang_thai;
            }
            SqlCommand cmd = new SqlCommand("Select ID_SV,DANGKY.MASV,TENSV,IDDK,dangky.GHICHU from DANGKY	INNER JOIN HOATDONGTHEONGAY ON DANGKY.IDBUOI = HOATDONGTHEONGAY.IDBUOI	INNER JOIN HOATDONG ON HOATDONG.IDHD = HOATDONGTHEONGAY.IDHD	INNER JOIN SINHVIEN ON SINHVIEN.ID_SV = DANGKY.IDSV	where HOATDONGTHEONGAY.IDBUOI = " + IDBUOI + " " + sql_where, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                DANGKY emp = new DANGKY();
                emp.IDSV = dr.GetValue(0).ToString();
                emp.MASV = dr.GetValue(1).ToString();
                emp.TENSV = dr.GetValue(2).ToString();
                emp.IDDK = dr.GetValue(3).ToString();
                emp.GHICHU = dr.GetValue(4).ToString();
                list.Add(emp);
            }
            con.Close();
            return list;
        }

        public int demSLSV(string IDBUOI, string trang_thai)
        {
            SqlConnection con = new SqlConnection(conf);
            var sql = "";
            if (trang_thai != "")
            {
                sql = "and TRANGTHAI =" + trang_thai;
            }
            SqlCommand cmd2 = new SqlCommand("select count(*) from DANGKY where IDBUOI= " + IDBUOI + " " + sql, con);
            cmd2.CommandType = CommandType.Text;
            con.Open();
            Object kq = cmd2.ExecuteScalar();
            int dr = int.Parse(kq.ToString());
            con.Close();
            return dr;
        }

        public int updateDiem(string idhk)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            SqlCommand cmd = new SqlCommand("update KETQUA set DIEM += HOATDONG.DIEMRL from DIEMDANH,HOATDONGTHEONGAY,HOATDONG where KETQUA.IDSV = DIEMDANH.IDSV and HOATDONGTHEONGAY.IDHD = HOATDONG.IDHD and DIEMDANH.IDBUOI = HOATDONGTHEONGAY.IDBUOI and HOATDONG.ID_HK = " + idhk + " and HOATDONGTHEONGAY.TRANGTHAI = 6", con);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteNonQuery();
            con.Close();

            con.Open();
            SqlCommand cmd2 = new SqlCommand("update KETQUA set DIEM += HOATDONG.DIEMRL from PHAN_HOI,HOATDONGTHEONGAY,HOATDONG where KETQUA.IDSV = PHAN_HOI.IDSV and HOATDONGTHEONGAY.IDHD = HOATDONG.IDHD and PHAN_HOI.IDBUOI = HOATDONGTHEONGAY.IDBUOI and PHAN_HOI.IDHK = " + idhk + " and PHAN_HOI.TRANGTHAI = 1 and HOATDONGTHEONGAY.TRANGTHAI = 6", con);
            cmd2.CommandType = CommandType.Text;
            dr = cmd2.ExecuteNonQuery();
            con.Close();

            con.Open();
            SqlCommand cmd3 = new SqlCommand("update KETQUA set DIEM -= HOATDONG.DIEMRL from DANGKY,HOATDONGTHEONGAY,HOATDONG where KETQUA.IDSV = DANGKY.IDSV and HOATDONGTHEONGAY.IDHD = HOATDONG.IDHD and DANGKY.IDBUOI = HOATDONGTHEONGAY.IDBUOI and HOATDONG.ID_HK = " + idhk + " and HOATDONGTHEONGAY.TRANGTHAI = 6 and DANGKY.TRANGTHAI = 3", con);
            cmd3.CommandType = CommandType.Text;
            dr = cmd3.ExecuteNonQuery();
            con.Close();

            con.Open();
            SqlCommand cmd4 = new SqlCommand("update KETQUA set DIEM += NOI_QUY.DIEM from PHAN_HOI,VI_PHAM,NOI_QUY where KETQUA.IDSV = PHAN_HOI.IDSV and PHAN_HOI.ID_VI_PHAM = VI_PHAM.ID_VI_PHAM and NOI_QUY.ID_NOI_QUY = VI_PHAM.ID_NOI_QUY and PHAN_HOI.IDHK = " + idhk + " and PHAN_HOI.TRANGTHAI = 1", con);
            cmd4.CommandType = CommandType.Text;
            dr = cmd4.ExecuteNonQuery();
            con.Close();

            con.Open();
            SqlCommand cmd5 = new SqlCommand("update KETQUA set DIEM -= NOI_QUY.DIEM from VI_PHAM,NOI_QUY where KETQUA.IDSV = VI_PHAM.IDSV  and NOI_QUY.ID_NOI_QUY = VI_PHAM.ID_NOI_QUY and VI_PHAM.IDHK = " + idhk + " and VI_PHAM.TRANGTHAI = 1", con);
            cmd5.CommandType = CommandType.Text;
            dr = cmd5.ExecuteNonQuery();
            con.Close();

            return dr;
        }
        public int ktraDangKyTrung(string ngayhoatdong, string loaibuoi, string idsv)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd2 = new SqlCommand("select count(*) from DANGKY,HOATDONGTHEONGAY where DANGKY.IDBUOI = HOATDONGTHEONGAY.IDBUOI and  LOAI_BUOI = " + loaibuoi + " and DANGKY.trangthai = 0 and idsv = " + idsv + " and NGAYBATDAUDIEMDANH = '" + ngayhoatdong + "'", con);
            cmd2.CommandType = CommandType.Text;
            con.Open();
            Object kq = cmd2.ExecuteScalar();
            con.Close();
            return int.Parse(kq.ToString());
        }
    }
}