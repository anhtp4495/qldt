using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CNTT129_NetCore.Extensions;

namespace CNTT129.Models
{
    public class LOAIQUYENOFGV
    {
        public string conf = AppSettings.ConnectionString;
        public string ID { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }
        public List<LOAIQUYENOFGV> dsDaCoQuyen(string ma, string quyen)
        {
            List<LOAIQUYENOFGV> listBH = new List<LOAIQUYENOFGV>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select LOAI_QUYEN.ID_LOAI_QUYEN,CODE_LOAI_QUYEN,TEN_LOAI_QUYEN from LOAIQUYENOFGV,GIANG_VIEN,LOAI_QUYEN where LOAIQUYENOFGV.ID_GV =  GIANG_VIEN.ID_GV and LOAI_QUYEN.ID_LOAI_QUYEN = LOAIQUYENOFGV.ID_LOAI_QUYEN and GIANG_VIEN.ID_VAI_TRO = '" + quyen + "' and GIANG_VIEN.ID_GV='" + ma + "' and LOAIQUYENOFGV.disabled = 0 and LOAI_QUYEN.ID_LOAI_QUYEN IN (select ID_LOAI_QUYEN from LOAIQUYENOFVAITRO where ID_VAI_TRO='" + quyen + "' and disabled = 0)", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                LOAIQUYENOFGV emp = new LOAIQUYENOFGV();
                emp.ID = dr.GetValue(0).ToString();
                emp.Ma = dr.GetValue(1).ToString();
                emp.Ten = dr.GetValue(2).ToString();
                listBH.Add(emp);
            }
            con.Close();
            return listBH;
        }

        public List<LOAIQUYENOFGV> dsDaCoQuyen2(string ma, string quyen)
        {
            List<LOAIQUYENOFGV> listBH = new List<LOAIQUYENOFGV>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select LOAI_QUYEN.ID_LOAI_QUYEN,CODE_LOAI_QUYEN,TEN_LOAI_QUYEN from LOAIQUYENOFGV,GIANG_VIEN,LOAI_QUYEN where LOAIQUYENOFGV.ID_GV =  GIANG_VIEN.ID_GV and LOAI_QUYEN.ID_LOAI_QUYEN = LOAIQUYENOFGV.ID_LOAI_QUYEN and GIANG_VIEN.ID_GV='" + ma + "' and LOAIQUYENOFGV.disabled = 0 and LOAI_QUYEN.ID_LOAI_QUYEN NOT IN (select ID_LOAI_QUYEN from LOAIQUYENOFVAITRO where ID_VAI_TRO='" + quyen + "' and disabled = 0)", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                LOAIQUYENOFGV emp = new LOAIQUYENOFGV();
                emp.ID = dr.GetValue(0).ToString();
                emp.Ma = dr.GetValue(1).ToString();
                emp.Ten = dr.GetValue(2).ToString();
                listBH.Add(emp);
            }
            con.Close();
            return listBH;
        }

        public List<LOAIQUYENOFGV> dsChuaCoQuyen(string ma)
        {
            List<LOAIQUYENOFGV> listBH = new List<LOAIQUYENOFGV>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select LOAI_QUYEN.ID_LOAI_QUYEN,CODE_LOAI_QUYEN,TEN_LOAI_QUYEN from LOAIQUYENOFGV,GIANG_VIEN,LOAI_QUYEN where LOAIQUYENOFGV.ID_GV =  GIANG_VIEN.ID_GV and LOAI_QUYEN.ID_LOAI_QUYEN = LOAIQUYENOFGV.ID_LOAI_QUYEN and GIANG_VIEN.ID_GV='" + ma + "' and LOAIQUYENOFGV.disabled = 1", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                LOAIQUYENOFGV emp = new LOAIQUYENOFGV();
                emp.ID = dr.GetValue(0).ToString();
                emp.Ma = dr.GetValue(1).ToString();
                emp.Ten = dr.GetValue(2).ToString();
                listBH.Add(emp);
            }
            con.Close();
            return listBH;
        }

        public List<LOAIQUYENOFGV> dsChuaCoQuyen2(string ma)
        {
            List<LOAIQUYENOFGV> listBH = new List<LOAIQUYENOFGV>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select ID_LOAI_QUYEN,CODE_LOAI_QUYEN,TEN_LOAI_QUYEN from LOAI_QUYEN where ID_LOAI_QUYEN not in (select ID_LOAI_QUYEN from LOAIQUYENOFVAITRO where ID_LOAI_QUYEN='" + ma + "')", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                LOAIQUYENOFGV emp = new LOAIQUYENOFGV();
                emp.ID = dr.GetValue(0).ToString();
                emp.Ma = dr.GetValue(1).ToString();
                emp.Ten = dr.GetValue(2).ToString();
                listBH.Add(emp);
            }
            con.Close();
            return listBH;
        }

        public int them(int maloaiquyen, string manv)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into LOAIQUYENOFGV(ID_GV,ID_LOAI_QUYEN) values(N'" + manv + "','" + maloaiquyen + "')", con);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteNonQuery();
            con.Close();
            return dr;
        }
        public int update(int maloaiquyen, string manv)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            SqlCommand cmd = new SqlCommand("update LOAIQUYENOFGV set disabled = 0 where IG_GV='" + manv + "' and ID_LOAI_QUYEN ='" + maloaiquyen + "'", con);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteNonQuery();
            con.Close();
            return dr;
        }
        public int update2(int maloaiquyen, string manv)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            SqlCommand cmd = new SqlCommand("update LOAIQUYENOFGV set disabled = 1 where IG_GV='" + manv + "' and ID_LOAI_QUYEN ='" + maloaiquyen + "'", con);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteNonQuery();
            con.Close();
            return dr;
        }

        public int getDataQuyenGV(string id_gv, string ma_loaiquyen)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd2 = new SqlCommand("select count(*) from LOAIQUYENOFGV,LOAI_QUYEN where id_gv='" + id_gv + "' and CODE_LOAI_QUYEN='" + ma_loaiquyen + "' and LOAIQUYENOFGV.ID_LOAI_QUYEN = LOAI_QUYEN.ID_LOAI_QUYEN", con);
            cmd2.CommandType = CommandType.Text;
            con.Open();
            Object kq = cmd2.ExecuteScalar();

            if (kq.Equals(0))
            {
                dr = 0;
            }
            else
            {
                dr = 1;
            }
            con.Close();
            return dr;
        }
    }
}