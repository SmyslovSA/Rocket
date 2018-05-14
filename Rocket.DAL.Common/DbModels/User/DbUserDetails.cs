﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Rocket.DAL.Common.DbModels.Enum;

namespace Rocket.DAL.Common.DbModels.User
{
    /// <summary>
    /// Представляет модель хранения детальных данных о пользователе
    /// </summary>
    public class DbUserDetails
    {
        /// <summary>
        /// Возвращает или задает уникальный идентификатор дополнительной информации пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает или задает необходимость подтверждения регистрации
        /// путем активации Email
        /// </summary>
        public bool ActivationNeeded { get; set; }

        /// <summary>
        /// Задает или возвращает гражданство пользователя
        /// </summary>
        public virtual DbCountry Sitizenship { get; set; }

        /// <summary>
        /// Задает или возвращает язык пользователя
        /// </summary>
        public virtual DbLanguage Language { get; set; }

        /// <summary>
        /// Задает или возвращает дату рождения пользователя
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Задает или возвращает пол пользователя
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// Задает или возвращает сведения о том, как обращаться к пользователю
        /// </summary>
        public virtual DbHowToCall HowToCall { get; set; }

        /// <summary>
        /// Задает или возвращает коллекцию телефонных номеров пользователя
        /// </summary>
        public virtual ICollection<string> Phones { get; set; } = new Collection<string>();

        /// <summary>
        /// Задает или возвращает коллекцию Email
        /// </summary>
        public virtual ICollection<string> EMailAddresses { get; set; } = new Collection<string>();

        /// <summary>
        /// Возвращает или задает почтовый адрес пользователя
        /// </summary>
        public virtual DbAddress MailAddress { get; set; }

        /// <summary>
        /// Возвращает или задает идентификатор пользователя,
        /// к которому относится эта дополнительная информация
        /// </summary>
        public int DbUserId { get; set; }

        /// <summary>
        /// Возвращает или задает пользователя,
        /// К которому относится эта дополнительная информация
        /// </summary>
        public DbUser DbUser { get; set; }
    }
}
