using Rocket.BL.Common.Models.User;

namespace Rocket.BL.Common.Services
{
    /// <summary>
    /// Задает или получает уровень пользователя,
    /// например, обычный или премиум.
    /// Уровень задан перечислением 'Level'
    /// </summary>
    public interface IUserAccountLevelService
    {
        /// <summary>
        /// Получает уровень аккаунта пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns>Уровень аккаунта пользователя</returns>
        Level GetUserAccountLevel(int id);

        /// <summary>
        /// Задает значение уровня аккаунта пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <param name="level">Задаваемый уровень аккаунта.</param>
        void SetUserAccountLevel(int id, Level level);
    }
}
