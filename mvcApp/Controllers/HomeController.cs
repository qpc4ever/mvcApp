using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using mvcApp.Models;


namespace mvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
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
                    new MailAddress(
                        _configuration["Contact:FromEmail"],
                        _configuration["Contact:FromName"]);

                //subject

                message.Subject = "New contact message";
                
                //To

                message.To.Add(new MailAddress(
                    _configuration["Contact:ToEmail"], _configuration["Contact:ToName"]));

                //message

                message.Body = $"new contact from {viewModel.Name} ({viewModel.Email})" + 
                    Environment.NewLine + 
                    viewModel.Message;

                var mailClient = new SmtpClient("email-smpt.us-east-1.amazonaws.com", 
                    Convert.ToInt32(_configuration["Contact:SmtpPort"]));
                

                mailClient.UseDefaultCredentials = false;
                mailClient.Credentials = new System.Net.NetworkCredential(
                    "AKIAJZT46OGOXRVDV55Q",
                    "AnQ9XTmy2Bb7g+adRah8ZLVkJzvwQr3y448eeVfqfGg"
                    );

                mailClient.EnableSsl = true;

                try
                {
                    mailClient.Send(message);
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
