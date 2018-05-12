using System;
using Bogus;
using Rocket.DAL.Common.DbModels.DbUserRole;
using System.Collections.Generic;

namespace Rocket.BL.Tests.User.FakeData
{
    /// <summary>
    /// Представляет набор сгенерированных разрешений ролей пользователя,
    /// в моделях хранения данных
    /// </summary>
    public class FakeDbPermissions
    {
        /// <summary>
        /// Возвращает генератор разрешений ролей пользователя
        /// </summary>
        public Faker<DbPermission> PermissionFaker { get; }

        /// <summary>
        /// Возвращает коллекцию сгенерированных разрешений ролей пользователя
        /// </summary>
        public List<DbPermission> Permissions { get; }

        /// <summary>
        /// Создает новый экземпляр сгенерированных данных о разрешенийях ролей пользователя
        /// </summary>
        /// <param name="permissionsCount">Необходимое количество сгенерированных разрешений ролей пользоватоля</param>
        public FakeDbPermissions(int permissionsCount)
        {
            this.PermissionFaker = new Faker<DbPermission>()
                .RuleFor(c => c.Id, f => f.IndexFaker)
                .RuleFor(c => c.Description, f => f.Lorem.Sentences(5))
                .RuleFor(c => c.ValueName, f => f.Lorem.Letter(5))
                .RuleFor(c => c.Roles, f => null);

            this.Permissions = this.PermissionFaker.Generate(permissionsCount);
        }
    }
}
