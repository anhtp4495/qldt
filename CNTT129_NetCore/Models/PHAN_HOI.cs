using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CNTT129_NetCore.Settings;

namespace CNTT129_NetCore.Models
{
    public class PHAN_HOI
    {
        public string conf = AppSettings.ConnectionString;
        public string ID_PHAN_HOI { get; set; }
        public string MA_PHAN_HOI { get; set; }
        public string IDSV { get; set; }
        public string TIEUDE { get; set; }
        public string NOIDUNG { get; set; }
        public string TRANGTHAI { get; set; }
        public string TEP { get; set; }
        public string LOAI_PHAN_HOI { get; set; }
        public string IDBUOI { get; set; }

        public string MA { get; set; }

        public string NGAY { get; set; }
        public string ID_VI_PHAM { get; set; }
        public string NGAYTAO { get; set; }
        public string NGAYDUYET { get; set; }
        public string NGUOIDUYET { get; set; }
        public string MASV { get; set; }
        public string TENSV { get; set; }

        public int save(string tieu_de, string loai, string hoat_dong, string loi_nhan, string idsv, string tep, string ngay_tao, string hoc_ky)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd2 = new SqlCommand("select count(*) from PHAN_HOI", con);
            cmd2.CommandType = CommandType.Text;
            con.Open();
            Object kq = cmd2.ExecuteScalar();

            var ma = "";
            int number = 1;
            string number2 = "";

            if (!kq.Equals(0))
            {
                number = int.Parse(kq.ToString()) + number;
            }
            int codeLength = 7;
            int lenOfNum = number.ToString().Length;
            if (lenOfNum < codeLength)
            {
                int loopNum = codeLength - lenOfNum;
                for (int i = 1; i <= loopNum; i++)
                {
                    number2 = '0' + number.ToString();
                }
            }


            var sql = "";
            if (loai == "1")
            {
                ma = "PHHD" + number2;
                sql = "insert into PHAN_HOI(MA_PHAN_HOI,IDSV,TIEUDE,NOIDUNG,TEP,NGAYTAO,IDBUOI,LOAI_PHAN_HOI,IDHK) values (N'" + ma + "',N'" + idsv + "',N'" + tieu_de + "',N'" + loi_nhan + "',N'" + tep + "',N'" + ngay_tao + "','" + hoat_dong + "','" + loai + "','" + hoc_ky + "')";
            }
            else
            {
                ma = "PHVP" + number2;
                sql = "insert into PHAN_HOI(MA_PHAN_HOI,IDSV,TIEUDE,NOIDUNG,TEP,NGAYTAO,ID_VI_PHAM,LOAI_PHAN_HOI,IDHK) values (N'" + ma + "',N'" + idsv + "',N'" + tieu_de + "',N'" + loi_nhan + "',N'" + tep + "',N'" + ngay_tao + "','" + hoat_dong + "','" + loai + "','" + hoc_ky + "')";
            }
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteNonQuery();
            con.Close();

            return dr;
        }

        public List<PHAN_HOI> find(string ma_ph, string trang_thai, string start_date, string end_date, string loai, string idsv)
        {
            string sql = " where PHAN_HOI.ngaytao >= '" + start_date + "' and PHAN_HOI.ngaytao <= '" + end_date + "' and PHAN_HOI.IDSV = " + idsv;

            if (ma_ph != "" && ma_ph != null)
            {
                sql += "and MA_PHAN_HOI = '" + ma_ph + "' ";
            }

            if (loai != "")
            {
                sql += "and LOAI_PHAN_HOI = " + loai + " ";
            }

            if (trang_thai != "")
            {
                sql += "and PHAN_HOI.TRANGTHAI = " + trang_thai + " ";
            }

            List<PHAN_HOI> listHD = new List<PHAN_HOI>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand(" select ID_PHAN_HOI,MA_PHAN_HOI,PHAN_HOI.TIEUDE,PHAN_HOI.NGAYTAO,HOATDONG.MAHD as MA, HOATDONGTHEONGAY.NGAYBATDAUDIEMDANH AS NGAY, PHAN_HOI.NGUOIDUYET,PHAN_HOI.NGAYDUYET,PHAN_HOI.TRANGTHAI,PHAN_HOI.LOAI_PHAN_HOI,PHAN_HOI.NOIDUNG,GIANG_VIEN.TENGV from PHAN_HOI   LEFT JOIN HOATDONGTHEONGAY ON HOATDONGTHEONGAY.IDBUOI = PHAN_HOI.IDBUOI   INNER JOIN HOATDONG ON HOATDONGTHEONGAY.IDHD = HOATDONG.IDHD  LEFT JOIN GIANG_VIEN ON GIANG_VIEN.ID_GV = PHAN_HOI.NGUOIDUYET  " + sql + " UNION ALL   select ID_PHAN_HOI,MA_PHAN_HOI,PHAN_HOI.TIEUDE,PHAN_HOI.NGAYTAO,VI_PHAM.MA_VI_PHAM as MA, VI_PHAM.NGAYTAO AS NGAY, PHAN_HOI.NGUOIDUYET,PHAN_HOI.NGAYDUYET,PHAN_HOI.TRANGTHAI,PHAN_HOI.LOAI_PHAN_HOI,PHAN_HOI.NOIDUNG,GIANG_VIEN.TENGV  from PHAN_HOI   LEFT JOIN VI_PHAM ON VI_PHAM.ID_VI_PHAM = PHAN_HOI.ID_VI_PHAM  INNER JOIN NOI_QUY ON NOI_QUY.ID_NOI_QUY = VI_PHAM.ID_NOI_QUY   LEFT JOIN GIANG_VIEN ON GIANG_VIEN.ID_GV = PHAN_HOI.NGUOIDUYET " + sql + " and VI_PHAM.TRANGTHAI = 1", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                PHAN_HOI emp = new PHAN_HOI();
                emp.ID_PHAN_HOI = dr.GetValue(0).ToString();
                emp.MA_PHAN_HOI = dr.GetValue(1).ToString();
                emp.TIEUDE = dr.GetValue(2).ToString();
                emp.NGAYTAO = dr.GetValue(3).ToString();
                emp.MA = dr.GetValue(4).ToString();
                emp.NGAY = dr.GetValue(5).ToString();
                emp.NGAYDUYET = dr.GetValue(7).ToString();
                emp.TRANGTHAI = dr.GetValue(8).ToString();
                emp.LOAI_PHAN_HOI = dr.GetValue(9).ToString();
                emp.NOIDUNG = dr.GetValue(10).ToString();
                emp.NGUOIDUYET = dr.GetValue(11).ToString();
                listHD.Add(emp);
            }
            con.Close();
            return listHD;
        }

        public List<PHAN_HOI> find2(string ma_ph, string trang_thai, string start_date, string end_date, string loai, string masv, string hoc_ky)
        {
            string sql = " where PHAN_HOI.ngaytao >= '" + start_date + "' and PHAN_HOI.ngaytao <= '" + end_date + "' ";

            if (ma_ph != "" && ma_ph != null)
            {
                sql += "and MA_PHAN_HOI = '" + ma_ph + "' ";
            }

            if (loai != "")
            {
                sql += "and LOAI_PHAN_HOI = " + loai + " ";
            }

            if (trang_thai != "")
            {
                sql += "and PHAN_HOI.TRANGTHAI = " + trang_thai + " ";
            }

            if (masv != "")
            {
                sql += "and SINHVIEN.MASV = " + masv + " ";
            }
            if (hoc_ky != "")
            {
                sql += "and PHAN_HOI.IDHK = " + hoc_ky + " ";
            }

            List<PHAN_HOI> listHD = new List<PHAN_HOI>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select ID_PHAN_HOI,MA_PHAN_HOI,PHAN_HOI.TIEUDE,PHAN_HOI.NGAYTAO,HOATDONG.MAHD as MA, HOATDONGTHEONGAY.NGAYBATDAUDIEMDANH AS NGAY, PHAN_HOI.NGUOIDUYET,PHAN_HOI.NGAYDUYET,PHAN_HOI.TRANGTHAI,PHAN_HOI.LOAI_PHAN_HOI,PHAN_HOI.NOIDUNG,GIANG_VIEN.TENGV,SINHVIEN.MASV,SINHVIEN.TENSV from PHAN_HOI   LEFT JOIN HOATDONGTHEONGAY ON HOATDONGTHEONGAY.IDBUOI = PHAN_HOI.IDBUOI   INNER JOIN HOATDONG ON HOATDONGTHEONGAY.IDHD = HOATDONG.IDHD  LEFT JOIN GIANG_VIEN ON GIANG_VIEN.ID_GV = PHAN_HOI.NGUOIDUYET INNER JOIN SINHVIEN ON SINHVIEN.ID_SV = PHAN_HOI.IDSV " + sql + " UNION ALL   select ID_PHAN_HOI,MA_PHAN_HOI,PHAN_HOI.TIEUDE,PHAN_HOI.NGAYTAO,VI_PHAM.MA_VI_PHAM as MA, VI_PHAM.NGAYTAO AS NGAY, PHAN_HOI.NGUOIDUYET,PHAN_HOI.NGAYDUYET,PHAN_HOI.TRANGTHAI,PHAN_HOI.LOAI_PHAN_HOI,PHAN_HOI.NOIDUNG,GIANG_VIEN.TENGV,SINHVIEN.MASV,SINHVIEN.TENSV  from PHAN_HOI   LEFT JOIN VI_PHAM ON VI_PHAM.ID_VI_PHAM = PHAN_HOI.ID_VI_PHAM  INNER JOIN NOI_QUY ON NOI_QUY.ID_NOI_QUY = VI_PHAM.ID_NOI_QUY   LEFT JOIN GIANG_VIEN ON GIANG_VIEN.ID_GV = PHAN_HOI.NGUOIDUYET INNER JOIN SINHVIEN ON SINHVIEN.ID_SV = PHAN_HOI.IDSV " + sql + " and VI_PHAM.TRANGTHAI = 1", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                PHAN_HOI emp = new PHAN_HOI();
                emp.ID_PHAN_HOI = dr.GetValue(0).ToString();
                emp.MA_PHAN_HOI = dr.GetValue(1).ToString();
                emp.TIEUDE = dr.GetValue(2).ToString();
                emp.NGAYTAO = dr.GetValue(3).ToString();
                emp.MA = dr.GetValue(4).ToString();
                emp.NGAY = dr.GetValue(5).ToString();
                emp.NGAYDUYET = dr.GetValue(7).ToString();
                emp.TRANGTHAI = dr.GetValue(8).ToString();
                emp.LOAI_PHAN_HOI = dr.GetValue(9).ToString();
                emp.NOIDUNG = dr.GetValue(10).ToString();
                emp.NGUOIDUYET = dr.GetValue(11).ToString();
                emp.MASV = dr.GetValue(12).ToString();
                emp.TENSV = dr.GetValue(13).ToString();
                listHD.Add(emp);
            }
            con.Close();
            return listHD;
        }

        public int updateTT(string id, string trang_thai)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            SqlCommand cmd = new SqlCommand("update phan_hoi set trangthai = " + trang_thai + " where ID_PHAN_HOI =" + id, con);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteNonQuery();
            con.Close();

            return dr;
        }

        public int updateTT2(string id, string trang_thai, string idgv)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            var sql = "";
            if (trang_thai == "2" || trang_thai == "1")
            {
                sql = ",NGUOIDUYET = '" + idgv + "', NGAYDUYET = GETDATE()";
            }
            SqlCommand cmd = new SqlCommand("update phan_hoi set trangthai = " + trang_thai + " " + sql + " where ID_PHAN_HOI =" + id, con);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteNonQuery();
            con.Close();

            return dr;
        }

        public List<PHAN_HOI> danhSachHD(string IDBUOI, string trang_thai)
        {
            List<PHAN_HOI> list = new List<PHAN_HOI>();
            SqlConnection con = new SqlConnection(conf);
            var sql = "";
            SqlCommand cmd = new SqlCommand("Select ID_SV,SINHVIEN.MASV,TENSV,ID_PHAN_HOI,PHAN_HOI.TRANGTHAI,PHAN_HOI.NOIDUNG,PHAN_HOI.TEP from PHAN_HOI	INNER JOIN HOATDONGTHEONGAY ON PHAN_HOI.IDBUOI = HOATDONGTHEONGAY.IDBUOI	INNER JOIN HOATDONG ON HOATDONG.IDHD = HOATDONGTHEONGAY.IDHD	INNER JOIN SINHVIEN ON SINHVIEN.ID_SV = PHAN_HOI.IDSV	where HOATDONGTHEONGAY.IDBUOI = " + IDBUOI + " " + sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                PHAN_HOI emp = new PHAN_HOI();
                emp.IDSV = dr.GetValue(0).ToString();
                emp.MASV = dr.GetValue(1).ToString();
                emp.TENSV = dr.GetValue(2).ToString();
                emp.ID_PHAN_HOI = dr.GetValue(3).ToString();
                emp.TRANGTHAI = dr.GetValue(4).ToString();
                emp.NOIDUNG = dr.GetValue(5).ToString();
                emp.TEP = dr.GetValue(6).ToString();
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
            SqlCommand cmd2 = new SqlCommand("select count(*) from PHAN_HOI where IDBUOI= " + IDBUOI + " " + sql, con);
            cmd2.CommandType = CommandType.Text;
            con.Open();
            Object kq = cmd2.ExecuteScalar();
            con.Close();
            int dr = int.Parse(kq.ToString());
            return dr;
        }

        public int demSLSV2(string ID_NOI_QUY, string trang_thai, string idhk)
        {
            SqlConnection con = new SqlConnection(conf);
            var sql = "";
            if (trang_thai != "")
            {
                sql = "and PHAN_HOI.TRANGTHAI =" + trang_thai;
            }
            con.Open();
            SqlCommand cmd2 = new SqlCommand("select count(*) from PHAN_HOI INNER JOIN VI_PHAM ON VI_PHAM.ID_VI_PHAM = PHAN_HOI.ID_VI_PHAM where VI_PHAM.ID_NOI_QUY= " + ID_NOI_QUY + " " + sql + " and PHAN_HOI.idhk=" + idhk, con);
            cmd2.CommandType = CommandType.Text;

            Object kq = cmd2.ExecuteScalar();
            con.Close();
            int dr = int.Parse(kq.ToString());
            return dr;
        }

        public int demSL(string idhk, string ngaybd, string ngaykt)
        {
            var sql = "";
            if (ngaybd != "")
            {
                sql += " and NGAYTAO >= '" + ngaybd + "' ";
            }
            if (ngaykt != "")
            {
                sql += " and NGAYTAO <= '" + ngaykt + "' ";
            }
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd2 = new SqlCommand("select count(*) from PHAN_HOI where trangthai = 0 and IDHK = " + idhk + " " + sql, con);
            cmd2.CommandType = CommandType.Text;
            con.Open();
            Object kq = cmd2.ExecuteScalar();
            int dr = int.Parse(kq.ToString());
            con.Close();
            return dr;
        }
    }
}