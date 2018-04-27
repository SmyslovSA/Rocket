using System;
using Rocket.BL.Common.Models.UserRoles;

namespace Rocket.BL.Common.Models.User
{
    /// <summary>
    /// Тип аккаунта.
    /// </summary>
    public class Account : IAccount
    {
        public int Id { get; set; } // Уникальный идентификатор.

        public string Login { get; set; } // Имя пользователя.

        public IPassword Password { get; set; } // Пароль. Содержит, как сам пароль, так и требования к нему.

        public StatusType Status { get; set; } // Статус аккаунта. Создан, деактивирован и так далее.

        public Level Level { get; set; } // Уровень аккаунта, золотой и т д.)).

        public bool ActivationNeeded { get; set; } // Требуется ли активация Email.

        public Role Role { get; set; } // Роль.

        public DateTime DateTimeStamp { get; set; } // Дата создания.
    }
}
