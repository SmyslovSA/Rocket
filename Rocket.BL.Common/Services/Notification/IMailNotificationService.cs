﻿using System;
using System.Collections.Generic;
using Rocket.BL.Common.Enums;
using Rocket.DAL.Common.DbModels.DbPersonalArea;
using Rocket.DAL.Common.DbModels.Subscription;

namespace Rocket.BL.Common.Services.Notification
{
    /// <summary>
    /// Интерфейс взаимодействия с сервисом email нотификации
    /// </summary>
    public interface IMailNotificationService : IDisposable
    {
        /// <summary>
        /// Отправка сообщения о релизе
        /// </summary>
        /// <param name="entity">подлежащий отправке релиз</param>
        void NotifyAboutRelease(SubscribableEntity entity);


        /// <summary>
        /// Отправка пользователю сообщения с благодарностью за совершенный донат
        /// либо оплату премиум аккаунта
        /// </summary>
        /// <param name="id">Идентификатор пользователя <see cref="DbAuthorisedUser"/></param>
        /// <param name="sum">Оплаченная сумма</param>
        /// <param name="currency">Валюта совершенного платежа</param>
        /// <param name="type">Цель оплаты: премиум или донат</param>
        void SendBillingUser(int id, decimal sum, string currency,
            BillingType type);

        /// <summary>
        /// Отправка гостю сообщения с благодарностью за совершенный донат
        /// </summary>
        /// <param name="name">Имя гостя (если указано)</param>
        /// <param name="email">Email гостя</param>
        /// <param name="sum">Оплаченная сумма</param>
        /// <param name="currency">Валюта совершенного платежа</param>
        void SendBillingGuest(string name, string email, decimal sum,
            string currency);

        /// <summary>
        /// Отправка посетителю сообщения со ссылкой, необходимой
        /// для завершения регистрации аккаунта
        /// </summary>
        /// <param name="email">Email адрес посетителя</param>
        /// <param name="url">Ссылка для завершения регистрации аккаунта</param>
        /// <param name="name">Имя посетителя</param>
        void SendConfirmation(string name, string email, string url);

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
        void SendCustom(string firstName, string lastName, ICollection<string> emails,
            string senderName, string subject, string body, bool html);
    }
}