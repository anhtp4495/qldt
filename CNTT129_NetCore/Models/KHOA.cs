using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CNTT129_NetCore.Extensions;

namespace CNTT129.Models
{
    public class KHOA
    {
        public string conf = AppSettings.ConnectionString;
        public string ID_KHOA { get; set; }
        public string MA_KHOA { get; set; }
        public string TEN_KHOA { get; set; }
        public int disabled { get; set; }

        public List<KHOA> find(string MA_KHOA, string TEN_KHOA, string disabled)
        {
            string sql = null;
            if (MA_KHOA != "" || TEN_KHOA != "" || disabled != "")
            {
                sql += " where ";
            }
            if (MA_KHOA != "")
            {
                sql += "MA_KHOA = '" + MA_KHOA + "' ";
            }
            if (MA_KHOA != "" && TEN_KHOA != "")
            {
                sql += " and ";
            }
            if (TEN_KHOA != "")
            {
                sql += "TEN_KHOA like '%" + TEN_KHOA + "%' ";
            }
            if (TEN_KHOA != "" && disabled != "" || MA_KHOA != "" && disabled != "")
            {
                sql += " and ";
            }

            if (disabled != "")
            {
                sql += "disabled = " + disabled + "";
            }
            List<KHOA> listKhoa = new List<KHOA>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select KHOA.* from KHOA " + sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                KHOA emp = new KHOA();
                emp.ID_KHOA = dr.GetValue(0).ToString();
                emp.MA_KHOA = dr.GetValue(1).ToString();
                emp.TEN_KHOA = dr.GetValue(2).ToString();
                emp.disabled = int.Parse(dr.GetValue(3).ToString());
                listKhoa.Add(emp);
            }
            con.Close();
            return listKhoa;
        }

        public List<KHOA> findByID(int id)
        {
            string sql = null;
            SqlConnection con = new SqlConnection(conf);
            List<KHOA> listKhoa = new List<KHOA>();
            if (id != 0)
            {
                sql = "where ID_KHOA = " + id + "";
                SqlCommand cmd = new SqlCommand("select KHOA.* from KHOA " + sql, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    KHOA emp = new KHOA();
                    emp.ID_KHOA = dr.GetValue(0).ToString();
                    emp.MA_KHOA = dr.GetValue(1).ToString();
                    emp.TEN_KHOA = dr.GetValue(2).ToString();
                    emp.disabled = int.Parse(dr.GetValue(3).ToString());
                    listKhoa.Add(emp);
                }
                con.Close();
            }
            return listKhoa;
        }

        public int updateTT(int id, int disabled)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            string sql = "";
            sql = "update KHOA set disabled = " + disabled + " where ID_KHOA =" + id;
            if (sql != "")
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteNonQuery();
                con.Close();
            }

            return dr;
        }

        public int save(string ma, string ten, string id)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            string sql = "";
            if (int.Parse(id) == 0)
            {
                SqlCommand cmd2 = new SqlCommand("select count(*) from KHOA where MA_KHOA='" + ma + "'", con);
                cmd2.CommandType = CommandType.Text;
                Object kq = cmd2.ExecuteScalar();
                if (kq.Equals(0))
                {
                    sql = "insert into KHOA(MA_KHOA,TEN_KHOA) values(N'" + ma + "',N'" + ten + "')";
                }
                else
                {
                    dr = 2;
                }

            }
            else
            {
                sql = "update KHOA set MA_KHOA='" + ma + "',TEN_KHOA=N'" + ten + "' where ID_KHOA =" + id;
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
    }
}