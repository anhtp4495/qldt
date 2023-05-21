using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CNTT129_NetCore.Extensions;

namespace CNTT129.Models
{
    public class VAI_TRO
    {
        public string conf = AppSettings.ConnectionString;
        public string ID_VAI_TRO { get; set; }
        public string CODE_VAI_TRO { get; set; }
        public string TEN_VAI_TRO { get; set; }
        public int disabled { get; set; }

        public List<VAI_TRO> find(string CODE_VAI_TRO, string TEN_VAI_TRO, string disabled)
        {
            string sql = null;
            if (CODE_VAI_TRO != "" || TEN_VAI_TRO != "" || disabled != "")
            {
                sql += " where ";
            }
            if (CODE_VAI_TRO != "")
            {
                sql += "CODE_VAI_TRO = '" + CODE_VAI_TRO + "' ";
            }
            if (CODE_VAI_TRO != "" && TEN_VAI_TRO != "")
            {
                sql += " and ";
            }
            if (TEN_VAI_TRO != "")
            {
                sql += "TEN_VAI_TRO like '%" + TEN_VAI_TRO + "%' ";
            }
            if (TEN_VAI_TRO != "" && disabled != "" || CODE_VAI_TRO != "" && disabled != "")
            {
                sql += " and ";
            }

            if (disabled != "")
            {
                sql += "disabled = " + disabled + "";
            }
            List<VAI_TRO> listBH = new List<VAI_TRO>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select VAI_TRO.* from VAI_TRO " + sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                VAI_TRO emp = new VAI_TRO();
                emp.ID_VAI_TRO = dr.GetValue(0).ToString();
                emp.CODE_VAI_TRO = dr.GetValue(1).ToString();
                emp.TEN_VAI_TRO = dr.GetValue(2).ToString();
                emp.disabled = int.Parse(dr.GetValue(3).ToString());
                listBH.Add(emp);
            }
            con.Close();
            return listBH;
        }

        public List<VAI_TRO> findByID(int id)
        {
            string sql = null;
            SqlConnection con = new SqlConnection(conf);
            List<VAI_TRO> listBH = new List<VAI_TRO>();
            if (id != 0)
            {
                sql = "where ID_VAI_TRO = " + id + "";
                SqlCommand cmd = new SqlCommand("select VAI_TRO.* from VAI_TRO " +sql, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    VAI_TRO emp = new VAI_TRO();
                    emp.ID_VAI_TRO = dr.GetValue(0).ToString();
                    emp.CODE_VAI_TRO = dr.GetValue(1).ToString();
                    emp.TEN_VAI_TRO = dr.GetValue(2).ToString();
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
                SqlCommand cmd2 = new SqlCommand("select count(*) from VAI_TRO where CODE_VAI_TRO='" + ma + "'", con);
                cmd2.CommandType = CommandType.Text;
                Object kq = cmd2.ExecuteScalar();
                if (kq.Equals(0))
                {
                    sql = "insert into VAI_TRO(CODE_VAI_TRO,TEN_VAI_TRO) values(N'" + ma + "',N'" + ten + "')";
                }
                else
                {
                    dr = 2;
                }

            }
            else
            {
                sql = "update VAI_TRO set CODE_VAI_TRO='" + ma + "',TEN_VAI_TRO=N'" + ten + "' where ID_VAI_TRO =" + id;
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
            sql = "update VAI_TRO set disabled = " + disabled + " where ID_VAI_TRO =" + id;
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