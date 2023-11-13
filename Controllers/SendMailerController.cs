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
        public ViewResult Index(ProjectWeb.Models.Mail _objModelMail)
        {
            if (ModelState.IsValid)
            {
                MailMessage mail = new MailMessage();
                mail.To.Add("gmail của mình");
                mail.From = new MailAddress("trungkien98744@gmail.com");
                mail.Subject = "Đặt lại mật khẩu";
                string Body = "Bấm vào đây để <a href='https://localhost:44304/Home/Index'>Đặt lại mật Khẩu</a>";
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("tên gmail của mình", "mật khẩu ứng dụng mail"); // Enter seders User name and password       
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return View("Index", _objModelMail);
            }
            else
            {
                return View();
            }
        }

    }
}