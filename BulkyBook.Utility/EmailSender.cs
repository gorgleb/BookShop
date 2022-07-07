using FluentEmail.Smtp;

using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentEmail.Core;
using MimeKit;
using Microsoft.Extensions.DependencyInjection;

namespace BulkyBook.Utility
{
    
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //var emailToSend = new MimeMessage();
            //emailToSend.From.Add(MailboxAddress.Parse("hello@gleb.com"));
            //emailToSend.To.Add(MailboxAddress.Parse(email));
            //emailToSend.Subject = subject;
            //emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

            ////send email
            //using (var emailClient = new SmtpClient())
            //{
            //    emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            //    emailClient.Authenticate("gorglebtest@gmail.com", "Gorgleb123!");
            //    emailClient.Send(emailToSend);
            //    emailClient.Disconnect(true);
            //}
            //SmtpClient smtp = new SmtpClient
            //{
            //    //The address of the SMTP server (I'll take mailbox 126 as an example, which can be set according to the specific mailbox you use)
            //    Host = "smtp.126.com",
            //    UseDefaultCredentials = true,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    //Enter the user name and password of your sending SMTP server here
            //    Credentials = new networkcredential("mailbox user name", "mailbox password")
            //};  
            //var sender = new SmtpSender(() => new SmtpClient("587")
            //{
            //    EnableSsl = false,
            //    UseDefaultCredentials = true,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    Credentials = new NetworkCredential("gorglebtest@gmail.com", "gorglebworking@gmail.com")
            //}) ;
            //Email.DefaultSender = sender;
            //var mail = await Email
            //    .From("gorglebtest@gmail.com")
            //    .To(email, "gleb")
            //    .Subject(subject)
            //    .Body(htmlMessage,true)
            //    .SendAsync();
           
            var sender = new SmtpSender(() => new SmtpClient("smtp.gmail.com")
            {
                UseDefaultCredentials = false,
                Port = 587,
                Credentials = new NetworkCredential("gorglebtest@gmail.com", "Gorgleb123!"),
                EnableSsl = true,
            });

            Email.DefaultSender = sender;
            var mail = Email
                .From("gorglebtest@gmail.com", "Gorgleb")
                .To(email)
                .Subject(subject)
                .Body(htmlMessage,true)
                .SendAsync();


        }
    }
}
