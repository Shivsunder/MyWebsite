using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Models;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Net;
namespace MyWebsite.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Index vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage msz = new MailMessage();
                    msz.From = new MailAddress(vm.Email);//Email which you are getting 
                                                         //from contact us page 
                    msz.To.Add("contactshiv@www.developershiv.com");//Where mail will be sent 
                    //msz.Body = vm.Message;
                    var text = new StringBuilder();
                    text.Append("Name: " + vm.Name + "." + "\n");
                    text.Append("Email: " + vm.Email + "." + "\n");
                    text.Append("Message: " + vm.Message + ".");
                    msz.Body = text.ToString();
                    //vm.Message.Contains(msz.From);
                    msz.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.Credentials = new System.Net.NetworkCredential
                    ("contactshivsunder@gmail.com", "pASSword@1");
                    smtp.EnableSsl = true;
                    smtp.Send(msz);
                    ModelState.Clear();
                    ViewBag.Message = "Thank you for Contacting me.";
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $" Sorry, {ex.Message}.";
                }
            }
            return View();
        }
    public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}