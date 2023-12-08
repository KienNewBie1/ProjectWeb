//using Fluent.Infrastructure.FluentModel;
using ProjectWeb.Data;
using ProjectWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace ProjectWeb.Controllers
{
    public class CustomerController : Controller
    {
        private ShopWatchEntities db = new ShopWatchEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ViewResult Index(ProjectWeb.Models.Mail _objModelMail, string Email)
        {
            Session["Email"] = Email;
            if (Session["Email"].ToString() == "")
            {
                ViewBag.ThongBao = "Vui lòng nhập Email";
            }
            else
            {
                if (ModelState.IsValid)
                {
                    //ViewBag.ThongBao = "Bạn đã gửi mail thành công!!! Vui lòng kiểm tra email.";
                    MailMessage mail = new MailMessage();
                    mail.To.Add("trungkien98744@gmail.com");
                    mail.From = new MailAddress("trungkien98744@gmail.com");
                    mail.Subject = "Đặt lại mật khẩu";
                    string Body = "Bấm vào đây để <a href='https://localhost:44304/Customer/Reset'>Đặt lại mật Khẩu</a>";
                    mail.Body = Body;
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential("trungkien98744@gmail.com", "gqtu bixx eypz gxyc"); // Enter seders User name and password       
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    ViewBag.ThongBao = "Bạn đã gửi mail thành công!!! Vui lòng kiểm tra email.";
                }

            }
            return View();
        }

    public ActionResult Reset()
        {
            var sEmail = Session["Email"].ToString();
            if (sEmail == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			//Customers customer = db.Customers.Find(sEmail);
			Customers customer = (Customers)(from l in db.Customers select l).Where(l => l.Email == sEmail).FirstOrDefault();
            //if (customer == null)
            //{
            //    return HttpNotFound();
            //}
            return View(customer);
        }

        // POST: Admin/Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Reset([Bind(Include = "Id,Password")] Customers customer)
        //{
        //    var sEmail = Session["Email"].ToString();
        //    //var TK = db.Customers.SingleOrDefault(n => n.Email == sEmail);
        //    var existingCustomer = db.Customers.SingleOrDefault(c => c.Email == sEmail);
        //    //var TK = (from l in db.Customers where l.Email == sEmail select l.Email).SingleOrDefault();
        //    if (existingCustomer !=null)
        //    {
        //        existingCustomer.Password = customer.Password;
        //        db.Entry(customer).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Login");
        //    }
        //    return View("Login",customer);
        //}

        public ActionResult Reset([Bind(Include = "Id,Password")] Customers customer)
        {
            var sEmail = Session["Email"]?.ToString(); // Chắc chắn rằng không null
            if (sEmail != null)
            {
                var existingCustomer = db.Customers.SingleOrDefault(c => c.Email == sEmail);
                if (existingCustomer != null)
                {
                    existingCustomer.Password = customer.Password; // Thay đổi mật khẩu

                    try
                    {
                        db.Entry(existingCustomer).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Login");
                    }
                    catch (DbEntityValidationException ex)
                    {
                        // Xử lý lỗi kiểm tra ở đây
                        foreach (var validationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                Console.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                            }
                        }
                    }
                }
            }

            // Nếu không thực hiện được việc đặt lại mật khẩu, quay về trang Login
            return View("Login", customer);
        }


        //      public ActionResult Reset()
        //{
        //          var sEmail = Session["Email"].ToString();
        //          Customers TK = db.Customers.SingleOrDefault(n => n.Email == sEmail);
        //          return View();
        //}
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
                Customers kh = new Customers();
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
                Customers kh = db.Customers.SingleOrDefault(n => n.UserName == tendn && n.Password == matkhau);
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
