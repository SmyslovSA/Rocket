﻿using Rocket.BL.Common.Models.User.Account.StatusAndLevel;

namespace Rocket.BL.Common.Services
{
    /// <summary>
    /// Представляет сервис для работы с уровнем пользователя (обычный, премиум)
    /// </summary>
    public interface IUserAccountLevelService
    {
        /// <summary>
        /// Получает уровень аккаунта пользователя с заданным идентификатором
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>Уровень аккаунта пользователя</returns>
        AccountLevel GetUserAccountLevel(int id);

        /// <summary>
        /// Задает значение уровня аккаунта пользователя с заданным идентификатором
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <param name="level">Задаваемый уровень аккаунта</param>
        void SetUserAccountLevel(int id, AccountLevel AccountLevel);
    }
}
