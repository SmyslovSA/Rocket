using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using AutoMapper;
using MailKit;
using MimeKit;
using RazorEngine;
using RazorEngine.Templating;
using Rocket.BL.Common.Enums;
using Rocket.BL.Common.Models.Notification;
using Rocket.BL.Common.Services.Notification;
using Rocket.BL.Properties;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.DbModels.ReleaseList;
using Rocket.DAL.Common.DbModels.Subscription;
using Rocket.DAL.Common.UoW;

namespace Rocket.BL.Services.Notification
{
    public class MailNotificationService : BaseService, IMailNotificationService
    {
        private IMailTransport _transport;
        private bool _isDisposed = false;

        public MailNotificationService(IUnitOfWork unitOfWork, 
            IMailTransport transport) 
            : base (unitOfWork)
        {
            _transport = transport;
        }

        public void NotifyAboutRelease(SubscribableEntity entity)
        {
            if (entity is DbMusic music)
            {
                NotifyMusic(music);
            }
            else if (entity is TvSeriasEntity tvSeries)
            {
                NotifyTvSeries(tvSeries);
            }
        }

        public void SendBillingUser(int id, decimal sum, string currency, 
            BillingType type)
        {
            var user = _unitOfWork.UserAuthorisedRepository.GetById(id);
            var billing = Mapper.Map<BillingNotification>(user);
            billing.Sum = sum;
            billing.Currency = currency;
            string body;
            if (type == BillingType.Donate)
            {
                var template = _unitOfWork.EmailTemplateRepository.Get(x => x.Title ==
                    Resources.Donate).First().Body;
                body = Engine.Razor.RunCompile(template, Resources.Donate, null, 
                    new { Donate = billing });
            }
            else
            {
                var template = _unitOfWork.EmailTemplateRepository.Get(x => x.Title ==
                    Resources.Premium).First().Body;
                body = Engine.Razor.RunCompile(template, Resources.Premium, null,
                    new { Premium = billing });
            }
            var message = CreateMessage(billing.Receiver, body);
            SendEmailAsync(message).Wait();
        }

        public void SendBillingGuest(string name, string email, decimal sum, string currency)
        {
            var billing = new BillingNotification()
            {
                Receiver = new Receiver()
                {
                    Emails = new List<string>() {email},
                    FirstName = name ?? Resources.GuestAlias
                },
                Sum = sum,
                Currency = currency
            };
            string template = _unitOfWork.EmailTemplateRepository.Get(x => x.Title ==
                Resources.Donate).First().Body;
            string body = Engine.Razor.RunCompile(template, Resources.Donate, 
                null, new { Donate = billing });
            var messageToSend = CreateMessage(billing.Receiver, body);
            SendEmailAsync(messageToSend).Wait();
        }

        public void SendConfirmation(string name, string email, string url)
        {
            var confirmation = new ConfirmationNotification()
            {
                Receiver = new Receiver()
                {
                    Emails = new List<string>() {email},
                    FirstName = name
                },
                Url = url
            };
            string template = _unitOfWork.EmailTemplateRepository.Get(x => x.Title ==
                Resources.Confirmation).First().Body;
            string body = Engine.Razor.RunCompile(template, Resources.Confirmation, null, 
                new { Confirmation = confirmation });
            var messageToSend = CreateMessage(confirmation.Receiver, body);
            SendEmailAsync(messageToSend).Wait();
        }

        public void SendCustom(string firstName, string lastName, ICollection<string> emails,
            string senderName, string subject, string body, bool html)
        {
            var custom = new CustomNotification()
            {
                Receiver = new Receiver()
                {
                    Emails = emails,
                    FirstName = firstName,
                    LastName = lastName
                },
                SenderName = senderName,
                Subject = subject,
                Body = body,
                HtmlBody = html
            };
            var message = CreateCustomMessage(custom);
            SendEmailAsync(message).Wait();
        }

        private void NotifyMusic(DbMusic music)
        {
            var release = Mapper.Map<MusicNotification>(music);
            string template = _unitOfWork.EmailTemplateRepository.
                Get(x => x.Title == Resources.Music).First().Body;

            var tasks = new List<Task>();

            for (int i = 0; i < release.Receivers.Count; i++)
            {
                var body = Engine.Razor.RunCompile(template,
                    Resources.Music, null, new { Music = release, Count = i });
                var message = CreateMessage(release.Receivers.ElementAt(i), 
                    body);
                tasks.Add(SendEmailAsync(message));
            }
            Task.WhenAll(tasks);
        }

        private void NotifyTvSeries(TvSeriasEntity tvSeries)
        {
            var release = Mapper.Map<TvSeriesNotification>(tvSeries);
            string template = _unitOfWork.EmailTemplateRepository.
                Get(x => x.Title == Resources.TvSeries).First().Body;

            var tasks = new List<Task>();

            for (int i = 0; i < release.Receivers.Count; i++)
            {
                var body = Engine.Razor.RunCompile(template,
                    Resources.TvSeries, null, new { TvSeries = release, Count = i });
                var message = CreateMessage(release.Receivers.ElementAt(i),
                    body);
                tasks.Add(SendEmailAsync(message));
            }
            Task.WhenAll(tasks);
        }

        private MimeMessage CreateMessage(Receiver receiver, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(Resources.Sender, Settings.Default.Login));

            foreach (var email in receiver.Emails)
            {
                message.To.Add(new MailboxAddress(email));
            }

            message.Subject = Resources.Subject;

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;
            message.Body = bodyBuilder.ToMessageBody();

            return message;
        }

        private MimeMessage CreateCustomMessage(CustomNotification custom)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(custom.SenderName, Settings.Default.Login));

            foreach (var email in custom.Receiver.Emails)
            {
                message.To.Add(new MailboxAddress(email));
            }

            message.Subject = custom.Subject;
            BodyBuilder bodyBuilder = new BodyBuilder();

            if (custom.HtmlBody)
            {
                bodyBuilder.HtmlBody = custom.Body;
            }
            else
            {
                bodyBuilder.TextBody = custom.Body;
            }
            message.Body = bodyBuilder.ToMessageBody();
            return message;
        }

        private async Task SendEmailAsync(MimeMessage message)
        {
            await _transport.ConnectAsync(
                Settings.Default.Host,
                Settings.Default.Port,
                true).ConfigureAwait(false);

            _transport.AuthenticationMechanisms.Remove("XOAUTH2");

            await _transport.AuthenticateAsync(
                Settings.Default.Login,
                Settings.Default.Password).ConfigureAwait(false);

            await _transport.SendAsync(message).ConfigureAwait(false);

            // Уточнить касательно необходимости !
            await _transport.DisconnectAsync(true).ConfigureAwait(false);
        }

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed) return;
            if (disposing)
            {
                GC.SuppressFinalize(this);
            }
            _transport?.Dispose();
            _transport = null;
            _isDisposed = true;
            base.Dispose(disposing);
        }

        ~MailNotificationService()
        {
            Dispose(false);
        }
    }
}