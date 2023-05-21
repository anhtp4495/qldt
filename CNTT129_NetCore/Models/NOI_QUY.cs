using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CNTT129_NetCore.Extensions;

namespace CNTT129.Models
{
    public class NOI_QUY
    {
        public string conf = AppSettings.ConnectionString;
        public string ID_NOI_QUY { get; set; }
        public string CODE_NOI_QUY { get; set; }
        public string TEN_NOI_QUY { get; set; }
        public string DIEM { get; set; }
        public int disabled { get; set; }

        public List<NOI_QUY> find(string CODE_NOI_QUY, string TEN_NOI_QUY, string disabled)
        {
            string sql = null;
            if (CODE_NOI_QUY != "" || TEN_NOI_QUY != "" || disabled != "")
            {
                sql += " where ";
            }
            if (CODE_NOI_QUY != "")
            {
                sql += "CODE_NOI_QUY = '" + CODE_NOI_QUY + "' ";
            }
            if (CODE_NOI_QUY != "" && TEN_NOI_QUY != "")
            {
                sql += " and ";
            }
            if (TEN_NOI_QUY != "")
            {
                sql += "TEN_NOI_QUY like '%" + TEN_NOI_QUY + "%' ";
            }
            if (TEN_NOI_QUY != "" && disabled != "" || CODE_NOI_QUY != "" && disabled != "")
            {
                sql += " and ";
            }

            if (disabled != "")
            {
                sql += "disabled = " + disabled + "";
            }
            List<NOI_QUY> listBH = new List<NOI_QUY>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select NOI_QUY.* from NOI_QUY " + sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                NOI_QUY emp = new NOI_QUY();
                emp.ID_NOI_QUY = dr.GetValue(0).ToString();
                emp.CODE_NOI_QUY = dr.GetValue(1).ToString();
                emp.TEN_NOI_QUY = dr.GetValue(2).ToString();
                emp.DIEM = dr.GetValue(3).ToString();
                emp.disabled = int.Parse(dr.GetValue(4).ToString());
                listBH.Add(emp);
            }
            con.Close();
            return listBH;
        }

        public List<NOI_QUY> findByID(int id)
        {
            string sql = null;
            SqlConnection con = new SqlConnection(conf);
            List<NOI_QUY> listBH = new List<NOI_QUY>();
            if (id != 0)
            {
                sql = "where ID_NOI_QUY = " + id + "";
                SqlCommand cmd = new SqlCommand("select NOI_QUY.* from NOI_QUY " + sql, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    NOI_QUY emp = new NOI_QUY();
                    emp.ID_NOI_QUY = dr.GetValue(0).ToString();
                    emp.CODE_NOI_QUY = dr.GetValue(1).ToString();
                    emp.TEN_NOI_QUY = dr.GetValue(2).ToString();
                    emp.DIEM = dr.GetValue(3).ToString();
                    emp.disabled = int.Parse(dr.GetValue(4).ToString());
                    listBH.Add(emp);
                }
                con.Close();
            }
            return listBH;
        }

        public int save(string ma, string ten, string id,string diem)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            string sql = "";
            if (int.Parse(id) == 0)
            {
                SqlCommand cmd2 = new SqlCommand("select count(*) from NOI_QUY where CODE_NOI_QUY='" + ma + "'", con);
                cmd2.CommandType = CommandType.Text;
                Object kq = cmd2.ExecuteScalar();
                if (kq.Equals(0))
                {
                    sql = "insert into NOI_QUY(CODE_NOI_QUY,TEN_NOI_QUY,DIEM) values(N'" + ma + "',N'" + ten + "',"+diem+")";
                }
                else
                {
                    dr = 2;
                }

            }
            else
            {
                sql = "update NOI_QUY set CODE_NOI_QUY='" + ma + "',TEN_NOI_QUY=N'" + ten + "',DIEM = "+diem+" where ID_NOI_QUY =" + id;
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
            sql = "update NOI_QUY set disabled = " + disabled + " where ID_NOI_QUY =" + id;
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