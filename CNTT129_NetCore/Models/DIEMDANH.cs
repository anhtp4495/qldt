using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CNTT129_NetCore.Settings;

namespace CNTT129_NetCore.Models
{
    public class DIEMDANH
    {
        public string conf = AppSettings.ConnectionString;
        public string IDDD { get; set; }
        public string IDSV { get; set; }
        public string MASV { get; set; }
        public string TENSV { get; set; }
        public string SDT { get; set; }
        public string EMAIL { get; set; }
        public string GHICHU { get; set; }
        public string NGAYDIEMDANH { get; set; }
        public string TRANGTHAI { get; set; }
        public string IDBUOI { get; set; }

        public string TIEUDE { get; set; }

        public string NGAYBATDAU { get; set; }

        public string NGAYHUY { get; set; }

        public string LOAI_BUOI { get; set; }

        public string MAHD { get; set; }
        public string TENHK { get; set; }

        public int ktTraSvDiemDanh(string idbuoi, string idsv)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd2 = new SqlCommand("select count(*) from DIEMDANH where IDBUOI=" + idbuoi + " and trangthai = 0 and IDSV =" + idsv + "", con);
            cmd2.CommandType = CommandType.Text;
            con.Open();
            object kq = cmd2.ExecuteScalar();
            con.Close();
            return int.Parse(kq.ToString());
        }

        public int ktraAll(string idsv, string idbuoi)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd2 = new SqlCommand("select count(*) from DIEMDANH where IDBUOI='" + idbuoi + "' and idsv = " + idsv, con);
            cmd2.CommandType = CommandType.Text;
            con.Open();
            object kq = cmd2.ExecuteScalar();
            con.Close();
            return int.Parse(kq.ToString());
        }

        public int layDsSvDaThamGia(string idsv)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd2 = new SqlCommand("select count(*) from DIEMDANH where trangthai = 0 and IDSV =" + idsv + "", con);
            cmd2.CommandType = CommandType.Text;
            con.Open();
            object kq = cmd2.ExecuteScalar();
            con.Close();
            return int.Parse(kq.ToString());
        }

        public int save(string idbuoi, string idsv, string masv, string sdt, string email, string ghichu, string ngaydiemdanh)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into DIEMDANH(IDSV,MASV,SDT,EMAIL,GHICHU,NGAYDIEMDANH,IDBUOI) values (N'" + idsv + "',N'" + masv + "',N'" + sdt + "',N'" + email + "',N'" + ghichu + "',N'" + ngaydiemdanh + "',N'" + idbuoi + "')", con);
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

        public List<DIEMDANH> danhSachHDDD(string IDSV, string IDHK)
        {
            List<DIEMDANH> list = new List<DIEMDANH>();
            SqlConnection con = new SqlConnection(conf);
            var sql = "";
            var sql_where = "";
            if (IDHK != "")
            {
                sql_where = " and HOATDONG.ID_HK = " + IDHK;
            }
            sql = " inner join HOATDONGTHEONGAY ON HOATDONGTHEONGAY.IDBUOI = DIEMDANH.IDBUOI inner join HOATDONG ON HOATDONG.IDHD = HOATDONGTHEONGAY.IDHD ";
            sql += " inner join HOC_KI ON HOC_KI.ID_HK = HOATDONG.ID_HK ";
            SqlCommand cmd = new SqlCommand("select HOATDONG.TIEUDE,DIEMDANH.NGAYDIEMDANH,HOATDONGTHEONGAY.LOAI_BUOI,HOATDONG.MAHD,TEN_HK,DIEMDANH.TRANGTHAI,HOATDONGTHEONGAY.NGAYBATDAUDIEMDANH  from DIEMDANH" + sql + " where IDSV='" + IDSV + "' and DIEMDANH.TRANGTHAI = 0 " + sql_where + " ORDER BY NGAYDIEMDANH", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                DIEMDANH emp = new DIEMDANH();
                emp.TIEUDE = dr.GetValue(0).ToString();
                emp.NGAYDIEMDANH = dr.GetValue(1).ToString();
                emp.LOAI_BUOI = dr.GetValue(2).ToString();
                emp.MAHD = dr.GetValue(3).ToString();
                emp.TENHK = dr.GetValue(4).ToString();
                emp.TRANGTHAI = dr.GetValue(5).ToString();
                emp.NGAYBATDAU = dr.GetValue(6).ToString();
                list.Add(emp);
            }
            con.Close();
            return list;
        }

        public List<DIEMDANH> danhSachHD(string IDBUOI, string trang_thai)
        {
            List<DIEMDANH> list = new List<DIEMDANH>();
            SqlConnection con = new SqlConnection(conf);
            var sql = "";
            var sql_where = "";
            if (trang_thai != "")
            {
                //sql_where = " and DANGKY.TRANGTHAI = " + trang_thai;
            }
            SqlCommand cmd = new SqlCommand("Select ID_SV,DIEMDANH.MASV,TENSV,IDDD from DIEMDANH	INNER JOIN HOATDONGTHEONGAY ON DIEMDANH.IDBUOI = HOATDONGTHEONGAY.IDBUOI	INNER JOIN HOATDONG ON HOATDONG.IDHD = HOATDONGTHEONGAY.IDHD	INNER JOIN SINHVIEN ON SINHVIEN.ID_SV = DIEMDANH.IDSV	where HOATDONGTHEONGAY.IDBUOI = " + IDBUOI + " " + sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                DIEMDANH emp = new DIEMDANH();
                emp.IDSV = dr.GetValue(0).ToString();
                emp.MASV = dr.GetValue(1).ToString();
                emp.TENSV = dr.GetValue(2).ToString();
                emp.IDDD = dr.GetValue(3).ToString();
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
            SqlCommand cmd2 = new SqlCommand("select count(*) from DIEMDANH where IDBUOI= " + IDBUOI + " " + sql, con);
            cmd2.CommandType = CommandType.Text;
            con.Open();
            object kq = cmd2.ExecuteScalar();
            int dr = int.Parse(kq.ToString());
            con.Close();
            return dr;
        }

    }
}