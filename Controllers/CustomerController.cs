using ProjectWeb.Data;
using ProjectWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectWeb.Controllers
{
    public class CustomerController : Controller
    {
        private ProjectWebContext db = new ProjectWebContext();
        public ActionResult Index()
        {
            if (Session["UserName"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult LoginLogoutPartial()
        {
            return PartialView("LoginLogoutPartial");
        }
        public ActionResult Logout(string url)
        {
            Session.Clear();
            //Session.RemoveAll();
            //Session.Abandon();
            return RedirectToAction("Login", "Customer");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]

        public ActionResult Register(FormCollection collection)
        {
            var hoten = collection["Name"];
            var tendn = collection["UserName"];
            var matkhau = collection["Password"];
            var matkhaunhaplai = collection["rePassword"];
            var diachi = collection["Address"];
            var email = collection["Email"];
            var dienthoai = collection["Phone"];
            //var ngaysinh = String.Format("{0:dd/MM/yyyy}", collection["Ngaysinh"]);
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên khách hàng không được để trống";
            }
            else if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Phải nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(matkhaunhaplai))
            {
                ViewData["Loi4"] = "Phải nhập lại mật khẩu";
            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi5"] = "Email không được bỏ trống";
            }
            else if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi6"] = "Phải nhập điện thoại";
            }
            else if (String.IsNullOrEmpty(diachi))
            {
                ViewData["Loi7"] = "Phải nhập địa chỉ";
            }
            else
            {
                Customer kh = new Customer();
                kh.Name = hoten;
                kh.UserName = tendn;
                kh.Password = matkhau;
                kh.Email = email;
                kh.Address = diachi;
                kh.Phone = dienthoai;
                //kh. = DateTime.Parse(ngaysinh);
                //db.Categories.InsertOnSubmit(kh);
                //db.Customers.InsertOnSubmit(kh);
                db.Customers.Add(kh);
                db.SaveChanges();
                return RedirectToAction("Login","Customer");
            }
            return this.Register();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendn = collection["UserName"];
            var matkhau = collection["Password"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                Customer kh = db.Customers.SingleOrDefault(n => n.UserName == tendn && n.Password == matkhau);
                //KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.TaiKhoan == tendn && n.MatKhau == matkhau);
                if (kh != null)
                {
                    //ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["UserName"] = kh;
                    return RedirectToAction("Index", "Home");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
    }
}
