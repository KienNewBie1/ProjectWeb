using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using ProjectWeb.Models;
using System.Web.Mvc;

namespace ProjectWeb.Controllers
{
    public class SendMailerController : Controller
    {
        // GET: SendMailer
        public ActionResult Index()
        {
            return View();  
        }
        [HttpPost]
        public ViewResult Index(ProjectWeb.Models.Mail _objModelMail,string Email)
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

    }
}