using AutoMapper;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Rocket.BL.Services;
using Rocket.BL.Tests.ReleaseList.FakeData;
using Rocket.DAL.Common.DbModels;
using Rocket.DAL.Common.Repositories;
using Rocket.DAL.Common.UoW;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Rocket.BL.Tests.ReleaseList
{
    /// <summary>
    /// Unit-тесты для сервиса <see cref="FilmDetailedInfoService"/>
    /// </summary>
    [TestFixture]
    public class FilmDetailedInfoServiceTest
    {
        private const int FilmsCount = 300;
        private FilmDetailedInfoService _filmDetailedInfoService;
        private FakeDbFilmsData _fakeDbFilmsData;

        /// <summary>
        /// Осуществляет все необходимые настройки для тестов.
        /// AutoMapper, Bogus (FakeDbFilmData), Moq
        /// </summary>
        [OneTimeSetUp]
        public void SetUp()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfiles("Rocket.BL.Common");
            });

            this._fakeDbFilmsData = new FakeDbFilmsData(100, 10, 10, FilmsCount);

            var mockDbFilmRepository = new Mock<IDbFilmRepository>();
            mockDbFilmRepository.Setup(mock => mock.Get(It.IsAny<Expression<Func<DbFilm, bool>>>(), null, ""))
                .Returns((Expression<Func<DbFilm, bool>> filter,
                    Func<IQueryable<DbFilm>, IOrderedQueryable<DbFilm>> orderBy,
                    string includeProperties) => this._fakeDbFilmsData.Films.Where(filter.Compile()));
            mockDbFilmRepository.Setup(mock => mock.GetById(It.IsAny<object>()))
                .Returns((object id) => this._fakeDbFilmsData.Films.Find(f => f.Id == (int)id));
            mockDbFilmRepository.Setup(mock => mock.Insert(It.IsAny<DbFilm>()))
                .Callback((DbFilm f) => this._fakeDbFilmsData.Films.Add(f));
            mockDbFilmRepository.Setup(mock => mock.Update(It.IsAny<DbFilm>()))
                .Callback((DbFilm f) => this._fakeDbFilmsData.Films.Find(d => d.Id == f.Id).Title = f.Title);
            mockDbFilmRepository.Setup(mock => mock.Delete(It.IsAny<object>()))
                .Callback((object id) => this._fakeDbFilmsData.Films
                    .Remove(this._fakeDbFilmsData.Films.Find(f => f.Id == (int)id)));

            var mockDbFilmUnitOfWork = new Mock<IDbFilmUnitOfWork>();
            mockDbFilmUnitOfWork.Setup(mock => mock.FilmRepository)
                .Returns(() => mockDbFilmRepository.Object);

            this._filmDetailedInfoService = new FilmDetailedInfoService(mockDbFilmUnitOfWork.Object);
        }

        /// <summary>
        /// Тест метода получения экземпляра фильма по заданному идентификатору.
        /// Фильм с передаваемым идентификатором существует
        /// </summary>
        /// <param name="id">Идентификатор фильма</param>
        [Test, Order(1)]
        public void GetExistedFilmTest([Random(0, FilmsCount - 1, 5)] int id)
        {
            var expectedFilm = this._fakeDbFilmsData.Films.Find(f => f.Id == id);

            var actualFilm = this._filmDetailedInfoService.GetFilm(id);

            actualFilm.Should().BeEquivalentTo(expectedFilm,
                options => options.ExcludingMissingMembers());
            actualFilm.Directors.Should().BeEquivalentTo(expectedFilm.Directors,
                options => options.ExcludingMissingMembers());
            actualFilm.Cast.Should().BeEquivalentTo(expectedFilm.Cast,
                options => options.ExcludingMissingMembers());
            actualFilm.Genres.Should().BeEquivalentTo(expectedFilm.Genres,
                options => options.ExcludingMissingMembers());
            actualFilm.Countries.Should().BeEquivalentTo(expectedFilm.Countries,
                options => options.ExcludingMissingMembers());
        }

        /// <summary>
        /// Тест метода получения экземпляра фильма по заданному идентификатору.
        /// Фильм с передаваемым идентификатором не существует
        /// </summary>
        /// <param name="id">Идентификатор фильма</param>
        [Test, Order(1)]
        public void GetNotExistedFilmTest([Random(FilmsCount, FilmsCount + 300, 5)] int id)
        {
            var actualFilm = this._filmDetailedInfoService.GetFilm(id);

            actualFilm.Should().BeNull();
        }

        /// <summary>
        /// Тест метода добавления фильма в хранилище данных
        /// </summary>
        [Test, Repeat(5), Order(2)]
        public void AddFilmTest()
        {
            var film = new FakeFilmsData(50, 10, 10, 0).FilmFaker.Generate();
            film.Id = this._fakeDbFilmsData.Films.Last().Id + 1;

            var actualId = this._filmDetailedInfoService.AddFilm(film);
            var actualFilm = this._filmDetailedInfoService.GetFilm(actualId);
            
            actualFilm.Should().BeEquivalentTo(film);
        }

        /// <summary>
        /// Тест метода обновления данных о фильме
        /// </summary>
        /// <param name="id">Идентификатор фильма для обновления</param>
        [Test, Order(2)]
        public void UpdateFilmTest([Random(0, FilmsCount - 1, 5)] int id)
        {
            var film = this._filmDetailedInfoService.GetFilm(id);
            film.Title = new Bogus.Faker().Lorem.Word();

            this._filmDetailedInfoService.UpdateFilm(film);
            var actualFilm = this._fakeDbFilmsData.Films.Find(f => f.Id == id);

            actualFilm.Title.Should().Be(film.Title);
        }

        /// <summary>
        /// Тест метода удаления фильма из хранилища данных
        /// </summary>
        /// <param name="id">Идентификатор фильма для удаления</param>
        [Test, Order(3)]
        public void DeleteFilmTest([Random(0, FilmsCount - 1, 5)] int id)
        {
            this._filmDetailedInfoService.DeleteFilm(id);

            var actualFilm = this._fakeDbFilmsData.Films.Find(film => film.Id == id);

            actualFilm.Should().BeNull();
        }

        /// <summary>
        /// Тест метода проверки наличия фильма в хранилище данных.
        /// Фильм существует
        /// </summary>
        /// <param name="id">Идентификатор фильма для поиска</param>
        [Test, Order(2)]
        public void FilmExistsTest([Random(0, FilmsCount - 1, 5)] int id)
        {
            var titleToFind = this._fakeDbFilmsData.Films.Find(dbf => dbf.Id == id).Title;

            var actual = this._filmDetailedInfoService
                .FilmExists(f => f.Title == titleToFind);

            actual.Should().BeTrue();
        }

        /// <summary>
        /// Тест метода проверки наличия фильма в хранилище данных.
        /// Фильм не существует
        /// </summary>
        /// <param name="title">Название фильма для поиска</param>
        [Test, Order(2)]
        public void FilmNotExistsTest([Values("1 1 1", "2 22 2", "", "4 word 4", "three words title")] string title)
        {
            var actual = this._filmDetailedInfoService
                .FilmExists(f => f.Title == title);

            actual.Should().BeFalse();
        }
    }
}
