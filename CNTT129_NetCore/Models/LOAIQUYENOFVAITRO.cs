using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CNTT129_NetCore.Settings;

namespace CNTT129_NetCore.Models
{
    public class LOAIQUYENOFVAITRO
    {
        public string conf = AppSettings.ConnectionString;
        public string ID { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }
        public List<LOAIQUYENOFVAITRO> getDSDaCo(string ma)
        {
            List<LOAIQUYENOFVAITRO> listBH = new List<LOAIQUYENOFVAITRO>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("Select LOAIQUYENOFVAITRO.ID_LOAI_QUYEN,LOAI_QUYEN.CODE_LOAI_QUYEN,LOAI_QUYEN.TEN_LOAI_QUYEN from LOAI_QUYEN,VAI_TRO,LOAIQUYENOFVAITRO where LOAIQUYENOFVAITRO.ID_LOAI_QUYEN=LOAI_QUYEN.ID_LOAI_QUYEN and VAI_TRO.ID_VAI_TRO = LOAIQUYENOFVAITRO.ID_VAI_TRO and LOAIQUYENOFVAITRO.disabled = 0 and LOAI_QUYEN.disabled = 0 and LOAIQUYENOFVAITRO.ID_VAI_TRO = '" + ma + "'", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                LOAIQUYENOFVAITRO emp = new LOAIQUYENOFVAITRO();
                emp.ID = dr.GetValue(0).ToString();
                emp.Ma = dr.GetValue(1).ToString();
                emp.Ten = dr.GetValue(2).ToString();
                listBH.Add(emp);
            }
            con.Close();
            return listBH;
        }
        public List<LOAIQUYENOFVAITRO> getDSDaXoa(string ma)
        {
            List<LOAIQUYENOFVAITRO> listBH = new List<LOAIQUYENOFVAITRO>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("Select LOAIQUYENOFVAITRO.ID_LOAI_QUYEN,LOAI_QUYEN.CODE_LOAI_QUYEN,LOAI_QUYEN.TEN_LOAI_QUYEN from LOAI_QUYEN,VAI_TRO,LOAIQUYENOFVAITRO where LOAIQUYENOFVAITRO.ID_LOAI_QUYEN=LOAI_QUYEN.ID_LOAI_QUYEN and VAI_TRO.ID_VAI_TRO = LOAIQUYENOFVAITRO.ID_VAI_TRO and LOAIQUYENOFVAITRO.disabled = 1 and LOAI_QUYEN.disabled = 0 and LOAIQUYENOFVAITRO.ID_VAI_TRO = '" + ma + "'", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                LOAIQUYENOFVAITRO emp = new LOAIQUYENOFVAITRO();
                emp.ID = dr.GetValue(0).ToString();
                emp.Ma = dr.GetValue(1).ToString();
                emp.Ten = dr.GetValue(2).ToString();
                listBH.Add(emp);
            }
            con.Close();
            return listBH;
        }
        public List<LOAIQUYENOFVAITRO> getDSChuaCo(string ma)
        {
            List<LOAIQUYENOFVAITRO> listBH = new List<LOAIQUYENOFVAITRO>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select ID_LOAI_QUYEN,CODE_LOAI_QUYEN,TEN_LOAI_QUYEN from LOAI_QUYEN where  ID_LOAI_QUYEN not in (select ID_LOAI_QUYEN from LOAIQUYENOFVAITRO where ID_VAI_TRO='" + ma + "')", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                LOAIQUYENOFVAITRO emp = new LOAIQUYENOFVAITRO();
                emp.ID = dr.GetValue(0).ToString();
                emp.Ma = dr.GetValue(1).ToString();
                emp.Ten = dr.GetValue(2).ToString();
                listBH.Add(emp);
            }
            con.Close();
            return listBH;
        }
        public int them(int maloaiquyen, string maquyen)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into LOAIQUYENOFVAITRO(ID_VAI_TRO,ID_LOAI_QUYEN) values(N'" + maquyen + "','" + maloaiquyen + "')", con);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteNonQuery();
            con.Close();
            con.Open();
            SqlCommand cmd3 = new SqlCommand("select ID_GV from GIANG_VIEN where ID_VAI_TRO = '" + maquyen + "'", con);
            cmd3.CommandType = CommandType.Text;
            SqlDataReader dr2 = cmd3.ExecuteReader();

            List<String> list = new List<String>();
            while (dr2.Read())
            {
                list.Add(dr2.GetValue(0).ToString());
            }
            con.Close();
            foreach (var item in list)
            {
                con.Open();
                SqlCommand cmd2 = new SqlCommand("insert into LOAIQUYENOFGV(ID_GV,ID_LOAI_QUYEN) values(N'" + item + "','" + maloaiquyen + "')", con);
                cmd2.CommandType = CommandType.Text;
                dr = cmd2.ExecuteNonQuery();
                con.Close();
            }
            return dr;
        }
        public int update(int maloaiquyen, string maquyen)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            SqlCommand cmd = new SqlCommand("update LOAIQUYENOFVAITRO set disabled = 0 where ID_VAI_TRO='" + maquyen + "' and ID_LOAI_QUYEN ='" + maloaiquyen + "'", con);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteNonQuery();
            con.Close();
            con.Open();
            SqlCommand cmd2 = new SqlCommand("update LOAIQUYENOFGV set LOAIQUYENOFGV.disabled = 0  from GIANG_VIEN where  ID_LOAI_QUYEN='" + maquyen + "' and LOAIQUYENOFGV.ID_LOAI_QUYEN ='" + maloaiquyen + "' and LOAIQUYENOFGV.ID_GV=GIANG_VIEN.ID_GV", con);
            cmd2.CommandType = CommandType.Text;
            dr = cmd2.ExecuteNonQuery();
            con.Close();
            return dr;
        }
        public int update2(int maloaiquyen, string maquyen)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            SqlCommand cmd = new SqlCommand("update LOAIQUYENOFVAITRO set disabled = 1 where ID_VAI_TRO='" + maquyen + "' and ID_LOAI_QUYEN ='" + maloaiquyen + "'", con);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteNonQuery();
            con.Close();
            con.Open();
            SqlCommand cmd2 = new SqlCommand("update LOAIQUYENOFGV set LOAIQUYENOFGV.disabled = 1  from GIANG_VIEN where  ID_LOAI_QUYEN='" + maquyen + "' and LOAIQUYENOFGV.ID_LOAI_QUYEN ='" + maloaiquyen + "' and LOAIQUYENOFGV.ID_GV=GIANG_VIEN.ID_GV", con);
            cmd2.CommandType = CommandType.Text;
            dr = cmd2.ExecuteNonQuery();
            con.Close();
            return dr;
        }
    }
}