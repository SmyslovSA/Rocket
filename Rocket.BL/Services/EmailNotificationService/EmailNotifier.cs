using Rocket.BL.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;

namespace Rocket.BL.Services.EmailNotificationService
{
    class EmailNotifier : IMailNotificationService
    {
        void IMailNotificationService.NotifyAboutRelease(int id)
        {
            var htmlStringBuilder = new HtmlStringBuilder();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("rocket.team.mail.service@gmail.com"));
            message.To.Add(new MailboxAddress(""/*todo Достать адреса*/));
            message.Subject = "Release information";
            var bodyBuilder = new BodyBuilder();

            bodyBuilder.HtmlBody = htmlStringBuilder.CreateBody(); 
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("rocket.team.mail.service@gmail.com", "4hqymel_ZP898qwe");
                client.Send(message);
                client.Disconnect(true);
            }
        }


        void IMailNotificationService.SendBillingPremium(int id)
        {
            var htmlStringBuilder = new HtmlStringBuilder();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("rocket.team.mail.service@gmail.com"));
            message.To.Add(new MailboxAddress(""/*todo Достать адреса*/));
            message.Subject = "Billing information";
            var bodyBuilder = new BodyBuilder();

            bodyBuilder.HtmlBody = htmlStringBuilder.CreateBody();
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("rocket.team.mail.service@gmail.com", "4hqymel_ZP898qwe");
                client.Send(message);
                client.Disconnect(true);
            }
        }

        void IMailNotificationService.SendBillingUser(int id)
        {
            var htmlStringBuilder = new HtmlStringBuilder();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("rocket.team.mail.service@gmail.com"));
            message.To.Add(new MailboxAddress(""/*todo Достать адреса*/));
            message.Subject = "Billing user information";
            var bodyBuilder = new BodyBuilder();

            bodyBuilder.HtmlBody = htmlStringBuilder.CreateBody();
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("rocket.team.mail.service@gmail.com", "4hqymel_ZP898qwe");
                client.Send(message);
                client.Disconnect(true);
            }
        }

        void IMailNotificationService.SendConfirmation(string email, string url, string name)
        {
            var htmlStringBuilder = new HtmlStringBuilder();
            //todo добавить ссылку для регистрации аккаунта в кастомный шаблон
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("rocket.team.mail.service@gmail.com"));
            message.To.Add(new MailboxAddress(name ,email));
            message.Subject = "Billing information";
            var bodyBuilder = new BodyBuilder();

            bodyBuilder.HtmlBody = htmlStringBuilder.CreateBody();
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("rocket.team.mail.service@gmail.com", "4hqymel_ZP898qwe");
                client.Send(message);
                client.Disconnect(true);
            }
        }

        void IMailNotificationService.SendCustomMessage(int id)
        {
            var htmlStringBuilder = new HtmlStringBuilder();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("rocket.team.mail.service@gmail.com"));
            message.To.Add(new MailboxAddress(""/*todo Достать адреса*/));
            message.Subject = "Billing user information";
            var bodyBuilder = new BodyBuilder();

            bodyBuilder.HtmlBody = htmlStringBuilder.CreateBody();
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("rocket.team.mail.service@gmail.com", "4hqymel_ZP898qwe");
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
