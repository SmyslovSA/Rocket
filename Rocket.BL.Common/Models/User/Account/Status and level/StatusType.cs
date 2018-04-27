using System;

namespace Rocket.BL.Common.Models.User
{
    /// <summary>
    /// Типы статуса аккаунта
    /// </summary>
    public enum StatusType
    {
        Registered = 1, // Зарегистрирован

        Activated = 2, // Активирован через Email

        Banned = 3, // Забанен

        Diactivated = 4 // Деактивирован.
    }
}
