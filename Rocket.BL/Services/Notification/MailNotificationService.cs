﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using RazorEngine;
using RazorEngine.Templating;
using Rocket.BL.Common.Enums;
using Rocket.BL.Common.Models.Notification;
using Rocket.BL.Common.Services.Notification;
using Rocket.BL.Properties;
using Rocket.DAL.Common.DbModels.DbPersonalArea;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.DbModels.ReleaseList;
using Rocket.DAL.Common.DbModels.Subscription;
using Rocket.DAL.Common.UoW;
using Rocket.DAL.UoW;

namespace Rocket.BL.Services.Notification
{
    /// <summary>
    /// Сервис email нотификации
    /// </summary>
    public class MailNotificationService : BaseService, IMailNotificationService
    {
        private IEnumerable<IMailTransport> _transport;
        private bool _isDisposed = false;

        /// <summary>
        /// Создает новый экземпляр <see cref="MailNotificationService"/>
        /// </summary>
        /// <param name="unitOfWork"> Экземпляр <see cref="UnitOfWork"/></param>
        /// <param name="transport"> Коллекция экземпляров <see cref="SmtpClient"/> </param>
        public MailNotificationService(IUnitOfWork unitOfWork,
            IEnumerable<IMailTransport> transport)
            : base(unitOfWork)
        {
            _transport = transport;
        }

        /// <summary>
        /// Отправка сообщения о релизе
        /// </summary>
        /// <param name="entities"> Подлежащие отправке релизы </param>
        public async Task NotifyAboutReleaseAsync(IEnumerable<SubscribableEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity is DbMusic music)
                {
                    await NotifyMusicAsync(music);
                }
                else if (entity is TvSeriasEntity tvSeries)
                {
                    await NotifyTvSeries(tvSeries);
                }
            }
        }

        /// <summary>
        /// Отправка пользователю сообщения с благодарностью за совершенный донат
        /// либо оплату премиум аккаунта
        /// </summary>
        /// <param name="id">Идентификатор пользователя <see cref="DbAuthorisedUser"/></param>
        /// <param name="sum">Оплаченная сумма</param>
        /// <param name="currency">Валюта совершенного платежа</param>
        /// <param name="type">Цель оплаты: премиум или донат</param>
        public async Task SendBillingUserAsync(int id, decimal sum, string currency,
            BillingType type)
        {
            var user = _unitOfWork.UserAuthorisedRepository.Get(
                x => x.DbUserId == id, null, "DbUser").First();
            var billing = Mapper.Map<BillingNotification>(user);
            billing.Sum = sum;
            billing.Currency = currency;
            string body;
            if (type == BillingType.Donate)
            {
                var template = _unitOfWork.EmailTemplateRepository.GetById(
                    Convert.ToInt32(Resources.Donate)).Body;
                body = Engine.Razor.RunCompile(template, Resources.Donate, null,
                    new { Donate = billing });
            }
            else
            {
                var template = _unitOfWork.EmailTemplateRepository.GetById(
                    Convert.ToInt32(Resources.Premium)).Body;
                body = Engine.Razor.RunCompile(template, Resources.Premium, null,
                    new { Premium = billing });
            }
            var message = CreateMessage(billing.Receiver, body);
            await SendEmailAsync(_transport.ElementAt(0), new[] { message });
        }

        /// <summary>
        /// Отправка гостю сообщения с благодарностью за совершенный донат
        /// </summary>
        /// <param name="name">Имя гостя (если указано)</param>
        /// <param name="email">Email гостя</param>
        /// <param name="sum">Оплаченная сумма</param>
        /// <param name="currency">Валюта совершенного платежа</param>
        public async Task SendBillingGuestAsync(string name, string email, decimal sum, string currency)
        {
            var billing = new BillingNotification()
            {
                Receiver = new Receiver()
                {
                    Emails = new List<string>() { email },
                    FirstName = name ?? Resources.GuestAlias,
                    LastName = ""
                },
                Sum = sum,
                Currency = currency
            };
            string template = _unitOfWork.EmailTemplateRepository.GetById(
                Convert.ToInt32(Resources.Donate)).Body;
            string body = Engine.Razor.RunCompile(template, Resources.Donate,
                null, new { Donate = billing });
            var messageToSend = CreateMessage(billing.Receiver, body);
            await SendEmailAsync(_transport.ElementAt(0), new[] { messageToSend });
        }

        /// <summary>
        /// Отправка посетителю сообщения со ссылкой, необходимой
        /// для завершения регистрации аккаунта
        /// </summary>
        /// <param name="email">Email адрес посетителя</param>
        /// <param name="url">Ссылка для завершения регистрации аккаунта</param>
        /// <param name="name">Имя посетителя</param>
        public async Task SendConfirmationAsync(string name, string email, string url)
        {
            var confirmation = new ConfirmationNotification()
            {
                Receiver = new Receiver()
                {
                    Emails = new List<string>() { email },
                    FirstName = name
                },
                Url = url
            };
            string template = _unitOfWork.EmailTemplateRepository.GetById(
                Convert.ToInt32(Resources.Confirmation)).Body;
            string body = Engine.Razor.RunCompile(template, Resources.Confirmation, null,
                new { Confirmation = confirmation });
            var messageToSend = CreateMessage(confirmation.Receiver, body);
            await SendEmailAsync(_transport.ElementAt(0), new[] { messageToSend });
        }

        /// <summary>
        /// Отправка сообщения произвольного содержания
        /// </summary>
        /// <param name="firstName">Имя получателя</param>
        /// <param name="lastName">Фамилия получателя</param>
        /// <param name="emails">Список email адресов получателя</param>
        /// <param name="senderName">Имя отправителя</param>
        /// <param name="subject">Тема сообщения</param>
        /// <param name="body">Содержание сообщения</param>
        /// <param name="html">Флаг указывающий является ли содержание разметкой HTML</param>
        public async Task SendCustomAsync(string firstName, string lastName, ICollection<string> emails,
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
            await SendEmailAsync(_transport.ElementAt(0), new[] { message });
        }

        private async Task NotifyMusicAsync(DbMusic music)
        {
            var release = Mapper.Map<MusicNotification>(music);
            string template = _unitOfWork.EmailTemplateRepository.
                GetById(Convert.ToInt32(Resources.Music)).Body;

            int quota = release.Receivers.Count / _transport.Count();
            if (quota < 1)
            {
                quota = 1;
            }

            var tasks = new List<Task>();
            var messages = new List<MimeMessage>();
            int smtpCount = 0;

            for (int i = 0; i < release.Receivers.Count; i++)
            {
                var body = Engine.Razor.RunCompile(template,
                    Resources.Music, null, new { Music = release, Count = i });
                var message = CreateMessage(release.Receivers.ElementAt(i),
                    body);
                messages.Add(message);
                if ((i + 1) % quota == 0)
                {
                    tasks.Add(SendEmailAsync(_transport.ElementAt(smtpCount), messages));
                    smtpCount++;
                    messages.Clear();
                }
            }
            await Task.WhenAll(tasks);
        }

        private async Task NotifyTvSeries(TvSeriasEntity tvSeries)
        {
            var release = Mapper.Map<TvSeriesNotification>(tvSeries);
            string template = _unitOfWork.EmailTemplateRepository.
                Get(x => x.Title == Resources.TvSeries).First().Body;

            int quota = release.Receivers.Count / _transport.Count();
            if (quota < 1)
            {
                quota = 1;
            }

            var tasks = new List<Task>();
            var messages = new List<MimeMessage>();
            int smtpCount = 0;

            for (int i = 0; i < release.Receivers.Count; i++)
            {
                var body = Engine.Razor.RunCompile(template,
                    Resources.TvSeries, null, new { TvSeries = release, Count = i });
                var message = CreateMessage(release.Receivers.ElementAt(i),
                    body);
                messages.Add(message);
                if ((i + 1) % quota == 0)
                {
                    tasks.Add(SendEmailAsync(_transport.ElementAt(smtpCount), messages));
                    smtpCount++;
                    messages.Clear();
                }
            }
            await Task.WhenAll(tasks);
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

        private async Task SendEmailAsync(IMailTransport transport, IEnumerable<MimeMessage> messages)
        {
            foreach (var message in messages.ToList())
            {
                transport.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await transport.ConnectAsync(
                    Settings.Default.Host,
                    Settings.Default.Port,
                    SecureSocketOptions.Auto).ConfigureAwait(false);

                transport.AuthenticationMechanisms.Remove("XOAUTH2");

                await transport.AuthenticateAsync(
                    Settings.Default.Login,
                    Settings.Default.Password).ConfigureAwait(false);

                await transport.SendAsync(message).ConfigureAwait(false);

                await transport.DisconnectAsync(true).ConfigureAwait(false);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed) return;
            if (disposing)
            {
                GC.SuppressFinalize(this);
            }

            foreach (var mailTransport in _transport)
            {
                mailTransport?.Dispose();
                _transport = null;
                _isDisposed = true;
            }
            base.Dispose(disposing);
        }

        ~MailNotificationService()
        {
            Dispose(false);
        }
    }
}