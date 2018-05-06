using AutoMapper;
using NUnit.Framework;
using Rocket.BL.Services.User;
using Rocket.DAL.Common.Repositories.IDbUserRepository;
using FluentAssertions;
using Rocket.DAL.Common.DbModels.User;
using Rocket.BL.Tests.User.FakeData;
using Moq;

namespace Rocket.BL.Tests.User
{
    /// <summary>
    /// Unit-тесты для сервиса <see cref="UserValidateService"/>
    /// </summary>
    [TestFixture]
    public class UserManagementServiceTest
    {
        private UserManagementService _userManagementService;
        
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
            moq.Setup(x => x.GetByUserLoginFromStore(It.IsAny<string>())).Returns( new DbUser()
            {
                Login = "petya12",
                FirstName = "Ivan",
                LastName = "Ivanov",
                Id = 1,
                Password = "12345"
            });

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
