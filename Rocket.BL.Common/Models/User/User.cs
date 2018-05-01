namespace Rocket.BL.Common.Models.User
{
    /// <summary>
    /// Представляет информацию о пользователе
    /// </summary>
    public class User
    {
        /// <summary>
        /// Возвращает или задает уникальный идентификатор пользователя
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Возвращает или задает информацию о человеке
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        /// Возвращает или задает информацию об аккаунте
        /// </summary>
        public Account Account { get; set; }
    }
}
