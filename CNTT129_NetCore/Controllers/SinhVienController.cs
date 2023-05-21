using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.Json;
using CNTT129_NetCore.Controllers;
using CNTT129.Models;
using QRCoder;

namespace CNTT129.Controllers
{
    public class SinhVienController : BaseController
    {
        //
        // GET: /SinhVien/

        public ActionResult Login()
        {
            if ((Boolean)Session["logsv"] == true)
            {
                return RedirectToAction("Home", "SinhVien");
            }
            return View();
        }

        public ActionResult Home()
        {
            if ((Boolean)Session["logsv"] == false)
            {
                return RedirectToAction("Login", "SinhVien");
            }
            DANGKY dang_ky = new DANGKY();
            List<DANGKY> list_dang_ky = new List<DANGKY>();
            list_dang_ky = dang_ky.danhSachHDDK(Session["id_sv"].ToString(), "");
            ViewData["list_dang_ky"] = list_dang_ky;
            List<DANGKY> list_dang_ky_huy = new List<DANGKY>();
            list_dang_ky_huy = dang_ky.danhSachHDHuy(Session["id_sv"].ToString(), "","2,3,4,5");
            ViewData["list_ds_hd_huy"] = list_dang_ky_huy;
            return View();
        }

        public ActionResult list_ds_dang_ky()
        {
            return View();
        }

        public ActionResult list_ds_hd_huy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string login, string username, string password)
        {
            if (!string.IsNullOrEmpty(login))
            {
                SINHVIEN sv = new SINHVIEN();
                var kq = sv.login(username, password);
                if (kq.Count == 0)
                {
                    ViewBag.TB = "Tài Khoản bạn khóa hoặc sai mật mật khẩu! Vui lòng liên hệ admin để đc giải quyết";
                }
                else
                {
                    foreach (var item in kq)
                    {
                        Session["Ten_sv"] = item.TENSV;
                        Session["ma_sv"] = item.MASV;
                        Session["id_sv"] = item.ID_SV;
                    }
                    Session["logsv"] = true;
                    return RedirectToAction("Home", "SinhVien");
                }
            }
            return View();
        }
        // Chi tiết hoạt động
        public ActionResult ChiTietHoatDong(string idhd)
        {
            if ((Boolean)Session["logsv"] == false)
            {
                return RedirectToAction("Login", "SinhVien");
            }
            HOATDONG hk = new HOATDONG();
            var data = hk.findByID(idhd.ToString());
            foreach (var item in data)
            {
                ViewBag.ma_hd = item.MAHD;
                ViewBag.tieu_de = item.TIEUDE;
                ViewBag.nguoi_tao = item.name_ngtao;
                ViewBag.so_nguoi = item.SONGUOI;
                ViewBag.diem = item.DIEMRL;
                ViewBag.khoa = item.ID_KHOA;
                ViewBag.hoc_ki = item.ID_HK;
                DateTime ngay_bd = DateTime.Parse(item.NGAYBATDAUDK);
                ViewBag.ngay_bd2 = ngay_bd;
                var datebd = ngay_bd.ToString("dd/MM/yyyy");
                ViewBag.ngay_bd = datebd;
                DateTime ngay_kt = DateTime.Parse(item.NGAYKETTHUCDK);
                ViewBag.ngay_kt2 = ngay_kt;
                var datekt = ngay_bd.ToString("dd/MM/yyyy");
                ViewBag.ngay_kt = datekt;
                ViewBag.loai_hd = item.LOAI;
                ViewBag.trang_thai = item.TRANGTHAI;
                ViewBag.tinh_trang = item.TINHTRANG;
                ViewBag.noi_dung = item.NOIDUNG;
                ViewBag.dia_diem = item.DIADIEM;
                ViewBag.ghi_chu = item.GHICHU;
            }
            ViewBag.id_hd = idhd;
            return View();
        }

        //Điểm danh hoạt động
        public ActionResult DiemDanhHoatDong(string idbuoi)
        {
            if ((Boolean)Session["logsv"] == false)
            {
                return RedirectToAction("Login", "SinhVien");
            }
            HOATDONG hk = new HOATDONG();
            var data = hk.findByBuoiID(idbuoi.ToString());
            foreach (var item in data)
            {
                ViewBag.ma_hd = item.MAHD;
                ViewBag.tieu_de = item.TIEUDE;
                ViewBag.nguoi_tao = item.name_ngtao;
                ViewBag.so_nguoi = item.SONGUOI;
                ViewBag.diem = item.DIEMRL;
                ViewBag.khoa = item.ID_KHOA;
                ViewBag.hoc_ki = item.ID_HK;
                DateTime ngay_bd = DateTime.Parse(item.NGAYBATDAUDK);
                var datebd = ngay_bd.ToString("dd/MM/yyyy");
                ViewBag.ngay_bd = datebd;
                DateTime ngay_kt = DateTime.Parse(item.NGAYKETTHUCDK);
                var datekt = ngay_bd.ToString("dd/MM/yyyy");
                ViewBag.ngay_kt = datekt;
                ViewBag.loai_hd = item.LOAI;
                ViewBag.trang_thai = item.TRANGTHAI;
                ViewBag.noi_dung = item.NOIDUNG;
                ViewBag.dia_diem = item.DIADIEM;
                ViewBag.ghi_chu = item.GHICHU;
                ViewBag.trang_thai_buoi = item.trang_thai_buoi;
                DateTime ngay_bd_hd = DateTime.Parse(item.NGAY_BAT_DAU);
                var dateBdhd = ngay_bd_hd.ToString("dd/MM/yyyy");
                ViewBag.ngay_bd_hd = dateBdhd;
            }
            ViewBag.idbuoi = idbuoi;
            return View();
        }
        public ActionResult TinTuc(int page = 1)
        {
            if ((Boolean)Session["logsv"] == false)
            {
                return RedirectToAction("Login", "SinhVien");
            }
            var obj = new HOATDONG();
            var emp = obj.getHoatDongPhanTrang(page, 10);
            return View(emp);
        }
        public JsonResult KtraTrangThaiHD(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            HoatDongBuoi hd = new HoatDongBuoi();
            foreach (var item in jsonCart)
            {
                ViewData["data_list"] = hd.findByIDBuoi2(item["id_buoi"]);
            }
           
            return Json(new
            {
                data = ViewData["data_list"],
            });
        }

        public JsonResult saveDangKy(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            DANGKY dk = new DANGKY();
            HOATDONG hd = new HOATDONG();
            foreach (var item in jsonCart)
            {
                if (dk.ktraAll(item["idsv"], item["id_buoi"]) == 0)
                {
                    dk.save(item["id_buoi"], item["idsv"], item["MASV"], item["SDT"], item["email"], item["loi_nhan"], item["NGAYDANGKY"]);
                }
                else
                {
                    dk.capnhat(item["id_buoi"], item["idsv"],"0");
                }
                
            }
            return Json(new
            {
                data = true,
            });
        }

        public JsonResult KiemTraTrangThaiForm(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            HoatDongBuoi hdb = new HoatDongBuoi();
            var id_buoi = jsonCart["idbuoi"];
            var data = hdb.findByIDBuoi(id_buoi);    
            return Json(new
            {
                data = data[0],
            });
        }

        public JsonResult saveDiemDanh(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            DIEMDANH diemdanh = new DIEMDANH();
            HOATDONG hd = new HOATDONG();
            DANGKY dk = new DANGKY();
            if (diemdanh.ktraAll(jsonCart[0]["idsv"], jsonCart[0]["id_buoi"]) == 0)
            {
                diemdanh.save(jsonCart[0]["id_buoi"], jsonCart[0]["idsv"], jsonCart[0]["MASV"], jsonCart[0]["SDT"], jsonCart[0]["email"], jsonCart[0]["loi_nhan"], jsonCart[0]["NGAYDANGKY"]);
            }
            else
            {
                diemdanh.capnhat(jsonCart[0]["id_buoi"], jsonCart[0]["idsv"], "0");
            }
            dk.capnhatPhieuDangKyDaThamGia(jsonCart[0]["id_buoi"], jsonCart[0]["idsv"]);
            return Json(new
            {
                data = true,
            });
        }
        [HttpPost]
        public PartialViewResult layDanhSachSVDangKyTheoHocKy(string id_hk, string id_sv)
        {
            DANGKY dang_ky = new DANGKY();
            List<DANGKY> list_dang_ky = new List<DANGKY>();
            list_dang_ky = dang_ky.danhSachHDDK(id_sv, id_hk);
            ViewData["list_dang_ky"] = list_dang_ky;
            return PartialView("list_ds_dang_ky", ViewData["list_dang_ky"]);
        }

        [HttpPost]
        public PartialViewResult layDanhSachSVHuyTheoHocKy(string id_hk, string id_sv)
        {
            DANGKY dang_ky = new DANGKY();
            List<DANGKY> list_dang_ky_huy = new List<DANGKY>();
            list_dang_ky_huy = dang_ky.danhSachHDHuy(id_sv, id_hk,"2,3");
            ViewData["list_ds_hd_huy"] = list_dang_ky_huy;
            return PartialView("list_ds_hd_huy", ViewData["list_ds_hd_huy"]);
        }

        // Tình hình tham gia hoạt động
        public ActionResult TinhThamGiaHDCuaSV()
        {
            if ((Boolean)Session["logsv"] == false)
            {
                return RedirectToAction("Login", "SinhVien");
            }
            DANGKY dang_ky = new DANGKY();
            DIEMDANH diem_danh = new DIEMDANH();
            List<DANGKY> list_dang_ky = new List<DANGKY>();
            list_dang_ky = dang_ky.danhSachHDDK(Session["id_sv"].ToString(), "");
            ViewData["list_dang_ky"] = list_dang_ky;
            List<DANGKY> list_dang_ky_huy = new List<DANGKY>();
            list_dang_ky_huy = dang_ky.danhSachHDHuy(Session["id_sv"].ToString(), "","2,5");
            ViewData["list_ds_hd_huy"] = list_dang_ky_huy;
            List<DANGKY> list_khong_tham_gia = new List<DANGKY>();
            list_khong_tham_gia = dang_ky.danhSachHDHuy(Session["id_sv"].ToString(), "", "3");
            ViewData["list_khong_tham_gia"] = list_khong_tham_gia;
            List<DANGKY> list_sv_huy = new List<DANGKY>();
            list_sv_huy = dang_ky.danhSachHDHuy(Session["id_sv"].ToString(), "", "4");
            ViewData["list_sv_huy"] = list_sv_huy;

            List<DIEMDANH> list_sv_diem_danh = new List<DIEMDANH>();
            list_sv_diem_danh = diem_danh.danhSachHDDD(Session["id_sv"].ToString(), "");
            ViewData["list_sv_diem_danh"] = list_sv_diem_danh;
            return View();
        }

        public ActionResult listTinhTrangHoatDong()
        {
            return View();
        }

        [HttpPost]
        public PartialViewResult layDanhSachTinhTrangThamGiaHDSV(string id_hk, string id_sv)
        {
            DANGKY dang_ky = new DANGKY();
            DIEMDANH diem_danh = new DIEMDANH();
            List<DANGKY> list_dang_ky = new List<DANGKY>();
            list_dang_ky = dang_ky.danhSachHDDK(id_sv, id_hk);
            ViewData["list_dang_ky"] = list_dang_ky;
            List<DANGKY> list_dang_ky_huy = new List<DANGKY>();
            list_dang_ky_huy = dang_ky.danhSachHDHuy(id_sv, id_hk, "2,5");
            ViewData["list_ds_hd_huy"] = list_dang_ky_huy;
            List<DANGKY> list_khong_tham_gia = new List<DANGKY>();
            list_khong_tham_gia = dang_ky.danhSachHDHuy(id_sv, id_hk, "3");
            ViewData["list_khong_tham_gia"] = list_khong_tham_gia;
            List<DANGKY> list_sv_huy = new List<DANGKY>();
            list_sv_huy = dang_ky.danhSachHDHuy(id_sv, id_hk, "4");
            ViewData["list_sv_huy"] = list_sv_huy;

            List<DIEMDANH> list_sv_diem_danh = new List<DIEMDANH>();
            list_sv_diem_danh = diem_danh.danhSachHDDD(id_sv, id_hk);
            ViewData["list_sv_diem_danh"] = list_sv_diem_danh;
            return PartialView("listTinhTrangHoatDong", ViewData["list_ds_hd_huy"]);
        }

        [HttpPost]
        public PartialViewResult huyHDSV(string id_hk, string id_sv,string iddk)
        {
            DANGKY dang_ky = new DANGKY();
            DIEMDANH diem_danh = new DIEMDANH();
            dang_ky.huyDangKySinhVien(iddk, "4");
            List<DANGKY> list_dang_ky = new List<DANGKY>();
            list_dang_ky = dang_ky.danhSachHDDK(id_sv, id_hk);
            ViewData["list_dang_ky"] = list_dang_ky;
            List<DANGKY> list_dang_ky_huy = new List<DANGKY>();
            list_dang_ky_huy = dang_ky.danhSachHDHuy(id_sv, id_hk, "2,5");
            ViewData["list_ds_hd_huy"] = list_dang_ky_huy;
            List<DANGKY> list_khong_tham_gia = new List<DANGKY>();
            list_khong_tham_gia = dang_ky.danhSachHDHuy(id_sv, id_hk, "3");
            ViewData["list_khong_tham_gia"] = list_khong_tham_gia;
            List<DANGKY> list_sv_huy = new List<DANGKY>();
            list_sv_huy = dang_ky.danhSachHDHuy(id_sv, id_hk, "4");
            ViewData["list_sv_huy"] = list_sv_huy;

            List<DIEMDANH> list_sv_diem_danh = new List<DIEMDANH>();
            list_sv_diem_danh = diem_danh.danhSachHDDD(id_sv, id_hk);
            ViewData["list_sv_diem_danh"] = list_sv_diem_danh;

            return PartialView("listTinhTrangHoatDong");
        }

        public ActionResult DangXuat()
        {
            Session["Ten_sv"] = "";
            Session["ma_sv"] = "";
            Session["id_sv"] = "";
            Session["logsv"] = false;
            if ((Boolean)Session["logsv"] == false)
            {
                return RedirectToAction("Login", "SinhVien");
            }
            return View();
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {
            //if (Request.Files.Count > 0)
            //{
            //    try
            //    {
            //        HttpFileCollectionBase files = Request.Files;
            //        for (int i = 0; i < files.Count; i++)
            //        {

            //            HttpPostedFileBase file = files[i];
            //            string fname;

            //            if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
            //            {
            //                string[] testfiles = file.FileName.Split(new char[] { '\\' });
            //                fname = testfiles[testfiles.Length - 1];
            //            }
            //            else
            //            {
            //                fname = file.FileName;
            //            }

            //            fname = Path.Combine(Server.MapPath("~/IMG/"), fname);
            //            file.SaveAs(fname);
            //        }
            //        // Returns message that successfully uploaded  
            //        return Json("File Uploaded Successfully!");
            //    }
            //    catch (Exception ex)
            //    {
            //        return Json("Error occurred. Error details: " + ex.Message);
            //    }
            //}
            //else
            //{
            //    return Json("No files selected.");
            //}
            return Json("No files selected.");
        }
        // Phan Hồi sinh viên

        public ActionResult Phan_Hoi()
        {
            if ((Boolean)Session["logsv"] == false)
            {
                return RedirectToAction("Login", "SinhVien");
            }
            return View();
        }

        [HttpPost]
        public PartialViewResult ChangeLoaiPhanHoi(string loai, string id_sv)
        {
            HOATDONG hoat_dong = new HOATDONG();
            VI_PHAM vi_pham = new VI_PHAM();
            var view = "";
            HOCKY hocky = new HOCKY();
            var menu_hocky = hocky.find();
            var idhk = "";
            foreach (var item in menu_hocky)
            {
                idhk = item.IDHK;
            }
            if (loai == "1")
            {
                ViewData["data_list"] = hoat_dong.layDanhHoatDongChuaChotSV(id_sv, idhk);
                view = "tb_hoat_dong";
            }
            if (loai == "2")
            {
                ViewData["data_list"] = vi_pham.layViPhamSinhVienDaDuyet(id_sv, idhk);
                view = "tb_vi_pham";
            }

            return PartialView(view);
        }

        public JsonResult savePhanHoi(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            PHAN_HOI phan_hoi = new PHAN_HOI();
            HOCKY hocky = new HOCKY();
            var menu_hocky = hocky.find();
            string tieu_de = jsonCart[0]["tieu_de"];
            string loai = jsonCart[0]["loai"];
            string hoat_dong = jsonCart[0]["hoat_dong"];
            string loi_nhan = jsonCart[0]["loi_nhan"];
            string idsv = jsonCart[0]["idsv"];
            string tep = jsonCart[0]["tep"];
            string ngay_tao = jsonCart[0]["ngay_tao"];
            var idhk = "";
            foreach (var item in menu_hocky)
            {
                idhk = item.IDHK;
            }
            int reslut = phan_hoi.save(tieu_de, loai, hoat_dong, loi_nhan, idsv, tep, ngay_tao, idhk);

            return Json(new
            {
                res = reslut,
            });
        }

        // danh sách phản hồi
        public ActionResult List_Phan_Hoi()
        {
            if ((Boolean)Session["logsv"] == false)
            {
                return RedirectToAction("Login", "SinhVien");
            }
            PHAN_HOI hk = new PHAN_HOI();
            DateTime now = DateTime.Now;
            ViewData["list_phan_hoi"] = hk.find("", "", now.Year + "/" + now.Month + "/" + now.Day, now.Year + "/" + now.Month + "/" + now.Day,"", Session["id_sv"].ToString());
            ViewBag.start_date = now.Day + "/" + now.Month + "/" + now.Year;
            ViewBag.end_date = now.Day + "/" + now.Month + "/" + now.Year;
            return View();
        }

        [HttpPost]
        public ActionResult List_Phan_Hoi(string find, string ma_ph, string trang_thai, string start_date, string end_date, string loai)
        {
            if (!string.IsNullOrEmpty(find))
            {
                PHAN_HOI hk = new PHAN_HOI();
                var dateBd = start_date.Split('/');
                var datekt = end_date.Split('/');
                ViewData["list_phan_hoi"] = hk.find(ma_ph, trang_thai, dateBd[2] + '/' + dateBd[1] + '/' + dateBd[0], datekt[2] + '/' + datekt[1] + '/' + datekt[0], loai, Session["id_sv"].ToString());
                ViewBag.ma_ph = ma_ph;
                ViewBag.selected = trang_thai;
                ViewBag.selected_loai = loai;
                ViewBag.start_date = start_date;
                ViewBag.end_date = end_date;
            }
            return View();
        }

        public JsonResult updatePhanHoi(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            PHAN_HOI phan_hoi = new PHAN_HOI();

            string id = jsonCart[0]["ID"];

            int reslut = phan_hoi.updateTT(id,"3");

            return Json(new
            {
                res = reslut,
            });
        }

        //Kết quả
        public ActionResult KetQua()
        {
            if ((Boolean)Session["logsv"] == false)
            {
                return RedirectToAction("Login", "SinhVien");
            }
            HOCKY hocky = new HOCKY();
            var menu_hocky = hocky.find();
            var idhk = "";
            foreach (var item in menu_hocky)
            {
                idhk = item.IDHK;
            }
            ViewData["data"] = hocky.find2(idhk);
            return View();
        }

        public ActionResult HOATDONGNGOAI()
        {
            if ((Boolean)Session["logsv"] == false)
            {
                return RedirectToAction("Login", "SinhVien");
            }
            HOATDONGNGOAI hk = new HOATDONGNGOAI();
            DateTime now = DateTime.Now;
            ViewBag.start_date = now.Day + "/" + now.Month + "/" + now.Year;
            ViewBag.end_date = now.Day + "/" + now.Month + "/" + now.Year;
            ViewData["data"] = hk.find("",Session["id_sv"].ToString(), "", now.Year + "/" + now.Month + "/" + now.Day, now.Year + "/" + now.Month + "/" + now.Day, "0", "", "0");
            return View();
        }

        [HttpPost]
        public ActionResult HOATDONGNGOAI(string find, string ma_hd, string lhdn, string trang_thai, string start_date, string end_date, string loai, string hocky)
        {
            if (!string.IsNullOrEmpty(find))
            {
                HOATDONGNGOAI hk = new HOATDONGNGOAI();
                var dateBd = start_date.Split('/');
                var datekt = end_date.Split('/');
                ViewData["data"] = hk.find(ma_hd,Session["id_sv"].ToString(), trang_thai, dateBd[2] + '/' + dateBd[1] + '/' + dateBd[0], datekt[2] + '/' + datekt[1] + '/' + datekt[0], hocky, loai, lhdn);
                ViewBag.ma_hd = ma_hd;
                ViewBag.selected = trang_thai;
                ViewBag.selected_hocky = hocky;
                ViewBag.selected_loai = loai;
                ViewBag.start_date = start_date;
                ViewBag.end_date = end_date;
                ViewBag.selected_lhdn = lhdn;
            }
            return View();
        }

        public JsonResult findHdNgoaiID(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            HOATDONGNGOAI hdn = new HOATDONGNGOAI();
            int id = jsonCart[0]["id"];
            return Json(new
            {
                res = hdn.findByID(id),
            });
        }
        public JsonResult saveHdNgoai2(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            HOATDONGNGOAI hdn = new HOATDONGNGOAI();
            HOCKY hocky = new HOCKY();
            var menu_hocky = hocky.find();
            var idhk = "";
            foreach (var item in menu_hocky)
            {
                idhk = item.IDHK;
            }
            return Json(new
            {

                res = hdn.save(jsonCart[0]["id_hd"], jsonCart[0]["noi_dung"], jsonCart[0]["dia_diem"], idhk, jsonCart[0]["hinh_anh"], jsonCart[0]["sinh_vien"], Session["id_sv"].ToString(), "1", jsonCart[0]["lhdn_edit"]),
            });
        }

        public JsonResult updateHdNgoaiTT(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            HOATDONGNGOAI hdn = new HOATDONGNGOAI();
            return Json(new
            {
                res = hdn.updateTT(jsonCart[0]["ID"], jsonCart[0]["trangthai"]),
            });
        }

        public JsonResult KtraMk(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            SINHVIEN sv = new SINHVIEN();
            return Json(new
            {
                res = sv.KtraMk(jsonCart[0]["ID"], jsonCart[0]["mk_cu"]),
            });
        }

        public JsonResult updateMk(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            SINHVIEN sv = new SINHVIEN();
            return Json(new
            {
                res = sv.updateMk(jsonCart[0]["ID"], jsonCart[0]["mk_moi"]),
            });
        }


    }
}
