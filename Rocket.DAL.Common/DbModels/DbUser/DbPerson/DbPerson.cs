namespace Rocket.DAL.Common.DbModels.DbUser.DbPerson
{
    /// <summary>
    /// Представляет модель хранения данных о человеке
    /// </summary>
    public class DbPerson
    {
        /// <summary>
        /// Задает или возвращает уникальный идентификатор пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает или задает имя и фамилию пользователя
        /// </summary>
        public DbPersonality Personality { get; set; }

        /// <summary>
        /// Возвращает или задает контактную информацию пользователя (телефон, Email, адрес и так далее)
        /// </summary>
        public DbCommunication Communication { get; set; }

        /// <summary>
        /// Возвращает или задает язык и гражданство пользователя
        /// </summary>
        public DbLocalization Locallization { get; set; }

        /// <summary>
        /// Возвращает или задает дату рождения и пол
        /// </summary>
        public DbIdentity Identity { get; set; }

        /// <summary>
        /// Возвращает или задает пользователя,
        /// к которому относится данный человек
        /// </summary>
        public DbUser DbUser { get; set; }
    }
}
