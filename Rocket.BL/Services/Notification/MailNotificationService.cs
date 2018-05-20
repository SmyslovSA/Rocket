﻿using MailKit.Net.Smtp;
using MimeKit;
using Rocket.BL.Common.Services;
using Rocket.BL.Services.EmailNotificationService;

namespace Rocket.BL.Services.Notification
{
    public class MailNotificationService : IMailNotificationService
    {
        public void NotifyAboutRelease(int id)
        {
            var htmlStringBuilder = new HtmlStringBuilder();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("rocket.team.mail.service@gmail.com"));
            message.To.Add(new MailboxAddress(string.Empty/*todo Достать адреса*/));
            message.Subject = "Release information";
            var bodyBuilder = new BodyBuilder();

            bodyBuilder.HtmlBody = htmlStringBuilder.CreateBody(); 
            message.Body = bodyBuilder.ToMessageBody();

            SmtpClientCreator(message);
        }

        public void SendBillingPremium(int id)
        {
            var htmlStringBuilder = new HtmlStringBuilder();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("rocket.team.mail.service@gmail.com"));
            message.To.Add(new MailboxAddress(string.Empty/*todo Достать адреса*/));
            message.Subject = "Billing information";
            var bodyBuilder = new BodyBuilder();

            bodyBuilder.HtmlBody = htmlStringBuilder.CreateBody();
            message.Body = bodyBuilder.ToMessageBody();

            SmtpClientCreator(message);
        }

        public void SendBillingUser(int id)
        {
            var htmlStringBuilder = new HtmlStringBuilder();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("rocket.team.mail.service@gmail.com"));
            message.To.Add(new MailboxAddress(string.Empty/*todo Достать адреса*/));
            message.Subject = "Billing user information";
            var bodyBuilder = new BodyBuilder();

            bodyBuilder.HtmlBody = htmlStringBuilder.CreateBody();
            message.Body = bodyBuilder.ToMessageBody();

            SmtpClientCreator(message);
        }

        public void SendConfirmation(string email, string url, string name)
        {
            var htmlStringBuilder = new HtmlStringBuilder();
            //todo добавить ссылку для регистрации аккаунта в кастомный шаблон
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("rocket.team.mail.service@gmail.com"));
            message.To.Add(new MailboxAddress(name, email));
            message.Subject = "Billing information";
            var bodyBuilder = new BodyBuilder();

            bodyBuilder.HtmlBody = htmlStringBuilder.CreateBody();
            message.Body = bodyBuilder.ToMessageBody();

            SmtpClientCreator(message);
        }

        public void SendCustomMessage(int id)
        {
            var htmlStringBuilder = new HtmlStringBuilder();
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("rocket.team.mail.service@gmail.com"));
            message.To.Add(new MailboxAddress(string.Empty/*todo Достать адреса*/));
            message.Subject = "Billing user information";
            var bodyBuilder = new BodyBuilder();

            bodyBuilder.HtmlBody = htmlStringBuilder.CreateBody();
            message.Body = bodyBuilder.ToMessageBody();

            SmtpClientCreator(message);
        }

        private void SmtpClientCreator(MimeMessage message)
        {
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