using MailKit_Configuration.Interface;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailKit_Configuration.Service
{
    public class MailHelper : IMailHelper
    {

        IConfiguration configuration;

        public MailHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void SendMail(string to, string from ,string subject, string Messagecontent)
        {
            //string fromAddress = configuration["SmtpConfig:FromAddress"];
            //string serverAddress = configuration["SmtpConfig:ServerAddress"];
            //string username = configuration["SmtpConfig:Username"];
            //string password = configuration["SmtpConfig:Password"];
            //int port = Convert.ToInt32(configuration["SmtpConfig:Port"]);
            //bool isUseSsl = Convert.ToBoolean(configuration["SmtpConfig:IsUseSsl"]);

            try
            {

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(from, from));
                message.To.Add(new MailboxAddress(to, to));
                message.Subject = subject;
                message.Body = new TextPart("html")
                {
                    Text = Messagecontent
                };

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {

                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("rashedmahamud93@gmail.com", "helloworld@02");
                    client.Send(message);
                    client.Disconnect(true);

                   
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
}
