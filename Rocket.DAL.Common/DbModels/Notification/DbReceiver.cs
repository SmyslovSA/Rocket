using System.Collections.Generic;
using Rocket.DAL.Common.DbModels.DbPersonalArea;

namespace Rocket.DAL.Common.DbModels.Notification
{
    /// <summary>
    /// Описывает модель хранения данных о получателе сообщения
    /// </summary>
    public class DbReceiver
    {
        /// <summary>
        /// Возвращает или задает идентификационный номер получателя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает или задает идентификационный номер получателя
        /// из модели <see cref="DbAuthorisedUser"/>
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Возвращает или задает имя получателя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию email адресов,
        /// принадлежащих получателю
        /// </summary>
        public ICollection<DbEmail> Emails { get; set; }

        /// <summary>
        /// Возвращает или задает флаг подписки на email нотификацию
        /// </summary>
        public bool NotifyByEmail { get; set; }

        /// <summary>
        /// Возвращает или задает флаг подписки на push нотификацию
        /// </summary>
        public bool NotifyByPush { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию сообщений о платежах,
        /// получателем которых является пользователь
        /// </summary>
        public ICollection<DbBillingMessage> BillingMessages { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию сообщений произвольного содержания,
        /// получателем которых является пользователь
        /// </summary>
        public ICollection<DbCustomMessage> CustomMessages { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию объектов, содержащих 
        /// сводные данные о получателе и сообщении о релизе фильма
        /// </summary>
        public ICollection<ReceiversJoinFilms> ReceiversJoinFilms { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию объектов, содержащих 
        /// сводные данные о получателе и сообщении о музыкальном релизе
        /// </summary>
        public ICollection<ReceiversJoinMusics> ReceiversJoinMusics { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию объектов, содержащих 
        /// сводные данные о получателе и сообщении релизе сериала
        /// </summary>
        public ICollection<ReceiversJoinTVSeries> ReceiversJoinTVSeries { get; set; }
    }
}