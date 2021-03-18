using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace WebAppEmailTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";




            MailAddress to = new MailAddress("eric.sondraal@gmail.com");
            MailAddress from = new MailAddress("ClientServerGroupProject@gmail.com");

            MailMessage message = new MailMessage(from, to);
            message.Subject = "Hi Eric";
            message.Body = "I need a break!";

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("ClientServerGroupProject@gmail.com", "h4G5_3Gsf57G_f324"),
                EnableSsl = true
            };
            // code in brackets above needed if authentication required 

            try
            {
                client.Send(message);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }


            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}