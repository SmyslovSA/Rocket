using System;
using Bogus;
using Rocket.DAL.Common.DbModels.DbUserRole;
using System.Collections.Generic;

namespace Rocket.BL.Tests.User.FakeData
{
    /// <summary>
    /// Представляет набор сгенерированных ролей пользователя,
    /// в моделях хранения данных
    /// </summary>
    public class FakeDbRoles
    {
        /// <summary>
        /// Возвращает генератор ролей пользователя
        /// </summary>
        public Faker<DbRole> RoleFaker { get; }

        /// <summary>
        /// Возвращает коллекцию сгенерированных ролей пользователя
        /// </summary>
        public List<DbRole> Roles { get; }

        /// <summary>
        /// Создает новый экземпляр сгенерированных данных о ролях пользователя
        /// </summary>
        /// <param name="rolesCount">Необходимое количество сгенерированных ролей пользоватоля</param>
        public FakeDbRoles(int rolesCount)
        {
            this.RoleFaker = new Faker<DbRole>()
                .RuleFor(c => c.Id, f => f.IndexFaker)
                .RuleFor(c => c.Name, f => f.Lorem.Word())
                .RuleFor(c => c.IsActive, f => f.PickRandomParam(new bool[] { true, false, false, true, true }))
                .RuleFor(c => c.Permissions, f => { return (new FakeDbPermissions((new Random()).Next(1, 5))).Permissions.ToArray(); });

            this.Roles = this.RoleFaker.Generate(rolesCount);
        }
    }
}
