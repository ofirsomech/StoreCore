using EASendMail;
using Microsoft.Extensions.Configuration;
using StoreProject.Models;
using StoreProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreProject.Services.Services
{
    public class EmailSender : IEmailSender
    {
        IConfiguration _config;

        public EmailSender(IConfiguration config)
        {
            _config = config;
        }



        public bool SendEmail(User user)
        {
            SmtpMail oMail = new SmtpMail("TryIt");
            SmtpClient oSmtp = new SmtpClient();

            // Set sender email address, please change it to yours
            oMail.From = "ofirsomech4@gmail.com";
            // Set recipient email address, please change it to yours
            oMail.To = user.Email;

            // Set email subject
            oMail.Subject = "Verify Account email";

            // Set email body
            oMail.HtmlBody = $"<div style='border: 2px solid orange;padding:10px ;border-radius: 8px;'><div><p>Hello , {user.UserName},</div><div><p>Youre almost done! Please click this link below to activate your The Store account and get started.</p><div><a href='https://localhost:44369/Account/VerifyUser/{user.Guid.ToString()}'><button style='background-color:green'>Activate Your Account</button></a></div><div><p>Love,</p></div><div><p>The Store</p></div></div>";

            // Your SMTP server address
            SmtpServer oServer = new SmtpServer("smtp.gmail.com");
            // User and password for ESMTP authentication, if your server doesn't require
            // User authentication, please remove the following codes.
            oServer.User = "ofirsomech4@gmail.com";
            oServer.Password = _config["mailPassword"];

            // If your smtp server requires TLS connection, please add this line
            // oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
            // If your smtp server requires implicit SSL connection on 465 port, please add this line
            oServer.Port = 587;
            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            try
            {
                Console.WriteLine("start to send email ...");
                oSmtp.SendMail(oServer, oMail);
                Console.WriteLine("email was sent successfully!");
            }
            catch (Exception ep)
            {
                Console.WriteLine("failed to send email with the following error:");
                Console.WriteLine(ep.Message);
            }

            return true;
        }
    }
}
