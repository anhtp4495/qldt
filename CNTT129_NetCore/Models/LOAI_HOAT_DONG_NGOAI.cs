using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CNTT129_NetCore.Extensions;

namespace CNTT129.Models
{
    public class LOAI_HOAT_DONG_NGOAI
    {
        public string conf = AppSettings.ConnectionString;
        public string ID_HD_NGOAI { get; set; }
        public string CODE_HD_NGOAI { get; set; }
        public string TEN_HD_NGOAI { get; set; }
        public string DIEM { get; set; }
        public int disabled { get; set; }

        public List<LOAI_HOAT_DONG_NGOAI> find(string CODE_HD_NGOAI, string TEN_HD_NGOAI, string disabled)
        {
            string sql = null;
            if (CODE_HD_NGOAI != "" || TEN_HD_NGOAI != "" || disabled != "")
            {
                sql += " where ";
            }
            if (CODE_HD_NGOAI != "")
            {
                sql += "CODE_HD_NGOAI = '" + CODE_HD_NGOAI + "' ";
            }
            if (CODE_HD_NGOAI != "" && TEN_HD_NGOAI != "")
            {
                sql += " and ";
            }
            if (TEN_HD_NGOAI != "")
            {
                sql += "TEN_HD_NGOAI like '%" + TEN_HD_NGOAI + "%' ";
            }
            if (CODE_HD_NGOAI != "" && disabled != "" || CODE_HD_NGOAI != "" && disabled != "")
            {
                sql += " and ";
            }

            if (disabled != "")
            {
                sql += "disabled = " + disabled + "";
            }
            List<LOAI_HOAT_DONG_NGOAI> listBH = new List<LOAI_HOAT_DONG_NGOAI>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select LOAI_HOAT_DONG_NGOAI.* from LOAI_HOAT_DONG_NGOAI " + sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                LOAI_HOAT_DONG_NGOAI emp = new LOAI_HOAT_DONG_NGOAI();
                emp.ID_HD_NGOAI = dr.GetValue(0).ToString();
                emp.CODE_HD_NGOAI = dr.GetValue(1).ToString();
                emp.TEN_HD_NGOAI = dr.GetValue(2).ToString();
                emp.DIEM = dr.GetValue(3).ToString();
                emp.disabled = int.Parse(dr.GetValue(4).ToString());
                listBH.Add(emp);
            }
            con.Close();
            return listBH;
        }

        public List<LOAI_HOAT_DONG_NGOAI> findByID(int id)
        {
            string sql = null;
            SqlConnection con = new SqlConnection(conf);
            List<LOAI_HOAT_DONG_NGOAI> listBH = new List<LOAI_HOAT_DONG_NGOAI>();
            if (id != 0)
            {
                sql = "where ID_HD_NGOAI = " + id + "";
                SqlCommand cmd = new SqlCommand("select LOAI_HOAT_DONG_NGOAI.* from LOAI_HOAT_DONG_NGOAI " + sql, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    LOAI_HOAT_DONG_NGOAI emp = new LOAI_HOAT_DONG_NGOAI();
                    emp.ID_HD_NGOAI = dr.GetValue(0).ToString();
                    emp.CODE_HD_NGOAI = dr.GetValue(1).ToString();
                    emp.TEN_HD_NGOAI = dr.GetValue(2).ToString();
                    emp.DIEM = dr.GetValue(3).ToString();
                    emp.disabled = int.Parse(dr.GetValue(4).ToString());
                    listBH.Add(emp);
                }
                con.Close();
            }
            return listBH;
        }

        public int save(string ma, string ten, string id, string diem)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            string sql = "";
            if (int.Parse(id) == 0)
            {
                SqlCommand cmd2 = new SqlCommand("select count(*) from LOAI_HOAT_DONG_NGOAI where CODE_HD_NGOAI='" + ma + "'", con);
                cmd2.CommandType = CommandType.Text;
                Object kq = cmd2.ExecuteScalar();
                if (kq.Equals(0))
                {
                    sql = "insert into LOAI_HOAT_DONG_NGOAI(CODE_HD_NGOAI,TEN_HD_NGOAI,DIEM) values(N'" + ma + "',N'" + ten + "'," + diem + ")";
                }
                else
                {
                    dr = 2;
                }

            }
            else
            {
                sql = "update LOAI_HOAT_DONG_NGOAI set CODE_HD_NGOAI='" + ma + "',TEN_HD_NGOAI=N'" + ten + "',Diem = " + diem + " where ID_HD_NGOAI =" + id;
            }
            if (sql != "")
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteNonQuery();
                con.Close();
            }

            return dr;
        }

        public int updateTT(int id, int disabled)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            string sql = "";
            sql = "update LOAI_HOAT_DONG_NGOAI set disabled = " + disabled + " where ID_HD_NGOAI =" + id;
            if (sql != "")
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteNonQuery();
                con.Close();
            }

            return dr;
        }

        
    }
}