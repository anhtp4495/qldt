using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CNTT129_NetCore.Extensions;

namespace CNTT129.Models
{
    public class HOCKY
    {
        public string conf = AppSettings.ConnectionString;
        public string IDHK { get; set; }
        public string MAHK { get; set; }
        public string TENHK { get; set; }
        public string DIEMMD { get; set; }
        public int TrangThai { get; set; }

        public List<HOCKY> find()
        {

            List<HOCKY> listHK = new List<HOCKY>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select HOC_KI.* from HOC_KI where TRANGTHAI = 1", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                HOCKY emp = new HOCKY();
                emp.IDHK = dr.GetValue(0).ToString();
                emp.MAHK = dr.GetValue(1).ToString();
                emp.TENHK = dr.GetValue(2).ToString();
                emp.DIEMMD = dr.GetValue(5).ToString();
                listHK.Add(emp);
            }
            con.Close();
            return listHK;
        }

        public List<HOCKY> find2()
        {

            List<HOCKY> listHK = new List<HOCKY>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select HOC_KI.* from HOC_KI", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                HOCKY emp = new HOCKY();
                emp.IDHK = dr.GetValue(0).ToString();
                emp.MAHK = dr.GetValue(1).ToString();
                emp.TENHK = dr.GetValue(2).ToString();
                listHK.Add(emp);
            }
            con.Close();
            return listHK;
        }

        public int updateDiem(string idhk, string diem, string diemmd)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            SqlCommand cmd = new SqlCommand("update KETQUA set DIEM += HOATDONG.DIEMRL from DIEMDANH,HOATDONGTHEONGAY,HOATDONG where KETQUA.IDSV = DIEMDANH.IDSV and HOATDONGTHEONGAY.IDHD = HOATDONG.IDHD and DIEMDANH.IDBUOI = HOATDONGTHEONGAY.IDBUOI and HOATDONG.ID_HK = " + idhk + " and HOATDONGTHEONGAY.TRANGTHAI = 6", con);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteNonQuery();
            con.Close();

            con.Open();
            SqlCommand cmd2 = new SqlCommand("update KETQUA set DIEM += HOATDONG.DIEMRL from PHAN_HOI,HOATDONGTHEONGAY,HOATDONG where KETQUA.IDSV = PHAN_HOI.IDSV and HOATDONGTHEONGAY.IDHD = HOATDONG.IDHD and PHAN_HOI.IDBUOI = HOATDONGTHEONGAY.IDBUOI and PHAN_HOI.IDHK = " + idhk + " and PHAN_HOI.TRANGTHAI = 1 and HOATDONGTHEONGAY.TRANGTHAI IN (5,6)", con);
            cmd2.CommandType = CommandType.Text;
            dr = cmd2.ExecuteNonQuery();
            con.Close();

            con.Open();
            SqlCommand cmd3 = new SqlCommand("update KETQUA set DIEM -= HOATDONG.DIEMRL from DANGKY,HOATDONGTHEONGAY,HOATDONG where KETQUA.IDSV = DANGKY.IDSV and HOATDONGTHEONGAY.IDHD = HOATDONG.IDHD and DANGKY.IDBUOI = HOATDONGTHEONGAY.IDBUOI and HOATDONG.ID_HK = " + idhk + " and HOATDONGTHEONGAY.TRANGTHAI = 6 and DANGKY.TRANGTHAI = 3", con);
            cmd3.CommandType = CommandType.Text;
            dr = cmd3.ExecuteNonQuery();
            con.Close();

            con.Open();
            SqlCommand cmd4 = new SqlCommand("update KETQUA set DIEM += NOI_QUY.DIEM from PHAN_HOI,VI_PHAM,NOI_QUY where KETQUA.IDSV = PHAN_HOI.IDSV and PHAN_HOI.ID_VI_PHAM = VI_PHAM.ID_VI_PHAM and NOI_QUY.ID_NOI_QUY = VI_PHAM.ID_NOI_QUY and PHAN_HOI.IDHK = " + idhk + " and PHAN_HOI.TRANGTHAI = 1", con);
            cmd4.CommandType = CommandType.Text;
            dr = cmd4.ExecuteNonQuery();
            con.Close();

            con.Open();
            SqlCommand cmd5 = new SqlCommand("update KETQUA set DIEM -= NOI_QUY.DIEM from VI_PHAM,NOI_QUY where KETQUA.IDSV = VI_PHAM.IDSV  and NOI_QUY.ID_NOI_QUY = VI_PHAM.ID_NOI_QUY and VI_PHAM.IDHK = " + idhk + " and VI_PHAM.TRANGTHAI = 1", con);
            cmd5.CommandType = CommandType.Text;
            dr = cmd5.ExecuteNonQuery();
            con.Close();

            con.Open();
            SqlCommand cmd12 = new SqlCommand("update KETQUA set DIEM += LOAI_HOAT_DONG_NGOAI.DIEM from HOATDONGNGOAI,LOAI_HOAT_DONG_NGOAI where KETQUA.IDSV = HOATDONGNGOAI.IDSV  and LOAI_HOAT_DONG_NGOAI.ID_HD_NGOAI = HOATDONGNGOAI.ID_HD_NGOAI and HOATDONGNGOAI.ID_HK = " + idhk + " and HOATDONGNGOAI.TRANGTHAI = 1", con);
            cmd12.CommandType = CommandType.Text;
            dr = cmd12.ExecuteNonQuery();
            con.Close();

            con.Open();
            SqlCommand cmd6 = new SqlCommand("update HOC_KI set TRANGTHAI = 0 where ID_HK = " + idhk, con);
            cmd6.CommandType = CommandType.Text;
            dr = cmd6.ExecuteNonQuery();
            con.Close();

            con.Open();
            SqlCommand cmd7 = new SqlCommand("update HOC_KI set TRANGTHAI = 1,DIEMMD = " + diem + " where ID_HK = " + (int.Parse(idhk) + 1), con);
            cmd7.CommandType = CommandType.Text;
            dr = cmd7.ExecuteNonQuery();
            con.Close();

            con.Open();
            SqlCommand cmd8 = new SqlCommand("update phan_hoi set TRANGTHAI = 4 where IDHK = " + idhk + "and trangthai = 0", con);// Đã chốt
            cmd8.CommandType = CommandType.Text;
            dr = cmd8.ExecuteNonQuery();
            con.Close();

            con.Open();
            SqlCommand cmd9 = new SqlCommand("update vi_pham set TRANGTHAI = 4 where IDHK = " + idhk + "and trangthai = 0", con);// Đã chốt
            cmd9.CommandType = CommandType.Text;
            dr = cmd9.ExecuteNonQuery();
            con.Close();

            con.Open();
            SqlCommand cmd10 = new SqlCommand("update HOATDONGTHEONGAY set HOATDONGTHEONGAY.TRANGTHAI = 8 from HOATDONG where HOATDONG.IDHD = HOATDONGTHEONGAY.IDHD and HOATDONG.ID_HK = " + idhk + " and HOATDONGTHEONGAY.TRANGTHAI IN (6,5)", con);// Đã chốt
            cmd10.CommandType = CommandType.Text;
            dr = cmd10.ExecuteNonQuery();
            con.Close();

            con.Open();
            SqlCommand cmd13 = new SqlCommand("update HOATDONGNGOAI set HOATDONGNGOAI.TRANGTHAI = 4 where HOATDONGNGOAI.ID_HK = " + idhk + " and HOATDONGNGOAI.trangthai = 0 ", con);// Đã chốt
            cmd13.CommandType = CommandType.Text;
            dr = cmd13.ExecuteNonQuery();
            con.Close();

            con.Open();
            SqlCommand cmd11 = new SqlCommand("INSERT INTO KETQUA(IDSV,IDHK,DIEMMD,DIEM,LOAI) select ID_SV," + (int.Parse(idhk) + 1) + "," + diem + ",0,'B' from SINHVIEN where disabled = 0 and ID_SV NOT IN (SELECT IDSV FROM KETQUA WHERE IDHK = " + (int.Parse(idhk) + 1) + ") ", con);
            cmd11.CommandType = CommandType.Text;
            dr = cmd11.ExecuteNonQuery();
            con.Close();
            return dr;
        }

        public int updateDiem2(string idhk, string ngaybd, string ngaykt)
        {
            var sql = "";
            if (ngaybd != "")
            {
                sql += " and HOATDONGTHEONGAY.NGAYBATDAUDIEMDANH >= '" + ngaybd + "'";
            }
            if (ngaykt != "")
            {
                sql += " and HOATDONGTHEONGAY.NGAYBATDAUDIEMDANH <= '" + ngaykt + "'";
            }
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            SqlCommand cmd = new SqlCommand("update KETQUA set DIEM += HOATDONG.DIEMRL from DIEMDANH,HOATDONGTHEONGAY,HOATDONG where KETQUA.IDSV = DIEMDANH.IDSV and HOATDONGTHEONGAY.IDHD = HOATDONG.IDHD and DIEMDANH.IDBUOI = HOATDONGTHEONGAY.IDBUOI and HOATDONG.ID_HK = " + idhk + " and HOATDONGTHEONGAY.TRANGTHAI = 6 " + sql, con);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteNonQuery();
            con.Close();

            con.Open();
            SqlCommand cmd2 = new SqlCommand("update KETQUA set DIEM += HOATDONG.DIEMRL from PHAN_HOI,HOATDONGTHEONGAY,HOATDONG where KETQUA.IDSV = PHAN_HOI.IDSV and HOATDONGTHEONGAY.IDHD = HOATDONG.IDHD and PHAN_HOI.IDBUOI = HOATDONGTHEONGAY.IDBUOI and PHAN_HOI.IDHK = " + idhk + " and PHAN_HOI.TRANGTHAI = 1 and HOATDONGTHEONGAY.TRANGTHAI IN (6,5) " + sql, con);
            cmd2.CommandType = CommandType.Text;
            dr = cmd2.ExecuteNonQuery();
            con.Close();

            con.Open();
            SqlCommand cmd3 = new SqlCommand("update KETQUA set DIEM -= HOATDONG.DIEMRL from DANGKY,HOATDONGTHEONGAY,HOATDONG where KETQUA.IDSV = DANGKY.IDSV and HOATDONGTHEONGAY.IDHD = HOATDONG.IDHD and DANGKY.IDBUOI = HOATDONGTHEONGAY.IDBUOI and HOATDONG.ID_HK = " + idhk + " and HOATDONGTHEONGAY.TRANGTHAI = 6 and DANGKY.TRANGTHAI = 3 " + sql, con);
            cmd3.CommandType = CommandType.Text;
            dr = cmd3.ExecuteNonQuery();
            con.Close();

            var sql3 = "";
            if (ngaybd != "")
            {
                sql3 += " and PHAN_HOI.NGAYTAO >= '" + ngaybd + "' ";
            }
            if (ngaykt != "")
            {
                sql3 += " and PHAN_HOI.NGAYTAO <= '" + ngaykt + "' ";
            }
            con.Open();
            SqlCommand cmd4 = new SqlCommand("update KETQUA set DIEM += NOI_QUY.DIEM from PHAN_HOI,VI_PHAM,NOI_QUY where KETQUA.IDSV = PHAN_HOI.IDSV and PHAN_HOI.ID_VI_PHAM = VI_PHAM.ID_VI_PHAM and NOI_QUY.ID_NOI_QUY = VI_PHAM.ID_NOI_QUY and PHAN_HOI.IDHK = " + idhk + " and PHAN_HOI.TRANGTHAI = 1 " + sql3, con);
            cmd4.CommandType = CommandType.Text;
            dr = cmd4.ExecuteNonQuery();
            con.Close();

            var sql2 = "";
            if (ngaybd != "")
            {
                sql2 += " and VI_PHAM.NGAYTAO >= '" + ngaybd + "' ";
            }
            if (ngaykt != "")
            {
                sql2 += " and VI_PHAM.NGAYTAO <= '" + ngaykt + "' ";
            }
            con.Open();
            SqlCommand cmd5 = new SqlCommand("update KETQUA set DIEM -= NOI_QUY.DIEM from VI_PHAM,NOI_QUY where KETQUA.IDSV = VI_PHAM.IDSV  and NOI_QUY.ID_NOI_QUY = VI_PHAM.ID_NOI_QUY and VI_PHAM.IDHK = " + idhk + " and VI_PHAM.TRANGTHAI = 1 " + sql2, con);
            cmd5.CommandType = CommandType.Text;
            dr = cmd5.ExecuteNonQuery();
            con.Close();

            var sql4 = "";
            if (ngaybd != "")
            {
                sql4 += " and HOATDONGNGOAI.NGAYTAO >= '" + ngaybd + "' ";
            }
            if (ngaykt != "")
            {
                sql4 += " and HOATDONGNGOAI.NGAYTAO <= '" + ngaykt + "' ";
            }
            con.Open();
            SqlCommand cmd12 = new SqlCommand("update KETQUA set DIEM += LOAI_HOAT_DONG_NGOAI.DIEM from HOATDONGNGOAI,LOAI_HOAT_DONG_NGOAI where KETQUA.IDSV = HOATDONGNGOAI.IDSV  and LOAI_HOAT_DONG_NGOAI.ID_HD_NGOAI = HOATDONGNGOAI.ID_HD_NGOAI and HOATDONGNGOAI.ID_HK = " + idhk + " and HOATDONGNGOAI.TRANGTHAI = 1 " + sql4, con);
            cmd12.CommandType = CommandType.Text;
            dr = cmd12.ExecuteNonQuery();
            con.Close();

            con.Open();
            SqlCommand cmd8 = new SqlCommand("update phan_hoi set TRANGTHAI = 4 where IDHK = " + idhk + "and trangthai = 0 " + sql3, con);// Đã chốt
            cmd8.CommandType = CommandType.Text;
            dr = cmd8.ExecuteNonQuery();
            con.Close();

            con.Open();
            SqlCommand cmd9 = new SqlCommand("update vi_pham set TRANGTHAI = 4 where IDHK = " + idhk + "and trangthai = 0" + sql2, con);// Đã chốt
            cmd9.CommandType = CommandType.Text;
            dr = cmd9.ExecuteNonQuery();
            con.Close();

            con.Open();
            SqlCommand cmd10 = new SqlCommand("update HOATDONGTHEONGAY set HOATDONGTHEONGAY.TRANGTHAI = 8 from HOATDONG where HOATDONG.IDHD = HOATDONGTHEONGAY.IDHD and HOATDONG.ID_HK = " + idhk + " and HOATDONGTHEONGAY.TRANGTHAI IN (5,6) " + sql, con);// Đã chốt
            cmd10.CommandType = CommandType.Text;
            dr = cmd10.ExecuteNonQuery();
            con.Close();

            con.Open();
            SqlCommand cmd11 = new SqlCommand("update HOATDONGNGOAI set HOATDONGNGOAI.TRANGTHAI = 4 where HOATDONGNGOAI.ID_HK = " + idhk + " and HOATDONGNGOAI.trangthai = 0 " + sql4, con);// Đã chốt
            cmd11.CommandType = CommandType.Text;
            dr = cmd11.ExecuteNonQuery();
            con.Close();

            return dr;
        }

        public List<HOCKY> find2(string idhk)
        {

            List<HOCKY> listHK = new List<HOCKY>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select HOC_KI.* from HOC_KI where ID_HK <= " + idhk, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                HOCKY emp = new HOCKY();
                emp.IDHK = dr.GetValue(0).ToString();
                emp.MAHK = dr.GetValue(1).ToString();
                emp.TENHK = dr.GetValue(2).ToString();
                listHK.Add(emp);
            }
            con.Close();
            return listHK;
        }
    }
}