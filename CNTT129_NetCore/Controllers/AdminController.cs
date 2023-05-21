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
using CNTT129_NetCore.Extensions;
using QRCoder;

namespace CNTT129.Controllers
{
    public class AdminController : BaseController
    {
        //
        // GET: /Admin/

        public ActionResult Login()
        {
            if (Session.Get<bool>("log_gv") == true)
            {
                return RedirectToAction("Home", "Admin");
            }
            return View();
        }
        public ActionResult Home()
        {
            if (Session.Get<bool>("log_gv") == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }

        public ActionResult DangXuat()
        {
            Session["Ten_gv"] = "";
            Session["ma_gv"] = "";
            Session["id_gv"] = "";
            Session["log_gv"] = false;
            if (Session.Get<bool>("log_gv") == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(string login, string username, string password)
        {
            if (!string.IsNullOrEmpty(login))
            {
                GIANG_VIEN gv = new GIANG_VIEN();
                var kq = gv.login(username, password);
                if (kq.Count == 0)
                {
                    ViewBag.TB = "Tài Khoản bạn khóa hoặc sai mật mật khẩu! Vui lòng liên hệ admin để đc giải quyết";
                }
                else
                {
                    foreach (var item in kq)
                    {
                        Session["Ten_gv"] = item.TENGV;
                        Session["ma_gv"] = item.MAGV;
                        Session["id_gv"] = item.ID_GV;
                        Session["name_vt"] = item.vai_tro_name;
                    }
                    Session["log_gv"] = true;
                    return RedirectToAction("Home", "Admin");
                }
            }
            return View();
        }

        // Vai trò
        public ActionResult VaiTro()
        {
            if (Session.Get<bool>("log_gv") == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            VAI_TRO vt = new VAI_TRO();
            ViewData["data"] = vt.find("", "", "");
            return View();
        }

        [HttpPost]
        public ActionResult VaiTro(string find, string vai_tro_code, string vai_tro_name, string trang_thai)
        {
            if (!string.IsNullOrEmpty(find))
            {
                VAI_TRO vt = new VAI_TRO();
                ViewData["data"] = vt.find(vai_tro_code, vai_tro_name, trang_thai);
                ViewBag.vai_tro_code = vai_tro_code;
                ViewBag.vai_tro_name = vai_tro_name;
                ViewBag.selected = trang_thai;
            }
            return View();
        }

        public JsonResult findVaiTro(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            VAI_TRO vt = new VAI_TRO();
            ViewData["data_list"] = vt.findByID(jsonCart[0]["ID_VAI_TRO"]);
            return Json(new
            {
                res = ViewData["data_list"],
            });
        }

        public JsonResult saveVT(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            VAI_TRO vt = new VAI_TRO();
            int reslut = vt.save(jsonCart[0]["CODE_VAI_TRO"], jsonCart[0]["TEN_VAI_TRO"], jsonCart[0]["ID_VAI_TRO"]);
            return Json(new
            {
                res = reslut,
            });
        }

        public JsonResult updateVT(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            VAI_TRO vt = new VAI_TRO();
            string tt = jsonCart[0]["disabled"];
            int id = jsonCart[0]["ID_VAI_TRO"];
            int reslut = vt.updateTT(id, int.Parse(tt));
            return Json(new
            {
                res = reslut,
            });
        }

        //Loại quyền
        public ActionResult LoaiQuyen()
        {
            if (Session.Get<bool>("log_gv") == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            LOAI_QUYEN vt = new LOAI_QUYEN();
            ViewData["data"] = vt.find("", "", "");
            return View();
        }

        [HttpPost]
        public ActionResult LoaiQuyen(string find, string loai_quyen_code, string loai_quyen_name, string trang_thai)
        {
            if (!string.IsNullOrEmpty(find))
            {
                LOAI_QUYEN vt = new LOAI_QUYEN();
                ViewData["data"] = vt.find(loai_quyen_code, loai_quyen_name, trang_thai);
                ViewBag.vai_tro_code = loai_quyen_code;
                ViewBag.vai_tro_name = loai_quyen_name;
                ViewBag.selected = trang_thai;
            }
            return View();
        }

        public JsonResult findLoaiQuyen(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            LOAI_QUYEN vt = new LOAI_QUYEN();
            ViewData["data_list"] = vt.findByID(jsonCart[0]["ID_LOAI_QUYEN"]);
            return Json(new
            {
                res = ViewData["data_list"],
            });
        }

        public JsonResult saveLQ(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            LOAI_QUYEN lq = new LOAI_QUYEN();
            int reslut = lq.save(jsonCart[0]["CODE_LOAI_QUYEN"], jsonCart[0]["TEN_LOAI_QUYEN"], jsonCart[0]["ID_LOAI_QUYEN"]);
            return Json(new
            {
                res = reslut,
            });
        }

        public JsonResult updateLQ(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            LOAI_QUYEN lq = new LOAI_QUYEN();
            string tt = jsonCart[0]["disabled"];
            int id = jsonCart[0]["ID_LOAI_QUYEN"];
            int reslut = lq.updateTT(id, int.Parse(tt));
            return Json(new
            {
                res = reslut,
            });
        }

        //Loại quyền theo vai trò
        public ActionResult LOAIQUYENOFVAITRO(string id)
        {
            if (Session.Get<bool>("log_gv") == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            ViewBag.ID_VAI_TRO = id;
            return View();
        }

        public JsonResult CNLOAIQUYENOFVAITRO(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            LOAIQUYENOFVAITRO lqvt = new LOAIQUYENOFVAITRO();
            var listThem = jsonCart[0]["listThem"];
            var listBo = jsonCart[0]["listBo"];
            var listCo = jsonCart[0]["listCo"];
            var id = jsonCart[0]["id"];
            foreach (var item in listThem)
            {
                lqvt.them(item, id);
            }
            foreach (var item in listBo)
            {
                lqvt.update(item, id);
            }
            foreach (var item in listCo)
            {
                lqvt.update2(item, id);
            }
            return Json(new
            {
                status = true,
            });
        }

        //Khoa
        public ActionResult Khoa()
        {
            if (Session.Get<bool>("log_gv") == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            KHOA khoa = new KHOA();
            ViewData["data"] = khoa.find("", "", "");
            return View();
        }

        [HttpPost]
        public ActionResult Khoa(string find, string khoa_code, string khoa_name, string trang_thai)
        {
            if (!string.IsNullOrEmpty(find))
            {
                KHOA khoa = new KHOA();
                ViewData["data"] = khoa.find(khoa_code, khoa_name, trang_thai);
                ViewBag.khoa_code = khoa_code;
                ViewBag.khoa_name = khoa_name;
                ViewBag.selected = trang_thai;
            }
            return View();
        }

        public JsonResult findKhoa(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            KHOA khoa = new KHOA();
            ViewData["data_list"] = khoa.findByID(jsonCart[0]["ID_KHOA"]);
            return Json(new
            {
                res = ViewData["data_list"],
            });
        }

        public JsonResult saveKhoa(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            KHOA khoa = new KHOA();
            int reslut = khoa.save(jsonCart[0]["MA_KHOA"], jsonCart[0]["TEN_KHOA"], jsonCart[0]["ID_KHOA"]);
            return Json(new
            {
                res = reslut,
            });
        }

        public JsonResult updateKhoa(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            KHOA khoa = new KHOA();
            string tt = jsonCart[0]["disabled"];
            int id = jsonCart[0]["ID_KHOA"];
            int reslut = khoa.updateTT(id, int.Parse(tt));
            return Json(new
            {
                res = reslut,
            });
        }

        //Giảng viên
        public ActionResult GiangVien()
        {
            if (Session.Get<bool>("log_gv") == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            GIANG_VIEN gv = new GIANG_VIEN();
            ViewData["data"] = gv.find("", "", "0", "0", "");
            return View();
        }

        [HttpPost]
        public ActionResult GiangVien(string find, string ma_gv, string ten_gv, string khoa, string vai_tro, string trang_thai)
        {
            if (!string.IsNullOrEmpty(find))
            {
                GIANG_VIEN gv = new GIANG_VIEN();
                ViewData["data"] = gv.find(ma_gv, ten_gv, khoa, vai_tro, trang_thai);
                ViewBag.ma_gv = ma_gv;
                ViewBag.ten_gv = ten_gv;
                ViewBag.selected = trang_thai;
                ViewBag.selected_khoa = khoa;
                ViewBag.selected_vt = vai_tro;
            }
            return View();
        }

        public JsonResult findGV(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            GIANG_VIEN gv = new GIANG_VIEN();
            ViewData["data_list"] = gv.findByID(jsonCart[0]["id"]);
            return Json(new
            {
                res = ViewData["data_list"],
            });
        }

        public JsonResult saveGV(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            GIANG_VIEN gv = new GIANG_VIEN();
            int reslut = gv.save(jsonCart[0]["ID"], jsonCart[0]["MA"], jsonCart[0]["TEN"], jsonCart[0]["SDT"], jsonCart[0]["DC"], jsonCart[0]["mat_khau"], jsonCart[0]["idkhoa"], jsonCart[0]["email"], jsonCart[0]["idvai_tro"], jsonCart[0]["ngay_sinh"]);
            return Json(new
            {
                res = reslut,
            });
        }

        public JsonResult updateGV(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            GIANG_VIEN gv = new GIANG_VIEN();
            string tt = jsonCart[0]["disabled"];
            int id = jsonCart[0]["ID"];
            int reslut = gv.updateTT(id, int.Parse(tt));
            return Json(new
            {
                res = reslut,
            });
        }

        //Loại quyền nhân viên
        public ActionResult LOAIQUYENOFGV(string id_gv, string id_vai_tro)
        {
            if (Session.Get<bool>("log_gv") == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            ViewBag.ID_GV = id_gv;
            ViewBag.ID_VAI_TRO = id_vai_tro;
            return View();
        }

        public JsonResult CNLOAIQUYENOFGV(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            LOAIQUYENOFGV sp = new LOAIQUYENOFGV();
            var listThem = jsonCart[0]["listThem"];
            var listBo = jsonCart[0]["listBo"];
            var id = jsonCart[0]["id"];
            var listCo = jsonCart[0]["listCo"];
            foreach (var item in listCo)
            {
                sp.update2(item, id);
            }
            foreach (var item in listThem)
            {
                sp.them(item, id);
            }
            foreach (var item in listBo)
            {
                sp.update(item, id);
            }
            return Json(new
            {
                status = true,
            });
        }

        //Sinh viên
        public ActionResult SinhVien()
        {
            if (Session.Get<bool>("log_gv") == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            SINHVIEN sv = new SINHVIEN();
            ViewData["data"] = sv.find("", "", "0", "");
            return View();
        }

        [HttpPost]
        public ActionResult SinhVien(string find, string ma_gv, string ten_gv, string khoa, string trang_thai)
        {
            if (!string.IsNullOrEmpty(find))
            {
                SINHVIEN sv = new SINHVIEN();
                ViewData["data"] = sv.find(ma_gv, ten_gv, khoa, trang_thai);
                ViewBag.ma_gv = ma_gv;
                ViewBag.ten_gv = ten_gv;
                ViewBag.selected = trang_thai;
                ViewBag.selected_khoa = khoa;
            }
            return View();
        }

        public JsonResult findSV(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            SINHVIEN sv = new SINHVIEN();
            ViewData["data_list"] = sv.findByID(jsonCart[0]["id"]);
            return Json(new
            {
                res = ViewData["data_list"],
            });
        }

        public JsonResult saveSV(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            SINHVIEN sv = new SINHVIEN();
            HOCKY hocky = new HOCKY();
            var menu_hocky = hocky.find();
            var idhk = "";
            var diemmd = "";
            foreach (var item in menu_hocky)
            {
                idhk = item.IDHK;
                diemmd = item.DIEMMD;
            }
            int reslut = sv.save(jsonCart[0]["ID"], jsonCart[0]["MA"], jsonCart[0]["TEN"], jsonCart[0]["SDT"], jsonCart[0]["DC"], jsonCart[0]["mat_khau"], jsonCart[0]["idkhoa"], jsonCart[0]["email"], jsonCart[0]["ngay_sinh"], jsonCart[0]["bac_dao_tao"], jsonCart[0]["lop"], jsonCart[0]["gioi_tinh"], jsonCart[0]["tinh_thanh"], jsonCart[0]["hinh_anh"], idhk, diemmd);
            return Json(new
            {
                res = reslut,
            });
        }

        public JsonResult updateSV(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            SINHVIEN sv = new SINHVIEN();
            string tt = jsonCart[0]["disabled"];
            int id = jsonCart[0]["ID"];
            int reslut = sv.updateTT(id, int.Parse(tt));
            return Json(new
            {
                res = reslut,
            });
        }

        //Hoạt Động
        public ActionResult ThemHoatDong()
        {
            if (Session.Get<bool>("log_gv") == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }

        public JsonResult saveHD(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            HOATDONG hd = new HOATDONG();
            HoatDongBuoi hdb = new HoatDongBuoi();
            string ten_hd = jsonCart[0]["ten_hd"];
            string ghi_chu_hd = jsonCart[0]["ghi_chu_hd"];
            string dia_diem_hd = jsonCart[0]["dia_diem_hd"];
            int so_nguoi_hd = int.Parse(jsonCart[0]["so_nguoi_hd"]);
            int diem_hd = int.Parse(jsonCart[0]["diem_hd"]);
            string noi_dung = jsonCart[0]["noi_dung"];
            int khoa = int.Parse(jsonCart[0]["khoa"]);
            int loai_hd = int.Parse(jsonCart[0]["loai_hd"]);
            int hocky = int.Parse(jsonCart[0]["hocky"]);
            string hinh_anh = jsonCart[0]["hinh_anh"];
            string ngay_bd_dk_hd = jsonCart[0]["ngay_bd_dk_hd"];
            string ngay_kt_dk_hd = jsonCart[0]["ngay_kt_dk_hd"];
            string ngay_tao = jsonCart[0]["ngay_tao"];
            string reslut = hd.save(ten_hd, noi_dung, ngay_bd_dk_hd, ngay_kt_dk_hd, khoa, hocky, so_nguoi_hd, diem_hd, ghi_chu_hd, loai_hd, hinh_anh, Session["id_gv"].ToString(), ngay_tao, dia_diem_hd);
            int id_hd = 0;
            foreach (var item in hd.findByCode(reslut))
            {
                id_hd = int.Parse(item.IDHD);
            }
            var detail = jsonCart[0]["detail"];
            foreach (var item in detail)
            {
                hdb.save(id_hd, item["loai_buoi"], item["ngay_bat_dau"]);
            }
            return Json(new
            {
                res = reslut,
            });
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

            return Json("Not implement!");
        }

        // Danh sách hoạt động
        public ActionResult DanhSachHoatDong()
        {
            if (Session.Get<bool>("log_gv") == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            HOATDONG hk = new HOATDONG();
            DateTime now = DateTime.Now;
            ViewBag.start_date = now.Day + "/" + now.Month + "/" + now.Year;
            ViewBag.end_date = now.Day + "/" + now.Month + "/" + now.Year;
            ViewData["data"] = hk.find("", "", "0", "", now.Year + "/" + now.Month + "/" + now.Day, now.Year + "/" + now.Month + "/" + now.Day, "0", "");
            return View();
        }

        [HttpPost]
        public ActionResult DanhSachHoatDong(string find, string ma_hd, string ten_hd, string khoa, string trang_thai, string start_date, string end_date, string loai, string hocky)
        {
            if (!string.IsNullOrEmpty(find))
            {
                HOATDONG hk = new HOATDONG();
                var dateBd = start_date.Split('/');
                var datekt = end_date.Split('/');
                ViewData["data"] = hk.find(ma_hd, ten_hd, khoa, loai, dateBd[2] + '/' + dateBd[1] + '/' + dateBd[0], datekt[2] + '/' + datekt[1] + '/' + datekt[0], hocky, trang_thai);
                ViewBag.ma_hd = ma_hd;
                ViewBag.ten_hd = ten_hd;
                ViewBag.selected = trang_thai;
                ViewBag.selected_khoa = khoa;
                ViewBag.selected_hocky = hocky;
                ViewBag.selected_loai = loai;
                ViewBag.start_date = start_date;
                ViewBag.end_date = end_date;
            }
            return View();
        }
        public JsonResult updateTT(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            HOATDONG hk = new HOATDONG();
            HoatDongBuoi hdb = new HoatDongBuoi();
            string tt = jsonCart[0]["trangthai"];
            int id = jsonCart[0]["ID"];
            string ngay = jsonCart[0]["ngay"];
            int reslut = hk.updateTT(id, int.Parse(tt), ngay, Session["id_gv"].ToString());
            if (int.Parse(tt) == 2)
            {
                hdb.huyAllHdb(id);
            }
            return Json(new
            {
                res = reslut,
            });
        }

        //Sửa hoạt động
        public ActionResult SuaHoatDong(string id)
        {
            if (Session.Get<bool>("log_gv") == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            HOATDONG hk = new HOATDONG();
            var data = hk.findByID(id);
            foreach (var item in data)
            {
                ViewBag.ma_hd = item.MAHD;
                ViewBag.tieu_de = item.TIEUDE;
                ViewBag.nguoi_tao = item.name_ngtao;
                ViewBag.ghi_chu = item.GHICHU;
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
                ViewBag.noi_dung = item.NOIDUNG;
                ViewBag.dia_diem = item.DIADIEM;
                ViewData["data_img"] = item.HINHANH.Split(new char[] { ',' });
            }
            ViewBag.id_hd = id;
            return View();
        }

        public JsonResult updateHD(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            HOATDONG hd = new HOATDONG();
            HoatDongBuoi hdb = new HoatDongBuoi();
            int id_hd = int.Parse(jsonCart[0]["id_hd"]);
            string ten_hd = jsonCart[0]["ten_hd"];
            string ghi_chu_hd = jsonCart[0]["ghi_chu_hd"];
            int so_nguoi_hd = int.Parse(jsonCart[0]["so_nguoi_hd"]);
            int diem_hd = int.Parse(jsonCart[0]["diem_hd"]);
            string noi_dung = jsonCart[0]["noi_dung"];
            int khoa = int.Parse(jsonCart[0]["khoa"]);
            int loai_hd = int.Parse(jsonCart[0]["loai_hd"]);
            int hocky = int.Parse(jsonCart[0]["hocky"]);
            string hinh_anh = jsonCart[0]["hinh_anh"];
            string ngay_bd_dk_hd = jsonCart[0]["ngay_bd_dk_hd"];
            string ngay_kt_dk_hd = jsonCart[0]["ngay_kt_dk_hd"];
            string ngay_sua = jsonCart[0]["ngay_tao"];
            string dia_diem_hd = jsonCart[0]["dia_diem_hd"];
            int reslut = hd.updatehd(id_hd, ten_hd, noi_dung, ngay_bd_dk_hd, ngay_kt_dk_hd, khoa, hocky, so_nguoi_hd, diem_hd, ghi_chu_hd, loai_hd, hinh_anh, Session["id_gv"].ToString(), ngay_sua, dia_diem_hd);
            hdb.updateTT(id_hd, 1);
            var detail = jsonCart[0]["detail"];
            foreach (var item in detail)
            {
                if (hdb.KtraHD(id_hd, item["ngay_bat_dau"], item["loai_buoi"]) == 0)
                {
                    hdb.save(id_hd, item["loai_buoi"], item["ngay_bat_dau"]);
                }
                else
                {
                    hdb.updateHdb(id_hd, item["ngay_bat_dau"], item["loai_buoi"], 0);
                }

            }
            return Json(new
            {
                res = reslut,
            });
        }

        //Chi tiết hoạt động
        public ActionResult ChiTietHoatDong(string id)
        {
            if (Session.Get<bool>("log_gv") == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            HOATDONG hk = new HOATDONG();
            var data = hk.findByID(id);
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
                var datekt = ngay_kt.ToString("dd/MM/yyyy");
                ViewBag.ngay_kt = datekt;
                ViewBag.loai_hd = item.LOAI;
                ViewBag.trang_thai = item.TRANGTHAI;
            }
            ViewBag.id_hd = id;
            return View();
        }

        public JsonResult taoMaQr(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            string link = jsonCart[0]["link"];
            int id = jsonCart[0]["id"];
            HoatDongBuoi hdb = new HoatDongBuoi();
            hdb.updateTTHdb(id, 2);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(link, QRCodeGenerator.ECCLevel.Q);
                BitmapByteQRCode qRCode = new BitmapByteQRCode(qRCodeData);
                ViewBag.QRCode = "data:image/png;base64," + Convert.ToBase64String(qRCode.GetGraphic(20));
            }

            Session["link_qr"] = link;
            Session["ma_qr"] = ViewBag.QRCode;
            return Json(new
            {
                res = ViewBag.QRCode,
            });
        }

        public ActionResult ShowMaQR()
        {
            if (Session.Get<bool>("log_gv") == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            ViewBag.QRCode = Session["ma_qr"];
            return View();
        }

        public JsonResult huyHoatDong(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            int id = jsonCart[0]["id"];
            HoatDongBuoi hdb = new HoatDongBuoi();
            HOATDONG hd = new HOATDONG();
            DANGKY dk = new DANGKY();
            hdb.updateTTHdb(id, 1);
            dk.capnhatHuyTatCa(id.ToString());
            DateTime date2 = new DateTime();
            var ngay = date2.ToString("yyyy-MM-dd");
            if (hdb.laySlBuoiHdChuaHuy(id) == 0)
            {
                hd.updateTT(id, 2, ngay, Session["id_gv"].ToString());
            }
            return Json(new
            {
                res = true,
            });
        }
        public JsonResult dongHoatDong(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            int id = jsonCart[0]["id"];
            int idhd = jsonCart[0]["idhd"];
            int trang_thai = jsonCart[0]["trang_thai"];
            HoatDongBuoi hdb = new HoatDongBuoi();
            HOATDONG hd = new HOATDONG();
            hdb.updateTTHdb(id, trang_thai);
            DateTime date2 = new DateTime();
            DANGKY dk = new DANGKY();
            var ngay = date2.ToString("yyyy-MM-dd");
            if (trang_thai == 4)
            {
                if (hdb.laySlBuoiHdChuaDongDangKy(idhd) == 0)
                {
                    hd.updateTINHTRANG(idhd, 1, ngay, Session["id_gv"].ToString());
                }
            }
            else
            {
                dk.capnhatKhongThamGiaTatCa(id);
                if (hdb.laySlBuoiHdChuaDongDiemDanh(idhd) == hdb.countHd(idhd))
                {
                    hd.updateTINHTRANG(idhd, 2, ngay, Session["id_gv"].ToString());
                }
            }
            return Json(new
            {
                res = true,
            });
        }

        public JsonResult dongHoatDong2(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            int id = int.Parse(jsonCart[0]["id"]);
            int idhd = int.Parse(jsonCart[0]["idhd"]);
            int trang_thai = jsonCart[0]["trang_thai"];
            HoatDongBuoi hdb = new HoatDongBuoi();
            HOATDONG hd = new HOATDONG();
            hdb.updateTTHdb(id, trang_thai);
            DateTime date2 = new DateTime();
            DANGKY dk = new DANGKY();
            var ngay = date2.ToString("yyyy-MM-dd");
            if (trang_thai == 4)
            {
                if (hdb.laySlBuoiHdChuaDongDangKy(idhd) == 0)
                {
                    hd.updateTINHTRANG(idhd, 1, ngay, Session["id_gv"].ToString());
                }
            }
            else
            {
                dk.capnhatKhongThamGiaTatCa(id);
                if (hdb.laySlBuoiHdChuaDongDiemDanh(idhd) == hdb.countHd(idhd))
                {
                    hd.updateTINHTRANG(idhd, 2, ngay, Session["id_gv"].ToString());
                }
            }
            return Json(new
            {
                res = true,
            });
        }

        //Tổng kết hoạt động
        public ActionResult TongKetHoatDong()
        {
            if (Session.Get<bool>("log_gv") == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            HoatDongBuoi hoat_dong_buoi = new HoatDongBuoi();
            DateTime now = DateTime.Now;
            ViewBag.start_date = now.Day + "/" + now.Month + "/" + now.Year;
            ViewBag.end_date = now.Day + "/" + now.Month + "/" + now.Year;
            ViewData["data"] = hoat_dong_buoi.find("", "", "0", now.Year + "/" + now.Month + "/" + now.Day, now.Year + "/" + now.Month + "/" + now.Day, "0", "");
            return View();
        }

        [HttpPost]
        public ActionResult TongKetHoatDong(string find, string ma_hd, string ten_hd, string khoa, string start_date, string end_date, string loai, string hocky)
        {
            if (!string.IsNullOrEmpty(find))
            {
                HoatDongBuoi hoat_dong_buoi = new HoatDongBuoi();
                var dateBd = start_date.Split('/');
                var datekt = end_date.Split('/');
                ViewData["data"] = hoat_dong_buoi.find(ma_hd, ten_hd, khoa, dateBd[2] + '/' + dateBd[1] + '/' + dateBd[0], datekt[2] + '/' + datekt[1] + '/' + datekt[0], hocky, loai);
                ViewBag.ma_hd = ma_hd;
                ViewBag.ten_hd = ten_hd;
                ViewBag.selected_khoa = khoa;
                ViewBag.selected_hk = hocky;
                ViewBag.selected_loai = loai;
                ViewBag.start_date = start_date;
                ViewBag.end_date = end_date;
            }
            return View();
        }

        public ActionResult ChiTietTongKet(string idbuoi)
        {
            if (Session.Get<bool>("log_gv") == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            HOATDONG hoat_dong = new HOATDONG();
            ViewData["data_hoat_dong"] = hoat_dong.findByBuoiID(idbuoi);
            DANGKY dang_ky = new DANGKY();
            ViewData["data_dang_ky"] = dang_ky.danhSachHD(idbuoi, "");
            ViewData["data_dang_ky_huy"] = dang_ky.danhSachHD(idbuoi, "4");
            ViewData["data_dang_ky_khong_tham_gia"] = dang_ky.danhSachHD(idbuoi, "3");
            PHAN_HOI phan_hoi = new PHAN_HOI();
            ViewData["data_phan_hoi"] = phan_hoi.danhSachHD(idbuoi, "");
            DIEMDANH diem_danh = new DIEMDANH();
            ViewData["data_diem_danh"] = diem_danh.danhSachHD(idbuoi, "");
            ViewBag.id_buoi = idbuoi;
            return View();
        }

        public JsonResult tongKetHD(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            int id = jsonCart[0]["id_buoi"];
            HoatDongBuoi hoat_dong = new HoatDongBuoi();
            PHAN_HOI phan_hoi = new PHAN_HOI();
            hoat_dong.updateTTHdb(id, 6);
            foreach (var item in jsonCart[0]["list_duyet"])
            {
                phan_hoi.updateTT(item, "1");
            }
            foreach (var item in jsonCart[0]["list_huy"])
            {
                phan_hoi.updateTT(item, "2");
            }
            return Json(new
            {
                res = true,
            });
        }

        public JsonResult tuChoiTongKetHD(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            int id = jsonCart[0]["id_buoi"];
            HoatDongBuoi hoat_dong = new HoatDongBuoi();
            hoat_dong.updateTTHdb(id, 7);
            return Json(new
            {
                res = true,
            });
        }
        // NỘi quy
        public ActionResult NOIQUY()
        {
            if (Session.Get<bool>("log_gv") == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            NOI_QUY vt = new NOI_QUY();
            ViewData["data"] = vt.find("", "", "");
            return View();
        }

        [HttpPost]
        public ActionResult NOIQUY(string find, string noi_quy_code, string noi_quy_name, string trang_thai)
        {
            if (!string.IsNullOrEmpty(find))
            {
                NOI_QUY vt = new NOI_QUY();
                ViewData["data"] = vt.find(noi_quy_code, noi_quy_name, trang_thai);
                ViewBag.vai_tro_code = noi_quy_code;
                ViewBag.vai_tro_name = noi_quy_name;
                ViewBag.selected = trang_thai;
            }
            return View();
        }

        public JsonResult findNoiQuy(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            NOI_QUY vt = new NOI_QUY();
            ViewData["data_list"] = vt.findByID(jsonCart[0]["ID_NOI_QUY"]);
            return Json(new
            {
                res = ViewData["data_list"],
            });
        }

        public JsonResult saveNQ(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            NOI_QUY vt = new NOI_QUY();
            int reslut = vt.save(jsonCart[0]["CODE_NOI_QUY"], jsonCart[0]["TEN_NOI_QUY"], jsonCart[0]["ID_NOI_QUY"], jsonCart[0]["DIEM"]);
            return Json(new
            {
                res = reslut,
            });
        }

        public JsonResult updateNQ(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            NOI_QUY vt = new NOI_QUY();
            string tt = jsonCart[0]["disabled"];
            int id = jsonCart[0]["ID_NOI_QUY"];
            int reslut = vt.updateTT(id, int.Parse(tt));
            return Json(new
            {
                res = reslut,
            });
        }
        // Thêm mới vi phạm
        public ActionResult ThemMoiViPham()
        {
            if (Session.Get<bool>("log_gv") == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }

        public JsonResult saveViPham(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            HOCKY hocky = new HOCKY();
            var menu_hocky = hocky.find();
            var idhk = "";
            foreach (var item in menu_hocky)
            {
                idhk = item.IDHK;
            }
            VI_PHAM vi_pham = new VI_PHAM();
            foreach (var item in jsonCart[0]["sinh_vien"])
            {
                vi_pham.save(jsonCart[0]["tieu_de"], jsonCart[0]["noi_dung"], Session["id_gv"].ToString(), jsonCart[0]["ngay_tao"], jsonCart[0]["noi_quy"], item, idhk);
            }
            return Json(new
            {
                res = true,
            });
        }

        // Danh Sách Vi Phạm
        public ActionResult DanhSachViPham()
        {
            if (Session.Get<bool>("log_gv") == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            VI_PHAM vt = new VI_PHAM();
            ViewData["data"] = vt.find("", "", "", "");
            return View();
        }

        [HttpPost]
        public ActionResult DanhSachViPham(string find, string noi_quy_code, string noi_quy_name, string trang_thai, string hoc_ky)
        {
            if (!string.IsNullOrEmpty(find))
            {
                VI_PHAM vt = new VI_PHAM();
                ViewData["data"] = vt.find(noi_quy_code, noi_quy_name, trang_thai, hoc_ky);
                ViewBag.vai_tro_code = noi_quy_code;
                ViewBag.vai_tro_name = noi_quy_name;
                ViewBag.selected = trang_thai;
                ViewBag.selected_hk = hoc_ky;
            }
            return View();
        }

        public JsonResult updateVP(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            VI_PHAM vt = new VI_PHAM();
            string tt = jsonCart[0]["disabled"];
            int id = jsonCart[0]["ID_VI_PHAM"];
            int reslut = vt.updateTT(id, int.Parse(tt));
            return Json(new
            {
                res = reslut,
            });
        }
        // danh sách phản hồi
        public ActionResult DanhSachPhanHoi()
        {
            if (Session.Get<bool>("log_gv") == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            PHAN_HOI hk = new PHAN_HOI();
            DateTime now = DateTime.Now;
            ViewData["list_phan_hoi"] = hk.find2("", "", now.Year + "/" + now.Month + "/" + now.Day, now.Year + "/" + now.Month + "/" + now.Day, "", "", "");
            ViewBag.start_date = now.Day + "/" + now.Month + "/" + now.Year;
            ViewBag.end_date = now.Day + "/" + now.Month + "/" + now.Year;
            return View();
        }

        [HttpPost]
        public ActionResult DanhSachPhanHoi(string find, string ma_ph, string trang_thai, string start_date, string end_date, string loai, string ma_sv, string hoc_ky)
        {
            if (!string.IsNullOrEmpty(find))
            {
                PHAN_HOI hk = new PHAN_HOI();
                var dateBd = start_date.Split('/');
                var datekt = end_date.Split('/');
                ViewData["list_phan_hoi"] = hk.find2(ma_ph, trang_thai, dateBd[2] + '/' + dateBd[1] + '/' + dateBd[0], datekt[2] + '/' + datekt[1] + '/' + datekt[0], loai, ma_sv, hoc_ky);
                ViewBag.ma_ph = ma_ph;
                ViewBag.selected = trang_thai;
                ViewBag.selected_loai = loai;
                ViewBag.start_date = start_date;
                ViewBag.ma_sv = ma_sv;
                ViewBag.end_date = end_date;
                ViewBag.selected_hk = hoc_ky;
            }
            return View();
        }

        public JsonResult updatePhanHoi(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            PHAN_HOI phan_hoi = new PHAN_HOI();

            string id = jsonCart[0]["ID"];
            string tt = jsonCart[0]["tt"];

            int reslut = phan_hoi.updateTT2(id, tt, Session["id_gv"].ToString());

            return Json(new
            {
                res = reslut,
            });
        }

        public ActionResult ChotKetQua()
        {
            if (Session.Get<bool>("log_gv") == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            HOCKY hocky = new HOCKY();
            var menu_hocky = hocky.find();
            var idhk = "";
            var tenhk = "";
            foreach (var item in menu_hocky)
            {
                idhk = item.IDHK;
                tenhk = item.TENHK;
            }
            HOATDONG hoat_dong = new HOATDONG();
            ViewData["data_hoat_dong"] = hoat_dong.findByIdHk(idhk);
            NOI_QUY noi_quy = new NOI_QUY();
            ViewData["data_noi_quy"] = noi_quy.find("","","0");
            ViewBag.idhk = idhk;
            ViewBag.tenhk = tenhk;
            return View();
        }
        public JsonResult updateDiemAll(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            HOCKY hocky = new HOCKY();
            SV_CHUCVU sv_cv = new SV_CHUCVU();
            var menu_hocky = hocky.find();
            
            var idhk = "";
            var diemmd = "";
            foreach (var item in menu_hocky)
            {
                idhk = item.IDHK;
                diemmd = item.DIEMMD;
            }
            sv_cv.updateDiem(idhk);
            int reslut = hocky.updateDiem(idhk, jsonCart[0]["diemmd"], diemmd);

            return Json(new
            {
                res = reslut,
            });
        }
        [HttpPost]
        public PartialViewResult showDialogDangKy(string id, string idhd)
        {
            DANGKY dang_ky = new DANGKY();
            var view = "";
            ViewData["data_list_dang_ky"] = dang_ky.danhSachHD(id, "0");
            view = "dialogDangKy";
            ViewBag.id_buoi = id;
            ViewBag.id_hd = idhd;
            return PartialView(view);
        }


        public ActionResult dialogDangKy()
        {
            return View();
        }

        public JsonResult khongchapnhansvthamgia(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            DANGKY dang_ky = new DANGKY();
            foreach (var item in jsonCart)
            {
                dang_ky.khongChoSinhVienThamGia(item,"5"); // 5 trạng thái k cho sinh viên tham gia
            }

            return Json(new
            {
                res = true,
            });
        }

        public ActionResult VaiTroSv()
        {
            if (Session.Get<bool>("log_gv") == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            VAI_TRO_SV vt = new VAI_TRO_SV();
            ViewData["data"] = vt.find("", "", "");
            return View();
        }

        [HttpPost]
        public ActionResult VaiTroSv(string find, string vai_tro_code, string vai_tro_name, string trang_thai)
        {
            if (!string.IsNullOrEmpty(find))
            {
                VAI_TRO_SV vt = new VAI_TRO_SV();
                ViewData["data"] = vt.find(vai_tro_code, vai_tro_name, trang_thai);
                ViewBag.vai_tro_code = vai_tro_code;
                ViewBag.vai_tro_name = vai_tro_name;
                ViewBag.selected = trang_thai;
            }
            return View();
        }

        public JsonResult findVaiTroSv(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            VAI_TRO_SV vt = new VAI_TRO_SV();
            ViewData["data_list"] = vt.findByID(jsonCart[0]["ID_VAI_TRO"]);
            return Json(new
            {
                res = ViewData["data_list"],
            });
        }

        public JsonResult saveVTSv(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            VAI_TRO_SV vt = new VAI_TRO_SV();
            int reslut = vt.save(jsonCart[0]["CODE_VAI_TRO"], jsonCart[0]["TEN_VAI_TRO"], jsonCart[0]["ID_VAI_TRO"], jsonCart[0]["DIEM"]);
            return Json(new
            {
                res = reslut,
            });
        }

        public JsonResult updateVTSv(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            VAI_TRO_SV vt = new VAI_TRO_SV();
            string tt = jsonCart[0]["disabled"];
            int id = jsonCart[0]["ID_VAI_TRO"];
            int reslut = vt.updateTT(id, int.Parse(tt));
            return Json(new
            {
                res = reslut,
            });
        }

        public ActionResult ThemSvVaoVt(string id_vt)
        {
            if (Session.Get<bool>("log_gv") == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            ViewBag.id_vt = id_vt;
            return View();
        }

        public JsonResult saveSvVaoVt(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            SV_CHUCVU svvt = new SV_CHUCVU();
            foreach (var item in jsonCart[0]["sinh_vien"])
            {
                svvt.save(item, jsonCart[0]["id_vt"]);
            }
           
            return Json(new
            {
                res = true,
            });
        }

        public JsonResult updateSvVaoVt(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            SV_CHUCVU vt = new SV_CHUCVU();
            string tt = jsonCart[0]["disabled"];
            string id_vt = jsonCart[0]["ID_VAI_TRO"];
            string id_sv = jsonCart[0]["ID_SV"];
            int reslut = vt.updateTT(id_sv, id_vt, tt);
            return Json(new
            {
                res = reslut,
            });
        }

        public ActionResult LoaiHdNgoai()
        {
            if (Session.Get<bool>("log_gv") == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            LOAI_HOAT_DONG_NGOAI hdn = new LOAI_HOAT_DONG_NGOAI();
            ViewData["data"] = hdn.find("", "", "");
            return View();
        }

        [HttpPost]
        public ActionResult LoaiHdNgoai(string find, string hd_code, string hd_name, string trang_thai)
        {
            if (!string.IsNullOrEmpty(find))
            {
                LOAI_HOAT_DONG_NGOAI hdn = new LOAI_HOAT_DONG_NGOAI();
                ViewData["data"] = hdn.find(hd_code, hd_name, trang_thai);
                ViewBag.hd_code = hd_code;
                ViewBag.hd_name = hd_name;
                ViewBag.selected = trang_thai;
            }
            return View();
        }

        public JsonResult findHdNgoai(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            LOAI_HOAT_DONG_NGOAI hdn = new LOAI_HOAT_DONG_NGOAI();
            ViewData["data_list"] = hdn.findByID(jsonCart[0]["ID_HD_NGOAI"]);
            return Json(new
            {
                res = ViewData["data_list"],
            });
        }

        public JsonResult saveHdNgoai(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            LOAI_HOAT_DONG_NGOAI hdn = new LOAI_HOAT_DONG_NGOAI();
            int reslut = hdn.save(jsonCart[0]["CODE_HD_NGOAI"], jsonCart[0]["TEN_HD_NGOAI"], jsonCart[0]["ID_HD_NGOAI"], jsonCart[0]["DIEM"]);
            return Json(new
            {
                res = reslut,
            });
        }

        public JsonResult updateHdNgoai(string cartModel)
        {
            var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
            LOAI_HOAT_DONG_NGOAI hdn = new LOAI_HOAT_DONG_NGOAI();
            string tt = jsonCart[0]["disabled"];
            int id = jsonCart[0]["ID_HD_NGOAI"];
            int reslut = hdn.updateTT(id, int.Parse(tt));
            return Json(new
            {
                res = reslut,
            });
        }

        public ActionResult HOATDONGNGOAI()
        {
            if (Session.Get<bool>("log_gv") == false)
            {
                return RedirectToAction("Login", "Admin");
            }
            HOATDONGNGOAI hk = new HOATDONGNGOAI();
            DateTime now = DateTime.Now;
            ViewBag.start_date = now.Day + "/" + now.Month + "/" + now.Year;
            ViewBag.end_date = now.Day + "/" + now.Month + "/" + now.Year;
            ViewData["data"] = hk.find("", "", "", now.Year + "/" + now.Month + "/" + now.Day, now.Year + "/" + now.Month + "/" + now.Day, "0", "", "0");
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
                ViewData["data"] = hk.find(ma_hd, "", trang_thai, dateBd[2] + '/' + dateBd[1] + '/' + dateBd[0], datekt[2] + '/' + datekt[1] + '/' + datekt[0], hocky, loai, lhdn);
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
                res = hdn.save(jsonCart[0]["id_hd"], jsonCart[0]["noi_dung"], jsonCart[0]["dia_diem"], idhk, jsonCart[0]["hinh_anh"], jsonCart[0]["sinh_vien"], Session["id_gv"].ToString(), "0", jsonCart[0]["lhdn_edit"]),
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
         public JsonResult ktraSlSvChuaXl(string cartModel)
         {
             var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
             DANGKY dang_ky = new DANGKY();
             DIEMDANH diem_danh = new DIEMDANH();
             PHAN_HOI phan_hoi = new PHAN_HOI();
             VI_PHAM vi_pham = new VI_PHAM();
             HOATDONG hoat_dong = new HOATDONG();
             HOATDONGNGOAI hoat_dong_ngoai = new HOATDONGNGOAI();
             HOATDONGNGOAI hdn = new HOATDONGNGOAI();
             HOCKY hocky = new HOCKY();
             var menu_hocky = hocky.find();
             var idhk = "";
             foreach (var item in menu_hocky)
             {
                 idhk = item.IDHK;
             }
             var demSlVP = vi_pham.demSL(idhk, jsonCart[0]["start_date"], jsonCart[0]["end_date"]);
             var demSlPH = phan_hoi.demSL(idhk, jsonCart[0]["start_date"], jsonCart[0]["end_date"]);
             var demSlHD = hoat_dong.demSL(idhk, jsonCart[0]["start_date"], jsonCart[0]["end_date"]);
             var demslHDN = hoat_dong_ngoai.demSL(idhk, jsonCart[0]["start_date"], jsonCart[0]["end_date"]);
             return Json(new
             {
                 res = new { demSlVP = demSlVP, demSlPH = demSlPH, demSlHD = demSlHD, demslHDN = demslHDN },
             });
         }

         public JsonResult updateDiemDot(string cartModel)
         {
             var jsonCart = JsonSerializer.Deserialize<dynamic>(cartModel);
             HOCKY hocky = new HOCKY();
             SV_CHUCVU sv_cv = new SV_CHUCVU();
             var menu_hocky = hocky.find();
             
             var idhk = "";
             foreach (var item in menu_hocky)
             {
                 idhk = item.IDHK;
             }
             //sv_cv.updateDiem(idhk);
             int reslut = hocky.updateDiem2(idhk, jsonCart[0]["start_date"], jsonCart[0]["end_date"]);

             return Json(new
             {
                 res = reslut,
             });
         }
        
    }

}
