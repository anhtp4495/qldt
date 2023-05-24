using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CNTT129_NetCore.Settings;

namespace CNTT129_NetCore.Models
{
    public class SINHVIEN
    {
        public string conf = AppSettings.ConnectionString;
        public string ID_SV { get; set; }
        public string MASV { get; set; }
        public string TENSV { get; set; }
        public string NGAYSINH { get; set; }
        public string MK { get; set; }
        public string dc { get; set; }
        public string SDT { get; set; }
        public string email { get; set; }
        public string idkhoa { get; set; }
        public int disabled { get; set; }

        public string TEN_KHOA { get; set; }
        public string NOISINH { get; set; }
        public string GIOITINH { get; set; }
        public string CODE_LOP { get; set; }
        public string ID_LOP { get; set; }
        public string BAC_DAO_TAO { get; set; }

        public string HINHANH { get; set; }

        public List<SINHVIEN> login(string user, string password)
        {
            SqlConnection con = new SqlConnection(conf);
            var sql = "select SINHVIEN.* from SINHVIEN where MASV='" + user + "' and matkhau='" + password + "' and disabled = 0";
            SqlCommand cmd2 = new SqlCommand(sql, con);
            cmd2.CommandType = CommandType.Text;
            con.Open();
            List<SINHVIEN> listSV = new List<SINHVIEN>();
            SqlDataReader dr = cmd2.ExecuteReader();
            while (dr.Read())
            {
                SINHVIEN emp = new SINHVIEN();
                emp.ID_SV = dr.GetValue(0).ToString();
                emp.MASV = dr.GetValue(1).ToString();
                emp.TENSV = dr.GetValue(2).ToString();
                emp.disabled = int.Parse(dr.GetValue(7).ToString());
                listSV.Add(emp);
            }
            return listSV;
        }

        public List<SINHVIEN> find(string CODE_SV, string TEN_SV, string ID_KHOA, string disabled)
        {
            string sql = null;
            if (CODE_SV != "" || TEN_SV != "" || disabled != "" || int.Parse(ID_KHOA) != 0)
            {
                sql += " where ";
            }
            if (CODE_SV != "")
            {
                sql += "MASV = '" + CODE_SV + "' ";
            }
            if (CODE_SV != "" && TEN_SV != "")
            {
                sql += " and ";
            }
            if (TEN_SV != "")
            {
                sql += "TENSV like '%" + TEN_SV + "%' ";
            }
            if (TEN_SV != "" && disabled != "" || CODE_SV != "" && disabled != "")
            {
                sql += " and ";
            }

            if (disabled != "")
            {
                sql += "disabled = " + disabled + "";
            }

            if ((TEN_SV != "" || CODE_SV != "" || disabled != "") && int.Parse(ID_KHOA) != 0)
            {
                sql += " and ";
            }

            if (int.Parse(ID_KHOA) != 0)
            {
                sql += "ID_KHOA = " + ID_KHOA + "";
            }


            List<SINHVIEN> listGV = new List<SINHVIEN>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select SINHVIEN.* from SINHVIEN " + sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                SINHVIEN emp = new SINHVIEN();
                emp.ID_SV = dr.GetValue(0).ToString();
                emp.MASV = dr.GetValue(1).ToString();
                emp.TENSV = dr.GetValue(2).ToString();
                emp.dc = dr.GetValue(4).ToString();
                emp.email = dr.GetValue(6).ToString();
                emp.SDT = dr.GetValue(5).ToString();
                emp.NGAYSINH = dr.GetValue(3).ToString();
                emp.MK = dr.GetValue(7).ToString();
                emp.idkhoa = dr.GetValue(8).ToString();
                emp.disabled = int.Parse(dr.GetValue(9).ToString());
                listGV.Add(emp);
            }
            con.Close();
            return listGV;
        }

        public List<SINHVIEN> find(string disabled)
        {
            string sql = null;
            if (disabled != "")
            {
                sql += " where ";
            }

            if (disabled != "")
            {
                sql += "disabled = " + disabled + "";
            }


            List<SINHVIEN> listGV = new List<SINHVIEN>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select SINHVIEN.* from SINHVIEN " + sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                SINHVIEN emp = new SINHVIEN();
                emp.ID_SV = dr.GetValue(0).ToString();
                emp.MASV = dr.GetValue(1).ToString();
                emp.TENSV = dr.GetValue(2).ToString();
                emp.dc = dr.GetValue(4).ToString();
                emp.email = dr.GetValue(6).ToString();
                emp.SDT = dr.GetValue(5).ToString();
                emp.NGAYSINH = dr.GetValue(3).ToString();
                emp.MK = dr.GetValue(7).ToString();
                emp.idkhoa = dr.GetValue(8).ToString();
                emp.disabled = int.Parse(dr.GetValue(9).ToString());
                listGV.Add(emp);
            }
            con.Close();
            return listGV;
        }

        public List<SINHVIEN> findByID(int id)
        {
            string sql = null;
            SqlConnection con = new SqlConnection(conf);
            List<SINHVIEN> listGV = new List<SINHVIEN>();
            if (id != 0)
            {
                sql = "where ID_SV = '" + id + "'";
                SqlCommand cmd = new SqlCommand("select SINHVIEN.* from SINHVIEN " + sql, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SINHVIEN emp = new SINHVIEN();
                    emp.ID_SV = dr.GetValue(0).ToString();
                    emp.MASV = dr.GetValue(1).ToString();
                    emp.TENSV = dr.GetValue(2).ToString();
                    emp.dc = dr.GetValue(4).ToString();
                    emp.email = dr.GetValue(6).ToString();
                    emp.SDT = dr.GetValue(5).ToString();
                    emp.NGAYSINH = dr.GetValue(3).ToString();
                    emp.MK = dr.GetValue(7).ToString();
                    emp.idkhoa = dr.GetValue(8).ToString();
                    emp.disabled = int.Parse(dr.GetValue(9).ToString());
                    emp.ID_LOP = dr.GetValue(13).ToString();
                    emp.NOISINH = dr.GetValue(11).ToString();
                    emp.GIOITINH = dr.GetValue(12).ToString();
                    emp.BAC_DAO_TAO = dr.GetValue(10).ToString();
                    emp.HINHANH = dr.GetValue(14).ToString();
                    listGV.Add(emp);
                }
                con.Close();
            }
            return listGV;
        }

        public List<SINHVIEN> findByID2(int id)
        {
            string sql = null;
            SqlConnection con = new SqlConnection(conf);
            List<SINHVIEN> listGV = new List<SINHVIEN>();
            if (id != 0)
            {
                sql = " and ID_SV = '" + id + "'";
                SqlCommand cmd = new SqlCommand("select SINHVIEN.*,TEN_KHOA,CODE_LOP from SINHVIEN,khoa,lop where sinhvien.ID_KHOA = khoa.ID_KHOA and sinhvien.IDLOP = lop.ID_LOP" + sql, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SINHVIEN emp = new SINHVIEN();
                    emp.ID_SV = dr.GetValue(0).ToString();
                    emp.MASV = dr.GetValue(1).ToString();
                    emp.TENSV = dr.GetValue(2).ToString();
                    emp.dc = dr.GetValue(4).ToString();
                    emp.email = dr.GetValue(6).ToString();
                    emp.SDT = dr.GetValue(5).ToString();
                    emp.NGAYSINH = dr.GetValue(3).ToString();
                    emp.MK = dr.GetValue(7).ToString();
                    emp.idkhoa = dr.GetValue(8).ToString();
                    emp.disabled = int.Parse(dr.GetValue(9).ToString());
                    emp.TEN_KHOA = dr.GetValue(15).ToString();
                    emp.CODE_LOP = dr.GetValue(16).ToString();
                    emp.NOISINH = dr.GetValue(11).ToString();
                    emp.GIOITINH = dr.GetValue(12).ToString();
                    emp.HINHANH = dr.GetValue(14).ToString();
                    emp.BAC_DAO_TAO = dr.GetValue(10).ToString();
                    listGV.Add(emp);
                }
                con.Close();
            }
            return listGV;
        }

        public int save(string id, string ma, string ten, string sdt, string dc, string mk, string khoa, string email, string ngaysinh, string bac_dao_tao, string idlop, string gt, string noi_sinh, string hinhanh, string hocky, string diem)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            if (id == "0")
            {
                SqlCommand cmd2 = new SqlCommand("select count(*) from SINHVIEN", con);
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
                ma = "200119" + number2;
                SqlCommand cmd = new SqlCommand("insert into SINHVIEN(MASV,TENSV,SDT,DIACHI,MATKHAU,ID_KHOA,EMAIL,NGAYSINH,BACDAOTAO,NOISINH,GIOITINH,IDLOP,hinhanh) values(N'" + ma + "',N'" + ten + "','" + sdt + "',N'" + dc + "','" + mk + "','" + khoa + "','" + email + "','" + ngaysinh + "',N'" + bac_dao_tao + "',N'" + noi_sinh + "',N'" + gt + "',N'" + idlop + "',N'" + hinhanh + "')", con);
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteNonQuery();
                con.Close();
                con.Open();
                SqlCommand cmd3 = new SqlCommand("INSERT INTO KETQUA(IDSV,IDHK,DIEMMD,DIEM,LOAI) select ID_SV," + hocky + "," + diem + ",0,'B' from SINHVIEN where MASV = '" + ma + "'", con);
                cmd3.CommandType = CommandType.Text;
                dr = cmd3.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update SINHVIEN set TENSV= N'" + ten + "',SDT = '" + sdt + "',DIACHI = N'" + dc + "',matkhau = N'" + mk + "',email = '" + email + "',ID_KHOA = '" + khoa + "',NGAYSINH = '" + ngaysinh + "',BACDAOTAO = N'" + bac_dao_tao + "',NOISINH  = N'" + noi_sinh + "',GIOITINH = N'" + gt + "',IDLOP  = N'" + idlop + "',HINHANH =N'" + hinhanh + "' where MASV='" + ma + "'", con);
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
            sql = "update SINHVIEN set disabled = " + disabled + " where ID_SV =" + id;
            if (sql != "")
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteNonQuery();
                con.Close();
            }

            return dr;
        }

        public List<SINHVIEN> findSvVT(string vt)
        {
            string sql = null;

            List<SINHVIEN> listGV = new List<SINHVIEN>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select SINHVIEN.* from SINHVIEN  where sinhvien.disabled = 0 and id_sv not in (select id_sv from SV_CHUCVU where ID_VAI_TRO_SV = " + vt + " and SV_CHUCVU.disabled = 0)", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                SINHVIEN emp = new SINHVIEN();
                emp.ID_SV = dr.GetValue(0).ToString();
                emp.MASV = dr.GetValue(1).ToString();
                emp.TENSV = dr.GetValue(2).ToString();
                emp.dc = dr.GetValue(4).ToString();
                emp.email = dr.GetValue(6).ToString();
                emp.SDT = dr.GetValue(5).ToString();
                emp.NGAYSINH = dr.GetValue(3).ToString();
                emp.MK = dr.GetValue(7).ToString();
                emp.idkhoa = dr.GetValue(8).ToString();
                emp.disabled = int.Parse(dr.GetValue(9).ToString());
                listGV.Add(emp);
            }
            con.Close();
            return listGV;
        }

        public List<SINHVIEN> findSvVT2(string vt)
        {
            string sql = null;

            List<SINHVIEN> listGV = new List<SINHVIEN>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select SINHVIEN.* from SINHVIEN,SV_CHUCVU  where SINHVIEN.id_sv = SV_CHUCVU.id_sv and ID_VAI_TRO_SV = " + vt + " and sinhvien.disabled = 0 and SV_CHUCVU.disabled = 0", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                SINHVIEN emp = new SINHVIEN();
                emp.ID_SV = dr.GetValue(0).ToString();
                emp.MASV = dr.GetValue(1).ToString();
                emp.TENSV = dr.GetValue(2).ToString();
                emp.dc = dr.GetValue(4).ToString();
                emp.email = dr.GetValue(6).ToString();
                emp.SDT = dr.GetValue(5).ToString();
                emp.NGAYSINH = dr.GetValue(3).ToString();
                emp.MK = dr.GetValue(7).ToString();
                emp.idkhoa = dr.GetValue(8).ToString();
                emp.disabled = int.Parse(dr.GetValue(9).ToString());
                listGV.Add(emp);
            }
            con.Close();
            return listGV;
        }

        public int KtraMk(string id, string mk_cu)
        {
            var sql = "";
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd2 = new SqlCommand("select count(*) from SINHVIEN where ID_SV =" + id + "  and matkhau = '" + mk_cu + "'", con);
            cmd2.CommandType = CommandType.Text;
            con.Open();
            Object kq = cmd2.ExecuteScalar();
            int dr = int.Parse(kq.ToString());
            con.Close();
            return dr;
        }

        public int updateMk(string id, string mk)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            string sql = "";
            sql = "update SINHVIEN set matkhau = " + mk + " where ID_SV =" + id;
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