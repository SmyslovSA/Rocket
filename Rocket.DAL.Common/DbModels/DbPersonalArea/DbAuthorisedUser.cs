using System.Collections.Generic;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.DbModels.User;

namespace Rocket.DAL.Common.DbModels.DbPersonalArea
{
    /// <summary>
    /// Модель хранения данных авторизованного пользователя.
    /// </summary>
    public class DbAuthorisedUser
    {
        /// <summary>
        /// Id авторизованного пользователя личного кабинета.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Ссылка на DbUser.
        /// </summary>
        public DbUser DbUser { get; set; }

        /// <summary>
        /// Относительный путь от корневой папки приложения к изображению аватара пользователя.
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Список e-mail пользователя.
        /// </summary>
        public virtual ICollection<DbEmail> Email { get; set; }

        /// <summary>
        /// Коллекция выбранных жанров пользователя.
        /// </summary>
        public virtual ICollection<GenreEntity> Genres { get; set; }
    }
}