using ProjectWeb.Data;
using ProjectWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectWeb.Controllers
{
    public class CartController : Controller
    {
        private ProjectWebContext db = new ProjectWebContext();
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PayPartial()
		{
            return PartialView();
		}
        public List<Cart> LayGioHang()
        {
            List<Cart> lstGioHang = Session["Cart"] as List<Cart>;
            if (lstGioHang == null)
            {
                // khoi tao Gio Hang (Gio hang chua ton tai)
                lstGioHang = new List<Cart>();
                Session["Cart"] = lstGioHang;
            }
            return lstGioHang;
        }
        public ActionResult ThemGioHang(int ms, string url)
        {
            List<Cart> lstGioHang = LayGioHang();
            Cart sp = lstGioHang.Find(n => n.iMaSP == ms);
            if (sp == null)
            {
                sp = new Cart(ms);
                lstGioHang.Add(sp);
            }
            else
            {
                sp.iCount++;
            }
            return Redirect(url);
        }
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<Cart> lstGioHang = Session["Cart"] as List<Cart>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iCount);
            }
            return iTongSoLuong;
        }
        private double TongTien()
        {
            double dDongTien = 0;
            List<Cart> lstGioHang = Session["Cart"] as List<Cart>;
            if (lstGioHang != null)
            {
                dDongTien = lstGioHang.Sum(n => n.dTotal);
            }
            return dDongTien;
        }
        public ActionResult GioHang()
        {
            List<Cart> lstGioHang = LayGioHang();
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "SachOnline");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }

        public ActionResult XoaSPKhoiGioHang(int iMaSach)
        {
            List<Cart> lstGioHang = LayGioHang();
            Cart sp = lstGioHang.SingleOrDefault(n => n.iMaSP == iMaSach);
            if (sp != null)
            {
                lstGioHang.RemoveAll(n => n.iMaSP == iMaSach);
                if (lstGioHang.Count == 0)
                {
                    return RedirectToAction("Index", "SachOnline");
                }
            }
            return RedirectToAction("GioHang");
        }


        public ActionResult CapNhatGioHang(int iMaSach, FormCollection f)
        {
            List<Cart> lstGioHang = LayGioHang();
            Cart sp = lstGioHang.SingleOrDefault(n => n.iMaSP == iMaSach);
            if (sp != null)
            {
                sp.iCount = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaGioHang()
        {
            List<Cart> lstGioHang = LayGioHang();
            lstGioHang.Clear();
            return RedirectToAction("Index", "SachOnline");
        }
        //[HttpGet]
        //public ActionResult DatHang()
        //{
        //    if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
        //    {
        //        return RedirectToAction("DangNhap", "NguoiDung");
        //    }
        //    if (Session["GioHang"] == null)
        //    {
        //        return RedirectToAction("Index", "SachOnline");
        //    }
        //    // lay hang tu session
        //    List<Cart> lstGioHang = LayGioHang();
        //    ViewBag.TongSoLuong = TongSoLuong();
        //    ViewBag.TongTien = TongTien();
        //    return View(lstGioHang);
        //}

        //[HttpPost]
        //public ActionResult DatHang(FormCollection f)
        //{
        //    DONDATHANG ddh = new DONDATHANG();
        //    KhachHang kh = (KhachHang)Session["TaiKhoan"];
        //    List<GioHang> lstGioHang = LayGioHang();
        //    ddh.MaKH = kh.MaKH;
        //    ddh.NgayDH = DateTime.Now;
        //    var NgayGiao = String.Format("{0:MM/dd/yyyy}", f["NgayGiao"]);
        //    ddh.NgayGiaoHang = DateTime.Parse(NgayGiao);
        //    ddh.HTGiaoHang = true;
        //    ddh.HTThanhToan = false;
        //    db.DONDATHANGs.InsertOnSubmit(ddh);
        //    db.SubmitChanges();
        //    foreach (var item in lstGioHang)
        //    {
        //        CTDATHANG ctdh = new CTDATHANG();
        //        ctdh.MaKH = (long)ddh.MaKH;
        //        ctdh.SoDH = ddh.SoDH;
        //        ctdh.MaSach = item.iMaSach;
        //        ctdh.SoLuong = item.iSoLuong;
        //        ctdh.DonGia = item.dDonGia;
        //        db.CTDATHANGs.InsertOnSubmit(ctdh);
        //    }
        //    db.SubmitChanges();
        //    Session["GioHang"] = null;
        //    return RedirectToAction("XacNhanDonHang", "GioHang");
        //}

        public ActionResult ConfirmCart()
        {
            return View();
        }
    }
}