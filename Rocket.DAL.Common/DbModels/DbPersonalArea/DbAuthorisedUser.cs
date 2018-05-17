using System.Collections.Generic;
using Rocket.DAL.Common.DbModels.User;

namespace Rocket.DAL.Common.DbModels.DbPersonalArea
{
    /// <summary>
    /// модель хранения данных авторизованного пользователя
    /// </summary>
    public class DbAuthorisedUser
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// внешний ключ к таблице DbUser
        /// </summary>
        public int? DbUserId { get; set; }

        /// <summary>
        /// ссылка на DbUser
        /// </summary>
        public User.DbUser DbUser { get; set; }

        /// <summary>
        /// относительный путь от корневой папки приложения к изображению аватара пользователя
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// список e-mail пользователя
        /// </summary>
        public virtual ICollection<DbEmail> Email { get; set; }

        /// <summary>
        /// коллекция выбранных жанров пользователя
        /// </summary>
        public virtual ICollection<DbGenre> Genres { get; set; }
    }
}