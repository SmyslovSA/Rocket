namespace Rocket.DAL.Common.DbModels.DbPersonalArea
{
    /// <summary>
    /// модель хранения данных e-mail
    /// </summary>
    public class DbEmail
    {
        /// <summary>
        /// Id email
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Имя Email
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// внешний ключ к таблице User
        /// </summary>
        public int? UserId { get; set; }
        /// <summary>
        /// ссылка на Userа
        /// </summary>
        public virtual DbAuthorisedUser User { get; set; }
    }
}
