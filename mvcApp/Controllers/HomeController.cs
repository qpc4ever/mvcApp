using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mvcApp.Models;


namespace mvcApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
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
            var contact = new ContactViewModel();

            return View(contact);
        }
        [HttpPost]
        public IActionResult Contact(ContactViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //do something
                var message = new MailMessage();
                message.From =
                    new MailAddress("do_not_reply@sightsource.net",
                        "Sightsource Contact Bot");

                //subject

                message.Subject = "New contact message";
                
                //To

                message.To.Add(new MailAddress("qpc4ever@gmail.com"));

                //message

                message.Body = $"new contact from {viewModel.Name} ({viewModel.Email})" + 
                    Environment.NewLine + 
                    viewModel.Message;

                var mailClient = new SmtpClient("email-smpt.us-east-1.amazonaws.com", 587);
                

                mailClient.UseDefaultCredentials = false;
                mailClient.Credentials = new System.Net.NetworkCredential(
                    "AKIAJZT46OGOXRVDV55Q",
                    "AnQ9XTmy2Bb7g+adRah8ZLVkJzvwQr3y448eeVfqfGg"
                    );

                mailClient.EnableSsl = true;
                try
                {
                    //mailClient.Send(message);
                    viewModel.CompletedAt = DateTime.UtcNow;
                }
                catch(Exception ex)
                {
                    viewModel.ErrorMessage = "Ooops. Something went wrong";
                }
            }
            return View(viewModel);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
