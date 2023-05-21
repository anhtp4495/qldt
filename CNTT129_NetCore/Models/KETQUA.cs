using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CNTT129_NetCore.Extensions;

namespace CNTT129.Models
{
    public class KETQUA
    {
        public string conf = AppSettings.ConnectionString;
        public string ID_KQ { get; set; }
        public string IDSV { get; set; }
        public string IDHK { get; set; }
        public string DIEM { get; set; }
        public string LOAI { get; set; }

        public int ketQuaSV(string idsv, string idhk)
        {
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd2 = new SqlCommand("select IIF(SUM(DIEM+DIEMMD) IS NULL, (SELECT DIEMMD FROM HOC_KI where ID_HK = " + idhk + "), SUM(DIEM+DIEMMD)) from KETQUA where IDSV = " + idsv + " and IDHK = " + idhk + "", con);
            cmd2.CommandType = CommandType.Text;
            con.Open();
            Object kq = cmd2.ExecuteScalar();
            int dr = kq == null ? 0 : int.Parse(kq.ToString());
            con.Close();
            return dr;
        }

        public int ketQuaSV(string idhk)
        {
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd2 = new SqlCommand("select Top 1 DIEMMD from KETQUA where IDHK = " + idhk + "", con);
            cmd2.CommandType = CommandType.Text;
            con.Open();
            Object kq = cmd2.ExecuteScalar();
            int dr = kq == null ? 0 : int.Parse(kq.ToString());
            con.Close();
            return dr;
        }
    }
}