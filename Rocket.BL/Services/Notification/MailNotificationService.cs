using MailKit.Net.Smtp;
using MimeKit;
using Rocket.BL.Common.Services;
using Rocket.BL.Services.EmailNotificationService;
using System.Threading.Tasks;

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

            SmtpClientCreator(message).Wait();
        }

        private async Task SmtpClientCreator(MimeMessage message)
        {
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 465, true).ConfigureAwait(false);
                await client.AuthenticateAsync("rocket.team.mail.service@gmail.com", "4hqymel_ZP898qwe").ConfigureAwait(false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.SendAsync(message).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }
    }
}