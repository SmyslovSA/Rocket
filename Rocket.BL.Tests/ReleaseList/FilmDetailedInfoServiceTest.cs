using AutoMapper;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Rocket.BL.Services.ReleaseList;
using Rocket.BL.Tests.ReleaseList.FakeData;
using Rocket.DAL.Common.DbModels.ReleaseList;
using Rocket.DAL.Common.Repositories.ReleaseList;
using Rocket.DAL.Common.UoW;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Rocket.BL.Tests.ReleaseList
{
    /// <summary>
    /// Unit-тесты для сервиса <see cref="UserDetailedInfoService"/>
    /// </summary>
    [TestFixture]
    public class UserDetailedInfoServiceTest
    {
        private const int UsersCount = 300;
        private UserDetailedInfoService _filmDetailedInfoService;
        private FakeDbUsersData _fakeDbUsersData;

        /// <summary>
        /// Осуществляет все необходимые настройки для тестов.
        /// AutoMapper, Bogus (FakeDbUserData), Moq
        /// </summary>
        [OneTimeSetUp]
        public void SetUp()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfiles("Rocket.BL.Common");
            });

            this._fakeDbUsersData = new FakeDbUsersData(100, 10, 10, UsersCount);

            var mockDbUserRepository = new Mock<IDbUserRepository>();
            mockDbUserRepository.Setup(mock => mock.Get(It.IsAny<Expression<Func<DbUser, bool>>>(), null, ""))
                .Returns((Expression<Func<DbUser, bool>> filter,
                    Func<IQueryable<DbUser>, IOrderedQueryable<DbUser>> orderBy,
                    string includeProperties) => this._fakeDbUsersData.Users.Where(filter.Compile()));
            mockDbUserRepository.Setup(mock => mock.GetById(It.IsAny<object>()))
                .Returns((object id) => this._fakeDbUsersData.Users.Find(f => f.Id == (int)id));
            mockDbUserRepository.Setup(mock => mock.Insert(It.IsAny<DbUser>()))
                .Callback((DbUser f) => this._fakeDbUsersData.Users.Add(f));
            mockDbUserRepository.Setup(mock => mock.Update(It.IsAny<DbUser>()))
                .Callback((DbUser f) => this._fakeDbUsersData.Users.Find(d => d.Id == f.Id).Title = f.Title);
            mockDbUserRepository.Setup(mock => mock.Delete(It.IsAny<object>()))
                .Callback((object id) => this._fakeDbUsersData.Users
                    .Remove(this._fakeDbUsersData.Users.Find(f => f.Id == (int)id)));

            var mockDbUserUnitOfWork = new Mock<IUnitOfWork>();
            mockDbUserUnitOfWork.Setup(mock => mock.UserRepository)
                .Returns(() => mockDbUserRepository.Object);

            this._filmDetailedInfoService = new UserDetailedInfoService(mockDbUserUnitOfWork.Object);
        }

        /// <summary>
        /// Тест метода получения экземпляра фильма по заданному идентификатору.
        /// Фильм с передаваемым идентификатором существует
        /// </summary>
        /// <param name="id">Идентификатор фильма</param>
        [Test, Order(1)]
        public void GetExistedUserTest([Random(0, UsersCount - 1, 5)] int id)
        {
            var expectedUser = this._fakeDbUsersData.Users.Find(f => f.Id == id);

            var actualUser = this._filmDetailedInfoService.GetUser(id);

            actualUser.Should().BeEquivalentTo(expectedUser,
                options => options.ExcludingMissingMembers());
            actualUser.Directors.Should().BeEquivalentTo(expectedUser.Directors,
                options => options.ExcludingMissingMembers());
            actualUser.Cast.Should().BeEquivalentTo(expectedUser.Cast,
                options => options.ExcludingMissingMembers());
            actualUser.Genres.Should().BeEquivalentTo(expectedUser.Genres,
                options => options.ExcludingMissingMembers());
            actualUser.Countries.Should().BeEquivalentTo(expectedUser.Countries,
                options => options.ExcludingMissingMembers());
        }

        /// <summary>
        /// Тест метода получения экземпляра фильма по заданному идентификатору.
        /// Фильм с передаваемым идентификатором не существует
        /// </summary>
        /// <param name="id">Идентификатор фильма</param>
        [Test, Order(1)]
        public void GetNotExistedUserTest([Random(UsersCount, UsersCount + 300, 5)] int id)
        {
            var actualUser = this._filmDetailedInfoService.GetUser(id);

            actualUser.Should().BeNull();
        }

        /// <summary>
        /// Тест метода добавления фильма в хранилище данных
        /// </summary>
        [Test, Repeat(5), Order(2)]
        public void AddUserTest()
        {
            var film = new FakeUsersData(50, 10, 10, 0).UserFaker.Generate();
            film.Id = this._fakeDbUsersData.Users.Last().Id + 1;

            var actualId = this._filmDetailedInfoService.AddUser(film);
            var actualUser = this._filmDetailedInfoService.GetUser(actualId);
            
            actualUser.Should().BeEquivalentTo(film);
        }

        /// <summary>
        /// Тест метода обновления данных о фильме
        /// </summary>
        /// <param name="id">Идентификатор фильма для обновления</param>
        [Test, Order(2)]
        public void UpdateUserTest([Random(0, UsersCount - 1, 5)] int id)
        {
            var film = this._filmDetailedInfoService.GetUser(id);
            film.Title = new Bogus.Faker().Lorem.Word();

            this._filmDetailedInfoService.UpdateUser(film);
            var actualUser = this._fakeDbUsersData.Users.Find(f => f.Id == id);

            actualUser.Title.Should().Be(film.Title);
        }

        /// <summary>
        /// Тест метода удаления фильма из хранилища данных
        /// </summary>
        /// <param name="id">Идентификатор фильма для удаления</param>
        [Test, Order(3)]
        public void DeleteUserTest([Random(0, UsersCount - 1, 5)] int id)
        {
            this._filmDetailedInfoService.DeleteUser(id);

            var actualUser = this._fakeDbUsersData.Users.Find(film => film.Id == id);

            actualUser.Should().BeNull();
        }

        /// <summary>
        /// Тест метода проверки наличия фильма в хранилище данных.
        /// Фильм существует
        /// </summary>
        /// <param name="id">Идентификатор фильма для поиска</param>
        [Test, Order(2)]
        public void UserExistsTest([Random(0, UsersCount - 1, 5)] int id)
        {
            var titleToFind = this._fakeDbUsersData.Users.Find(dbf => dbf.Id == id).Title;

            var actual = this._filmDetailedInfoService
                .UserExists(f => f.Title == titleToFind);

            actual.Should().BeTrue();
        }

        /// <summary>
        /// Тест метода проверки наличия фильма в хранилище данных.
        /// Фильм не существует
        /// </summary>
        /// <param name="title">Название фильма для поиска</param>
        [Test, Order(2)]
        public void UserNotExistsTest([Values("1 1 1", "2 22 2", "", "4 word 4", "three words title")] string title)
        {
            var actual = this._filmDetailedInfoService
                .UserExists(f => f.Title == title);

            actual.Should().BeFalse();
        }
    }
}
