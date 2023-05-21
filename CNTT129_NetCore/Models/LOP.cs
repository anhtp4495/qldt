using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CNTT129_NetCore.Extensions;

namespace CNTT129.Models
{
    public class LOP
    {
        public string conf = AppSettings.ConnectionString;
        public string ID_LOP { get; set; }
        public string CODE_LOP { get; set; }
        public string TEN_LOP { get; set; }
        public int TrangThai { get; set; }


        public List<LOP> find()
        {

            List<LOP> listHK = new List<LOP>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select LOP.* from LOP where disabled = 0", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                LOP emp = new LOP();
                emp.ID_LOP = dr.GetValue(0).ToString();
                emp.CODE_LOP = dr.GetValue(1).ToString();
                emp.TEN_LOP = dr.GetValue(2).ToString();
                listHK.Add(emp);
            }
            con.Close();
            return listHK;
        }
    }
}