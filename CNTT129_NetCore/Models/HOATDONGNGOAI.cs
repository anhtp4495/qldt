using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CNTT129_NetCore.Extensions;

namespace CNTT129.Models
{
    public class HOATDONGNGOAI
    {
        public string conf = AppSettings.ConnectionString;
        public string IDHD { get; set; }
        public string MAHD { get; set; }
        public string IDSV { get; set; }
        public string ID_HD_NGOAI { get; set; }
        public string NOIDUNG { get; set; }
        public string NGUOITAO { get; set; }
        public string SINHVIENTAO { get; set; }
        public string NGAYTAO { get; set; }
        public string NGUOISUA { get; set; }
        public string NGAYSUA { get; set; }
        public string NGUOIDUYET { get; set; }
        public string NGAYDUYET { get; set; }
        public string TRANGTHAI { get; set; }
        public string LOAI { get; set; }

        public string HINHANH { get; set; }
        public string ID_HK { get; set; }
        public string DIADIEM { get; set; }

        public string DIEM { get; set; }
        public string TEN_HD_NGOAI { get; set; }
        public string TEN { get; set; }
        public string TEN2 { get; set; }
        public string MASV { get; set; }
        public string TENHK { get; set; }
        public string HINH_THUC { get; set; }
        public List<HOATDONGNGOAI> find(string MAHD, string IDSV, string disabled, string ngaybd, string ngaykt, string idhk, string loai, string loai_hd)
        {
            string sql = null;
            sql += " where HOATDONGNGOAI.NGAYTAO >= '" + ngaybd + "' and  HOATDONGNGOAI.NGAYTAO <= '" + ngaykt + "'";
            if (MAHD != "")
            {
                sql += " and HOATDONGNGOAI.MAHD = '" + MAHD + "' ";
            }
            if (IDSV != "")
            {
                sql += " and HOATDONGNGOAI.IDSV =" + IDSV + "";
            }
            if (disabled != "")
            {
                sql += " and HOATDONGNGOAI.TRANGTHAI = " + disabled + "";
            }
            if (int.Parse(idhk) != 0)
            {
                sql += " and HOATDONGNGOAI.ID_HK = " + idhk + "";
            }
            if (loai != "")
            {
                sql += " and HOATDONGNGOAI.LOAI = " + loai + "";
            }
            if (int.Parse(loai_hd) != 0)
            {
                sql += " and LOAI_HOAT_DONG_NGOAI.ID_HD_NGOAI = " + loai_hd + "";
            }
            List<HOATDONGNGOAI> listBH = new List<HOATDONGNGOAI>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select HOATDONGNGOAI.*,LOAI_HOAT_DONG_NGOAI.TEN_HD_NGOAI,LOAI_HOAT_DONG_NGOAI.DIEM,GIANG_VIEN.TENGV as TEN ,SINHVIEN.TENSV as TEN2,SINHVIEN.MASV,TEN_HK from HOATDONGNGOAI INNER JOIN GIANG_VIEN ON GIANG_VIEN.ID_GV = HOATDONGNGOAI.NGUOITAO INNER JOIN LOAI_HOAT_DONG_NGOAI ON LOAI_HOAT_DONG_NGOAI.ID_HD_NGOAI = HOATDONGNGOAI.ID_HD_NGOAI INNER JOIN SINHVIEN ON SINHVIEN.ID_SV = HOATDONGNGOAI.IDSV INNER JOIN HOC_KI ON HOC_KI.ID_HK = HOATDONGNGOAI.ID_HK " + sql + " union all select HOATDONGNGOAI.*,LOAI_HOAT_DONG_NGOAI.TEN_HD_NGOAI,LOAI_HOAT_DONG_NGOAI.DIEM,SINHVIEN.TENSV as TEN,sv2.TENSV as TEN2,sv2.MASV,TEN_HK from HOATDONGNGOAI  INNER JOIN SINHVIEN ON SINHVIEN.ID_SV = HOATDONGNGOAI.SINHVIENTAO INNER JOIN LOAI_HOAT_DONG_NGOAI ON LOAI_HOAT_DONG_NGOAI.ID_HD_NGOAI = HOATDONGNGOAI.ID_HD_NGOAI INNER JOIN SINHVIEN as sv2 ON sv2.ID_SV = HOATDONGNGOAI.IDSV INNER JOIN HOC_KI ON HOC_KI.ID_HK = HOATDONGNGOAI.ID_HK " + sql + "", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                HOATDONGNGOAI emp = new HOATDONGNGOAI();
                emp.IDHD = dr.GetValue(0).ToString();
                emp.MAHD = dr.GetValue(1).ToString();
                emp.IDSV = dr.GetValue(2).ToString();
                emp.ID_HD_NGOAI = dr.GetValue(3).ToString();
                emp.NOIDUNG = dr.GetValue(4).ToString();
                emp.NGUOITAO = dr.GetValue(5).ToString();
                emp.NGAYTAO = dr.GetValue(6).ToString();
                emp.NGUOISUA = dr.GetValue(7).ToString();
                emp.NGAYSUA = dr.GetValue(8).ToString();
                emp.NGUOIDUYET = dr.GetValue(9).ToString();
                emp.NGAYDUYET = dr.GetValue(10).ToString();
                emp.TRANGTHAI = dr.GetValue(11).ToString();
                emp.LOAI = dr.GetValue(12).ToString();
                emp.HINHANH = dr.GetValue(13).ToString();
                emp.ID_HK = dr.GetValue(14).ToString();
                emp.DIADIEM = dr.GetValue(15).ToString();
                emp.SINHVIENTAO = dr.GetValue(16).ToString();
                emp.TEN_HD_NGOAI = dr.GetValue(17).ToString();
                emp.DIEM = dr.GetValue(18).ToString();
                emp.TEN = dr.GetValue(19).ToString();
                emp.TEN2 = dr.GetValue(20).ToString();
                emp.MASV = dr.GetValue(21).ToString();
                emp.TENHK = dr.GetValue(22).ToString();
                listBH.Add(emp);
            }
            con.Close();
            return listBH;
        }

        public List<HOATDONGNGOAI> findByID(int id)
        {
            string sql = null;
            SqlConnection con = new SqlConnection(conf);
            List<HOATDONGNGOAI> listBH = new List<HOATDONGNGOAI>();
            if (id != 0)
            {
                sql = "where HOATDONGNGOAI.IDHD = " + id + "";
                SqlCommand cmd = new SqlCommand("select HOATDONGNGOAI.*,LOAI_HOAT_DONG_NGOAI.TEN_HD_NGOAI,LOAI_HOAT_DONG_NGOAI.DIEM,GIANG_VIEN.TENGV as TEN ,SINHVIEN.TENSV as TEN2,SINHVIEN.MASV,TEN_HK from HOATDONGNGOAI INNER JOIN GIANG_VIEN ON GIANG_VIEN.ID_GV = HOATDONGNGOAI.NGUOITAO INNER JOIN LOAI_HOAT_DONG_NGOAI ON LOAI_HOAT_DONG_NGOAI.ID_HD_NGOAI = HOATDONGNGOAI.ID_HD_NGOAI INNER JOIN SINHVIEN ON SINHVIEN.ID_SV = HOATDONGNGOAI.IDSV INNER JOIN HOC_KI ON HOC_KI.ID_HK = HOATDONGNGOAI.ID_HK " + sql + " union all select HOATDONGNGOAI.*,LOAI_HOAT_DONG_NGOAI.TEN_HD_NGOAI,LOAI_HOAT_DONG_NGOAI.DIEM,SINHVIEN.TENSV as TEN,sv2.TENSV as TEN2,sv2.MASV,TEN_HK from HOATDONGNGOAI  INNER JOIN SINHVIEN ON SINHVIEN.ID_SV = HOATDONGNGOAI.SINHVIENTAO INNER JOIN LOAI_HOAT_DONG_NGOAI ON LOAI_HOAT_DONG_NGOAI.ID_HD_NGOAI = HOATDONGNGOAI.ID_HD_NGOAI INNER JOIN SINHVIEN as sv2 ON sv2.ID_SV = HOATDONGNGOAI.IDSV INNER JOIN HOC_KI ON HOC_KI.ID_HK = HOATDONGNGOAI.ID_HK " + sql + "", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    HOATDONGNGOAI emp = new HOATDONGNGOAI();
                    emp.IDHD = dr.GetValue(0).ToString();
                    emp.MAHD = dr.GetValue(1).ToString();
                    emp.IDSV = dr.GetValue(2).ToString();
                    emp.ID_HD_NGOAI = dr.GetValue(3).ToString();
                    emp.NOIDUNG = dr.GetValue(4).ToString();
                    emp.NGUOITAO = dr.GetValue(5).ToString();
                    emp.NGAYTAO = dr.GetValue(6).ToString();
                    emp.NGUOISUA = dr.GetValue(7).ToString();
                    emp.NGAYSUA = dr.GetValue(8).ToString();
                    emp.NGUOIDUYET = dr.GetValue(9).ToString();
                    emp.NGAYDUYET = dr.GetValue(10).ToString();
                    emp.TRANGTHAI = dr.GetValue(11).ToString();
                    emp.LOAI = dr.GetValue(12).ToString();
                    emp.HINHANH = dr.GetValue(13).ToString();
                    emp.ID_HK = dr.GetValue(14).ToString();
                    emp.DIADIEM = dr.GetValue(15).ToString();
                    emp.SINHVIENTAO = dr.GetValue(16).ToString();
                    emp.TEN_HD_NGOAI = dr.GetValue(17).ToString();
                    emp.DIEM = dr.GetValue(18).ToString();
                    emp.TEN = dr.GetValue(19).ToString();
                    emp.TEN2 = dr.GetValue(20).ToString();
                    emp.MASV = dr.GetValue(21).ToString();
                    emp.TENHK = dr.GetValue(22).ToString();
                    listBH.Add(emp);
                }
                con.Close();
            }
            return listBH;
        }

        public int save(string id, string noi_dung, string dia_diem, string hk, string hinh_anh, string id_sv, string nguoitao, string loai, string loaihd)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);

            if (id == "0")
            {
                SqlCommand cmd2 = new SqlCommand("select count(*) from HOATDONGNGOAI", con);
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
                string ma = "HĐN00" + number2;
                var sql = "insert into HOATDONGNGOAI(MAHD,IDSV,ID_HD_NGOAI,NOIDUNG,NGUOITAO,NGAYTAO,LOAI,HINHANH,ID_HK,DIADIEM) values (N'" + ma + "',N'" + id_sv + "','" + loaihd + "',N'" + noi_dung + "',N'" + nguoitao + "',GETDATE(),N'" + loai + "',N'" + hinh_anh + "',N'" + hk + "',N'" + dia_diem + "')";
                if (loai == "1")
                {
                    sql = "insert into HOATDONGNGOAI(MAHD,IDSV,ID_HD_NGOAI,NOIDUNG,SINHVIENTAO,NGAYTAO,LOAI,HINHANH,ID_HK,DIADIEM) values (N'" + ma + "',N'" + id_sv + "','" + loaihd + "',N'" + noi_dung + "',N'" + nguoitao + "',GETDATE(),N'" + loai + "',N'" + hinh_anh + "',N'" + hk + "',N'" + dia_diem + "')";
                }
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update HOATDONGNGOAI set ID_HD_NGOAI = " + loaihd + ",NOIDUNG =N'" + noi_dung + "',NGUOISUA = N'" + nguoitao + "',NGAYSUA = GETDATE(),HINHANH = N'" + hinh_anh + "',DIADIEM = N'" + dia_diem + "' where IDHD='" + id + "'", con);
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteNonQuery();
                con.Close();
            }
            return dr;
        }

        public int updateTT(string id, string disabled)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            string sql = "";
            sql = "update HOATDONGNGOAI set trangthai = " + disabled + " where IDHD =" + id;
            if (sql != "")
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteNonQuery();
                con.Close();
            }

            return dr;
        }

        public int demSL(string idhk, string ngaybd, string ngaykt)
        {
            var sql = "";
            if (ngaybd != "")
            {
                sql += " and NGAYTAO >= '" + ngaybd + "' ";
            }
            if (ngaykt != "")
            {
                sql += " and NGAYTAO <= '" + ngaykt + "' ";
            }
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd2 = new SqlCommand("select count(*) from HOATDONGNGOAI where HOATDONGNGOAI.trangthai = 0 and HOATDONGNGOAI.ID_HK = " + idhk + " " + sql, con);
            cmd2.CommandType = CommandType.Text;
            con.Open();
            Object kq = cmd2.ExecuteScalar();
            int dr = int.Parse(kq.ToString());
            con.Close();
            return dr;
        }

        public List<HOATDONGNGOAI> showKetQua(string IDSV, string idhk)
        {
            List<HOATDONGNGOAI> listHD = new List<HOATDONGNGOAI>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select HOATDONGNGOAI.MAHD,LOAI_HOAT_DONG_NGOAI.TEN_HD_NGOAI,HOATDONGNGOAI.NGAYTAO,N'Hộng động ngoài trường' as hinh_thuc, HOATDONGNGOAI.NOIDUNG,LOAI_HOAT_DONG_NGOAI.DIEM from HOATDONGNGOAI inner join LOAI_HOAT_DONG_NGOAI ON HOATDONGNGOAI.ID_HD_NGOAI = LOAI_HOAT_DONG_NGOAI.ID_HD_NGOAI where HOATDONGNGOAI.ID_HK = " + idhk + " and HOATDONGNGOAI.IDSV = " + IDSV + " and HOATDONGNGOAI.TRANGTHAI = 4", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                HOATDONGNGOAI emp = new HOATDONGNGOAI();
                emp.MAHD = dr.GetValue(0).ToString();
                emp.TEN_HD_NGOAI = dr.GetValue(1).ToString();
                emp.NGAYTAO = dr.GetValue(2).ToString();
                emp.HINH_THUC = dr.GetValue(3).ToString();
                emp.NOIDUNG = dr.GetValue(4).ToString();
                emp.DIEM = dr.GetValue(5).ToString();
                listHD.Add(emp);
            }
            con.Close();
            return listHD;
        }
    }
}