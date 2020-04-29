using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MailKit_Configuration.Models;
using MailKit_Configuration.Interface;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MailKit_Configuration.Service;
using Microsoft.AspNetCore.Hosting;

namespace MailKit_Configuration.Controllers
{
    public class HomeController : Controller
    {


       
        private EmailConfiguration emailConfiguration;
        private readonly IEmailService _Mailer;
        private ITemplateHelper _templateHelper;
        private IMailHelper _mail;
        private IHostingEnvironment _environment;


        public HomeController( IEmailService Mailer, ITemplateHelper helper, IMailHelper mail, IHostingEnvironment environment)
        {
            _Mailer = Mailer;
            _templateHelper = helper;
            _mail = mail;
            _environment = environment;

        }
       

        public IActionResult Index()
        {
            // Instansiate minemessage 
            var message = new MimeMessage();
            //From Address
            message.From.Add(new MailboxAddress("Rashed ", "rashedmahamud93@gmail.com"));
            // To Address
            message.To.Add(new MailboxAddress("Dev Rashed ", "rashedmahamud93@gmail.com"));

            //Subject
            message.Subject = "Mail Kit test";
            //Body 

            //message.Body = new TextPart("Plain")
            //{
            //    Text = "This is from mailKit, hello there!"
            //};

            string response = GetTemplateForMail();
            message.Body = new TextPart("html")
            {
                Text = response
            };

            // configure and send email
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("rashedmahamud93@gmail.com", "helloworld@02");
                client.Send(message);
                client.Disconnect(true);

            }



            // sending mail using mailservice


            //var emailMessage = new EmailMessage();
            //emailMessage.FromAddresses.Add(new EmailAddress { Name="Rashed", Address= "rashedmahamud93@gmail.com" });
            //emailMessage.ToAddresses.Add(new EmailAddress { Name = "Rashed Personal", Address = "rashedmahamud93@gmail.com" });
            //emailMessage.Subject = "Forget Password";
            //emailMessage.Content = "This mail is sent to reset your password.";

            //_Mailer.Send(emailMessage);

            return View();
        }

        //[HttpGet]
        //[Route("template")]
        //public string GetTemplate()
        //{
        //    var model = new MailViewModel();
        //    Reader data =  new Reader { Id = 1, EmailAddress = "rashedmahamud93@gmail.com", UserName = "rasded1320" };
        //    //model.Data.Add(data);
             
        //   // model.Data = ReaderStore.Readers;

        //    var response =  _templateHelper.GetTemplateHtmlAsStringAsync<MailViewModel>("Templates/ForgetPassword", model);

        //    _mail.SendMail("bro@aka.com", "rashedmahamud93@gmail.com", "Reader Test Data", response);

        //    return response;
        //}


        [HttpGet]

        private string GetTemplateForMail()
        {
            var model = new MailViewModel();
            Reader data = new Reader { Id = 1, EmailAddress = "rashedmahamud93@gmail.com", UserName = "rasded1320" };
            //model.Data.Add(data);

            // model.Data = ReaderStore.Readers;

            var response = _templateHelper.GetTemplateHtmlAsStringAsync<MailViewModel>("Templates/ForgetPassword", model);

            _mail.SendMail("bro@aka.com", "rashedmahamud93@gmail.com", "Reader Test Data", response);

            return response;
        }


        public class MailViewModel
        {
            public string HeaderImage { get; set; }
            public List<Reader> Data { get; set; }
            public string FooterImage { get; set; }
        }

        public class Reader {

            public  int  Id { get; set; }

            public string UserName { get; set; }

            public string EmailAddress { get; set; }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
