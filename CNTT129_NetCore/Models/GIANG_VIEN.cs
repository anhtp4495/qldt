using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CNTT129_NetCore.Settings;

namespace CNTT129_NetCore.Models
{
    public class GIANG_VIEN
    {
        public string conf = AppSettings.ConnectionString;
        public string ID_GV { get; set; }
        public string MAGV { get; set; }
        public string TENGV { get; set; }
        public string NGAYSINH { get; set; }
        public string MK { get; set; }
        public string dc { get; set; }
        public string SDT { get; set; }
        public string email { get; set; }
        public string idkhoa { get; set; }
        public string idvaitro { get; set; }
        public int disabled { get; set; }

        public string vai_tro_name { get; set; }

        public List<GIANG_VIEN> login(string user, string password)
        {
            SqlConnection con = new SqlConnection(conf);
            var sql = "select GIANG_VIEN.*,TEN_VAI_TRO from GIANG_VIEN,VAI_TRO where magv='" + user + "' and matkhau='" + password + "' and GIANG_VIEN.disabled = 0 and GIANG_VIEN.ID_VAI_TRO = VAI_TRO.ID_VAI_TRO and VAI_TRO.disabled = 0";
            if (user == "admin")
            {
                sql = "select GIANG_VIEN.*,'Administrator' as TEN_VAI_TRO from GIANG_VIEN where magv='" + user + "' and matkhau='" + password + "'";
            }
            SqlCommand cmd2 = new SqlCommand(sql, con);
            cmd2.CommandType = CommandType.Text;
            con.Open();
            List<GIANG_VIEN> listGV = new List<GIANG_VIEN>();
            try
            {
                SqlDataReader dr = cmd2.ExecuteReader();
                while (dr.Read())
                {
                    GIANG_VIEN emp = new GIANG_VIEN();
                    emp.ID_GV = dr.GetValue(0).ToString();
                    emp.MAGV = dr.GetValue(1).ToString();
                    emp.TENGV = dr.GetValue(2).ToString();
                    emp.dc = dr.GetValue(3).ToString();
                    emp.email = dr.GetValue(4).ToString();
                    emp.SDT = dr.GetValue(5).ToString();
                    emp.NGAYSINH = dr.GetValue(6).ToString();
                    emp.MK = dr.GetValue(8).ToString();
                    emp.idkhoa = dr.GetValue(9).ToString();
                    emp.idvaitro = dr.GetValue(10).ToString();
                    emp.disabled = int.Parse(dr.GetValue(7).ToString());
                    emp.vai_tro_name = dr.GetValue(11).ToString();
                    listGV.Add(emp);
                }
            }
            catch(Exception e)
            { }
            return listGV;
        }

        public List<GIANG_VIEN> find(string CODE_GV, string TEN_GV, string ID_KHOA, string ID_VAI_TRO, string disabled)
        {
            string sql = null;
            if (CODE_GV != "" || TEN_GV != "" || disabled != "" || int.Parse(ID_VAI_TRO) != 0 || int.Parse(ID_KHOA) != 0)
            {
                sql += " where ";
            }
            if (CODE_GV != "")
            {
                sql += "MAGV = '" + CODE_GV + "' ";
            }
            if (CODE_GV != "" && TEN_GV != "")
            {
                sql += " and ";
            }
            if (TEN_GV != "")
            {
                sql += "TENGV like '%" + TEN_GV + "%' ";
            }
            if (TEN_GV != "" && disabled != "" || CODE_GV != "" && disabled != "")
            {
                sql += " and ";
            }

            if (disabled != "")
            {
                sql += "disabled = " + disabled + "";
            }

            if ((TEN_GV != "" || CODE_GV != "" || disabled != "") && int.Parse(ID_KHOA) != 0)
            {
                sql += " and ";
            }

            if (int.Parse(ID_KHOA) != 0)
            {
                sql += "ID_KHOA = " + ID_KHOA + "";
            }

            if ((TEN_GV != "" || disabled != "" || CODE_GV != "" || disabled != "" || int.Parse(ID_KHOA) != 0) && int.Parse(ID_VAI_TRO) != 0)
            {
                sql += " and ";
            }

            if (int.Parse(ID_VAI_TRO) != 0)
            {
                sql += "ID_VAI_TRO = " + ID_VAI_TRO + "";
            }

            List<GIANG_VIEN> listGV = new List<GIANG_VIEN>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select GIANG_VIEN.* from GIANG_VIEN " + sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                GIANG_VIEN emp = new GIANG_VIEN();
                emp.ID_GV = dr.GetValue(0).ToString();
                emp.MAGV = dr.GetValue(1).ToString();
                emp.TENGV = dr.GetValue(2).ToString();
                emp.dc = dr.GetValue(3).ToString();
                emp.email = dr.GetValue(4).ToString();
                emp.SDT = dr.GetValue(5).ToString();
                emp.NGAYSINH = dr.GetValue(6).ToString();
                emp.MK = dr.GetValue(8).ToString();
                emp.idkhoa = dr.GetValue(9).ToString();
                emp.idvaitro = dr.GetValue(10).ToString();
                emp.disabled = int.Parse(dr.GetValue(7).ToString());
                listGV.Add(emp);
            }
            con.Close();
            return listGV;
        }

        public List<GIANG_VIEN> findByID(int id)
        {
            string sql = null;
            SqlConnection con = new SqlConnection(conf);
            List<GIANG_VIEN> listGV = new List<GIANG_VIEN>();
            if (id != 0)
            {
                sql = "where ID_GV = '" + id + "'";
                SqlCommand cmd = new SqlCommand("select GIANG_VIEN.* from GIANG_VIEN " + sql, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    GIANG_VIEN emp = new GIANG_VIEN();
                    emp.ID_GV = dr.GetValue(0).ToString();
                    emp.MAGV = dr.GetValue(1).ToString();
                    emp.TENGV = dr.GetValue(2).ToString();
                    emp.dc = dr.GetValue(3).ToString();
                    emp.email = dr.GetValue(4).ToString();
                    emp.SDT = dr.GetValue(5).ToString();
                    emp.NGAYSINH = dr.GetValue(6).ToString();
                    emp.MK = dr.GetValue(8).ToString();
                    emp.idkhoa = dr.GetValue(9).ToString();
                    emp.idvaitro = dr.GetValue(10).ToString();
                    emp.disabled = int.Parse(dr.GetValue(7).ToString());
                    listGV.Add(emp);
                }
                con.Close();
            }
            return listGV;
        }

        public int save(string id, string ma, string ten, string sdt, string dc, string mk, string khoa, string email, string vai_tro, string ngaysinh)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            if (id == "0")
            {
                SqlCommand cmd2 = new SqlCommand("select count(*) from GIANG_VIEN", con);
                cmd2.CommandType = CommandType.Text;
                con.Open();
                Object kq = cmd2.ExecuteScalar();

                int number = 1;
                string number2 = "";

                if (!kq.Equals(0))
                {
                    number = int.Parse(kq.ToString()) + number;
                }
                int codeLength = 7;
                int lenOfNum = number.ToString().Length;
                if (lenOfNum < codeLength)
                {
                    int loopNum = codeLength - lenOfNum;
                    for (int i = 1; i <= loopNum; i++)
                    {
                        number2 = '0' + number.ToString();
                    }
                }
                ma = "GV00" + number2;
                SqlCommand cmd = new SqlCommand("insert into GIANG_VIEN(MAGV,TENGV,SDT,DC,MATKHAU,ID_KHOA,EMAIL,ID_VAI_TRO,NGAYSINH) values(N'" + ma + "',N'" + ten + "','" + sdt + "',N'" + dc + "','" + mk + "','" + khoa + "','" + email + "','" + vai_tro + "','" + ngaysinh + "')", con);
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update GIANG_VIEN set TENGV= N'" + ten + "',SDT = '" + sdt + "',dc = N'" + dc + "',matkhau = N'" + mk + "',email = '" + email + "',ID_KHOA = '" + khoa + "',ID_VAI_TRO = '" + vai_tro + "',NGAYSINH = '" + ngaysinh + "' where MAGV='" + ma + "'", con);
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteNonQuery();
                con.Close();

                con.Open();
                SqlCommand cmd3 = new SqlCommand("delete from LOAIQUYENOFGV where ID_GV='" + id + "'", con);
                cmd3.CommandType = CommandType.Text;
                dr = cmd3.ExecuteNonQuery();
                con.Close();
            }
            if (id == "0")
            {
                SqlCommand cmd2 = new SqlCommand("select top 1 ID_GV from GIANG_VIEN order by ID_GV DESC", con);
                cmd2.CommandType = CommandType.Text;
                con.Open();
                Object kq = cmd2.ExecuteScalar();
                con.Close();
                LOAIQUYENOFVAITRO vaitro = new LOAIQUYENOFVAITRO();
                var menu = vaitro.getDSDaCo(vai_tro);
                foreach (var item in menu)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into LOAIQUYENOFGV(ID_GV,ID_LOAI_QUYEN) values(N'" + kq + "',N'" + item.ID + "')", con);
                    cmd.CommandType = CommandType.Text;
                    dr = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            else
            {
                LOAIQUYENOFVAITRO vaitro = new LOAIQUYENOFVAITRO();
                var menu = vaitro.getDSDaCo(vai_tro);
                foreach (var item in menu)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into LOAIQUYENOFGV(ID_GV,ID_LOAI_QUYEN) values(N'" + id + "',N'" + item.ID + "')", con);
                    cmd.CommandType = CommandType.Text;
                    dr = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return dr;
        }
        public int updateTT(int id, int disabled)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            string sql = "";
            sql = "update GIANG_VIEN set disabled = " + disabled + " where ID_GV =" + id;
            if (sql != "")
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteNonQuery();
            }
            con.Close();
            return dr;
        }
    }
}