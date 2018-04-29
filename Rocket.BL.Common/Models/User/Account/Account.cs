using System;
using Rocket.BL.Common.Models.UserRoles;

namespace Rocket.BL.Common.Models.User
{
    /// <summary>
    /// Представляет информацию об аккануте.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Возвращает или задает идентификационный номер аккаунта
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает или задает логин пользователя
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Возвращает или задает пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Возвращает или задает статус акканута
        /// (активирован, не активирован, деактивирован, забанен)
        /// </summary>
        public AccountStatus AccountStatus { get; set; }

        /// <summary>
        /// Возвращает или задает уровень пользователя
        /// (пока что это - обычный и премиум пользователь)
        /// </summary>
        public AccountLevel AccountLevel { get; set; } // Уровень аккаунта, золотой и т д.)).

        /// <summary>
        /// Возвращает или задает необходимость подтверждения регистрации
        /// путем активации Email
        /// </summary>
        public bool ActivationNeeded { get; set; } // Требуется ли активация Email.
        
        /// <summary>
        /// Возвращает или задачт дату и время создания
        /// или изменения статуса аккаунта.
        /// </summary>
        public DateTime DateTimeStamp { get; set; } // Дата создания.
    }
}
