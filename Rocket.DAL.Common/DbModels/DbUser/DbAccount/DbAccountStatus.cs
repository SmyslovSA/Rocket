namespace Rocket.DAL.Common.DbModels
{
    /// <summary>
    /// Представляет модель хранения данных о статусе аккаунта
    /// </summary>
    public class DbAccountStatus
    {
        /// <summary>
        /// Возвращает или задает уникальный идентификатор статуса аккаунта пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает или задает название статуса аккаунта
        /// (активирован, не активирован, деактивирован, забанен)
        /// </summary>
        public string Name { get; set; }
    }
}
