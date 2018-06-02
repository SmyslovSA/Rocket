using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Rocket.DAL.Common.DbModels.DbPersonalArea;
using Rocket.DAL.Common.DbModels.Identity;
using Rocket.DAL.Common.DbModels.Subscription;

namespace Rocket.DAL.Common.DbModels.User
{
    /// <summary>
    /// Представляет модель хранения данных о пользователе.
    /// </summary>
    public class DbUser : IdentityUser<int, DbUserLogin, DbUserRole, DbUserClaim>, IUser<int>
    {
        /// <summary>
        /// Возвращает или задает уникальный идентификатор пользователя.
        /// </summary>
        public override int Id { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Возвращает или задает логин пользователя.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Возвращает или задает пароль.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Возвращает или задает идентификатор статуса аккаунта пользователя,
        /// к которому относится эта дополнительная информация.
        /// </summary>
        public int? AccountStatusId { get; set; }

        /// <summary>
        /// Возвращает или задает статус аккаунта пользователя
        /// (активирован, не активирован, деактивирован, забанен и так далее).
        /// </summary>
        public virtual DbAccountStatus AccountStatus { get; set; }

        /// <summary>
        /// Возвращает или задает идентификатор уровня аккаунта пользователя,
        /// к которому относится эта дополнительная информация.
        /// </summary>
        public int? AccountLevelId { get; set; }

        /// <summary>
        /// Возвращает или задает уровень пользователя
        /// (пока что это - обычный и премиум пользователь).
        /// </summary>
        public virtual DbAccountLevel AccountLevel { get; set; }
        
        /// <summary>
        /// Возвращает или задает детальную информация пользователя.
        /// </summary>
        public virtual DbUserDetail UserDetail { get; set; }

        /// <summary>
        /// Ссылка на DbAuthorisedUser.
        /// </summary>
        public virtual DbAuthorisedUser DbAuthorisedUser { get; set; }

        /// <summary>
        /// Коллекция подписок пользователя
        /// </summary>
        public ICollection<SubscribableEntity> Subscriptions { get; set; } = new Collection<SubscribableEntity>();
    }
}