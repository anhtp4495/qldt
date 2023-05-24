using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using PagedList;
using CNTT129_NetCore.Settings;

namespace CNTT129_NetCore.Models
{
    public class HOATDONG
    {
        public string conf = AppSettings.ConnectionString;
        public string IDHD { get; set; }
        public string MAHD { get; set; }
        public string TIEUDE { get; set; }
        public string NOIDUNG { get; set; }
        public string NGUOITAO { get; set; }
        public string NGAYTAO { get; set; }
        public string NGUOISUA { get; set; }
        public string NGAYSUA { get; set; }
        public string NGUOIDUYET { get; set; }
        public string NGAYDUYET { get; set; }
        public int TRANGTHAI { get; set; }
        public string NGAYBATDAUDK { get; set; }
        public string NGAYKETTHUCDK { get; set; }
        public string GHICHU { get; set; }
        public string DIEMRL { get; set; }
        public int LOAI { get; set; }
        public int TINHTRANG { get; set; }
        public int SONGUOI { get; set; }
        public int ID_HK { get; set; }
        public int ID_KHOA { get; set; }
        public string HINHANH { get; set; }
        public string name_ngtao { get; set; }
        public string name_ngsua { get; set; }
        public string name_ngduyet { get; set; }
        public string name_khoa { get; set; }
        public string name_hocky { get; set; }

        public string DIADIEM { get; set; }

        public string NGAY_BAT_DAU { get; set; }

        public string trang_thai_buoi { get; set; }

        public string IDBUOI { get; set; }
        public string LOAI_BUOI { get; set; }
        public string HINH_THUC { get; set; }

        public string save(string tieude, string noidung, string ngaybatdaudk, string ngayketthudk, int khoa, int idhk, int songuoi, int diem, string ghichu, int loai, string hinhanh, string nguoitao, string ngaytao, string dia_diem)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);

            SqlCommand cmd2 = new SqlCommand("select count(*) from HOATDONG", con);
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
            string ma = "HĐ00" + number2;
            SqlCommand cmd = new SqlCommand("insert into HOATDONG(MAHD,TIEUDE,NOIDUNG,NGAYBATDAUDK,NGAYKETTHUCDK,GHICHU,DIEMRL,LOAI,SONGUOI,ID_HK,ID_KHOA,HINHANH,NGUOITAO,NGAYTAO,DIADIEM) values (N'" + ma + "',N'" + tieude + "',N'" + noidung + "',N'" + ngaybatdaudk + "',N'" + ngayketthudk + "',N'" + ghichu + "',N'" + diem + "',N'" + loai + "',N'" + songuoi + "',N'" + idhk + "',N'" + khoa + "',N'" + hinhanh + "',N'" + nguoitao + "',N'" + ngaytao + "',N'" + dia_diem + "')", con);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteNonQuery();
            con.Close();

            return ma;
        }

        public List<HOATDONG> findByCode(string CODE_HD)
        {
            List<HOATDONG> listHD = new List<HOATDONG>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select HOATDONG.* from HOATDONG where MAHD=N'" + CODE_HD + "'", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                HOATDONG emp = new HOATDONG();
                emp.IDHD = dr.GetValue(0).ToString();
                emp.MAHD = dr.GetValue(1).ToString();
                emp.TIEUDE = dr.GetValue(2).ToString();
                emp.NOIDUNG = dr.GetValue(3).ToString();
                emp.NGUOITAO = dr.GetValue(4).ToString();
                emp.NGAYTAO = dr.GetValue(5).ToString();
                emp.NGUOISUA = dr.GetValue(6).ToString();
                emp.NGAYSUA = dr.GetValue(7).ToString();
                emp.NGUOIDUYET = dr.GetValue(8).ToString();
                emp.NGAYDUYET = dr.GetValue(9).ToString();
                emp.TRANGTHAI = int.Parse(dr.GetValue(10).ToString());
                emp.NGAYBATDAUDK = dr.GetValue(11).ToString();
                emp.NGAYKETTHUCDK = dr.GetValue(12).ToString();
                emp.GHICHU = dr.GetValue(13).ToString();
                emp.DIEMRL = dr.GetValue(14).ToString();
                emp.LOAI = int.Parse(dr.GetValue(15).ToString());
                emp.TINHTRANG = int.Parse(dr.GetValue(16).ToString());
                emp.SONGUOI = int.Parse(dr.GetValue(17).ToString());
                emp.HINHANH = dr.GetValue(18).ToString();
                emp.ID_HK = int.Parse(dr.GetValue(19).ToString());
                emp.ID_KHOA = int.Parse(dr.GetValue(20).ToString());
                emp.DIADIEM = dr.GetValue(21).ToString();
                listHD.Add(emp);
            }
            con.Close();
            return listHD;
        }

        public List<HOATDONG> find(string CODE_HD, string TEN_HD, string ID_KHOA, string LOAI, string ngaybd, string ngaykt, string ID_HK, string trang_thai)
        {
            string sql = " where ngaytao >= '" + ngaybd + "' and ngaytao <= '" + ngaykt + "' ";

            if (CODE_HD != "" && CODE_HD != null)
            {
                sql += "and MAHD = '" + CODE_HD + "' ";
            }

            if (TEN_HD != "" && TEN_HD != null)
            {
                sql += "and TIEUDE like '%" + TEN_HD + "%' ";
            }

            if (int.Parse(ID_KHOA) != 0)
            {
                sql += "and HOATDONG.ID_KHOA = " + ID_KHOA + " ";
            }

            if (LOAI != "")
            {
                sql += "and LOAI = " + LOAI + " ";
            }

            if (int.Parse(ID_HK) != 0)
            {
                sql += "and HOATDONG.ID_HK = " + ID_HK + " ";
            }

            if (trang_thai != "")
            {
                sql += "and HOATDONG.TRANGTHAI = " + trang_thai + " ";
            }

            List<HOATDONG> listHD = new List<HOATDONG>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select HOATDONG.*,ngtao.TENGV,ngsua.TENGV,ngduyet.TENGV,KHOA.TEN_KHOA,HOC_KI.TEN_HK from HOATDONG LEFT JOIN GIANG_VIEN as ngtao ON ngtao.ID_GV = HOATDONG.NGUOITAO LEFT JOIN GIANG_VIEN as ngsua ON ngsua.ID_GV = HOATDONG.NGUOITAO LEFT JOIN GIANG_VIEN as ngduyet ON ngduyet.ID_GV = HOATDONG.NGUOIDUYET LEFT JOIN KHOA ON KHOA.ID_KHOA = HOATDONG.ID_KHOA LEFT JOIN HOC_KI ON HOC_KI.ID_HK = HOATDONG.ID_HK " + sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                HOATDONG emp = new HOATDONG();
                emp.IDHD = dr.GetValue(0).ToString();
                emp.MAHD = dr.GetValue(1).ToString();
                emp.TIEUDE = dr.GetValue(2).ToString();
                emp.NOIDUNG = dr.GetValue(3).ToString();
                emp.NGUOITAO = dr.GetValue(4).ToString();
                emp.NGAYTAO = dr.GetValue(5).ToString();
                emp.NGUOISUA = dr.GetValue(6).ToString();
                emp.NGAYSUA = dr.GetValue(7).ToString();
                emp.NGUOIDUYET = dr.GetValue(8).ToString();
                emp.NGAYDUYET = dr.GetValue(9).ToString();
                emp.TRANGTHAI = int.Parse(dr.GetValue(10).ToString());
                emp.NGAYBATDAUDK = dr.GetValue(11).ToString();
                emp.NGAYKETTHUCDK = dr.GetValue(12).ToString();
                emp.GHICHU = dr.GetValue(13).ToString();
                emp.DIEMRL = dr.GetValue(14).ToString();
                emp.LOAI = int.Parse(dr.GetValue(15).ToString());
                emp.TINHTRANG = int.Parse(dr.GetValue(16).ToString());
                emp.SONGUOI = int.Parse(dr.GetValue(17).ToString());
                emp.HINHANH = dr.GetValue(18).ToString();
                emp.ID_HK = int.Parse(dr.GetValue(19).ToString());
                emp.ID_KHOA = int.Parse(dr.GetValue(20).ToString());
                emp.name_ngtao = dr.GetValue(22).ToString();
                emp.name_ngsua = dr.GetValue(23).ToString();
                emp.name_ngduyet = dr.GetValue(24).ToString();
                emp.name_khoa = dr.GetValue(25).ToString();
                emp.name_hocky = dr.GetValue(26).ToString();
                emp.DIADIEM = dr.GetValue(21).ToString();
                listHD.Add(emp);
            }
            con.Close();
            return listHD;
        }

        public List<HOATDONG> findByID(string CODE_ID)
        {
            List<HOATDONG> listHD = new List<HOATDONG>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select HOATDONG.*,ngtao.TENGV,ngsua.TENGV,ngduyet.TENGV,KHOA.TEN_KHOA,HOC_KI.TEN_HK from HOATDONG LEFT JOIN GIANG_VIEN as ngtao ON ngtao.ID_GV = HOATDONG.NGUOITAO LEFT JOIN GIANG_VIEN as ngsua ON ngsua.ID_GV = HOATDONG.NGUOITAO LEFT JOIN GIANG_VIEN as ngduyet ON ngduyet.ID_GV = HOATDONG.NGUOIDUYET LEFT JOIN KHOA ON KHOA.ID_KHOA = HOATDONG.ID_KHOA LEFT JOIN HOC_KI ON HOC_KI.ID_HK = HOATDONG.ID_HK where IDHD = '" + CODE_ID + "'", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                HOATDONG emp = new HOATDONG();
                emp.IDHD = dr.GetValue(0).ToString();
                emp.MAHD = dr.GetValue(1).ToString();
                emp.TIEUDE = dr.GetValue(2).ToString();
                emp.NOIDUNG = dr.GetValue(3).ToString();
                emp.NGUOITAO = dr.GetValue(4).ToString();
                emp.NGAYTAO = dr.GetValue(5).ToString();
                emp.NGUOISUA = dr.GetValue(6).ToString();
                emp.NGAYSUA = dr.GetValue(7).ToString();
                emp.NGUOIDUYET = dr.GetValue(8).ToString();
                emp.NGAYDUYET = dr.GetValue(9).ToString();
                emp.TRANGTHAI = int.Parse(dr.GetValue(10).ToString());
                emp.NGAYBATDAUDK = dr.GetValue(11).ToString();
                emp.NGAYKETTHUCDK = dr.GetValue(12).ToString();
                emp.GHICHU = dr.GetValue(13).ToString();
                emp.DIEMRL = dr.GetValue(14).ToString();
                emp.LOAI = int.Parse(dr.GetValue(15).ToString());
                emp.TINHTRANG = int.Parse(dr.GetValue(16).ToString());
                emp.SONGUOI = int.Parse(dr.GetValue(17).ToString());
                emp.HINHANH = dr.GetValue(18).ToString();
                emp.ID_HK = int.Parse(dr.GetValue(19).ToString());
                emp.ID_KHOA = int.Parse(dr.GetValue(20).ToString());
                emp.name_ngtao = dr.GetValue(22).ToString();
                emp.name_ngsua = dr.GetValue(23).ToString();
                emp.name_ngduyet = dr.GetValue(24).ToString();
                emp.name_khoa = dr.GetValue(25).ToString();
                emp.name_hocky = dr.GetValue(26).ToString();
                emp.DIADIEM = dr.GetValue(21).ToString();
                listHD.Add(emp);
            }
            con.Close();
            return listHD;
        }

        public int updateTT(int id, int trangthai, string ngay, string idgv)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            string sql = "";
            sql = "update HOATDONG"; // 0 chưa duyệt  1 // đã duyệt 2 hủy
            if (trangthai == 1)
            {
                sql += " set TRANGTHAI = " + trangthai + ",ngayduyet = '" + ngay + "',nguoiduyet = '" + idgv + "' where IDHD =" + id;
            }
            else
            {
                sql += " set TRANGTHAI = " + trangthai + ",ngaysua = '" + ngay + "',nguoisua = '" + idgv + "' where IDHD =" + id;
            }
            if (sql != "")
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteNonQuery();
            }
            con.Close();
            return dr;
        }

        public int updateTINHTRANG(int id, int TINHTRANG, string ngay, string idgv)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            string sql = "";
            sql = "update HOATDONG"; // 0 Đăng đăng ký  1 Đóng form đăng ký //  2 Đóng form điểm danh
            sql += " set TINHTRANG = " + TINHTRANG + ",ngaysua = '" + ngay + "',nguoisua = '" + idgv + "' where idhd =" + id;
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteNonQuery();
            con.Close();
            return dr;
        }

        public int updatehd(int id_hd, string tieude, string noidung, string ngaybatdaudk, string ngayketthudk, int khoa, int idhk, int songuoi, int diem, string ghichu, int loai, string hinhanh, string nguoisua, string ngaysua, string dia_diem)
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();

            SqlCommand cmd = new SqlCommand("update HOATDONG set TIEUDE = N'" + tieude + "',NOIDUNG  = N'" + noidung + "',NGAYBATDAUDK  = N'" + ngaybatdaudk + "',NGAYKETTHUCDK  = N'" + ngayketthudk + "',GHICHU  = N'" + ghichu + "',DIEMRL  = N'" + diem + "',LOAI  = N'" + loai + "',SONGUOI = N'" + songuoi + "',ID_HK = N'" + idhk + "',ID_KHOA = N'" + khoa + "',HINHANH = N'" + hinhanh + "',NGUOISUA = N'" + nguoisua + "',NGAYSUA = N'" + ngaysua + "',DIADIEM = N'" + dia_diem + "' where IDHD =" + id_hd, con);
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteNonQuery();
            con.Close();

            return dr;
        }

        public int findCountHD()
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);

            SqlCommand cmd2 = new SqlCommand("select count(*) from HOATDONG where trangthai = 1 and NGAYDUYET IS NOT NULL", con);
            cmd2.CommandType = CommandType.Text;
            con.Open();
            Object kq = cmd2.ExecuteScalar();

            dr = int.Parse(kq.ToString());
            return dr;
        }

        public List<HOATDONG> findBy(int top)
        {
            List<HOATDONG> listHD = new List<HOATDONG>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select top " + top + " HOATDONG.* from HOATDONG where trangthai = 1 and NGAYDUYET IS NOT NULL order by NGAYDUYET desc", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                HOATDONG emp = new HOATDONG();
                emp.IDHD = dr.GetValue(0).ToString();
                emp.MAHD = dr.GetValue(1).ToString();
                emp.TIEUDE = dr.GetValue(2).ToString();
                emp.NOIDUNG = dr.GetValue(3).ToString();
                emp.NGUOITAO = dr.GetValue(4).ToString();
                emp.NGAYTAO = dr.GetValue(5).ToString();
                emp.NGUOISUA = dr.GetValue(6).ToString();
                emp.NGAYSUA = dr.GetValue(7).ToString();
                emp.NGUOIDUYET = dr.GetValue(8).ToString();
                emp.NGAYDUYET = dr.GetValue(9).ToString();
                emp.TRANGTHAI = int.Parse(dr.GetValue(10).ToString());
                emp.NGAYBATDAUDK = dr.GetValue(11).ToString();
                emp.NGAYKETTHUCDK = dr.GetValue(12).ToString();
                emp.GHICHU = dr.GetValue(13).ToString();
                emp.DIEMRL = dr.GetValue(14).ToString();
                emp.LOAI = int.Parse(dr.GetValue(15).ToString());
                emp.TINHTRANG = int.Parse(dr.GetValue(16).ToString());
                emp.SONGUOI = int.Parse(dr.GetValue(17).ToString());
                emp.HINHANH = dr.GetValue(18).ToString();
                emp.ID_HK = int.Parse(dr.GetValue(19).ToString());
                emp.ID_KHOA = int.Parse(dr.GetValue(20).ToString());
                emp.DIADIEM = dr.GetValue(21).ToString();
                listHD.Add(emp);
            }
            con.Close();
            return listHD;
        }

        public List<HOATDONG> layDanhSachSvDangKy()
        {
            List<HOATDONG> listHD = new List<HOATDONG>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select top 5 HOATDONG.* from HOATDONG,DANGKY where HOATDONG.trangthai = 1 and NGAYDUYET IS NOT NULL order by NGAYDUYET desc", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                HOATDONG emp = new HOATDONG();
                emp.IDHD = dr.GetValue(0).ToString();
                emp.MAHD = dr.GetValue(1).ToString();
                emp.TIEUDE = dr.GetValue(2).ToString();
                emp.NOIDUNG = dr.GetValue(3).ToString();
                emp.NGUOITAO = dr.GetValue(4).ToString();
                emp.NGAYTAO = dr.GetValue(5).ToString();
                emp.NGUOISUA = dr.GetValue(6).ToString();
                emp.NGAYSUA = dr.GetValue(7).ToString();
                emp.NGUOIDUYET = dr.GetValue(8).ToString();
                emp.NGAYDUYET = dr.GetValue(9).ToString();
                emp.TRANGTHAI = int.Parse(dr.GetValue(10).ToString());
                emp.NGAYBATDAUDK = dr.GetValue(11).ToString();
                emp.NGAYKETTHUCDK = dr.GetValue(12).ToString();
                emp.GHICHU = dr.GetValue(13).ToString();
                emp.DIEMRL = dr.GetValue(14).ToString();
                emp.LOAI = int.Parse(dr.GetValue(15).ToString());
                emp.TINHTRANG = int.Parse(dr.GetValue(16).ToString());
                emp.SONGUOI = int.Parse(dr.GetValue(17).ToString());
                emp.HINHANH = dr.GetValue(18).ToString();
                emp.ID_HK = int.Parse(dr.GetValue(19).ToString());
                emp.ID_KHOA = int.Parse(dr.GetValue(20).ToString());
                emp.DIADIEM = dr.GetValue(21).ToString();
                listHD.Add(emp);
            }
            con.Close();
            return listHD;
        }

        public List<HOATDONG> findByBuoiID(string CODE_ID)
        {
            List<HOATDONG> listHD = new List<HOATDONG>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select HOATDONG.*,ngtao.TENGV,ngsua.TENGV,ngduyet.TENGV,KHOA.TEN_KHOA,HOC_KI.TEN_HK,HOATDONGTHEONGAY.NGAYBATDAUDIEMDANH as ngay_bat_dau,HOATDONGTHEONGAY.trangthai as trang_thai_hd_buoi from HOATDONG INNER JOIN HOATDONGTHEONGAY ON HOATDONGTHEONGAY.IDHD = HOATDONG.IDHD LEFT JOIN GIANG_VIEN as ngtao ON ngtao.ID_GV = HOATDONG.NGUOITAO LEFT JOIN GIANG_VIEN as ngsua ON ngsua.ID_GV = HOATDONG.NGUOITAO LEFT JOIN GIANG_VIEN as ngduyet ON ngduyet.ID_GV = HOATDONG.NGUOIDUYET LEFT JOIN KHOA ON KHOA.ID_KHOA = HOATDONG.ID_KHOA LEFT JOIN HOC_KI ON HOC_KI.ID_HK = HOATDONG.ID_HK where HOATDONGTHEONGAY.IDBUOI =  '" + CODE_ID + "'", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                HOATDONG emp = new HOATDONG();
                emp.IDHD = dr.GetValue(0).ToString();
                emp.MAHD = dr.GetValue(1).ToString();
                emp.TIEUDE = dr.GetValue(2).ToString();
                emp.NOIDUNG = dr.GetValue(3).ToString();
                emp.NGUOITAO = dr.GetValue(4).ToString();
                emp.NGAYTAO = dr.GetValue(5).ToString();
                emp.NGUOISUA = dr.GetValue(6).ToString();
                emp.NGAYSUA = dr.GetValue(7).ToString();
                emp.NGUOIDUYET = dr.GetValue(8).ToString();
                emp.NGAYDUYET = dr.GetValue(9).ToString();
                emp.TRANGTHAI = int.Parse(dr.GetValue(10).ToString());
                emp.NGAYBATDAUDK = dr.GetValue(11).ToString();
                emp.NGAYKETTHUCDK = dr.GetValue(12).ToString();
                emp.GHICHU = dr.GetValue(13).ToString();
                emp.DIEMRL = dr.GetValue(14).ToString();
                emp.LOAI = int.Parse(dr.GetValue(15).ToString());
                emp.TINHTRANG = int.Parse(dr.GetValue(16).ToString());
                emp.SONGUOI = int.Parse(dr.GetValue(17).ToString());
                emp.HINHANH = dr.GetValue(18).ToString();
                emp.ID_HK = int.Parse(dr.GetValue(19).ToString());
                emp.ID_KHOA = int.Parse(dr.GetValue(20).ToString());
                emp.name_ngtao = dr.GetValue(22).ToString();
                emp.name_ngsua = dr.GetValue(23).ToString();
                emp.name_ngduyet = dr.GetValue(24).ToString();
                emp.name_khoa = dr.GetValue(25).ToString();
                emp.name_hocky = dr.GetValue(26).ToString();
                emp.DIADIEM = dr.GetValue(21).ToString();
                emp.NGAY_BAT_DAU = dr.GetValue(27).ToString();
                emp.trang_thai_buoi = dr.GetValue(28).ToString();
                listHD.Add(emp);
            }
            con.Close();
            return listHD;
        }
        public int khoaFormDangKyAll()
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            //con.Open();
            //string sql = "";
            //sql = "update HOATDONG set TINHTRANG = 1 where NGAYKETTHUCDK < FORMAT(GETDATE(),'yyyy-MM-dd')";
            //SqlCommand cmd = new SqlCommand(sql, con);
            //cmd.CommandType = CommandType.Text;
            //dr = cmd.ExecuteNonQuery();
            //con.Close();
            con.Open();
            string sql2 = "";
            sql2 = "update HOATDONGTHEONGAY set TRANGTHAI = 4 from HOATDONG where disabled = 0 and HOATDONG.IDHD = HOATDONGTHEONGAY.IDHD and NGAYKETTHUCDK < FORMAT(GETDATE(),'yyyy-MM-dd') and HOATDONGTHEONGAY.TRANGTHAI = 0 and HOATDONG.TRANGTHAI = 1";
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            cmd2.CommandType = CommandType.Text;
            dr = cmd2.ExecuteNonQuery();
            con.Close();

            return dr;
        }
        public int khoaFormDiemDanhAll()
        {
            int dr = 0;
            SqlConnection con = new SqlConnection(conf);
            con.Open();
            string sql2 = "";
            sql2 = "update HOATDONGTHEONGAY set TRANGTHAI = 3 where disabled = 0 and NGAYBATDAUDIEMDANH < FORMAT(GETDATE(),'yyyy-MM-dd') and HOATDONGTHEONGAY.TRANGTHAI IN (2)";
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            cmd2.CommandType = CommandType.Text;
            dr = cmd2.ExecuteNonQuery();
            con.Close();

            return dr;
        }
        public IEnumerable<HOATDONG> getHoatDongPhanTrang(int page, int pagesize)
        {
            List<HOATDONG> listHD = new List<HOATDONG>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select HOATDONG.* from HOATDONG where trangthai = 1 and NGAYDUYET IS NOT NULL order by NGAYDUYET desc", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                HOATDONG emp = new HOATDONG();
                emp.IDHD = dr.GetValue(0).ToString();
                emp.MAHD = dr.GetValue(1).ToString();
                emp.TIEUDE = dr.GetValue(2).ToString();
                emp.NOIDUNG = dr.GetValue(3).ToString();
                emp.NGUOITAO = dr.GetValue(4).ToString();
                emp.NGAYTAO = dr.GetValue(5).ToString();
                emp.NGUOISUA = dr.GetValue(6).ToString();
                emp.NGAYSUA = dr.GetValue(7).ToString();
                emp.NGUOIDUYET = dr.GetValue(8).ToString();
                emp.NGAYDUYET = dr.GetValue(9).ToString();
                emp.TRANGTHAI = int.Parse(dr.GetValue(10).ToString());
                emp.NGAYBATDAUDK = dr.GetValue(11).ToString();
                emp.NGAYKETTHUCDK = dr.GetValue(12).ToString();
                emp.GHICHU = dr.GetValue(13).ToString();
                emp.DIEMRL = dr.GetValue(14).ToString();
                emp.LOAI = int.Parse(dr.GetValue(15).ToString());
                emp.TINHTRANG = int.Parse(dr.GetValue(16).ToString());
                emp.SONGUOI = int.Parse(dr.GetValue(17).ToString());
                emp.HINHANH = dr.GetValue(18).ToString();
                emp.ID_HK = int.Parse(dr.GetValue(19).ToString());
                emp.ID_KHOA = int.Parse(dr.GetValue(20).ToString());
                emp.DIADIEM = dr.GetValue(21).ToString();
                listHD.Add(emp);
            }
            con.Close();
            return listHD.OrderBy(x => x.IDHD).ToPagedList(page, pagesize);
        }

        public int laySoNguoiDk(string ID_HD)
        {
            List<HOATDONG> listHD = new List<HOATDONG>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select SONGUOI from HOATDONG where  IDHD = " + ID_HD + "", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            Object kq = cmd.ExecuteScalar();

            con.Close();
            return int.Parse(kq.ToString());
        }

        public List<HOATDONG> layDanhHoatDongChuaChotSV(string IDSV, string idhk)
        {
            List<HOATDONG> listHD = new List<HOATDONG>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("SELECT HOATDONG.MAHD,HOATDONG.IDHD,HOATDONG.TIEUDE,HOATDONGTHEONGAY.IDBUOI,HOATDONGTHEONGAY.NGAYBATDAUDIEMDANH FROM HOATDONGTHEONGAY  LEFT JOIN DANGKY ON HOATDONGTHEONGAY.IDBUOI = DANGKY.IDBUOI   INNER JOIN HOATDONG ON HOATDONGTHEONGAY.IDHD = HOATDONG.IDHD WHERE DANGKY.IDSV = " + IDSV + "   AND HOATDONGTHEONGAY.TRANGTHAI IN (5,3)  and HOATDONGTHEONGAY.disabled = 0  and DANGKY.TRANGTHAI NOT IN (1,2) and HOATDONG.ID_HK = " + idhk + "   and HOATDONGTHEONGAY.IDBUOI  not in  ( SELECT HOATDONGTHEONGAY.IDBUOI FROM HOATDONGTHEONGAY  INNER JOIN HOATDONG ON HOATDONGTHEONGAY.IDHD = HOATDONG.IDHD INNER JOIN PHAN_HOI ON PHAN_HOI.IDBUOI = HOATDONGTHEONGAY.IDBUOI    WHERE PHAN_HOI.IDSV = " + IDSV + " AND HOATDONGTHEONGAY.TRANGTHAI IN(5,3) and HOATDONGTHEONGAY.disabled = 0  and HOATDONG.ID_HK = " + idhk + ") and (DATEDIFF(day, HOATDONGTHEONGAY.NGAYBATDAUDIEMDANH,GETDATE()) < 7  or DATEDIFF(day, HOATDONGTHEONGAY.NGAYBATDAUDIEMDANH,GETDATE()) = 7 )  UNION ALL	  SELECT HOATDONG.MAHD,HOATDONG.IDHD,HOATDONG.TIEUDE,HOATDONGTHEONGAY.IDBUOI,HOATDONGTHEONGAY.NGAYBATDAUDIEMDANH FROM HOATDONGTHEONGAY INNER JOIN HOATDONG ON HOATDONGTHEONGAY.IDHD = HOATDONG.IDHD     where  HOATDONGTHEONGAY.disabled = 0 and HOATDONGTHEONGAY.IDBUOI  not in (SELECT HOATDONGTHEONGAY.IDBUOI FROM HOATDONGTHEONGAY      LEFT JOIN DANGKY ON HOATDONGTHEONGAY.IDBUOI = DANGKY.IDBUOI     INNER JOIN HOATDONG ON HOATDONGTHEONGAY.IDHD = HOATDONG.IDHD      WHERE DANGKY.IDSV = " + IDSV + " AND HOATDONGTHEONGAY.TRANGTHAI IN (5,3)   and HOATDONGTHEONGAY.disabled = 0 and DANGKY.TRANGTHAI NOT IN (1,2)   and HOATDONG.ID_HK = " + idhk + ")   AND HOATDONGTHEONGAY.TRANGTHAI IN (5,3)   and HOATDONGTHEONGAY.disabled = 0 and HOATDONG.ID_HK = " + idhk + " and HOATDONGTHEONGAY.IDBUOI  NOT IN (SELECT HOATDONGTHEONGAY.IDBUOI FROM HOATDONGTHEONGAY  INNER JOIN HOATDONG ON HOATDONGTHEONGAY.IDHD = HOATDONG.IDHD   INNER JOIN PHAN_HOI ON PHAN_HOI.IDBUOI = HOATDONGTHEONGAY.IDBUOI   WHERE PHAN_HOI.IDSV = " + IDSV + " AND HOATDONGTHEONGAY.TRANGTHAI IN (5,3) and HOATDONGTHEONGAY.disabled = 0  and HOATDONG.ID_HK = " + idhk + ")   and HOATDONGTHEONGAY.IDBUOI  not in (SELECT HOATDONGTHEONGAY.IDBUOI FROM HOATDONGTHEONGAY       LEFT JOIN DIEMDANH ON HOATDONGTHEONGAY.IDBUOI = DIEMDANH.IDBUOI   INNER JOIN HOATDONG ON HOATDONGTHEONGAY.IDHD = HOATDONG.IDHD    WHERE DIEMDANH.IDSV = " + IDSV + " AND HOATDONGTHEONGAY.TRANGTHAI IN (5,3)  and HOATDONGTHEONGAY.disabled = 0  and HOATDONG.ID_HK = " + idhk + ") and (DATEDIFF(day, HOATDONGTHEONGAY.NGAYBATDAUDIEMDANH,GETDATE()) < 7  or DATEDIFF(day, HOATDONGTHEONGAY.NGAYBATDAUDIEMDANH,GETDATE()) = 7 ) ", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                HOATDONG emp = new HOATDONG();
                emp.MAHD = dr.GetValue(0).ToString();
                emp.IDHD = dr.GetValue(1).ToString();
                emp.TIEUDE = dr.GetValue(2).ToString();
                emp.IDBUOI = dr.GetValue(3).ToString();
                emp.NGAY_BAT_DAU = dr.GetValue(4).ToString();
                listHD.Add(emp);
            }
            con.Close();
            return listHD;
        }

        public List<HOATDONG> findByIdHk(string idhk)
        {
            List<HOATDONG> listHD = new List<HOATDONG>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select HOATDONG.MAHD,HOATDONG.TIEUDE,HOATDONGTHEONGAY.LOAI_BUOI,HOATDONGTHEONGAY.NGAYBATDAUDIEMDANH,HOATDONGTHEONGAY.IDBUOI from HOATDONG INNER JOIN HOATDONGTHEONGAY ON HOATDONG.IDHD = HOATDONGTHEONGAY.IDHD where ID_HK = " + idhk + " and HOATDONGTHEONGAY.TRANGTHAI IN (5,6)  and HOATDONGTHEONGAY.disabled = 0 order by HOATDONGTHEONGAY.NGAYBATDAUDIEMDANH desc", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                HOATDONG emp = new HOATDONG();
                emp.MAHD = dr.GetValue(0).ToString();
                emp.TIEUDE = dr.GetValue(1).ToString();
                emp.LOAI_BUOI = dr.GetValue(2).ToString();
                emp.NGAY_BAT_DAU = dr.GetValue(3).ToString();
                emp.IDBUOI = dr.GetValue(4).ToString();
                listHD.Add(emp);
            }
            con.Close();
            return listHD;
        }

        public List<HOATDONG> showKetQua(string IDSV, string idhk)
        {
            List<HOATDONG> listHD = new List<HOATDONG>();
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd = new SqlCommand("select HOATDONG.MAHD,HOATDONG.TIEUDE,HOATDONGTHEONGAY.NGAYBATDAUDIEMDANH,HOATDONGTHEONGAY.LOAI_BUOI,DANGKY.GHICHU,N'Không tham gia' as 'hinh_thuc',HOATDONG.DIEMRL from DANGKY inner join HOATDONGTHEONGAY on HOATDONGTHEONGAY.IDBUOI = DANGKY.IDBUOI inner join HOATDONG on HOATDONG.IDHD = HOATDONGTHEONGAY.IDHD where DANGKY.TRANGTHAI = 3 and IDSV = " + IDSV + " and HOATDONG.ID_HK = " + idhk + " and HOATDONGTHEONGAY.trangthai = 8 union all select HOATDONG.MAHD,HOATDONG.TIEUDE,HOATDONGTHEONGAY.NGAYBATDAUDIEMDANH,HOATDONGTHEONGAY.LOAI_BUOI,DIEMDANH.GHICHU,N'Tham gia' as 'hinh_thuc',HOATDONG.DIEMRL from DIEMDANH inner join HOATDONGTHEONGAY on HOATDONGTHEONGAY.IDBUOI = DIEMDANH.IDBUOI inner join HOATDONG on HOATDONG.IDHD = HOATDONGTHEONGAY.IDHD where IDSV = " + IDSV + " and HOATDONG.ID_HK = " + idhk + " and HOATDONGTHEONGAY.TrangThai = 8  and HOATDONGTHEONGAY.disabled = 0 union all select HOATDONG.MAHD,HOATDONG.TIEUDE,HOATDONGTHEONGAY.NGAYBATDAUDIEMDANH,HOATDONGTHEONGAY.LOAI_BUOI,PHAN_HOI.NOIDUNG as 'GHICHU',N'Phản hồi hoạt động' as 'hinh_thuc',HOATDONG.DIEMRL from PHAN_HOI inner join HOATDONGTHEONGAY on HOATDONGTHEONGAY.IDBUOI = PHAN_HOI.IDBUOI inner join HOATDONG on HOATDONG.IDHD = HOATDONGTHEONGAY.IDHD where IDSV = " + IDSV + " and HOATDONG.ID_HK = " + idhk + " and PHAN_HOI.TRANGTHAI = 1 and HOATDONGTHEONGAY.trangthai = 8  and HOATDONGTHEONGAY.disabled = 0", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                HOATDONG emp = new HOATDONG();
                emp.MAHD = dr.GetValue(0).ToString();
                emp.TIEUDE = dr.GetValue(1).ToString();
                emp.NGAY_BAT_DAU = dr.GetValue(2).ToString();
                emp.LOAI_BUOI = dr.GetValue(3).ToString();
                emp.GHICHU = dr.GetValue(4).ToString();
                emp.HINH_THUC = dr.GetValue(5).ToString();
                emp.DIEMRL = dr.GetValue(6).ToString();
                listHD.Add(emp);
            }
            con.Close();
            return listHD;
        }

        public int demSL(string idhk, string ngaybd, string ngaykt)
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
            SqlConnection con = new SqlConnection(conf);
            SqlCommand cmd2 = new SqlCommand("select count(*) from HOATDONG INNER JOIN HOATDONGTHEONGAY ON HOATDONGTHEONGAY.IDHD = HOATDONG.IDHD where HOATDONGTHEONGAY.trangthai NOT IN (6,7,8,1,5) and HOATDONG.ID_HK = " + idhk + " and HOATDONGTHEONGAY.disabled = 0 " + sql, con);
            cmd2.CommandType = CommandType.Text;
            con.Open();
            Object kq = cmd2.ExecuteScalar();
            int dr = int.Parse(kq.ToString());
            con.Close();
            return dr;
        }
    }
}