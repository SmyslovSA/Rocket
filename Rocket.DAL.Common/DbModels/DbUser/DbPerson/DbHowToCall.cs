namespace Rocket.DAL.Common.DbModels
{
    /// <summary>
    /// Представляет модель хранения сведений о том, как обращаться к пользователю
    /// </summary>
    public class DbHowToCall
    {
        /// <summary>
        /// Возвращает или задает уникальный идентификатор сведений о том, как обращаться к пользователю
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает или задает обращение к пользователю
        /// </summary>
        public string Name { get; set; }
    }
}
