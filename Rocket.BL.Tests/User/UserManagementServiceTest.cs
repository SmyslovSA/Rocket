using AutoMapper;
using NUnit.Framework;
using Rocket.BL.Services.User;
using Rocket.DAL.Common.Repositories.User;
using Rocket.DAL.Common.UoW;
using FluentAssertions;
using Rocket.DAL.Common.DbModels.User;
using Rocket.BL.Tests.User.FakeData;
using System;
using System.Linq;
using System.Linq.Expressions;
using Moq;

namespace Rocket.BL.Tests.User
{
    /// <summary>
    /// Unit-тесты для сервиса <see cref="UserValidateService"/>
    /// </summary>
    [TestFixture]
    public class UserManagementServiceTest
    {
        private const int UserCount = 300;
        private UserManagementService _userManagementService;
        private FakeDbUsers _fakeDbFilmsData;

        /// <summary>
        /// Осуществляет настройки
        /// </summary>
        [OneTimeSetUp]
        public void SetUp()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfiles("Rocket.BL.Common");
            });
            var moq = new Mock<IDbUserRepository>();
            moq.Setup(mock => mock.Get(It.IsAny<Expression<Func<DbUser, bool>>>(), null, ""))
                .Returns((Expression<Func<DbUser, bool>> filter,
                    Func<IQueryable<DbUser>, IOrderedQueryable<DbUser>> orderBy,
                    string includeProperties) => this._fakeDbFilmsData.Films.Where(filter.Compile()));
            moq.Setup(mock => mock.GetById(It.IsAny<object>()))
                .Returns((object id) => this._fakeDbFilmsData.Films.Find(f => f.Id == (int)id));
            moq.Setup(mock => mock.Insert(It.IsAny<DbUser>()))
                .Callback((DbUser f) => this._fakeDbFilmsData.Films.Add(f));
            moq.Setup(mock => mock.Update(It.IsAny<DbUser>()))
                .Callback((DbUser f) => this._fakeDbFilmsData.Films.Find(d => d.Id == f.Id).Title = f.Title);
            moq.Setup(mock => mock.Delete(It.IsAny<object>()))
                .Callback((object id) => this._fakeDbFilmsData.Films
                    .Remove(this._fakeDbFilmsData.Films.Find(f => f.Id == (int)id)));


            this._userManagementService = new UserManagementService(moq.Object);
        }

        /// <summary>
        /// Метод AddUser() сервиса UserManagementService должен возвращать
        /// 0, если пользователь null.
        /// </summary>
        [Test, Order(1)]
        public void UserManagementServiceAddUserUserIsNullTest()
        {
            // Arrange
            Common.Models.User.User user = null;

            // Act
            var result = this._userManagementService.AddUser(user);

            // Assert
            result.Should().Equals(-1);
        }

        /// <summary>
        /// Метод AddUser() сервиса UserManagementService должен возвращать
        /// -1, если пользователь если значение User-а не валидно, например,
        /// логин пустая строка.
        /// </summary>
        [Test, Order(1)]
        public void UserManagementServiceAddUserUserInvalidTest()
        {
            // Arrange
            var fakerNewUsers = new FakeUser(
                usersCount: 1,
                isFirstNameNullOrEmpty: true,
                isLastNameNullOrEmpty: false,
                isLoginNullOrEmpty: false,
                isPasswordNullOrEmpty: false,
                minLoginLenght: 5,
                minPasswordLenght: 5);
            var user = fakerNewUsers.UsersFaker[0];

            // Act
            var result = this._userManagementService.AddUser(user);

            // Assert
            result.Should().Equals(-1);
        }

        /// <summary>
        /// Метод AddUser() сервиса UserManagementService должен возвращать
        /// -1, если пользователь если значение User-а не валидно, например,
        /// логин пустая строка.
        /// </summary>
        [Test, Order(1)]
        public void UserManagementServiceAddUserIfUserValid()
        {
            // Arrange
            var fakerNewUsers = new FakeUser(
                usersCount: 1,
                isFirstNameNullOrEmpty: false,
                isLastNameNullOrEmpty: false,
                isLoginNullOrEmpty: false,
                isPasswordNullOrEmpty: false,
                minLoginLenght: 5,
                minPasswordLenght: 5);
            var user = fakerNewUsers.UsersFaker[0];

            // Act
            var result = this._userManagementService.AddUser(user);

            // Assert
            result.Should().Equals(1);
        }
    }
}
