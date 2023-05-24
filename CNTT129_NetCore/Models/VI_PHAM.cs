using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CNTT129_NetCore.Settings;

namespace CNTT129_NetCore.Models
{
    public class VI_PHAM
    {
        public string conf = AppSettings.ConnectionString;
        public string ID_VI_PHAM { get; set; }
        public string MA_VI_PHAM { get; set; }
        public string TIEUDE { get; set; }
        public string NOIDUNG { get; set; }
        public string NGUOITAO { get; set; }
        public string NGAYTAO { get; set; }
        public string NGUOISUA { get; set; }
        public string NGAYSUA { get; set; }
        public string NGUOIDUYET { get; set; }
        public string NGAYDUYET { get; set; }
        public string TRANGTHAI { get; set; }
        public string ID_NOI_QUY { get; set; }
        public string IDSV { get; set; }
        public string IDHK { get; set; }
        public string MASV { get; set; }
        public string TENSV { get; set; }
        public string TENNQ { get; set; }
        public string DIEM { get; set; }
        public string HINH_THUC { get; set; }
        public int save(string tieude, string noidung, string nguoitao, string ngaytao, string id_noi_quy, string idsv, string idhk)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd2 = new SqlCommand("select count(*) from VI_PHAM", con);
            cmd2.CommandType = CommandType.Text;
            con.Open();
            Object kq = cmd2.ExecuteScalar();

            int number = 1;
            var ma = "";
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
            ma = "VP" + number2;
            SqlCommand cmd = new SqlCommand("insert into VI_PHAM(MA_VI_PHAM,TIEUDE,NOIDUNG,NGUOITAO,NGAYTAO,ID_NOI_QUY,IDSV,IDHK) values(N'" + ma + "',N'" + tieude + "',N'" + noidung + "',N'" + nguoitao + "','" + ngaytao + "','" + id_noi_quy + "','" + idsv + "','" + idhk + "')", con);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteNonQuery();
            con.Close();
            return dr;
        }

        public List<VI_PHAM> find(string CODE_VI_PHAM, string CODE_SINH_VIEN, string disabled, string idhk)
        {
            string sql = null;
            if (CODE_VI_PHAM != "" || CODE_SINH_VIEN != "" || disabled != "")
            {
                sql += " where ";
            }
            if (CODE_VI_PHAM != "")
            {
                sql += "MA_VI_PHAM = '" + CODE_VI_PHAM + "' ";
            }
            if (CODE_VI_PHAM != "" && CODE_SINH_VIEN != "")
            {
                sql += " and ";
            }
            if (CODE_SINH_VIEN != "")
            {
                sql += "MASV = '" + CODE_SINH_VIEN + "' ";
            }
            if (CODE_SINH_VIEN != "" && disabled != "" || CODE_VI_PHAM != "" && disabled != "")
            {
                sql += " and ";
            }

            if (disabled != "")
            {
                sql += "VI_PHAM.TRANGTHAI = " + disabled + "";
            }

            if (CODE_SINH_VIEN != "" && disabled != "" || CODE_VI_PHAM != "" && disabled != "" || idhk != "" && disabled != "" || CODE_VI_PHAM != "" && idhk != "" || CODE_SINH_VIEN != "" && idhk != "")
            {
                sql += " and ";
            }

            if (idhk != "")
            {
                sql += "VI_PHAM.IDHK = " + idhk + "";
            }
            List<VI_PHAM> listBH = new List<VI_PHAM>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select VI_PHAM.*,MASV,TENSV,TEN_NOI_QUY,DIEM from VI_PHAM inner join SINHVIEN on SINHVIEN.ID_SV = VI_PHAM.IDSV inner join NOI_QUY on NOI_QUY.ID_NOI_QUY = VI_PHAM.ID_NOI_QUY" + sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                VI_PHAM emp = new VI_PHAM();
                emp.ID_VI_PHAM = dr.GetValue(0).ToString();
                emp.MA_VI_PHAM = dr.GetValue(1).ToString();
                emp.TIEUDE = dr.GetValue(2).ToString();
                emp.NOIDUNG = dr.GetValue(3).ToString();
                emp.NGUOITAO = dr.GetValue(4).ToString();
                emp.NGAYTAO = dr.GetValue(5).ToString();
                emp.NGUOISUA = dr.GetValue(6).ToString();
                emp.NGAYSUA = dr.GetValue(7).ToString();
                emp.NGUOIDUYET = dr.GetValue(8).ToString();
                emp.NGAYDUYET = dr.GetValue(9).ToString();
                emp.TRANGTHAI = dr.GetValue(10).ToString();
                emp.ID_NOI_QUY = dr.GetValue(11).ToString();
                emp.IDSV = dr.GetValue(12).ToString();
                emp.IDHK = dr.GetValue(13).ToString();
                emp.MASV = dr.GetValue(14).ToString();
                emp.TENSV = dr.GetValue(15).ToString();
                emp.TENNQ = dr.GetValue(16).ToString();
                emp.DIEM = dr.GetValue(17).ToString();
                listBH.Add(emp);
            }
            con.Close();
            return listBH;
        }

        public int updateTT(int id, int disabled)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            string sql = "";
            sql = "update VI_PHAM set TRANGTHAI = " + disabled + ",NGAYDUYET = GETDATE() where ID_VI_PHAM =" + id;
            if (sql != "")
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteNonQuery();
                con.Close();
            }

            return dr;
        }

        public List<VI_PHAM> layViPhamSinhVienDaDuyet(string idsv, string idhk)
        {
            List<VI_PHAM> listBH = new List<VI_PHAM>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select VI_PHAM.ID_VI_PHAM,VI_PHAM.MA_VI_PHAM,TEN_NOI_QUY from VI_PHAM inner join NOI_QUY on NOI_QUY.ID_NOI_QUY = VI_PHAM.ID_NOI_QUY where IDSV = " + idsv + " and VI_PHAM.IDHK = " + idhk + " and VI_PHAM.TRANGTHAI = 1 and ID_VI_PHAM not in (select PHAN_HOI.ID_VI_PHAM from PHAN_HOI inner join VI_PHAM on VI_PHAM.ID_VI_PHAM = PHAN_HOI.ID_VI_PHAM where PHAN_HOI.IDSV = " + idsv + ") and (DATEDIFF(day,VI_PHAM.NGAYDUYET, GETDATE()) < 7  or DATEDIFF(day,VI_PHAM.NGAYDUYET, GETDATE()) = 7 )", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                VI_PHAM emp = new VI_PHAM();
                emp.ID_VI_PHAM = dr.GetValue(0).ToString();
                emp.MA_VI_PHAM = dr.GetValue(1).ToString();
                emp.TENNQ = dr.GetValue(2).ToString();
                listBH.Add(emp);
            }
            con.Close();
            return listBH;
        }

        public int demSLSV(string ID_NOI_QUY, string trang_thai, string idhk)
        {
            SqlConnection con = new SqlConnection(conf);
            var sql = "";
            if (trang_thai != "")
            {
                sql = "and TRANGTHAI =" + trang_thai;
            }
            SqlCommand cmd2 = new SqlCommand("select count(*) from VI_PHAM where ID_NOI_QUY= " + ID_NOI_QUY + " " + sql + " and IDHK =" + idhk, con);
            cmd2.CommandType = CommandType.Text;
            con.Open();
            Object kq = cmd2.ExecuteScalar();
            int dr = int.Parse(kq.ToString());
            con.Close();
            return dr;
        }

        public List<VI_PHAM> showKetQua(string IDSV, string idhk)
        {
            List<VI_PHAM> listHD = new List<VI_PHAM>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select VI_PHAM.MA_VI_PHAM,NOI_QUY.TEN_NOI_QUY,VI_PHAM.NGAYTAO,VI_PHAM.NOIDUNG,N'Vi phạm nội quy' as 'hinh_thuc',NOI_QUY.DIEM from VI_PHAM INNER JOIN NOI_QUY on NOI_QUY.ID_NOI_QUY = VI_PHAM.ID_NOI_QUY where IDSV = " + IDSV + " and VI_PHAM.IDHK = " + idhk + " and VI_PHAM.TRANGTHAI = 4 UNION ALL select VI_PHAM.MA_VI_PHAM,NOI_QUY.TEN_NOI_QUY,VI_PHAM.NGAYTAO,PHAN_HOI.NOIDUNG,N'Phản hồi vi phạm nội quy' as 'hinh_thuc',NOI_QUY.DIEM from VI_PHAM  INNER JOIN NOI_QUY on NOI_QUY.ID_NOI_QUY = VI_PHAM.ID_NOI_QUY INNER JOIN PHAN_HOI on PHAN_HOI.ID_VI_PHAM = VI_PHAM.ID_VI_PHAM where PHAN_HOI.IDSV = " + IDSV + " and VI_PHAM.IDHK = " + idhk + " and VI_PHAM.TRANGTHAI = 4 ", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                VI_PHAM emp = new VI_PHAM();
                emp.MA_VI_PHAM = dr.GetValue(0).ToString();
                emp.TIEUDE = dr.GetValue(1).ToString();
                emp.NGAYTAO = dr.GetValue(2).ToString();
                emp.NOIDUNG = dr.GetValue(3).ToString();
                emp.HINH_THUC = dr.GetValue(4).ToString();
                emp.DIEM = dr.GetValue(5).ToString();
                listHD.Add(emp);
            }
            con.Close();
            return listHD;
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
            SqlCommand cmd2 = new SqlCommand("  select count(*) from VI_PHAM where trangthai = 0 and IDHK = " + idhk + " "+ sql, con);
            cmd2.CommandType = CommandType.Text;
            con.Open();
            Object kq = cmd2.ExecuteScalar();
            int dr = int.Parse(kq.ToString());
            con.Close();
            return dr;
        }
    }
}