using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CNTT129_NetCore.Settings;

namespace CNTT129_NetCore.Models
{
    public class SV_CHUCVU
    {
        public string conf = AppSettings.ConnectionString;

        public int save(string idsv, string idvt)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            string sql = "";
            SqlCommand cmd2 = new SqlCommand("select count(*) from SV_CHUCVU where ID_SV='" + idsv + "' and ID_VAI_TRO_SV = " + idvt + "", con);
            cmd2.CommandType = CommandType.Text;
            Object kq = cmd2.ExecuteScalar();
            if (kq.Equals(0))
            {
                sql = "insert into SV_CHUCVU(ID_SV,ID_VAI_TRO_SV) values(N'" + idsv + "',N'" + idvt + "')";
            }
            else
            {
                sql = "update SV_CHUCVU set disabled = 0  where ID_SV='" + idsv + "' and ID_VAI_TRO_SV = " + idvt + "";
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

        public int updateTT(string idsv, string idvt, string disabled)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            string sql = "";
            sql = "update SV_CHUCVU set disabled = " + disabled + "where ID_SV='" + idsv + "' and ID_VAI_TRO_SV = " + idvt + "";
            if (sql != "")
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteNonQuery();
                con.Close();
            }

            return dr;
        }

        public int updateDiem(string idhk)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            string sql = "";
            sql = "update KETQUA set DIEM += VAI_TRO_SV.DIEM from SV_CHUCVU,VAI_TRO_SV where KETQUA.IDSV = SV_CHUCVU.ID_SV  and SV_CHUCVU.ID_VAI_TRO_SV = VAI_TRO_SV.ID_VAI_TRO_SV and SV_CHUCVU.disabled = 0 and ketqua.idhk = " + idhk;
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