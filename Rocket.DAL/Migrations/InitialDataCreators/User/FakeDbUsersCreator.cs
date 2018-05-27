using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Rocket.DAL.Common.DbModels.DbUserRole;
using Rocket.DAL.Common.DbModels.ReleaseList;
using Rocket.DAL.Common.DbModels.User;
using Rocket.DAL.Migrations.InitialDataCreators.User.FakeData;

namespace Rocket.DAL.Migrations.InitialDataCreators.User
{
    /// <summary>
    /// Представляет набор сгенерированных данных о пользователях,
    /// в моделях хранинения данных.
    /// </summary>
    public class FakeDbUsersCreator
    {
        /// <summary>
        /// Создает новый экземпляр сгенерированных данных о пользователях.
        /// </summary>
        /// <param name="dbAccountLevels">Коллекция уровней аккаунта пользователей.</param>
        /// <param name="dbAccountStatuses">Коллекция статусов аккаунта пользователей.</param>
        /// <param name="dbCountries">Коллекция всех стран.</param>
        /// <param name="dbGenders">Коллекция сведений о половой идентификации дополнительной информации пользователей.</param>
        /// <param name="dbHowToCalls">Коллекция сведений о том, как надо обращаться к пользователям.</param>
        /// <param name="dbLanguages">Коллекция всех используемых разговорных языков пользователей.</param>
        public FakeDbUsersCreator(
            ICollection<DbAccountLevel> dbAccountLevels,
            ICollection<DbAccountStatus> dbAccountStatuses,
            ICollection<DbCountry> dbCountries,
            ICollection<DbGender> dbGenders,
            ICollection<DbHowToCall> dbHowToCalls,
            ICollection<DbLanguage> dbLanguages)
        {
            var roles = new List<DbRole>()
            {
                new DbRole() { Id = 1, Name = "unregister" },
                new DbRole() { Id = 2, Name = "user" },
                new DbRole() { Id = 3, Name = "moderator" },
                new DbRole() { Id = 4, Name = "admin" },
            };

            var resultUnregisteredUsersSamples = new Faker<DbUser>()
                .RuleFor(p => p.Id, f => f.IndexFaker)
                .RuleFor(p => p.AccountStatus, f => dbAccountStatuses.ToArray()[new Random().Next(1, 3)])
                .RuleFor(p => p.AccountLevel, f => dbAccountLevels.ToArray()[new Random().Next(1, 2)])
                .RuleFor(p => p.Roles, f => roles.Where(role => role.Name == "unregister"))
                .RuleFor(p => p.FirstName, f => f.Person.FirstName)
                .RuleFor(p => p.LastName, f => f.Person.LastName)
                .RuleFor(p => p.Login, f => f.Lorem.Letter(5))
                .RuleFor(p => p.Password, f => f.Lorem.Letter(5))
                .RuleFor(p => p.UserDetail, f => new FakeDbUserDetails(dbCountries, dbGenders, dbHowToCalls, dbLanguages).UserDetails.ToArray()[0]);

            Users = resultUnregisteredUsersSamples.Generate(5); ;

            var resultUserUsersSamples = new Faker<DbUser>()
                .RuleFor(p => p.Id, f => f.IndexFaker + 5)
                .RuleFor(p => p.AccountStatus, f => dbAccountStatuses.ToArray()[new Random().Next(1, 3)])
                .RuleFor(p => p.AccountLevel, f => dbAccountLevels.ToArray()[new Random().Next(1, 2)])
                .RuleFor(p => p.Roles, f => roles.Where(role => role.Name == "user"))
                .RuleFor(p => p.FirstName, f => f.Person.FirstName)
                .RuleFor(p => p.LastName, f => f.Person.LastName)
                .RuleFor(p => p.Login, f => f.Lorem.Letter(5))
                .RuleFor(p => p.Password, f => f.Lorem.Letter(5))
                .RuleFor(p => p.UserDetail, f => new FakeDbUserDetails(dbCountries, dbGenders, dbHowToCalls, dbLanguages).UserDetails.ToArray()[0]);

            Users.AddRange(resultUserUsersSamples.Generate(5));

            var resultModeratorUsersSamples = new Faker<DbUser>()
                .RuleFor(p => p.Id, f => f.IndexFaker + 10)
                .RuleFor(p => p.AccountStatus, f => dbAccountStatuses.ToArray()[new Random().Next(1, 3)])
                .RuleFor(p => p.AccountLevel, f => dbAccountLevels.ToArray()[new Random().Next(1, 2)])
                .RuleFor(p => p.Roles, f => roles.Where(role => role.Name == "moderator"))
                .RuleFor(p => p.FirstName, f => f.Person.FirstName)
                .RuleFor(p => p.LastName, f => f.Person.LastName)
                .RuleFor(p => p.Login, f => f.Lorem.Letter(5))
                .RuleFor(p => p.Password, f => f.Lorem.Letter(5))
                .RuleFor(p => p.UserDetail, f => new FakeDbUserDetails(dbCountries, dbGenders, dbHowToCalls, dbLanguages).UserDetails.ToArray()[0]);

            Users.AddRange(resultModeratorUsersSamples.Generate(5));

            var resultAdminUsersSamples = new Faker<DbUser>()
                .RuleFor(p => p.Id, f => f.IndexFaker + 15)
                .RuleFor(p => p.AccountStatus, f => dbAccountStatuses.ToArray()[new Random().Next(1, 3)])
                .RuleFor(p => p.AccountLevel, f => dbAccountLevels.ToArray()[new Random().Next(1, 2)])
                .RuleFor(p => p.Roles, f => roles.Where(role => role.Name == "admin"))
                .RuleFor(p => p.FirstName, f => f.Person.FirstName)
                .RuleFor(p => p.LastName, f => f.Person.LastName)
                .RuleFor(p => p.Login, f => f.Lorem.Letter(5))
                .RuleFor(p => p.Password, f => f.Lorem.Letter(5))
                .RuleFor(p => p.UserDetail, f => new FakeDbUserDetails(dbCountries, dbGenders, dbHowToCalls, dbLanguages).UserDetails.ToArray()[0]);

            Users.AddRange(resultAdminUsersSamples.Generate(5)); ;
        }

        /// <summary>
        /// Возвращает коллекцию сгенерированных пользователей.
        /// </summary>
        public List<DbUser> Users { get; }
    }
}