using System;
using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels.User
{
    /// <summary>
    /// Представляет модель хранения данных о пользователе
    /// </summary>
    public class DbUser 
    {
        /// <summary>
        /// Возвращает или задает уникальный идентификатор пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Возвращает или задает логин пользователя
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Возвращает или задает пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Возвращает или задает статус аккаунта
        /// (активирован, не активирован, деактивирован, забанен и так далее)
        /// </summary>
        public DbAccountStatus AccountStatus { get; set; }

        /// <summary>
        /// Возвращает или задает уровень пользователя
        /// (пока что это - обычный и премиум пользователь)
        /// </summary>
        public DbAccountLevel AccountLevel { get; set; }

        /// <summary>
        /// Возвращает или задает необходимость подтверждения регистрации
        /// путем активации Email
        /// </summary>
        public bool ActivationNeeded { get; set; }

        /// <summary>
        /// Задает или возвращает гражданство пользователя
        /// </summary>
        public DbCountry Sitizenship { get; set; }

        /// <summary>
        /// Задает или возвращает язык пользователя
        /// </summary>
        public DbLanguage Language { get; set; }

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
        public DbHowToCall HowToCall { get; set; }

        /// <summary>
        /// Задает или возвращает коллекцию телефонных номеров пользователя
        /// </summary>
        public ICollection<string> Phones { get; set; }

        /// <summary>
        /// Задает или возвращает коллекцию Email
        /// </summary>
        public ICollection<string> EMailAddresses { get; set; }

        /// <summary>
        /// Возвращает или задает почтовый адрес пользователя
        /// </summary>
        public DbAddress MailAddress { get; set; }
    }
}
