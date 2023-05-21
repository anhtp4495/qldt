using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CNTT129_NetCore.Extensions;

namespace CNTT129.Models
{
    public class VAI_TRO_SV
    {
        public string conf = AppSettings.ConnectionString;
        public string ID_VAI_TRO { get; set; }
        public string CODE_VAI_TRO { get; set; }
        public string TEN_VAI_TRO { get; set; }
        public string DIEM { get; set; }
        public int disabled { get; set; }

        public List<VAI_TRO_SV> find(string CODE_VAI_TRO, string TEN_VAI_TRO, string disabled)
        {
            string sql = null;
            if (CODE_VAI_TRO != "" || TEN_VAI_TRO != "" || disabled != "")
            {
                sql += " where ";
            }
            if (CODE_VAI_TRO != "")
            {
                sql += "CODE_VAI_TRO_SV = '" + CODE_VAI_TRO + "' ";
            }
            if (CODE_VAI_TRO != "" && TEN_VAI_TRO != "")
            {
                sql += " and ";
            }
            if (TEN_VAI_TRO != "")
            {
                sql += "TEN_VAI_TRO_SV like '%" + TEN_VAI_TRO + "%' ";
            }
            if (TEN_VAI_TRO != "" && disabled != "" || CODE_VAI_TRO != "" && disabled != "")
            {
                sql += " and ";
            }

            if (disabled != "")
            {
                sql += "disabled = " + disabled + "";
            }
            List<VAI_TRO_SV> listBH = new List<VAI_TRO_SV>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select VAI_TRO_SV.* from VAI_TRO_SV " + sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                VAI_TRO_SV emp = new VAI_TRO_SV();
                emp.ID_VAI_TRO = dr.GetValue(0).ToString();
                emp.CODE_VAI_TRO = dr.GetValue(1).ToString();
                emp.TEN_VAI_TRO = dr.GetValue(2).ToString();
                emp.DIEM = dr.GetValue(3).ToString();
                emp.disabled = int.Parse(dr.GetValue(4).ToString());
                listBH.Add(emp);
            }
            con.Close();
            return listBH;
        }

        public List<VAI_TRO_SV> findByID(int id)
        {
            string sql = null;
            SqlConnection con = new SqlConnection(conf);
            List<VAI_TRO_SV> listBH = new List<VAI_TRO_SV>();
            if (id != 0)
            {
                sql = "where ID_VAI_TRO_SV = " + id + "";
                SqlCommand cmd = new SqlCommand("select VAI_TRO_SV.* from VAI_TRO_SV " + sql, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    VAI_TRO_SV emp = new VAI_TRO_SV();
                    emp.ID_VAI_TRO = dr.GetValue(0).ToString();
                    emp.CODE_VAI_TRO = dr.GetValue(1).ToString();
                    emp.TEN_VAI_TRO = dr.GetValue(2).ToString();
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
                SqlCommand cmd2 = new SqlCommand("select count(*) from VAI_TRO_SV where CODE_VAI_TRO_SV='" + ma + "'", con);
                cmd2.CommandType = CommandType.Text;
                Object kq = cmd2.ExecuteScalar();
                if (kq.Equals(0))
                {
                    sql = "insert into VAI_TRO_SV(CODE_VAI_TRO_SV,TEN_VAI_TRO_SV,DIEM) values(N'" + ma + "',N'" + ten + "'," + diem + ")";
                }
                else
                {
                    dr = 2;
                }

            }
            else
            {
                sql = "update VAI_TRO_SV set CODE_VAI_TRO_SV='" + ma + "',TEN_VAI_TRO_SV=N'" + ten + "',Diem = " + diem + " where ID_VAI_TRO_SV =" + id;
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
            sql = "update VAI_TRO_SV set disabled = " + disabled + " where ID_VAI_TRO_SV =" + id;
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