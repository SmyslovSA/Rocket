using AutoMapper;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Rocket.BL.Services.User;
using Rocket.DAL.Common.DbModels.User;
using Rocket.DAL.Common.Repositories.User;
using Rocket.DAL.Common.UoW;
using System;
using Rocket.BL.Tests.User.FakeData;
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
        private const int UserCount = 300;
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

            this._fakeDbUsers = new FakeDbUsers(UserCount, false, false, false, false, 5, 5);

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
        /// Тест метода получения экземпляра пользователя по заданному идентификатору.
        /// пользователь с передаваемым идентификатором существует
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        [Test, Order(1)]
        public void GetExistedUserTest([Random(0, UserCount - 1, 5)] int id)
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
        public void GetNotExistedUserTest([Random(UserCount, UserCount + 300, 5)] int id)
        {
            var actualUser = this._userManagementService.GetUser(id);

            actualUser.Should().BeNull();
        }

        /// <summary>
        /// Тест метода добавления фильма в хранилище данных
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
        /// Тест метода обновления данных о пользователе
        /// </summary>
        /// <param name="id">Идентификатор пользователя для обновления</param>
        [Test, Order(2)]
        public void UpdateUserTest([Random(0, UserCount - 1, 5)] int id)
        {
            var user = this._userManagementService.GetUser(id);
            user.Login = new Bogus.Faker().Lorem.Word();

            this._userManagementService.UpdateUser(user);
            var actualUser = this._fakeDbUsers.Users.Find(f => f.Id == id);

            actualUser.Login.Should().Be(user.Login);
        }

        /// <summary>
        /// Тест метода удаления фильма из хранилища данных
        /// </summary>
        /// <param name="id">Идентификатор фильма для удаления</param>
        [Test, Order(3)]
        public void DeleteUserTest([Random(0, UserCount - 1, 5)] int id)
        {
            this._userManagementService.DeleteUser(id);

            var actualUser = this._fakeDbUsers.Users.Find(user => user.Id == id);

            actualUser.Should().BeNull();
        }

        /// <summary>
        /// Тест метода проверки наличия фильма в хранилище данных.
        /// Фильм существует
        /// </summary>
        /// <param name="id">Идентификатор фильма для поиска</param>
        [Test, Order(2)]
        public void UserExistsTest([Random(0, UserCount - 1, 5)] int id)
        {
            var loginToFind = this._fakeDbUsers.Users.Find(dbf => dbf.Id == id).Login;

            var actual = this._userManagementService
                .UserExists(f => f.Login == loginToFind);

            actual.Should().BeTrue();
        }

        /// <summary>
        /// Тест метода проверки наличия пользователя в хранилище данных.
        /// Пользователь не существует
        /// </summary>
        /// <param name="title">Логин пользователя для поиска</param>
        [Test, Order(2)]
        public void UserNotExistsTest([Values("1 1 1", "2 22 2", "", "4 word 4", "three words title")] string title)
        {
            var actual = this._userManagementService
                .UserExists(f => f.Login == title);

            actual.Should().BeFalse();
        }
    }
}
