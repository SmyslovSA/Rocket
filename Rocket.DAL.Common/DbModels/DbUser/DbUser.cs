namespace Rocket.DAL.Common.DbModels.DbUser.DbAccount
{
    /// <summary>
    /// Представляет модель хранения данных о пользователе
    /// </summary>
    public class DbUser
    {
        /// <summary>
        /// Возвращает или задает уникальный идентификатор пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает или задает информацию о человеке
        /// </summary>
        public DbPerson.DbPerson Person { get; set; }

        /// <summary>
        /// Возвращает или задает информацию об аккаунте
        /// </summary>
        public DbAccount Account { get; set; }
    }
}
