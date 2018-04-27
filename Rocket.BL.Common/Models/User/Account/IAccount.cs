using System;
using Rocket.BL.Common.Models.UserRoles;

namespace Rocket.BL.Common.Models.User
{
    /// <summary>
    /// Интерфейс аккаунта.
    /// </summary>
    public interface IAccount
    {
        int Id { get; set; } // Уникальный идентификатор.

        string Login { get; set; } // Имя пользователя.

        IPassword Password { get; set; } // Пароль. Содержит, как сам пароль, так и требования к нему.

        StatusType Status { get; set; } // Статус аккаунта. Создан, деактивирован и так далее.

        Level Level { get; set; } // Уровень аккаунта, золотой и т д.))

        bool ActivationNeeded {get; set; } // Требуется ли активация Email.

        DateTime DateTimeStamp { get; set; } // Дата создания, обновления аккаунта.

        Role Role { get; set; } // Роль.
    }
}
