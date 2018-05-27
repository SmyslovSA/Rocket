using System;
using System.Collections.Generic;
using Rocket.DAL.Common.DbModels.DbPersonalArea;
using Rocket.DAL.Common.DbModels.DbUserRole;
using Rocket.DAL.Common.DbModels.ReleaseList;
using Rocket.DAL.Common.DbModels.User;

namespace Rocket.DAL.Migrations.InitialDataCreators.User
{
    /// <summary>
    /// Представляет набор сгенерированных данных о пользователях,
    /// в моделях хранинения данных.
    /// </summary>
    public class FakeDbUsersCreator
    {
        /// <summary>
        /// Создает экземпляр сгенерированных данных о пользователях c различными ролями,
        /// пара Пользователь -> Роль.
        /// </summary>
        public FakeDbUsersCreator()
        {
            // Тестовая модель хранения данных пользователя с ролью "unregister".
            var userUnregistrated = new DbUser()
            {
                FirstName = "Сергей",
                LastName = "Иванов",
                Login = "sergey",
                Password = "qwert",
                AccountStatus = new DbAccountStatus() { Id = 1, Name = "Зарегистрирован" },
                AccountLevel = new DbAccountLevel() { Id = 1, Name = "Обычный" },
                Roles = new List<DbRole>() { new DbRole() { Name = "unregister" } },
                UserDetail = new DbUserDetail()
                {
                    ActivationNeeded = false,
                    SitizenshipId = 21,
                    LanguageId = 1,
                    DateOfBirth = new DateTime(1996, 6, 3, 22, 15, 0),
                    GenderId = 1,
                    HowToCallId = 1,
                    PhoneNumbers = new List<DbPhoneNumber>()
                    {
                        new DbPhoneNumber() { Number = "+375-345-12-22" },
                        new DbPhoneNumber() { Number = "+375-445-15-45" }
                    },
                    EMailAddresses = new List<DbEmailAddress>()
                    {
                        new DbEmailAddress() { Address = "serg@mail.ru" },
                        new DbEmailAddress() { Address = "serg1555@gmail.com" }
                    },
                    MailAddress = new DbAddress()
                    {
                        ZipCode = "220045",
                        CountryId = 21,
                        Country = new DbCountry() { Id = 21, Name = "Беларусь" },
                        City = "Минск",
                        Building = "35",
                        BuildingBlock = "B",
                        Flat = "35"
                    }
                },
                DbAuthorisedUser = new DbAuthorisedUser()
                {
                    Id = 1,
                    Avatar = @"\temp\avatars\sergey"
                }
            };

            // Тестовая модель хранения данных пользователя с ролью "user".
            var userUser = new DbUser()
            {
                FirstName = "Петр",
                LastName = "Желтый",
                Login = "petr",
                Password = "asdf",
                AccountStatus = new DbAccountStatus() { Id = 1, Name = "Зарегистрирован" },
                AccountLevel = new DbAccountLevel() { Id = 1, Name = "Обычный" },
                Roles = new List<DbRole>() {new DbRole() { Id = 3 } },
                UserDetail = new DbUserDetail()
                {
                    ActivationNeeded = false,
                    SitizenshipId = 21,
                    LanguageId = 1,
                    DateOfBirth = new DateTime(1996, 6, 3, 22, 15, 0),
                    GenderId = 1,
                    HowToCallId = 1,
                    PhoneNumbers = new List<DbPhoneNumber>()
                    {
                        new DbPhoneNumber() {Number = "+375-555-34-34"},
                        new DbPhoneNumber() {Number = "+375-23-23-23"}
                    },
                    EMailAddresses = new List<DbEmailAddress>()
                    {
                        new DbEmailAddress() {Address = "petrg@mail.ru"},
                        new DbEmailAddress() {Address = "petr1555@gmail.com"}
                    },
                    MailAddress = new DbAddress()
                    {
                        ZipCode = "220023",
                        CountryId = 21,
                        Country = new DbCountry() {Id = 21, Name = "Беларусь"},
                        City = "Минск",
                        Building = "25",
                        BuildingBlock = "",
                        Flat = "5"
                    }
                },
                DbAuthorisedUser = new DbAuthorisedUser()
                {
                    Id = 2,
                    Avatar = @"\temp\avatars\petr"
                }
            };

            // Тестовая модель хранения данных пользователя с ролью "moderator".
            var userModerator = new DbUser()
            {
                FirstName = "Лазарь",
                LastName = "Игнатьевич",
                Login = "qlazar",
                Password = "sdfgfg",
                AccountStatus = new DbAccountStatus() { Id = 1, Name = "Зарегистрирован" },
                AccountLevel = new DbAccountLevel() { Id = 1, Name = "Обычный" },
                Roles = new List<DbRole>() { new DbRole() { Id = 3 } },
                UserDetail = new DbUserDetail()
                {
                    ActivationNeeded = false,
                    SitizenshipId = 21,
                    LanguageId = 1,
                    DateOfBirth = new DateTime(1996, 6, 3, 22, 15, 0),
                    GenderId = 1,
                    HowToCallId = 1,
                    PhoneNumbers = new List<DbPhoneNumber>()
                    {
                        new DbPhoneNumber() { Number = "+375-555-34-34" },
                        new DbPhoneNumber() { Number = "+375-777-44-22" }
                    },
                    EMailAddresses = new List<DbEmailAddress>()
                    {
                        new DbEmailAddress() { Address = "qlazar@mail.ru" },
                        new DbEmailAddress() { Address = "qlazar1555@gmail.com" }
                    },
                    MailAddress = new DbAddress()
                    {
                        ZipCode = "220066",
                        CountryId = 21,
                        Country = new DbCountry() { Name = "Беларусь" },
                        City = "Минск",
                        Building = "45",
                        BuildingBlock = "А",
                        Flat = "5"
                    }
                },
                DbAuthorisedUser = new DbAuthorisedUser()
                {
                    Id = 3,
                    Avatar = @"\temp\avatars\qlazar"
                }
            };


            // Тестовая модель хранения данных пользователя с ролью "moderator".
            var userАdmin = new DbUser()
            {
                FirstName = "Филл",
                LastName = "Малый",
                Login = "fillip",
                Password = "qwerqwer",
                AccountStatus = new DbAccountStatus() { Id = 1, Name = "Зарегистрирован" },
                AccountLevel = new DbAccountLevel() { Id = 1, Name = "Обычный" },
                Roles = new List<DbRole>() { new DbRole() { Id = 1 } },
                UserDetail = new DbUserDetail()
                {
                    ActivationNeeded = false,
                    SitizenshipId = 21,
                    LanguageId = 1,
                    DateOfBirth = new DateTime(1996, 6, 3, 22, 15, 0),
                    GenderId = 1,
                    HowToCallId = 1,
                    PhoneNumbers = new List<DbPhoneNumber>()
                    {
                        new DbPhoneNumber() { Number = "+375-233-55-44" },
                        new DbPhoneNumber() { Number = "+375-444-63-66" }
                    },
                    EMailAddresses = new List<DbEmailAddress>()
                    {
                        new DbEmailAddress() { Address = "fillip@mail.ru" },
                        new DbEmailAddress() { Address = "fillip@gmail.com" }
                    },
                    MailAddress = new DbAddress()
                    {
                        ZipCode = "220060",
                        CountryId = 15,
                        Country = new DbCountry() { Id = 21, Name = "Беларусь" },
                        City = "Минск",
                        Building = "80",
                        BuildingBlock = "А",
                        Flat = "6"
                    }
                },
                DbAuthorisedUser = new DbAuthorisedUser()
                {
                    Id = 4,
                    Avatar = @"\temp\avatars\fillip"
                }
            };

            Users = new List<DbUser> {userUnregistrated, userUser, userModerator, userАdmin};
        }

        /// <summary>
        /// Возвращает коллекцию сгенерированных пользователей.
        /// </summary>
        public List<DbUser> Users { get; }
    }
}