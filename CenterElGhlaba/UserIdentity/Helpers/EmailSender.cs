using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace Center_ElGhalaba.Helpers
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailAddress mailFrom = new MailAddress("centerelghalaba@gmail.com", "CenterElGhalaba");
            string mailPWD = "yqmxkjyimkapdhoo";

            MailMessage msg = new MailMessage();
            msg.From = mailFrom;
            msg.Subject = subject;
            msg.Body = $"<html><body>{htmlMessage}</body></html>";
            msg.To.Add(email);
            msg.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(mailFrom.Address, mailPWD)
            };

            smtp.Send(msg);

            //var fromAddress = new MailAddress("centerelghalaba@gmail.com", "CenterElGhalaba");
            //var toAddress = new MailAddress(email);
            //string fromPassword = "yqmxkjyimkapdhoo";
            //string body = htmlMessage;

            //var smtp = new SmtpClient
            //{
            //    Host = "smtp.gmail.com",
            //    Port = 587,
            //    EnableSsl = true,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    UseDefaultCredentials = false,
            //    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            //};
            //using (var message = new MailMessage(fromAddress, toAddress)
            //{
            //    Subject = subject,
            //    Body = body
            //})
            //{
            //    smtp.Send(message);
            //}

        }
    }
}

