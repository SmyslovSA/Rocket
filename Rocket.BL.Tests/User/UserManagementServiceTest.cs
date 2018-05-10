using AutoMapper;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Rocket.BL.Services.User;
using Rocket.BL.Tests.User.FakeData;
using Rocket.DAL.Common.DbModels.User;
using Rocket.DAL.Common.Repositories.User;
using Rocket.DAL.Common.UoW;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Rocket.BL.Tests.User
{
    /// <summary>
    /// Unit-тесты для сервиса <see cref="UserValidateService"/>
    /// </summary>
    [TestFixture]
    public class UserManagementServiceTest
    {
        private const int UsersCount = 300;
        private UserManagementService _userManagementService;
        private FakeDbUsers _fakeDbUsers;

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
                    string includeProperties) => this._fakeDbUsers.Users.Where(filter.Compile()));
            moq.Setup(mock => mock.GetById(It.IsAny<object>()))
                .Returns((object id) => this._fakeDbUsers.Users.Find(f => f.Id == (int)id));
            moq.Setup(mock => mock.Insert(It.IsAny<DbUser>()))
                .Callback((DbUser f) => this._fakeDbUsers.Users.Add(f));
            moq.Setup(mock => mock.Update(It.IsAny<DbUser>()))
                .Callback((DbUser f) => this._fakeDbUsers.Users.Find(d => d.Id == f.Id).Login = f.Login);
            moq.Setup(mock => mock.Delete(It.IsAny<object>()))
                .Callback((object id) => this._fakeDbUsers.Users
                    .Remove(this._fakeDbUsers.Users.Find(f => f.Id == (int)id)));

            var mockDbUserUnitOfWork = new Mock<IUnitOfWork>();
            mockDbUserUnitOfWork.Setup(mock => mock.UserRepository)
                .Returns(() => moq.Object);

            this._userManagementService = new UserManagementService(mockDbUserUnitOfWork.Object);
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
        /// Тест метода получения экземпляра пользователя по заданному идентификатору.
        /// пользователь с передаваемым идентификатором существует
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        [Test, Order(1)]
        public void GetExistedUserTest([Random(0, UsersCount - 1, 5)] int id)
        {
            var expectedUser = this._fakeDbUsers.Users.Find(f => f.Id == id);

            var actualUser = this._userManagementService.GetUser(id);

            actualUser.Should().BeEquivalentTo(expectedUser,
                options => options.ExcludingMissingMembers());
            actualUser.Phones.Should().BeEquivalentTo(expectedUser.Phones,
                options => options.ExcludingMissingMembers());
            actualUser.EMailAddresses.Should().BeEquivalentTo(expectedUser.EMailAddresses,
                options => options.ExcludingMissingMembers());
            actualUser.Login.Should().BeEquivalentTo(expectedUser.Login);
            actualUser.FirstName.Should().BeEquivalentTo(expectedUser.FirstName);
            actualUser.LastName.Should().BeEquivalentTo(expectedUser.LastName);
        }

        /// <summary>
        /// Тест метода получения экземпляра пользователя по заданному идентификатору.
        /// пользователь с передаваемым идентификатором не существует
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        [Test, Order(1)]
        public void GetNotExistedUserTest([Random(UsersCount, UsersCount + 300, 5)] int id)
        {
            var actualUser = this._userManagementService.GetUser(id);

            actualUser.Should().BeNull();
        }

        /// <summary>
        /// Тест метода добавления пользователя в хранилище данных
        /// </summary>
        [Test, Repeat(5), Order(2)]
        public void AddUserTest()
        {
            var user = new FakeUsers(0, false, false, false, false, 5, 5).UserFaker.Generate();
            user.Id = this._fakeDbUsers.Users.Last().Id + 1;

            var actualId = this._userManagementService.AddUser(user);
            var actualUser = this._userManagementService.GetUser(actualId);

            actualUser.Should().BeEquivalentTo(user);
        }

        /// <summary>
        /// Тест метода обновления данных о фильме
        /// </summary>
        /// <param name="id">Идентификатор пользователя для обновления</param>
        [Test, Order(2)]
        public void UpdateUserTest([Random(0, UsersCount - 1, 5)] int id)
        {
            var user = this._userManagementService.GetUser(id);
            user.Login = new Bogus.Faker().Internet.UserName();

            this._userManagementService.UpdateUser(user);
            var actualUser = this._fakeDbUsers.Users.Find(f => f.Id == id);

            actualUser.Login.Should().Be(user.Login);
        }

        /// <summary>
        /// Тест метода удаления пользователя из хранилища данных
        /// </summary>
        /// <param name="id">Идентификатор пользователя для удаления</param>
        [Test, Order(3)]
        public void DeleteUserTest([Random(0, UsersCount - 1, 5)] int id)
        {
            this._userManagementService.DeleteUser(id);

            var actualUser = this._fakeDbUsers.Users.Find(user => user.Id == id);

            actualUser.Should().BeNull();
        }

        /// <summary>
        /// Тест метода проверки наличия пользователя в хранилище данных.
        /// пользователь существует
        /// </summary>
        /// <param name="id">Идентификатор пользователя для поиска</param>
        [Test, Order(2)]
        public void UserExistsTest([Random(0, UsersCount - 1, 5)] int id)
        {
            var titleToFind = this._fakeDbUsers.Users.Find(dbf => dbf.Id == id).Title;

            var actual = this._userManagementService
                .UserExists(f => f.Title == titleToFind);

            actual.Should().BeTrue();
        }

        /// <summary>
        /// Тест метода проверки наличия пользователя в хранилище данных.
        /// пользователь не существует
        /// </summary>
        /// <param name="title">Название пользователя для поиска</param>
        [Test, Order(2)]
        public void UserNotExistsTest([Values("1 1 1", "2 22 2", "", "4 word 4", "three words title")] string title)
        {
            var actual = this._userManagementService
                .UserExists(f => f.Title == title);

            actual.Should().BeFalse();
        }
    }
}
