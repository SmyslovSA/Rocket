using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels.DbPersonalArea
{
    /// <summary>
    /// модель хранения данных обычного пользователя
    /// </summary>
    public class DbSimpleUser
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public ulong Id { get; set; }
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
        public ICollection<string> Email { get; set; }
        /// <summary>
        /// внешний ключ к модели PersonalizedTape пользвателя
        /// </summary>
        public int? PersonalizedTapeId { get; set; }
        /// <summary>
        /// персональный список релизов пользователя
        /// </summary>
        public DbPersonalizedTape PersonalizedTape { get; set; }
    }
}
