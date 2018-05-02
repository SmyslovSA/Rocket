using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels.DbPersonalArea
{
    /// <summary>
    /// модель хранения данных пользователя
    /// </summary>
    public class DbUser
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// имя пользователя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// фамилия пользователя
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// логин пользователя
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// пароль пользователя
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// относительный путь от корневой папки приложения к изображению аватара пользователя
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// список e-mail пользователя
        /// </summary>
        public ICollection<DbEmail> Email { get; set; }
        /// <summary>
        /// коллекция выбранных жанров пользователя
        /// </summary>
        public ICollection<DbGenre> Genres { get; set; }
    }
}
