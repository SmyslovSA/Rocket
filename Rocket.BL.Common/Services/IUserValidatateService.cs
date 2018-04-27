using System;
using Rocket.BL.Common.Models.User;

namespace Rocket.BL.Common.Services
{
    /// <summary>
    /// Валидирует (проверяет) значение экземпляра 
    /// пользователя при его регистрации, а также изменении.
    /// </summary>
    public interface IUserValidateService
    {
        /// <summary>
        /// Валидирует (проверяет) экземпляр пользователя
        /// при его (вернее, перед) добавлении (регистрации) в хранилище данных.
        /// </summary>
        /// <param name="user">Экземпляр пользователя, который проверяется</param>
        /// <returns>Возвращает <see langword='true'/>, если проверка завершена успешно</returns>
        bool IUserValidateOnAddition(IUser user);

        /// <summary>
        /// Валидирует (проверяем) экземпляр пользователя
        /// при его (вернее, перед) изменением.
        /// </summary>
        /// <param name="user">Экземпляр пользователя, который проверяется</param>
        /// <returns>Возвращает <see langword='true'/>, если проверка завершена успешно</returns>
        bool IUserValidateOnUpdating(IUser user);
    }
}
