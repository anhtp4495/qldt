using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CNTT129_NetCore.Settings;

namespace CNTT129_NetCore.Models
{
    public class LOAI_QUYEN
    {
        public string conf = AppSettings.ConnectionString;
        public string ID_LOAI_QUYEN { get; set; }
        public string CODE_LOAI_QUYEN { get; set; }
        public string TEN_LOAI_QUYEN { get; set; }
        public int disabled { get; set; }

        public List<LOAI_QUYEN> find(string CODE_LOAI_QUYEN, string TEN_LOAI_QUYEN, string disabled)
        {
            string sql = null;
            if (CODE_LOAI_QUYEN != "" || TEN_LOAI_QUYEN != "" || disabled != "")
            {
                sql += " where ";
            }
            if (CODE_LOAI_QUYEN != "")
            {
                sql += "CODE_LOAI_QUYEN = '" + CODE_LOAI_QUYEN + "' ";
            }
            if (CODE_LOAI_QUYEN != "" && TEN_LOAI_QUYEN != "")
            {
                sql += " and ";
            }
            if (TEN_LOAI_QUYEN != "")
            {
                sql += "TEN_LOAI_QUYEN like '%" + TEN_LOAI_QUYEN + "%' ";
            }
            if (TEN_LOAI_QUYEN != "" && disabled != "" || CODE_LOAI_QUYEN != "" && disabled != "")
            {
                sql += " and ";
            }

            if (disabled != "")
            {
                sql += "disabled = " + disabled + "";
            }
            List<LOAI_QUYEN> listBH = new List<LOAI_QUYEN>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select LOAI_QUYEN.* from LOAI_QUYEN" + sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                LOAI_QUYEN emp = new LOAI_QUYEN();
                emp.ID_LOAI_QUYEN = dr.GetValue(0).ToString();
                emp.CODE_LOAI_QUYEN = dr.GetValue(1).ToString();
                emp.TEN_LOAI_QUYEN = dr.GetValue(2).ToString();
                emp.disabled = int.Parse(dr.GetValue(3).ToString());
                listBH.Add(emp);
            }
            con.Close();
            return listBH;
        }

        public List<LOAI_QUYEN> findByID(int id)
        {
            string sql = null;
            SqlConnection con = new SqlConnection(conf);
            List<LOAI_QUYEN> listBH = new List<LOAI_QUYEN>();
            if (id != 0)
            {
                sql = "where ID_LOAI_QUYEN = " + id + "";
                SqlCommand cmd = new SqlCommand("select LOAI_QUYEN.* from LOAI_QUYEN " + sql, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    LOAI_QUYEN emp = new LOAI_QUYEN();
                    emp.ID_LOAI_QUYEN = dr.GetValue(0).ToString();
                    emp.CODE_LOAI_QUYEN = dr.GetValue(1).ToString();
                    emp.TEN_LOAI_QUYEN = dr.GetValue(2).ToString();
                    emp.disabled = int.Parse(dr.GetValue(3).ToString());
                    listBH.Add(emp);
                }
                con.Close();
            }
            return listBH;
        }

        public int save(string ma, string ten, string id)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            string sql = "";
            if (int.Parse(id) == 0)
            {
                SqlCommand cmd2 = new SqlCommand("select count(*) from LOAI_QUYEN where CODE_LOAI_QUYEN='" + ma + "'", con);
                cmd2.CommandType = CommandType.Text;
                Object kq = cmd2.ExecuteScalar();
                if (kq.Equals(0))
                {
                    sql = "insert into LOAI_QUYEN(CODE_LOAI_QUYEN,TEN_LOAI_QUYEN) values(N'" + ma + "',N'" + ten + "')";
                }
                else
                {
                    dr = 2;
                }

            }
            else
            {
                sql = "update LOAI_QUYEN set CODE_LOAI_QUYEN='" + ma + "',TEN_LOAI_QUYEN=N'" + ten + "' where ID_LOAI_QUYEN =" + id;
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
            sql = "update LOAI_QUYEN set disabled = " + disabled + " where ID_LOAI_QUYEN =" + id;
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