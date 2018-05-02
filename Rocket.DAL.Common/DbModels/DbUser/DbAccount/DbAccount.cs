namespace Rocket.DAL.Common.DbModels
{
    using System;
    
    /// <summary>
    /// Представляет модель хранения данных об аккаунта пользователя
    /// </summary>
    public class DbAccount
    {
        /// <summary>
        /// Возвращает или задает уникальный идентификационный номер аккаунта
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
        /// Возвращает или задаёт дату и время создания
        /// или изменения статуса аккаунта.
        /// </summary>
        public DateTime DateTimeStamp { get; set; }
    }
}
