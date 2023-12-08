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
        private ShopWatchEntities db = new ShopWatchEntities();
        // GET: Cart
        public ActionResult Index()
        {
            List<Cart> lstGioHang = LayGioHang();
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
            //return View(db.Products.ToList());
        }
        public ActionResult PayPartial()
		{
            return PartialView();
		}
        //public ActionResult PartialCart()
        //{
        //    return PartialView(db.Products.ToList());
        //}
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
			//return RedirectToAction("Index","Cart");
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
        public ActionResult PartialCart()
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
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index","Cart");
        }

        public List<Cart> LayDsSpThanhToan()
        {
            List<Cart> lstSPThanhToan = Session["DSThanhToan"] as List<Cart>;
            if (lstSPThanhToan == null)
            {
                lstSPThanhToan = new List<Cart>();
                Session["DSThanhToan"] = lstSPThanhToan;
            }
            return lstSPThanhToan;
        }

        public ActionResult CapNhatGioHang(int iMaSach, FormCollection f)
        {
            List<Cart> lstGioHang = LayGioHang();
            Cart sp = lstGioHang.SingleOrDefault(n => n.iMaSP == iMaSach);
            if (sp != null)
            {
                sp.iCount = int.Parse(f["quantity1"].ToString());
            }
            return RedirectToAction("Index");
        }
        public ActionResult XoaGioHang()
        {
            List<Cart> lstGioHang = LayGioHang();
            lstGioHang.Clear();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult DatHang(List<int> selectedValues)
        {
            if (Session["Customers"] == null || Session["Customers"].ToString() == "")
            {
                return RedirectToAction("Login", "Customers");
            }
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Cart");
            }
            List<Cart> lstSPThanhToan = LayDsSpThanhToan();
            List<Cart> lstGioHang = LayGioHang();

            if (selectedValues != null)
            {
                foreach (var id in selectedValues)
                {
                    Cart sp = lstGioHang.Find(n => n.iMaSP == id);
                    lstSPThanhToan.Add(sp);
                }
            }
            return RedirectToAction("Dat", lstSPThanhToan);
        }

        [HttpGet]
        public ActionResult DatHang()
        {
            List<Cart> lstSPThanhToan = LayDsSpThanhToan();
            return View(lstSPThanhToan);

        }
          public ActionResult Dat(FormCollection f)
        {
            if (Session["UserName"] == null || Session["UserName"].ToString() == "")
            {
                return RedirectToAction("Login", "Customer");
            }
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Order order = new Order();
            Customers kh = (Customers)Session["UserName"];
            List<Cart> lstGioHang = LayGioHang();
            order.MaKH = kh.Id;
            order.NgayDH = DateTime.Now;
            var NgayGiao = DateTime.Now;
            order.NgayGiaoHang = NgayGiao;
			order.HTGiaoHang = true;
            order.HTThanhToan = false;
            db.Order.Add(order);
            db.SaveChanges();
            //foreach (var item in lstGioHang)
            //{
            //    ct ctdh = new CTDATHANG();
            //    ctdh.SoDH = ddh.SoDH;
            //    ctdh.MaSach = item.iMaSach;
            //    ctdh.SoLuong = item.iSoLuong;
            //    ctdh.DonGia = (decimal)item.dDonGia;
            //    db.CTDATHANGs.InsertOnSubmit(ctdh);
            //}
            //db.SubmitChanges();
            Session["Cart"] = null;
            ViewBag.mess = "Đặt Hàng Thành công";
            return RedirectToAction("Index", "Home");

        }
        public ActionResult ConfirmCart()
        {
            return View();
        }
    }
}